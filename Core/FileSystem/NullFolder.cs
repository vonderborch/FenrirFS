// ***********************************************************************
// Assembly         : FenrirFS
// Component        : NullFolder.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-14-2016
// ***********************************************************************
// <copyright file="NullFolder.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      A null implementation of the FSFolder class.
// </summary>
//
// Changelog: 
//            - 1.0.0 (07-14-2016) - Initial version created.
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace FenrirFS.FileSystem
{
    /// <summary>
    /// Class NullFolder.
    /// </summary>
    /// <seealso cref="FenrirFS.FSFolder" />
    public class NullFolder : FSFolder
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NullFolder"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public NullFolder(string path) : base(path)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullFolder" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        public NullFolder(string path, string name) : base(path, name)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether [file exists].
        /// </summary>
        /// <value><c>true</c> if [file exists]; otherwise, <c>false</c>.</value>
        public override bool Exists
        {
            get { return false; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Copies the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool Copy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool CreateFile(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool CreateFolder(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool Delete()
        {
            return false;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool DeleteFile(string name)
        {
            return false;
        }

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool DeleteFolder(string name)
        {
            return false;
        }

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool FileExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return false;
        }

        /// <summary>
        /// Folders the exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool FolderExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return false;
        }

        /// <summary>
        /// Gets the creation time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>DateTime.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override DateTime GetCreationTime(bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>FSFile.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override FSFile GetFile(string name)
        {
            return null;
        }

        /// <summary>
        /// Gets the file names.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override List<string> GetFileNames()
        {
            return null;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <returns>List&lt;FSFile&gt;.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override List<FSFile> GetFiles()
        {
            return null;
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>FSFolder.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override FSFolder GetFolder(string name)
        {
            return null;
        }

        /// <summary>
        /// Gets the folder names.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override List<string> GetFolderNames()
        {
            return null;
        }

        /// <summary>
        /// Gets the folders.
        /// </summary>
        /// <returns>List&lt;FSFolder&gt;.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override List<FSFolder> GetFolders()
        {
            return null;
        }

        /// <summary>
        /// Gets the last accessed time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>DateTime.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override DateTime GetLastAccessedTime(bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>DateTime.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override DateTime GetLastModifiedTime(bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        /// <summary>
        /// Moves the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool Move(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Renames the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool Rename(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            return false;
        }

        #endregion Public Methods
    }
}