// ***********************************************************************
// Assembly         : FenrirFS
// Component        : NullFile.cs
// Author           : vonderborch
// Created          : 07-13-2016
// 
// Version          : 1.0.1
// Last Modified By : vonderborch
// Last Modified On : 09-22-2016
// ***********************************************************************
// <copyright file="NullFile.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      A null implementation of the FSFile class.
// </summary>
//
// Changelog: 
//            - 1.0.1 (09-22-2016) - Added RemoveAttribute function.
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace FenrirFS.FileSystem
{
    /// <summary>
    /// Class NullFile.
    /// </summary>
    /// <seealso cref="FenrirFS.FSFile" />
    public class NullFile : FSFile
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NullFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public NullFile(string path) : base(path)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullFile" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        /// <param name="extension">The extension.</param>
        public NullFile(string path, string name, string extension) : base(path, name, extension)
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
        /// Changes the extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool ChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return false;
        }

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
        public override bool Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
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
        /// Gets the encoding.
        /// </summary>
        /// <returns>Encoding.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override Encoding GetEncoding()
        {
            return Encoding.UTF8;
        }

        /// <summary>
        /// Gets the file attributes.
        /// </summary>
        /// <returns>FileAttributes.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override FileAttributes GetFileAttributes()
        {
            return FileAttributes.Offline;
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <returns>System.Int64.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override long GetFileSize()
        {
            return long.MinValue;
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
        public override bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Opens the specified file access.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>IO.Stream.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override Stream Open(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            return null;
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns>System.String.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override string ReadAll()
        {
            return null;
        }

        /// <summary>
        /// Reads all as x document.
        /// </summary>
        /// <returns>XDocument.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override XDocument ReadAllAsXDocument()
        {
            return null;
        }

        /// <summary>
        /// Reads all lines.
        /// </summary>
        /// <returns>System.String[].</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override string[] ReadAllLines()
        {
            return null;
        }

        /// <summary>
        /// Reads the line.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override IEnumerable<string> ReadLine()
        {
            return null;
        }

        /// <summary>
        /// Removes an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Changelog:
        /// - 1.0.0 (09-22-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (09-22-2016) - Initial version.
        public override bool RemoveAttribute(FileAttributes attribute)
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
        public override bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Sets the encoding.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool SetEncoding(Encoding encoding)
        {
            return false;
        }

        /// <summary>
        /// Sets the file attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool SetFileAttributes(FileAttributes attributes)
        {
            return false;
        }

        /// <summary>
        /// Writes all.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            return false;
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// Changelog:
        /// - 1.0.0 (07-12-2016) - Initial version.
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override bool WriteLine(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            return false;
        }

        #endregion Public Methods
    }
}