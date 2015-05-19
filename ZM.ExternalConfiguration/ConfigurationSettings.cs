namespace ZM.ExternalConfiguration
{
    using System;

    /// <summary>
    /// Class representing the required values to properly configure the configuration for external storage.
    /// </summary>
    public class ConfigurationSettings : IConfigurationSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to an object representing the amount of time to cache the external file before clearing and re-pulling the file across the network.
        /// </summary>
        /// <value>A <see cref="TimeSpan"/> based object.</value>
        public TimeSpan CacheTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a reference to a value representing a flag indicating the loader type to use for processing the configuration file.
        /// </summary>
        /// <value>A <see cref="ConfigurationSourceType"/> value.</value>
        public ConfigurationSourceType SourceType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a reference to a value representing the path / uri / location of the configuration file.
        /// </summary>
        /// <value>A <see cref="string"/> value.</value>
        public string FileLocation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a reference to a value representing the configuration section that contains the settings for the current application.
        /// </summary>
        /// <value>A <see cref="string"/> value.</value>
        public string ConfigurationSection
        {
            get;
            set;
        }

        #endregion
    }
}
