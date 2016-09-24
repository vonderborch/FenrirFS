// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FSFile.cs
// Author           : vonderborch
// Created          : 07-12-2016
// 
// Version          : 1.0.1
// Last Modified By : vonderborch
// Last Modified On : 09-22-2016
// ***********************************************************************
// <copyright file="FSFile.cs"=>
//		Copyright ©  2016
// </copyright>
// <summary>
//      An abstract class representing a File.
// </summary>
//
// Changelog: 
//            - 1.0.1 (09-22-2016) - Added RemoveAttribute function.
//            - 1.0.0 (07-12-2016) - Initial version created.
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
    /// Class FSFile.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="System.IEquatable{FenrirFS.FSFile}" />
    public abstract class FSFile : FSFileSystemEntry, IDisposable, IEquatable<FSFile>
    {
        #region Private Fields

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false;

        protected Encoding encoding = Encoding.UTF8;
        protected bool encodingHasSet = false;

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
        /// Gets a value indicating whether this <see cref="FSFile"/> is eos.
        /// </summary>
        /// <value><c>true</c> if eos; otherwise, <c>false</c>.</value>
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
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public static implicit operator string(FSFile file)
        {
            return file.FullPath;
        }

        /// <summary>
        /// Asynchronouses the change extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ChangeExtension(extension);
        }

        /// <summary>
        /// Asynchronouses the clear.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncClear(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Clear();
        }

        /// <summary>
        /// Asynchronouses the close.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncClose(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Close();
        }

        /// <summary>
        /// Asynchronouses the copy.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncCopy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Copy(destination, collisionOption);
        }

        /// <summary>
        /// Asynchronouses the delete.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncDelete(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Delete();
        }

        /// <summary>
        /// Asynchronouses the get encoding.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;Encoding&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<Encoding> AsyncGetEncoding(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetEncoding();
        }

        /// <summary>
        /// Asynchronouses the get file attributes.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;FileAttributes&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<FileAttributes> AsyncGetFileAttributes(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileAttributes();
        }

        /// <summary>
        /// Asynchronouses the size of the get file.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Int64&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<long> AsyncGetFileSize(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSize();
        }

        /// <summary>
        /// Asynchronouses the move.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncMove(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Move(destination, collisionOption);
        }

        /// <summary>
        /// Asynchronouses the open.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IO.Stream&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<IO.Stream> AsyncOpen(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Open(fileAccess, fileMode);
        }

        /// <summary>
        /// Asynchronouses the read all.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<string> AsyncReadAll(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ReadAll();
        }

        /// <summary>
        /// Asynchronouses the read all as x document.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;XDocument&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<XDocument> AsyncReadAllAsXDocument(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ReadAllAsXDocument();
        }

        /// <summary>
        /// Asynchronouses the read all lines.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String[]&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<string[]> AsyncReadAllLines(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ReadAllLines();
        }

        /// <summary>
        /// Asynchronouses the read line.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<IEnumerable<string>> AsyncReadLine(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ReadLine();
        }

        /// <summary>
        /// Asynchronouses the remove attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (09-22-2016) - Initial version.
        public async Task<bool> AsyncRemoveAttribute(FileAttributes attribute, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return RemoveAttribute(attribute);
        }

        /// <summary>
        /// Asynchronouses the rename.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncRename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Rename(name, collisionOption);
        }

        /// <summary>
        /// Asynchronouses the set encoding.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncSetEncoding(Encoding encoding, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return SetEncoding(encoding);
        }

        /// <summary>
        /// Asynchronouses the set file attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncSetFileAttributes(FileAttributes attributes, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return SetFileAttributes(attributes);
        }

        /// <summary>
        /// Asynchronouses the stream read.
        /// </summary>
        /// <param name="chars">The chars.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<string> AsyncStreamRead(int chars, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamRead(chars);
        }

        /// <summary>
        /// Asynchronouses the stream read all.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<string> AsyncStreamReadAll(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamReadAll();
        }

        /// <summary>
        /// Asynchronouses the stream read line.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<string> AsyncStreamReadLine(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamReadLine();
        }

        /// <summary>
        /// Asynchronouses the stream set position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncStreamSetPosition(int position, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamSetPosition(position);
        }

        /// <summary>
        /// Asynchronouses the stream write.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncStreamWrite(string contents, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamWrite(contents);
        }

        /// <summary>
        /// Asynchronouses the stream write line.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncStreamWriteLine(string contents, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return StreamWriteLine(contents);
        }

        /// <summary>
        /// Asynchronouses the write all.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncWriteAll(string contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return WriteAll(contents, writeMode);
        }

        /// <summary>
        /// Asynchronouses the write line.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncWriteLine(string contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return WriteLine(contents, writeMode);
        }

        /// <summary>
        /// Changes the extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool ChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public bool Clear()
        {
            return IsFileOpen
                ? false
                : WriteAll("", WriteMode.Truncate);
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
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
        /// Copies the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool Delete();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
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
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public bool Equals(FSFile other)
        {
            return this.FullPath == other.FullPath;
        }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <returns>Encoding.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract Encoding GetEncoding();

        /// <summary>
        /// Gets the file attributes.
        /// </summary>
        /// <returns>FileAttributes.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract FileAttributes GetFileAttributes();

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <returns>System.Int64.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract long GetFileSize();

        /// <summary>
        /// Moves the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Opens the specified file access.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>IO.Stream.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract IO.Stream Open(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns>System.String.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract string ReadAll();

        /// <summary>
        /// Reads all as x document.
        /// </summary>
        /// <returns>XDocument.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract XDocument ReadAllAsXDocument();

        /// <summary>
        /// Reads all lines.
        /// </summary>
        /// <returns>System.String[].</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract string[] ReadAllLines();

        /// <summary>
        /// Reads the line.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract IEnumerable<string> ReadLine();

        /// <summary>
        /// Removes an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (09-22-2016) - Initial version.
        public abstract bool RemoveAttribute(FileAttributes attribute);

        /// <summary>
        /// Renames the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Sets the encoding.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool SetEncoding(Encoding encoding);

        /// <summary>
        /// Sets the file attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool SetFileAttributes(FileAttributes attributes);

        /// <summary>
        /// Streams the read.
        /// </summary>
        /// <param name="chars">The chars.</param>
        /// <returns>System.String.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
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
        /// Streams the read all.
        /// </summary>
        /// <returns>System.String.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
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
        /// Streams the read line.
        /// </summary>
        /// <returns>System.String.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
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
        /// Streams the set position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
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
        /// Streams the write.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
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
        /// Streams the write line.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
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
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public override string ToString()
        {
            return FullPath;
        }

        /// <summary>
        /// Writes all.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate);

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool WriteLine(string contents, WriteMode writeMode = WriteMode.Truncate);

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
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