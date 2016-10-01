// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FSFile.cs
// Author           : vonderborch
// Created          : 07-12-2016
//
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="FSFile.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      An abstract class representing a File.
// </summary>
//
// Changelog:
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using IO = System.IO;

/// <summary>
/// The FenrirFS namespace.
/// </summary>
namespace FenrirFS
{
    /// <summary>
    /// An abstract representation of a file.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="System.IEquatable{FenrirFS.FSFile}" />
    public abstract class FSFile : FSFileSystemEntry, IDisposable, IEquatable<FSFile>
    {
        #region Private Fields

        /// <summary>
        /// The encoding
        /// </summary>
        protected Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// Whether the encoding has been set
        /// </summary>
        protected bool encodingHasSet = false;

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FSFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FSFile(string path)
        {
            Name = IO.Path.GetFileNameWithoutExtension(path);
            Path = IO.Path.GetDirectoryName(path);
            Extension = IO.Path.GetExtension(path);

            FileSystemEntryType = FileSystemEntryType.File;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        /// <param name="extension">The extension.</param>
        public FSFile(string path, string name, string extension)
        {
            Name = name;
            Path = path;
            Extension = extension;

            FileSystemEntryType = FileSystemEntryType.File;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        public Encoding Encoding
        {
            get { return encodingHasSet ? encoding : GetEncoding(); }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="FSFile"/> is at the end of the stream.
        /// </summary>
        /// <value><c>true</c> if at the end of the stream; otherwise, <c>false</c>.</value>
        public bool EOS
        {
            get
            {
                return IsFileOpen
                    ? Stream.Length - 1 == Stream.Position
                    : false;
            }
        }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <value>The extension.</value>
        public string Extension { get; protected set; }

        /// <summary>
        /// Gets the file attributes.
        /// </summary>
        /// <value>The file attributes.</value>
        public FileAttributes FileAttributes
        {
            get { return GetFileAttributes(); }
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <value>The size of the file.</value>
        public long FileSize
        {
            get { return GetFileSize(); }
        }

        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        /// <value>The full path.</value>
        public override string FullPath
        {
            get { return IO.Path.Combine(Path, $"{Name}{Extension}"); }
            protected set
            {
                Path = IO.Path.GetDirectoryName(value);
                Name = IO.Path.GetFileNameWithoutExtension(value);
                Extension = IO.Path.GetExtension(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is file open.
        /// </summary>
        /// <value><c>true</c> if this instance is file open; otherwise, <c>false</c>.</value>
        public bool IsFileOpen
        {
            get { return Stream != null; }
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public IO.Stream Stream { get; protected set; }

        /// <summary>
        /// Gets the stream file access mode.
        /// </summary>
        /// <value>The stream file access mode.</value>
        public FileAccess StreamFileAccessMode { get; protected set; }

        /// <summary>
        /// Gets the stream file mode.
        /// </summary>
        /// <value>The stream file mode.</value>
        public FileMode StreamFileMode { get; protected set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="FSFile"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The result of the conversion.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static implicit operator string(FSFile file)
        {
            return file.FullPath;
        }

        /// <summary>
        /// Changes the extension of the file.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <param name="collisionOption">The collision option, if another file already exists.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the extension change succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ChangeExtension(extension);
        }

        /// <summary>
        /// Clears the contents of the file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the contents were cleared, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncClear(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Clear();
        }

        /// <summary>
        /// Closes the file stream if it is open.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the file stream was closed, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncClose(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Close();
        }

        /// <summary>
        /// Copies the file to the specified destination
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncCopy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Copy(destination, collisionOption);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the deletion succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncDelete(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Delete();
        }

        /// <summary>
        /// Gets the encoding of the file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The encoding of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<Encoding> AsyncGetEncoding(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetEncoding();
        }

        /// <summary>
        /// Gets the file attributes.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The file attributes.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<FileAttributes> AsyncGetFileAttributes(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileAttributes();
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The size of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<long> AsyncGetFileSize(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSize();
        }

        /// <summary>
        /// Moves the file to the specified specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncMove(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Move(destination, collisionOption);
        }

        /// <summary>
        /// Opens the file with the specified access and file mode.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A stream representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<IO.Stream> AsyncOpen(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Open(fileAccess, fileMode);
        }

        /// <summary>
        /// Reads all the contents of the file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The contents of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<string> AsyncReadAll(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ReadAll();
        }

        /// <summary>
        /// Reads the contents of the file as an XDocument.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>An XDocument representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<XDocument> AsyncReadAllAsXDocument(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ReadAllAsXDocument();
        }

        /// <summary>
        /// Reads the contents of the file and returns it as a string array.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A string array representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<string[]> AsyncReadAllLines(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ReadAllLines();
        }

