// ***********************************************************************
// Assembly         : FenrirFS
// Component        : OpenMode.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="OpenMode.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the OpenMode enum.
// </summary>
//
// Changelog: 
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Enum OpenMode
    /// </summary>
    public enum OpenMode
    {
        /// <summary>
        /// The create if does not exist
        /// </summary>
        CreateIfDoesNotExist = 1,

        /// <summary>
        /// The throw if does not exist
        /// </summary>
        ThrowIfDoesNotExist = 0,

        /// <summary>
        /// The return null if does not exist
        /// </summary>
        ReturnNullIfDoesNotExist = 2
    }
}