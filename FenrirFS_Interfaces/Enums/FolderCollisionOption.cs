/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

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
