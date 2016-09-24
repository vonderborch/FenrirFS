// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FileCollisionOption.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="FileCollisionOption.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the FileCollisionOption enum.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Defines the options that can occur when a file collision occurs.
    /// </summary>
    public enum FileCollisionOption
    {
        /// <summary>
        /// Generate a unique name on a collision
        /// </summary>
        GenerateUniqueName = 0,

        /// <summary>
        /// Replace an existing file on a collision
        /// </summary>
        ReplaceExisting = 1,

        /// <summary>
        /// Fail on a collision
        /// </summary>
        FailIfExists = 2,

        /// <summary>
        /// Throw an error on a collision
        /// </summary>
        ThrowIfExists = 3,

        /// <summary>
        /// Open the existing directory on a collision
        /// </summary>
        OpenIfExists = 4
    }
}