// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FileAccess.cs
// Author           : Christian Webber
// Created          : 2016-07-13
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="FileAccess.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     Defines the FileAccess enum.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Updated enum values.
//            - 2.0.0 (2016-07-13) - Beta version.
// ***********************************************************************

namespace FenrirFS
{
    /// <summary>
    /// Defines the possible file access modes.
    /// </summary>
    public enum FileAccess
    {
        /// <summary>
        /// Read mode
        /// </summary>
        Read = 1,

        /// <summary>
        /// Read and Write mode
        /// </summary>
        ReadWrite = 11,

        /// <summary>
        /// Write mode
        /// </summary>
        Write = 10,

        /// <summary>
        /// No file access mode selected
        /// </summary>
        None = -11
    }
}
