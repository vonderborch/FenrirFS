// ***********************************************************************
// Assembly         : FenrirFS
// Component        : WriteMode.cs
// Author           : Christian Webber
// Created          : 2016-07-13
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="WriteMode.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     Defines the WriteMode enum.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Updated enum values.
//            - 2.0.0 (2016-07-13) - Beta version.
// ***********************************************************************

namespace FenrirFS
{
    /// <summary>
    /// Defines the modes to write to a file
    /// </summary>
    public enum WriteMode
    {
        /// <summary>
        /// Append the text to the file
        /// </summary>
        Append = 2,

        /// <summary>
        /// Truncate the file
        /// </summary>
        Truncate = 1,
    }
}
