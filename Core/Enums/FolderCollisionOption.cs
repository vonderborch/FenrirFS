// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FolderCollisionOption.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="FolderCollisionOption.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the FolderCollisionOption enum.
// </summary>
//
// Changelog: 
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Enum FolderCollisionOption
    /// </summary>
    public enum FolderCollisionOption
    {
        /// <summary>
        /// The generate unique name
        /// </summary>
        GenerateUniqueName = 0,

        /// <summary>
        /// The replace existing
        /// </summary>
        ReplaceExisting = 1,

        /// <summary>
        /// The fail if exists
        /// </summary>
        FailIfExists = 2,

        /// <summary>
        /// The throw if exists
        /// </summary>
        ThrowIfExists = 3,

        /// <summary>
        /// The open if exists
        /// </summary>
        OpenIfExists = 4,
    }
}