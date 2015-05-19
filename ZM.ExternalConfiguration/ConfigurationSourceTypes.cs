namespace ZM.ExternalConfiguration
{
    /// <summary>
    /// Enumeration flagging the type of remote configurations are available to the package.
    /// </summary>
    public enum ConfigurationSourceType
    {
        /// <summary>
        /// Source is unknown.  This is bad and probably will result in an error.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Configuration file is stored in a azure blob / table.  
        /// </summary>
        AzureBlob = 1,
        /// <summary>
        /// Configuration file is stored on a shared network directory that the application has access too.
        /// </summary>
        SharedDirectory = 2
    }
}
