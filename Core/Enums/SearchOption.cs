// ***********************************************************************
// Assembly         : FenrirFS
// Component        : SearchOption.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="SearchOption.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the SearchOption enum.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
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
        All = 0,

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