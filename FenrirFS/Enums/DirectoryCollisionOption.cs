// ***********************************************************************
// Assembly         : FenrirFS
// Component        : DirectoryCollisionOption.cs
// Author           : Christian Webber
// Created          : 2016-07-13
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="DirectoryCollisionOption.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     Defines the DirectoryCollisionOption enum.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Added GenerateUniqueNameForExistingDirectory option. Updated comments.
//            - 2.0.0 (2016-07-13) - Beta version.
// ***********************************************************************

namespace FenrirFS
{
    /// <summary>
    /// Defines the actions that can occur when a directory collision occurs.
    /// </summary>
    public enum DirectoryCollisionOption
    {
        /// <summary>
        /// Generate a unique name for the new directory on a collision
        /// </summary>
        GenerateUniqueName = 0,

        /// <summary>
        /// Replace the existing directory on a collision
        /// </summary>
        ReplaceExisting = 1,

        /// <summary>
        /// Fail on a collision
        /// </summary>
        FailIfExists = 2,

        /// <summary>
        /// Throw an error on a collision
        /// </summary>
        ThrowIfExists = 3,

        /// <summary>
        /// Open the existing directory on a collision
        /// </summary>
        OpenIfExists = 4,

        /// <summary>
        /// Generate a unique name for the existing directory
        /// </summary>
        GenerateUniqueNameForExistingDirectory = 5,
    }
}
