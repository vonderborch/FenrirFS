// ***********************************************************************
// Assembly         : FenrirFS
// Component        : NullFolder.cs
// Author           : vonderborch
// Created          : 07-13-2016
//
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="NullFolder.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      A null implementation of the FSFolder class.
// </summary>
//
// Changelog:
//            - 2.0.0 (09-24-2016) - Beta version.
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
        /// Gets a value indicating whether [directory exists].
        /// </summary>
        /// <value><c>true</c> if [directory exists]; otherwise, <c>false</c>.</value>
        public override bool Exists
        {
            get { return false; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Copies the directory to the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override FSDirectory Copy(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            return null;
        }

        /// <summary>
        /// Creates a new file in the directory.
        /// </summary>
        /// <param name="file">The name for the file.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns>A file structure representing the new file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return null;
        }

        /// <summary>
        /// Creates a new directory in the directory.
        /// </summary>
        /// <param name="directory">The name for the directory.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns>A directory structure representing the new directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override FSDirectory CreateDirectory(string directory, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            return null;
        }

        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <returns><c>true</c> if the directory was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Delete()
        {
            return false;
        }

        /// <summary>
        /// Deletes a file in the directory.
        /// </summary>
        /// <param name="file">The name of the file to delete.</param>
        /// <returns><c>true</c> if the file was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool DeleteFile(string file)
        {
            return false;
        }

        /// <summary>
        /// Deletes a directory in the directory.
        /// </summary>
        /// <param name="directory">The name of the directory to delete.</param>
        /// <returns><c>true</c> if the directory was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool DeleteDirectory(string directory)
        {
            return false;
        }

        /// <summary>
        /// Gets the creation time of the directory.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The creation time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetCreationTime(bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the last accessed time of the directory.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last accessed time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetLastAccessedTime(bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the last modified time of the directory.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last modified time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetLastModifiedTime(bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        /// <summary>
        /// Moves the directory to the specified specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Move(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Renames the directory to the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option if a file with the new name already exists.</param>
        /// <returns><c>true</c> if the rename was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Rename(string name, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Internal method to get file system entries.
        /// </summary>
        /// <param name="grabFiles">if set to <c>true</c> [grabs files].</param>
        /// <param name="grabDirectories">if set to <c>true</c> [grabs directories].</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of file system entries matching the desired parameters.</returns>
        /// Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        protected override List<FSFileSystemEntry> InternalGetFileSystemEntries(bool grabFiles, bool grabDirectories, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            return new List<FSFileSystemEntry>();
        }

        #endregion Public Methods
    }
}