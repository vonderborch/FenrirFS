// ***********************************************************************
// Assembly         : FenrirFS
// Component        : WriteMode.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.0
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
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Enum WriteMode
    /// </summary>
    public enum WriteMode
    {
        /// <summary>
        /// The append
        /// </summary>
        Append = 1,

        /// <summary>
        /// The truncate
        /// </summary>
        Truncate = 0
    }
}