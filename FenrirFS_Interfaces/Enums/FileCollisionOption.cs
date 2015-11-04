/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

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
