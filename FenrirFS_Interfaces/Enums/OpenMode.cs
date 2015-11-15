/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

namespace FenrirFS
{
    /// <summary>
    /// Modes that a file can be opened to as.
    /// </summary>
    public enum OpenMode
    {
        /// <summary>
        /// Normal, open the file or create it if it doesn't exist.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Open if it exists, otherwise fail.
        /// </summary>
        FailIfDoesNotExist = 1,
    }
}
