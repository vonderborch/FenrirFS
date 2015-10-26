namespace FenrirFS
{
    /// <summary>
    /// Options for collisions between folders.
    /// </summary>
    public enum FolderCollisionOption
    {
        /// <summary>
        /// Attempt to generate a unique name
        /// </summary>
        GenerateUniqueName = 0,

        /// <summary>
        /// Replace any existing folder
        /// </summary>
        ReplaceExisting = 1,

        /// <summary>
        /// Fail if a folder already exists
        /// </summary>
        FailIfExists = 2,

        /// <summary>
        /// Open if a folder exists
        /// </summary>
        OpenIfExists = 3
    }
}
