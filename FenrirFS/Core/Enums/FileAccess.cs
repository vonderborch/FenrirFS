namespace FenrirFS
{
    /// <summary>
    /// Modes that a stream can be opened as.
    /// </summary>
    public enum FileAccess
    {
        /// <summary>
        /// Data can be read (retrieved) from the file.
        /// </summary>
        Read = 0,

        /// <summary>
        /// Data can be added to and retrieved from the file.
        /// </summary>
        ReadWrite = 2,

        /// <summary>
        /// Data can be written (added) to the file.
        /// </summary>
        Write = 1,

        /// <summary>
        /// The file is not open for read or write.
        /// </summary>
        None = -1
    }
}
