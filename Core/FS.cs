// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FS.cs
// Author           : vonderborch
// Created          : 07-12-2016
// 
// Version          : 1.1.0
// Last Modified By : vonderborch
// Last Modified On : 07-14-2016
// ***********************************************************************
// <copyright file="FS.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Access point for getting a File or Folder object.
// </summary>
//
// Changelog: 
//            - 1.1.0 (07-14-2016) - Added OpenMode parameter to GetFile and GetFolder functions and added a new version of both to handle both constructors.
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
        /// <param name="openMode">The open mode.</param>
        /// <returns>FenrirFS.FSFile.</returns>
        ///  Changelog:
        ///             - 1.1.0 (07-14-2016) - Added OpenMode parameter
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public static FSFile GetFile(string path, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

#if CORE
            return new NullFile(path);
#elif IMPLEMENTATION
            return new FenrirFile(path);
#else
            throw new NotSupportedException("There is no File implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="openMode">The open mode.</param>
        /// <returns>FSFile.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-14-2016) - Initial version.
        public static FSFile GetFile(string path, string name, string extension, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Helpers.Validation.NotNullOrWhiteSpaceCheck(name, nameof(name));
            Helpers.Validation.NotNullOrWhiteSpaceCheck(extension, nameof(extension));

#if CORE
            return new NullFile(path, name, extension);
#elif IMPLEMENTATION
            return new FenrirFile(path, name, extension);
#else
            throw new NotSupportedException("There is no File implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode.</param>
        /// <returns>FSFolder.</returns>
        ///  Changelog:
        ///             - 1.1.0 (07-14-2016) - Added OpenMode parameter
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public static FSFolder GetFolder(string path, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

#if CORE
            return new NullFolder(path);
#elif IMPLEMENTATION
            return new FenrirFolder(path);
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode.</param>
        /// <returns>FSFolder.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-14-2016) - Initial version.
        public static FSFolder GetFolder(string path, string name, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Helpers.Validation.NotNullOrWhiteSpaceCheck(name, nameof(name));

#if CORE
            return new NullFolder(path, name);
#elif IMPLEMENTATION
            return new FenrirFolder(path, name);
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

        #endregion Public Methods
    }
}