﻿namespace FenrirFS
{
    /// <summary>
    /// Modes that a file can be opened to as.
    /// </summary>
    public enum OpenMode
    {
        /// <summary>
        /// Normal, open the file or fail if it doesn't exist.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Open if it exists, otherwise fail.
        /// </summary>
        FailIfDoesNotExist = 1,
    }
}
