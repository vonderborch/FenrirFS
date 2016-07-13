// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FS.cs
// Author           : vonderborch
// Created          : 07-12-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="FS.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Access point for getting a File or Folder object.
// </summary>
//
// Changelog: 
//            - 1.0.0 (07-12-2016) - Initial version created.
// ***********************************************************************
using FenrirFS.FileSystem;

namespace FenrirFS
{
    /// <summary>
    /// Class FS.
    /// </summary>
    public static class FS
    {
        #region Public Methods

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>FSFile.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public static FSFile GetFile(string path)
        {
#if CORE
            return new NullFile(path);
#elif IMPLEMENTATION
            return null;
#else
            throw new NotSupportedException("There is no File implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>FSFolder.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public static FSFolder GetFolder(string path)
        {
#if CORE
            return new NullFolder(path);
#elif IMPLEMENTATION
            return null;
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

        #endregion Public Methods
    }
}