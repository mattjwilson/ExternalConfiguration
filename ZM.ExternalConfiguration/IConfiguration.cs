namespace ZM.ExternalConfiguration
{
    using System;
using System.Collections.Generic;

    /// <summary>
    /// Defines the structure required of classes providing read only access to a common / remote configuration.
    /// </summary>
    public interface IConfiguration
    {
        #region Methods

        /// <summary>
        /// Gets a value from the configuration, casting it to a specified type for consumption.
        /// </summary>
        /// <typeparam name="T">The type expected that the configuration value can be created as.</typeparam>
        /// <param name="key">A <see cref="string"/> value representing the key used to locate the requested value / object.</param>
        /// <returns>A <see cref="T"/> based object / value.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        T Get<T>(string key);

        /// <summary>
        /// Gets a value from the configuration, returning the raw string alue.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value representing the key used to locate the requested value.</param>
        /// <returns>A <see cref="string"/> value.</returns>
        string GetString(string key);

        /// <summary>
        /// Gets a value from the configuration returning a collection of elements nested under a key.
        /// </summary>
        /// <param name="key">A <see cref="string"/> representing the key of the parent element to locate.</param>
        /// <returns>An <see cref="IEnumberable{T}"/> based collection of <see cref="string"/> values.</returns>
        IEnumerable<string> GetStrings(string key);

        /// <summary>
        /// Searches the configuration source for a specified key.
        /// </summary>
        /// <param name="key">A <see cref="string"/> representing the key to search for.</param>
        /// <returns>A <see cref="bool"/> value indica</returns>
        bool ContainsKey(string key);

        #endregion

        #region Events

        /// <summary>
        /// An event raised when an unhandled exception occurs in the background processing of the cache or file retrieval.
        /// </summary>
        /// <value>An event of type <see cref="EventHandler{T}"/> of type <see cref="Exception"/> based objects.</value>
        event EventHandler<BackgroundExceptionEventArgs> ExceptionOccurred;

        #endregion
    }
}
