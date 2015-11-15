/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

namespace FenrirFS
{
    /// <summary>
    /// The option to use during searching.
    /// </summary>
    public enum SearchOption
    {
        /// <summary>
        /// Search the top directory and all sub-directories.
        /// </summary>
        AllDirectories = 0,

        /// <summary>
        /// Search the top directory only.
        /// </summary>
        TopDirectoryOnly = 1,

        /// <summary>
        /// Search the sub-directories only
        /// </summary>
        SubDirectoriesOnly = 2,
    }
}
