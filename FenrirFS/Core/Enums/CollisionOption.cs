namespace FenrirFS
{
    /// <summary>
    /// Options for collisions between files.
    /// </summary>
    public enum CollisionOption
    {
        /// <summary>
        /// Attempt to generate a unique name
        /// </summary>
        GenerateUniqueName = 0,

        /// <summary>
        /// Replace any existing file/folder
        /// </summary>
        ReplaceExisting = 1,

        /// <summary>
        /// Fail if a file/folder already exists
        /// </summary>
        FailIfExists = 2
    }
}
