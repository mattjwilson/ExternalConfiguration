namespace ZM.ExternalConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Xml.Linq;

    /// <summary>
    /// Class representing an object capable of read only access to an external xml configuration file.
    /// </summary>
    public class XmlConfigurationClient : IConfiguration, IDisposable
    {
        #region Fields

        private readonly IConfigurationSettings settings;
        private readonly IFileLoader loader;
        private readonly Timer cacheTimer;
        private XDocument cachedConfiguration;
        private readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        private volatile bool isDisposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="XmlConfigurationClient"/> class.
        /// </summary>
        /// <param name="loader">An <see cref="IFileLoader"/> based object capable of aquiring the required configuration resource.</param>
        /// <param name="settings">An <see cref="IConfigurationSettings"/> based object containing all settings required to connect and parse the xml configuration file.</param>
        public XmlConfigurationClient(IFileLoader loader, IConfigurationSettings settings)
        {
            if (loader == null)
                throw new ArgumentNullException("loader");

            if (settings == null)
                throw new ArgumentNullException("settings");

            this.loader = loader;
            this.settings = settings;

            // Initialize the timer, but wait to start until cache is loaded.
            this.cacheTimer = new Timer(this.OnCacheRefresh, null, Timeout.Infinite, Timeout.Infinite);
            this.OnCacheRefresh(null);
        }

        #endregion

        #region Methods

        private void OnCacheRefresh(object state)
        {
            // Stop cache timer till we've refreshed.
            cacheTimer.Change(Timeout.Infinite, Timeout.Infinite);
            var enteredWrite = false;
            try
            {
                using (var stream = this.loader.GetFile())
                {
                    if (this.locker.TryEnterWriteLock(1000))
                    {
                        enteredWrite = true;
                        stream.Position = 0;
                        this.cachedConfiguration = null;
                        this.cachedConfiguration = XDocument.Load(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.ExceptionOccurred != null)
                {
                    this.ExceptionOccurred(this, new BackgroundExceptionEventArgs(ex));
                }
                else
                {
                    // If there are no observers, throw and don't swallow.
                    throw;
                }
            }
            finally
            {
                if (enteredWrite)
                    this.locker.ExitWriteLock();
            }

            // Start cache timer, want to clear in next period.
            cacheTimer.Change(this.settings.CacheTimeout, this.settings.CacheTimeout);
        }

        /// <summary>
        /// Releases the resources managed by the <see cref="XmlConfigurationClient"/> class.
        /// </summary>
        /// <param name="isDisposing">A <see cref="bool"/> value indicating the source of the dispose call.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (this.isDisposed)
                return;

            this.isDisposed = true;

            if (!isDisposing)
                return;

            this.cacheTimer.Dispose();
            this.locker.Dispose();
        }

        #endregion

        #region IConfiguration members

        /// <summary>
        /// Gets a value from the configuration, casting it to a specified type for consumption.
        /// </summary>
        /// <typeparam name="T">The type expected that the configuration value can be created as.</typeparam>
        /// <param name="key">A <see cref="string"/> value representing the key used to locate the requested value / object.</param>
        /// <returns>A <see cref="T"/> based object / value.</returns>
        public virtual T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a value from the configuration, returning the raw string alue.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value representing the key used to locate the requested value.</param>
        /// <returns>A <see cref="string"/> value.</returns>
        /// <exception cref="KeyNotFoundException" />
        /// <exception cref="CacheBusyException"/>
        public virtual string GetString(string key)
        {
            if (this.locker.TryEnterReadLock(1000))
            {
                try
                {
                    var requestedValue = this.cachedConfiguration.Element("Configurations")
                        .Element(this.settings.ConfigurationSection)
                        .Elements()
                            .Where(r => r.Attributes().First(a => a.Name == "key").Value == key)
                        .Select(e => e.Value).FirstOrDefault();

                    if (requestedValue == null)
                        throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, "Requested key: {0} was not found in the current configuration file.", key));

                    return requestedValue;
                }
                finally
                {
                    this.locker.ExitReadLock();
                }
            }

            throw new CacheBusyException("Unable to acquire a read lock for the cache object.");
        }

        /// <summary>
        /// Gets a value from the configuration returning a collection of elements nested under a key.
        /// </summary>
        /// <param name="key">A <see cref="string"/> representing the key of the parent element to locate.</param>
        /// <returns>An <see cref="IEnumberable{T}"/> based collection of <see cref="string"/> values.</returns>
        public IEnumerable<string> GetStrings(string key)
        {
            var collection = new List<string>();

            var requestedValue = this.cachedConfiguration.Element("Configurations")
                        .Element(this.settings.ConfigurationSection)
                        .Elements()
                            .Where(r => r.Attributes().First(a => a.Name == "key").Value == key);
            if (requestedValue == null)
                return collection;

            if(requestedValue.Elements().Any())
            {
                collection.AddRange(requestedValue.Elements()
                    .Select(e => e.Value));
            }
            else if(requestedValue.Any())
            {
                collection.Add(requestedValue.FirstOrDefault().Value);
            }

            if (requestedValue == null)
                throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, "Requested key: {0} was not found in the current configuration file.", key));

            return collection;
        }

        /// <summary>
        /// Searches the configuration source for a specified key.
        /// </summary>
        /// <param name="key">A <see cref="string"/> representing the key to search for.</param>
        /// <returns>A <see cref="bool"/> value indicating if the key is present in the current configuration cache.</returns>
        /// <exception cref="CacheBusyException"/>
        public virtual bool ContainsKey(string key)
        {
            if (this.locker.TryEnterReadLock(1000))
            {
                try
                {
                    return this.cachedConfiguration.Element("Configurations")
                        .Element(this.settings.ConfigurationSection)
                        .Elements()
                        .Any(r => r.Attributes().First(a => a.Name == "key").Value == key);
                }
                finally
                {
                    this.locker.ExitReadLock();
                }
            }

            throw new CacheBusyException("Unable to acquire a read lock for the cache object.");
        }

        /// <summary>
        /// An event raised when an unhandled exception occurs in the background processing of the cache or file retrieval.
        /// </summary>
        /// <remarks>If no handler is registered, and an exception is caught, the client will rethrow so to not swallow the exception / problem.</remarks>
        /// <value>An event of type <see cref="EventHandler{T}"/> of type <see cref="Exception"/> based objects.</value>
        public event EventHandler<BackgroundExceptionEventArgs> ExceptionOccurred;

        #endregion

        #region IDisposable members

        /// <summary>
        /// Releases the managed / disposable resources managed by the configuration object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
