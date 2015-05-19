namespace ZM.ExternalConfiguration.Azure
{
    using Microsoft.WindowsAzure.Storage;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class responsible for loading a configuration file located in an Azure Blob Storage location.
    /// </summary>
    public class AzureBlobLoader : IFileLoader
    {
        #region Fields

        private readonly IAzureConfigurationSettings settings;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="AzureBlobLoader"/> class.
        /// </summary>
        /// <param name="settings">An <see cref="IAzureConfigurationSettings"/> based object containing the information needed to retrieve the configuration file from blob storage.</param>
        public AzureBlobLoader(IAzureConfigurationSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            this.settings = settings;
        }

        #endregion

        #region IFileLoader memers

        /// <summary>
        /// Triggers the retrieval of the external file, loading the result into memory.
        /// </summary>
        /// <returns>A <see cref="MemoryStream"/> object containing the external file.</returns>
        /// <exception cref="FileAccessException" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "MemoryStream is to be used external.  The caller is responsible for cleanup.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public MemoryStream GetFile()
        {
            try
            {
                var account = CloudStorageAccount.Parse(this.settings.FileLocation);
                var container = account.CreateCloudBlobClient().GetContainerReference(this.settings.ContainerName);
                var file = container.GetBlockBlobReference(this.settings.FileName);

                var memoryStream = new MemoryStream();
                file.DownloadToStream(memoryStream);

                return memoryStream;
            }
            catch (Exception ex)
            {
                throw new FileAccessException("An error was encountered while retrieving the file from: " + this.settings.FileLocation, ex);
            }
        }

        #endregion
    }
}
