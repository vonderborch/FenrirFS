// ***********************************************************************
// Assembly         : FenrirFS
// Component        : SearchOption.cs
// Author           : Christian Webber
// Created          : 2016-07-13
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="SearchOption.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     Defines the SearchOption enum.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Updated enum values.
//            - 2.0.0 (2016-07-13) - Beta version.
// ***********************************************************************

namespace FenrirFS
{
    /// <summary>
    /// Defines the locations to search for files
    /// </summary>
    public enum SearchOption
    {
        /// <summary>
        /// Both the top directory and any sub-directories
        /// </summary>
        All = 3,

        /// <summary>
        /// The top directory only
        /// </summary>
        TopDirectoryOnly = 1,

        /// <summary>
        /// The sub directories only
        /// </summary>
        SubDirectoriesOnly = 2,
    }
}
