// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FileAccess.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="FileAccess.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the FileAccess enum.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
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
        Read = 0,

        /// <summary>
        /// Read and Write mode
        /// </summary>
        ReadWrite = 2,

        /// <summary>
        /// Write mode
        /// </summary>
        Write = 1,

        /// <summary>
        /// No file access mode selected
        /// </summary>
        None = -1
    }
}