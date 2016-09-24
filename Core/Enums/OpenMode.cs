// ***********************************************************************
// Assembly         : FenrirFS
// Component        : OpenMode.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="OpenMode.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the OpenMode enum.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Defines the various modes to open a directory or file object.
    /// </summary>
    public enum OpenMode
    {
        /// <summary>
        /// Create the file if it does not exist
        /// </summary>
        CreateIfDoesNotExist = 1,

        /// <summary>
        /// Throw an error if the file does not exist
        /// </summary>
        ThrowIfDoesNotExist = 0,

        /// <summary>
        /// Retrun a null object if the file does not exist
        /// </summary>
        ReturnNullIfDoesNotExist = 2
    }
}