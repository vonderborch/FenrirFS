// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FileMode.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="FileMode.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the FileMode enum.
// </summary>
//
// Changelog: 
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Enum FileMode
    /// </summary>
    public enum FileMode
    {
        /// <summary>
        /// The append
        /// </summary>
        Append = 3,

        /// <summary>
        /// The create
        /// </summary>
        Create = 1,

        /// <summary>
        /// The create new
        /// </summary>
        CreateNew = 2,

        /// <summary>
        /// The open
        /// </summary>
        Open = 4,

        /// <summary>
        /// The open or create
        /// </summary>
        OpenOrCreate = 5,

        /// <summary>
        /// The truncate
        /// </summary>
        Truncate = 0,

        /// <summary>
        /// The none
        /// </summary>
        None = -1
    }
}