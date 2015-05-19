namespace ZM.ExternalConfiguration.Azure
{
    /// <summary>
    /// Object containing all values required by the azure file loader to connect, and access the remote configuration file.
    /// </summary>
    public class AzureConfigurationSettings : ConfigurationSettings, IAzureConfigurationSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to a value representing the name of the blob container in which the file resides.
        /// </summary>
        /// <value>A <see cref="string"/> value.</value>
        public string ContainerName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a reference to a value represeting the file name associated with the file in the container.
        /// </summary>
        /// <value>A <see cref="string"/> value.</value>
        public string FileName
        {
            get;
            set;
        }

        #endregion
    }
}
