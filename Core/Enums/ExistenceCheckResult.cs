// ***********************************************************************
// Assembly         : FenrirFS
// Component        : ExistenceCheckResult.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="ExistenceCheckResult.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the ExistenceCheckResult enum.
// </summary>
//
// Changelog: 
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
namespace FenrirFS
{
    /// <summary>
    /// Enum ExistenceCheckResult
    /// </summary>
    public enum ExistenceCheckResult
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The file exists
        /// </summary>
        FileExists = 1,

        /// <summary>
        /// The folder exists
        /// </summary>
        FolderExists = 2,

        /// <summary>
        /// The file and folder exists
        /// </summary>
        FileAndFolderExists = 3
    }
}