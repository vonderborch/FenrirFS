/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

namespace FenrirFS
{
    /// <summary>
    /// File/folder creation modes.
    /// </summary>
    public enum FileMode
    {
        /// <summary>
        /// Opens the file and adds data to the end. If no file exists, creates a new file. Should be used with the FileAccess.Write option.
        /// </summary>
        Append = 3,

        /// <summary>
        /// Creates a new file, overwriting any existing file.
        /// </summary>
        Create = 1,

        /// <summary>
        /// Creates a new file. If the file already exists, throws an exception.
        /// </summary>
        CreateNew = 2,

        /// <summary>
        /// Opens an existing file. If no file exists, throws an exception.
        /// </summary>
        Open = 4,

        /// <summary>
        /// Opens an existing file. If no file exists, creates a new file.
        /// </summary>
        OpenOrCreate = 5,

        /// <summary>
        /// Opens and truncates an existing file. If no file exists, creates a new file.
        /// </summary>
        Truncate = 0,

        /// <summary>
        /// The file is not open for read or write.
        /// </summary>
        None = -1
    }
}