        /// <summary>
        /// Reads the contents of the file and returns a string enumerable.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>An enumerable representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<IEnumerable<string>> AsyncReadLines(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ReadLines();
        }

        /// <summary>
        /// Removes an attribute from the file.
        /// </summary>
        /// <param name="attribute">The attribute to remove.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the attribute was removed, <c>false</c> otherwise.</returns>
        /// <exception cref="NotImplementedException"></exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Initial version.
        public async Task<bool> AsyncRemoveAttribute(FileAttributes attribute, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return RemoveAttribute(attribute);
        }

        /// <summary>
        /// Renames the file to the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option if a file with the new name already exists.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the rename was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncRename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Rename(name, collisionOption);
        }

        /// <summary>
        /// Sets the encoding of the file.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the encoding change was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncSetEncoding(Encoding encoding, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return SetEncoding(encoding);
        }

        /// <summary>
        /// Adds an attribute to the file.
        /// </summary>
        /// <param name="attribute">The attribute to add.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the attribute was added, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncSetFileAttribute(FileAttributes attribute, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return SetFileAttribute(attribute);
        }

        /// <summary>
        /// Reads the specified number of characters from the stream and returns it as a string.
        /// </summary>
        /// <param name="chars">The number of characters to read.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A string representing the characters.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<string> AsyncStreamRead(int chars, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamRead(chars);
        }

        /// <summary>
        /// Reads the entire stream and returns it as a string.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The contents of the stream.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<string> AsyncStreamReadAll(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamReadAll();
        }

        /// <summary>
        /// Reads the a line from the stream and returns it as a string.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The next line of the stream.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<string> AsyncStreamReadLine(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamReadLine();
        }

        /// <summary>
        /// Sets the position of the stream.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the position was set, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncStreamSetPosition(int position, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamSetPosition(position);
        }

        /// <summary>
        /// Writes the contents to the stream.
        /// </summary>
        /// <param name="contents">The contents to write to the stream.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the stream was written too, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncStreamWrite(string contents, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamWrite(contents);
        }

        /// <summary>
        /// Writes the contents to the stream and adds a new line to the end of the contents.
        /// </summary>
        /// <param name="contents">The contents to write to the stream.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the stream was written too, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncStreamWriteLine(string contents, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamWriteLine(contents);
        }

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode to use.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the write was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncWriteAll(string contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return WriteAll(contents, writeMode);
        }

        /// <summary>
        /// Writes the contents to the file and add a new line at the end.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode to use.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the write was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncWriteLine(string contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return WriteLine(contents, writeMode);
        }

        /// <summary>
        /// Changes the extension of the file.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <param name="collisionOption">The collision option, if another file already exists.</param>
        /// <returns><c>true</c> if the extension change succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool ChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Clears the contents of the file.
        /// </summary>
        /// <returns><c>true</c> if the contents were cleared, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public bool Clear()
        {
            return IsFileOpen
                ? false
                : WriteAll("", WriteMode.Truncate);
        }

        /// <summary>
        /// Closes the file stream if it is open.
        /// </summary>
        /// <returns><c>true</c> if the file stream was closed, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public bool Close()
        {
            if (IsFileOpen)
            {
                try
                {
                    switch (StreamFileAccessMode)
                    {
                        case FileAccess.ReadWrite:
                        case FileAccess.Write:
                            Stream.Flush();
                            break;
                    }

                    Stream.Dispose();
                    StreamFileAccessMode = FileAccess.None;
                    StreamFileMode = FileMode.None;
                    Stream = null;
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Copies the file to the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <returns><c>true</c> if the deletion succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool Delete();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public void Dispose()
        {
            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~FSFile() {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            //   Dispose(false);
            // }

            // This code added to correctly implement the disposable pattern.

            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public bool Equals(FSFile other)
        {
            return this.FullPath == other.FullPath;
        }

        /// <summary>
        /// Gets the encoding of the file.
        /// </summary>
        /// <returns>The encoding of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract Encoding GetEncoding();

        /// <summary>
        /// Gets the file attributes.
        /// </summary>
        /// <returns>The file attributes.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract FileAttributes GetFileAttributes();

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <returns>The size of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract long GetFileSize();

        /// <summary>
        /// Moves the file to the specified specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Opens the file with the specified access and file mode.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>A stream representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract IO.Stream Open(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate);

        /// <summary>
        /// Reads all the contents of the file.
        /// </summary>
        /// <returns>The contents of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract string ReadAll();

        /// <summary>
        /// Reads the contents of the file as an XDocument.
        /// </summary>
        /// <returns>An XDocument representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract XDocument ReadAllAsXDocument();

        /// <summary>
        /// Reads the contents of the file and returns it as a string array.
        /// </summary>
        /// <returns>A string array representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract string[] ReadAllLines();

        /// <summary>
        /// Reads the contents of the file and returns a string enumerable.
        /// </summary>
        /// <returns>An enumerable representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract IEnumerable<string> ReadLines();

        /// <summary>
        /// Removes an attribute from the file.
        /// </summary>
        /// <param name="attribute">The attribute to remove.</param>
        /// <returns><c>true</c> if the attribute was removed, <c>false</c> otherwise.</returns>
        /// <exception cref="NotImplementedException"></exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Initial version.
        public abstract bool RemoveAttribute(FileAttributes attribute);

        /// <summary>
        /// Renames the file to the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option if a file with the new name already exists.</param>
        /// <returns><c>true</c> if the rename was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Sets the encoding of the file.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns><c>true</c> if the encoding change was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool SetEncoding(Encoding encoding);

        /// <summary>
        /// Adds an attribute to the file.
        /// </summary>
        /// <param name="attribute">The attribute to add.</param>
        /// <returns><c>true</c> if the attribute was added, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool SetFileAttribute(FileAttributes attribute);

        /// <summary>
        /// Reads the specified number of characters from the stream and returns it as a string.
        /// </summary>
        /// <param name="chars">The number of characters to read.</param>
        /// <returns>A string representing the characters.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public string StreamRead(int chars)
        {
            if (!IsFileOpen && StreamFileAccessMode != FileAccess.Write)
            {
                byte[] buffer = new byte[chars];

                Stream.Read(buffer, 0, chars);

                return Encoding.GetString(buffer, 0, chars);
            }

            return null;
        }

        /// <summary>
        /// Reads the entire stream and returns it as a string.
        /// </summary>
        /// <returns>The contents of the stream.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public string StreamReadAll()
        {
            if (IsFileOpen && StreamFileAccessMode != FileAccess.Write)
            {
                byte[] buffer = new byte[Stream.Length];

                Stream.Read(buffer, 0, buffer.Length);

                return Encoding.GetString(buffer, 0, buffer.Length);
            }

            return null;
        }

        /// <summary>
        /// Reads the a line from the stream and returns it as a string.
        /// </summary>
        /// <returns>The next line of the stream.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public string StreamReadLine()
        {
            if (IsFileOpen && StreamFileAccessMode != FileAccess.Write)
            {
                StringBuilder str = new StringBuilder();

                int value = -1;
                do
                {
                    value = Stream.ReadByte();
                    if (value != -1)
                    {
                        char c = Convert.ToChar((byte)value);
                        str.Append(c);

                        switch (c)
                        {
                            case '\n': value = -1; break;
                            case '\r':
                                value = Stream.ReadByte();
                                char nc = Convert.ToChar((byte)value);
                                str.Append(nc);
                                if (nc == '\n')
                                    value = -1;
                                break;
                        }
                    }
                } while (value != -1);

                return str.ToString();
            }

            return null;
        }

        /// <summary>
        /// Sets the position of the stream.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns><c>true</c> if the position was set, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public bool StreamSetPosition(int position)
        {
            if (IsFileOpen && position >= 0 && position < Stream.Length)
            {
                Stream.Position = position;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes the contents to the stream.
        /// </summary>
        /// <param name="contents">The contents to write to the stream.</param>
        /// <returns><c>true</c> if the stream was written too, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public bool StreamWrite(string contents)
        {
            if (IsFileOpen && StreamFileAccessMode != FileAccess.Read)
            {
                byte[] buffer = new byte[contents.Length];

                buffer = Encoding.GetBytes(contents);

                Stream.Write(buffer, 0, buffer.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes the contents to the stream and adds a new line to the end of the contents.
        /// </summary>
        /// <param name="contents">The contents to write to the stream.</param>
        /// <returns><c>true</c> if the stream was written too, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public bool StreamWriteLine(string contents)
        {
            if (IsFileOpen && StreamFileAccessMode != FileAccess.Read && contents.Length == int.MaxValue)
            {
                byte[] buffer = new byte[contents.Length + 1];

                buffer = Encoding.GetBytes($"{contents}{Environment.NewLine}");

                Stream.Write(buffer, 0, buffer.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override string ToString()
        {
            return FullPath;
        }

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode to use.</param>
        /// <returns><c>true</c> if the write was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate);

        /// <summary>
        /// Writes the contents to the file and add a new line at the end.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode to use.</param>
        /// <returns><c>true</c> if the write was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool WriteLine(string contents, WriteMode writeMode = WriteMode.Truncate);

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (IsFileOpen)
                        Close();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}