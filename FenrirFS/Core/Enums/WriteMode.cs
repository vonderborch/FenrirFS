﻿namespace FenrirFS
{
    /// <summary>
    /// Modes that a file can be written to as.
    /// </summary>
    public enum WriteMode
    {
        /// <summary>
        /// Append new text to any existing text.
        /// </summary>
        Append = 1,

        /// <summary>
        /// Replace any existing text with new text.
        /// </summary>
        Truncate = 0
    }
}