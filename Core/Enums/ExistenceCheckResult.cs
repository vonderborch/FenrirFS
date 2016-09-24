// ***********************************************************************
// Assembly         : FenrirFS
// Component        : ExistenceCheckResult.cs
// Author           : vonderborch
// Created          : 07-13-2016
//
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="ExistenceCheckResult.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the ExistenceCheckResult enum.
// </summary>
//
// Changelog:
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Defines the results that can occur on an existence check
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
        FileAndFolderExists = 3
    }
}