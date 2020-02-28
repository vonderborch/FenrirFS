// ***********************************************************************
// Assembly         : FenrirFS
// Component        : ExistenceCheckResult.cs
// Author           : Christian Webber
// Created          : 2016-07-13
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="ExistenceCheckResult.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     Defines the ExistenceCheckResult enum.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Updated comments.
//            - 2.0.0 (2016-07-13) - Beta version.
// ***********************************************************************

namespace FenrirFS
{
    /// <summary>
    /// Defines the results of an existence check
    /// </summary>
    public enum ExistenceCheckResult
    {
        /// <summary>
        /// No collision has occurred
        /// </summary>
        None = 0,

        /// <summary>
        /// A file exists
        /// </summary>
        FileExists = 1,

        /// <summary>
        /// A folder exists
        /// </summary>
        FolderExists = 2,

        /// <summary>
        /// A file and a folder exists
        /// </summary>
        FileAndFolderExists = 3,
    }
}
