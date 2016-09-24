// ***********************************************************************
// Assembly         : FenrirFS
// Component        : WriteMode.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="WriteMode.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the WriteMode enum.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
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
        Append = 1,

        /// <summary>
        /// Truncate the file
        /// </summary>
        Truncate = 0
    }
}