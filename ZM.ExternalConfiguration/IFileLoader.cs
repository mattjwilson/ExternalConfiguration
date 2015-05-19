namespace ZM.ExternalConfiguration
{
    using System.IO;

    /// <summary>
    /// Defines the structure required of all objects responsible from loading the external configuration file from a remote source.
    /// </summary>
    public interface IFileLoader
    {
        #region Methods

        /// <summary>
        /// Triggers the retrieval of the external file, loading the result into memory.
        /// </summary>
        /// <returns>A <see cref="MemoryStream"/> object containing the external file.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        MemoryStream GetFile();

        #endregion
    }
}
