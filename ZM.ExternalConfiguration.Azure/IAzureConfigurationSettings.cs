namespace ZM.ExternalConfiguration.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the structure required of objects containing the azure settings for the configuration cache.
    /// </summary>
    public interface IAzureConfigurationSettings : IConfigurationSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to a value representing the name of the blob container in which the file resides.
        /// </summary>
        /// <value>A <see cref="string"/> value.</value>
        string ContainerName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a reference to a value represeting the file name associated with the file in the container.
        /// </summary>
        /// <value>A <see cref="string"/> value.</value>
        string FileName
        {
            get;
            set;
        }

        #endregion
    }
}
