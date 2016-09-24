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
    /// <seealso cref="FenrirFS.FSDirectory" />
    public class NullDirectory : FSDirectory
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NullDirectory"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public NullDirectory(string path) : base(path)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullDirectory" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        public NullDirectory(string path, string name) : base(path, name)
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
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override FSDirectory Copy(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            return null;
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return null;
        }

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override FSDirectory CreateFolder(string folder, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            return null;
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
        /// <param name="file">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool DeleteFile(string file)
        {
            return false;
        }

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool DeleteFolder(string folder)
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
        public override bool Move(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
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
        public override bool Rename(string name, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            return false;
        }

        protected override List<FSFileSystemEntry> InternalGetFileSystemEntries(bool grabFiles, bool grabDirectories, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            return new List<FSFileSystemEntry>();
        }

        #endregion Public Methods
    }
}