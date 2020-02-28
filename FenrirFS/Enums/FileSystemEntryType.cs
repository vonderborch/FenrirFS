// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FileSystemEntryType.cs
// Author           : Christian Webber
// Created          : 2016-07-13
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="FileSystemEntryType.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     Defines the FileSystemEntryType enum.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Updated comments. Updated values.
//            - 2.0.0 (2016-07-13) - Beta version.
// ***********************************************************************

namespace FenrirFS
{
    /// <summary>
    /// Defines the possible types available for File System Entries
    /// </summary>
    public enum FileSystemEntryType
    {
        /// <summary>
        /// The entry has no type
        /// </summary>
        None = -100,

        /// <summary>
        /// The entry is a directory
        /// </summary>
        Directory = 1,

        /// <summary>
        /// The entry is a file
        /// </summary>
        File = 2,

        /// <summary>
        /// The entry is a symbolically linked file
        /// </summary>
        SymbolicDirectory = 4,

        /// <summary>
        /// The entry is a symbolically linked directory
        /// </summary>
        SymbolicFile = 8,
    }
}
