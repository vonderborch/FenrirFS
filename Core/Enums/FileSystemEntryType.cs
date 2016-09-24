// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FileSystemEntryType.cs
// Author           : vonderborch
// Created          : 09-23-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="FileSystemEntryType.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the FileSystemEntryType enum.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
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
        None = 0,

        /// <summary>
        /// The entry is a directory
        /// </summary>
        Directory = 1,

        /// <summary>
        /// The entry is a file
        /// </summary>
        File = 2
    }
}