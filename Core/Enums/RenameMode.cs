// ***********************************************************************
// Assembly         : FenrirFS
// Component        : RenameMode.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="RenameMode.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the RenameMode enum.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Define the various modes to rename a file system entry on a collision
    /// </summary>
    public enum RenameMode
    {
        /// <summary>
        /// Rename with an integer representing the rename number attempt
        /// </summary>
        Integer = 0,

        /// <summary>
        /// Rename with a number representing the time stamp ticks
        /// </summary>
        TimeStampTicks = 1
    }
}