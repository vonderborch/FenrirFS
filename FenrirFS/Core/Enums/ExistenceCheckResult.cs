namespace FenrirFS
{
    /// <summary>
    /// The result of an existence check. Defines what type of object exists, if any, at a given path.
    /// </summary>
    public enum ExistenceCheckResult
    {
        /// <summary>
        /// A file exists at the given path.
        /// </summary>
        FileExists = 1,

        /// <summary>
        /// A folder exists at the given path.
        /// </summary>
        FolderExists = 2,

        /// <summary>
        /// A file and a folder with the same name exist at the given path.
        /// </summary>
        FileAndFolderExists = 3,

        /// <summary>
        /// No file or folder found at the given path.
        /// </summary>
        NotFound = -1
    }
}
