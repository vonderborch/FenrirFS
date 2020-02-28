// ***********************************************************************
// Assembly         : FenrirFS
// Component        : RenameMode.cs
// Author           : Christian Webber
// Created          : 2016-07-13
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="RenameMode.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     Defines the RenameMode enum.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Added TimeStamp options. Updated comments.
//            - 2.0.0 (2016-07-13) - Beta version.
// ***********************************************************************

namespace FenrirFS
{
    /// <summary>
    /// Define the various modes to rename a file system entry on a collision
    /// </summary>
    public enum RenameMode
    {
        /// <summary>
        /// Rename with an integer representing the rename number attempt post-fixed to the file name
        /// </summary>
        Integer = 0,

        /// <summary>
        /// Rename with a number representing the ticks for the current timestamp post-fixed to the file name
        /// </summary>
        TimeStampTicks = 1,

        /// <summary>
        /// Rename with the current timestamp post-fixed to the file name
        /// </summary>
        TimeStamp = 2,

        /// <summary>
        /// Rename with the current UTC timestamp post-fixed to the file name
        /// </summary>
        TimeStampUtc = 3,
    }
}
