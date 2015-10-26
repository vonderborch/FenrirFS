namespace FenrirFS
{
    /// <summary>
    /// Options for collisions between files.
    /// </summary>
    public enum FileCollisionOption
    {
        /// <summary>
        /// Attempt to generate a unique name
        /// </summary>
        GenerateUniqueName = 0,

        /// <summary>
        /// Replace any existing file
        /// </summary>
        ReplaceExisting = 1,

        /// <summary>
        /// Fail if a file already exists
        /// </summary>
        FailIfExists = 2
    }
}
