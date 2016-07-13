// ***********************************************************************
// Assembly         : FenrirFS
// Component        : SearchOption.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="SearchOption.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the SearchOption enum.
// </summary>
//
// Changelog: 
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Enum SearchOption
    /// </summary>
    public enum SearchOption
    {
        /// <summary>
        /// All
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