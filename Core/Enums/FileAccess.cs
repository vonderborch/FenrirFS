// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FileAccess.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="FileAccess.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the FileAccess enum.
// </summary>
//
// Changelog: 
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Enum FileAccess
    /// </summary>
    public enum FileAccess
    {
        /// <summary>
        /// The read
        /// </summary>
        Read = 0,

        /// <summary>
        /// The read write
        /// </summary>
        ReadWrite = 2,

        /// <summary>
        /// The write
        /// </summary>
        Write = 1,

        /// <summary>
        /// The none
        /// </summary>
        None = -1
    }
}