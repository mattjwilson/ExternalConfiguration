namespace ZM.ExternalConfiguration
{
    using System;
    using System.IO;

    /// <summary>
    /// Class capable of opening the Xcs configuration file stored on a shared directory or local file system.
    /// </summary>
    public class WindowsDirectoryLoader : IFileLoader
    {
        #region Fields

        private readonly ConfigurationSettings settings;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="WindowsDirectoryLoader"/> class, requiring an external settings object.
        /// </summary>
        /// <param name="settings">A <see cref="ConfigurationSettings"/> based object containing the settings required to locate the configuration file.</param>
        public WindowsDirectoryLoader(ConfigurationSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            this.settings = settings;
        }

        #endregion

        #region IFileLoader members

        /// <summary>
        /// Triggers the retrieval of the external file, loading the result into memory.
        /// </summary>
        /// <returns>A <see cref="MemoryStream"/> object containing the external file.</returns>
        /// <exception cref="FileAccessException" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Stream will be used by consumer.  Consumer will dispose / be responsible for disposing of stream.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public MemoryStream GetFile()
        {
            try
            {
                using (var fileStream = File.Open(this.settings.FileLocation, FileMode.Open))
                {
                    var memoryStream = new MemoryStream();
                    var fileBytes = new byte[fileStream.Length];

                    fileStream.Read(fileBytes, 0, (int)fileStream.Length);
                    memoryStream.Write(fileBytes, 0, (int)fileStream.Length);
                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                throw new FileAccessException("An error was encountered while retrieving the file from: " + this.settings.FileLocation, ex);
            }
        }

        #endregion
    }
}
