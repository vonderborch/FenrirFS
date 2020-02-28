// ***********************************************************************
// Assembly         : FenrirFS
// Component        : OpenMode.cs
// Author           : Christian Webber
// Created          : 2016-07-13
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="OpenMode.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     Defines the OpenMode enum.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Updated comments.
//            - 2.0.0 (2016-07-13) - Beta version.
// ***********************************************************************

namespace FenrirFS
{
    /// <summary>
    /// Defines the various modes to open a file system object.
    /// </summary>
    public enum OpenMode
    {
        /// <summary>
        /// Create the file system object if it does not exist
        /// </summary>
        CreateIfDoesNotExist = 1,

        /// <summary>
        /// Throw an error if the file system object does not exist
        /// </summary>
        ThrowIfDoesNotExist = 0,

        /// <summary>
        /// Return null if the file system object does not exist
        /// </summary>
        ReturnNullIfDoesNotExist = 2,
    }
}
