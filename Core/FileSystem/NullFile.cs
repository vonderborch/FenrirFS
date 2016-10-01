// ***********************************************************************
// Assembly         : FenrirFS
// Component        : NullFile.cs
// Author           : vonderborch
// Created          : 07-13-2016
//
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="NullFile.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      A null implementation of the FSFile class.
// </summary>
//
// Changelog:
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace FenrirFS.FileSystem
{
    /// <summary>
    /// Defines the null version of an FSFile.
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
        /// Gets a value indicating whether the [file exists].
        /// </summary>
        /// <value><c>true</c> if the [file exists]; otherwise, <c>false</c>.</value>
        public override bool Exists
        {
            get { return false; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Changes the extension of the file.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <param name="collisionOption">The collision option, if another file already exists.</param>
        /// <returns><c>true</c> if the extension change succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool ChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Copies the file to the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <returns><c>true</c> if the deletion succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Delete()
        {
            return false;
        }

        /// <summary>
        /// Gets the creation time of the file.
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
        /// Gets the encoding of the file.
        /// </summary>
        /// <returns>The encoding of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override Encoding GetEncoding()
        {
            return Encoding.UTF8;
        }

        /// <summary>
        /// Gets the file attributes.
        /// </summary>
        /// <returns>The file attributes.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override FileAttributes GetFileAttributes()
        {
            return FileAttributes.Offline;
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <returns>The size of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override long GetFileSize()
        {
            return long.MinValue;
        }

        /// <summary>
        /// Gets the last accessed time of the file.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last accessed time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetLastAccessedTime(bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the last modified time of the file.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last modified time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetLastModifiedTime(bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        /// <summary>
        /// Moves the file to the specified specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Opens the file with the specified access and file mode.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>A stream representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override Stream Open(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            return null;
        }

        /// <summary>
        /// Reads all the contents of the file.
        /// </summary>
        /// <returns>The contents of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override string ReadAll()
        {
            return null;
        }

        /// <summary>
        /// Reads the contents of the file as an XDocument.
        /// </summary>
        /// <returns>An XDocument representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override XDocument ReadAllAsXDocument()
        {
            return null;
        }

        /// <summary>
        /// Reads the contents of the file and returns it as a string array.
        /// </summary>
        /// <returns>A string array representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override string[] ReadAllLines()
        {
            return null;
        }

        /// <summary>
        /// Reads the contents of the file and returns a string enumerable.
        /// </summary>
        /// <returns>An enumerable representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override IEnumerable<string> ReadLines()
        {
            return null;
        }

        /// <summary>
        /// Removes an attribute from the file.
        /// </summary>
        /// <param name="attribute">The attribute to remove.</param>
        /// <returns><c>true</c> if the attribute was removed, <c>false</c> otherwise.</returns>
        /// <exception cref="NotImplementedException"></exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Initial version.
        public override bool RemoveAttribute(FileAttributes attribute)
        {
            return false;
        }

        /// <summary>
        /// Renames the file to the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option if a file with the new name already exists.</param>
        /// <returns><c>true</c> if the rename was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return false;
        }

        /// <summary>
        /// Sets the encoding of the file.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns><c>true</c> if the encoding change was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool SetEncoding(Encoding encoding)
        {
            return false;
        }

        /// <summary>
        /// Adds an attribute to the file.
        /// </summary>
        /// <param name="attribute">The attribute to add.</param>
        /// <returns><c>true</c> if the attribute was added, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool SetFileAttribute(FileAttributes attribute)
        {
            return false;
        }

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode to use.</param>
        /// <returns><c>true</c> if the write was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            return false;
        }

        /// <summary>
        /// Writes the contents to the file and add a new line at the end.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode to use.</param>
        /// <returns><c>true</c> if the write was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool WriteLine(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            return false;
        }

        #endregion Public Methods
    }
}