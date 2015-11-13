/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */
 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FenrirFS
{
    /// <summary>
    /// Represents an abstract file in FenrirFS.
    /// </summary>
    public abstract class AFile : IDisposable, IEquatable<AFile>
    {
        #region Protected Fields

        /// <summary>
        /// The disposed value
        /// </summary>
        protected bool disposedValue = false;

        /// <summary>
        /// The encoding of the file
        /// </summary>
        protected Encoding encoding = null;

        #endregion Protected Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        protected AFile(string path)
        {
            SetupFile(path);
            var enc = Encoding;
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Gets the creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        public virtual DateTime CreationTime { get; }

        /// <summary>
        /// Gets the UTC creation time.
        /// </summary>
        /// <value>
        /// The creation time UTC.
        /// </value>
        public virtual DateTime CreationTimeUtc { get; }

        /// <summary>
        /// Gets or sets the encoding of the file.
        /// </summary>
        /// <value>
        /// The encoding.
        /// </value>
        public virtual Encoding Encoding { get; set; }

        /// <summary>
        /// Gets a value indicating whether the open stream is at the end of the stream.
        /// </summary>
        /// <value>
        ///   <c>true</c> if EOS; otherwise, <c>false</c>.
        /// </value>
        public bool EOS
        {
            get
            {
                return IsOpen
                    ? Stream.Length - 1 == Stream.Position
                    : false;
            }
        }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public virtual string Extension { get; protected set; }
        
        /// <summary>
        /// Gets or sets the file access.
        /// </summary>
        /// <value>
        /// The file access.
        /// </value>
        public virtual FileAccess FileAccessMode { get; protected set; }

        /// <summary>
        /// Gets or sets the file attributes.
        /// </summary>
        /// <value>
        /// The file attributes.
        /// </value>
        public virtual FileAttributes FileAttributes { get; set; }
        
        /// <summary>
        /// Gets or sets the file mode.
        /// </summary>
        /// <value>
        /// The file mode.
        /// </value>
        public virtual FileMode FileMode { get; protected set; }

        /// <summary>
        /// Gets the full path of the file.
        /// </summary>
        /// <value>
        /// The full path.
        /// </value>
        public string FullPath
        {
            get
            {
                return System.IO.Path.Combine(Path,
                              String.Format("{0}{1}", Name, Extension));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the file is open.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this file is open; otherwise, <c>false</c>.
        /// </value>
        public bool IsOpen
        {
            get { return Stream != null; }
        }

        /// <summary>
        /// Gets the last accessed time.
        /// </summary>
        /// <value>
        /// The last accessed time.
        /// </value>
        public virtual DateTime LastAccessedTime { get; }

        /// <summary>
        /// Gets the UTC last accessed time.
        /// </summary>
        /// <value>
        /// The last accessed time UTC.
        /// </value>
        public virtual DateTime LastAccessedTimeUtc { get; }

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        /// <value>
        /// The last modified time.
        /// </value>
        public virtual DateTime LastModifiedTime { get; }

        /// <summary>
        /// Gets the UTC last modified time.
        /// </summary>
        /// <value>
        /// The last modified time UTC.
        /// </value>
        public virtual DateTime LastModifiedTimeUtc { get; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; protected set; }
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public virtual string Path { get; protected set; }

        /// <summary>
        /// Gets the size of the file, in bytes.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public virtual long Size { get; protected set; }

        /// <summary>
        /// Gets or sets the stream.
        /// </summary>
        /// <value>
        /// The stream.
        /// </value>
        public virtual Stream Stream { get; protected set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Changes the file's extension.
        /// </summary>
        /// <param name="extension">The new extension.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>Whether the extension was changed (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool ChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Changes the file's extension asynchronously.
        /// </summary>
        /// <param name="extension">The new extension.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to change the extension. Boolean represents whether the extension was changed (true) or not (false).</returns>
        public async Task<bool> ChangeExtensionAsync(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ChangeExtension(extension, collisionOption);
        }

        /// <summary>
        /// Clears the file of all text. Will return false if the file is open.
        /// </summary>
        /// <returns>Whether the file was cleared (true) or not (false).</returns>
        public bool Clear()
        {
            return IsOpen
                ? false
                : WriteAll("", WriteMode.Truncate);
        }

        /// <summary>
        /// Clears the file of all text asynchronously. Will return false if the file is open.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to change the extension. Boolean represents whether the file was cleared (true) or not (false).</returns>
        public async Task<bool> ClearAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Clear();
        }

        /// <summary>
        /// Closes the file if the file is opened.
        /// </summary>
        /// <returns>Whether the file was closed (true) or not (false).</returns>
        public bool Close()
        {
            if (IsOpen)
            {
                // if the file was opened for read, make sure we flush the stream before we close it.
                switch (FileAccessMode)
                {
                    case FileAccess.ReadWrite:
                    case FileAccess.Write:
                        Stream.Flush();
                        break;
                }

                Stream.Dispose();
                FileAccessMode = FileAccess.None;
                FileMode = FileMode.None;
                Stream = null;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Closes the file if the file is opened asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to change the extension. Boolean represents whether the file was closed (true) or not (false).</returns>
        public async Task<bool> CloseAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Close();
        }

        /// <summary>
        /// Copies the file.
        /// </summary>
        /// <param name="destination">The full path of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the copied file.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual AFile Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies the file.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the copied file.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual AFile Copy(AFolder destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies the file.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the copied file.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual AFile Copy(string destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies the file asynchronously.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to copy the file. The AFile represents the copied file.</returns>
        public async Task<AFile> CopyAsync(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(destination, collisionOption);
        }

        /// <summary>
        /// Copies the file asynchronously.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to copy the file. The AFile represents the copied file.</returns>
        public async Task<AFile> CopyAsync(AFolder destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(destinationPath, destinationName, collisionOption);
        }

        /// <summary>
        /// Copies the file asynchronously.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to copy the file. The AFile represents the copied file.</returns>
        public async Task<AFile> CopyAsync(string destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(destinationPath, destinationName, collisionOption);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <returns>Whether the file was deleted (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool Delete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the file asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to change the extension. Boolean represents whether the file was deleted (true) or not (false).</returns>
        public async Task<bool> DeleteAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Delete();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return obj != null
                ? FullPath == obj.ToString()
                : false;
        }

        /// <summary>
        /// Determines whether the specified file is equal to this file.
        /// </summary>
        /// <param name="file">The file to check.</param>
        /// <returns>Whether the files are equal (true) or not (false).</returns>
        public bool Equals(AFile file)
        {
            return file.FullPath == FullPath;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return FullPath.GetHashCode();
        }

        /// <summary>
        /// Moves the file.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>Whether the file was moved or not.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the file.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>Whether the file was moved or not.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool Move(AFolder destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the file.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>Whether the file was moved or not.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool Move(string destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the file asynchronously.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to move the file. The boolean represents whether the file was moved or not.</returns>
        public async Task<bool> MoveAsync(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(destination, collisionOption);
        }

        /// <summary>
        /// Moves the file asynchronously.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to move the file. The boolean represents whether the file was moved or not.</returns>
        public async Task<bool> MoveAsync(AFolder destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(destinationPath, destinationName, collisionOption);
        }

        /// <summary>
        /// Moves the file asynchronously.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to move the file. The boolean represents whether the file was moved or not.</returns>
        public async Task<bool> MoveAsync(string destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(destinationPath, destinationName, collisionOption);
        }

        /// <summary>
        /// Opens the file to the Stream.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>A reference to the Stream.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual Stream Open(FileAccess fileAccess, FileMode fileMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens the file to the Stream asynchronously.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A Stream task to open the file. The Stream represents the stream.</returns>
        public async Task<Stream> OpenAsync(FileAccess fileAccess, FileMode fileMode, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Open(fileAccess, FileMode);
        }

        /// <summary>
        /// Reads all the contents of the file.
        /// </summary>
        /// <returns>A string representing the contents of the file.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual string ReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads all the contents of the file asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A task to reaad the contents of the file. The string represents the contents of the file.</returns>
        public async Task<string> ReadAllAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadAll();
        }

        /// <summary>
        /// Reads all as the contents of the file as an XDocument.
        /// </summary>
        /// <returns>An XDocument representing the contents of the file.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual XDocument ReadAllAsXElement()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously reads all as the contents of the file as an XDocument.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An XDocument task to read the contents of the file. The XDocument represents the contents of the file..</returns>
        public async Task<XDocument> ReadAllAsXElementAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadAllAsXElement();
        }

        /// <summary>
        /// Reads all lines in the file.
        /// </summary>
        /// <returns>An array of strings, each item representing a line in the file.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual string[] ReadAllLines()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads all lines in the file asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A string arary task to read all the lines in the file. The array of strings represents each line in the file.</returns>
        public async Task<string[]> ReadAllLinesAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadAllLines();
        }

        /// <summary>
        /// Reads a line.
        /// </summary>
        /// <returns>A string representing a line in the file.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual IEnumerable<string> ReadLine()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a line asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A IEnumerable string task to read a line. The IEnumerable string represents a line.</returns>
        public async Task<IEnumerable<string>> ReadLine(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadLine();
        }

        /// <summary>
        /// Renames the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>Whether the file was renamed (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Renames the file asynchronously.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to rename the file. The boolean represents whether the file was renamed (true) or not (false).</returns>
        public async Task<bool> RenameAsync(string name, FileCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Rename(name, collisionOption);
        }

        /// <summary>
        /// Reads from the stream.
        /// </summary>
        /// <param name="chars">The number of chars to read.</param>
        /// <returns>A string representing the read characters.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual string StreamRead(int chars)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads all contents from the stream.
        /// </summary>
        /// <returns>A string representing all the contents in the stream.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual string StreamReadAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads all contents from the stream asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A string task to read all the contents from the stream. The string represents all the contents in the stream.</returns>
        public async Task<string> StreamReadAllAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return StreamReadAll();
        }

        /// <summary>
        /// Reads from the stream asynchronously.
        /// </summary>
        /// <param name="chars">The chars.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A string task to read contents from the stream. The string represents the read characters.</returns>
        public async Task<string> StreamReadAsync(int chars, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return StreamRead(chars);
        }

        /// <summary>
        /// Reads a line from the stream.
        /// </summary>
        /// <returns>A string representing a line.</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual string StreamReadLine()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a line from the stream asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A string task to read contents from the stream. The string represents a line.</returns>
        public async Task<string> StreamReadLineAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return StreamReadLine();
        }

        /// <summary>
        /// Sets the position of the stream.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>Whether the position was set (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool StreamSetPosition(int position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the position of the stream asynchronously.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to set the position of the stream. The boolean represents whether the position was set (true) or not (false).</returns>
        public async Task<bool> StreamSetPositionAsync(int position, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return StreamSetPosition(position);
        }

        /// <summary>
        /// Writes contents to the stream.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns>Whether the stream was written to (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool StreamWrite(string contents)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes contents to the stream asynchronously.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to write to the stream. The boolean represents whether the stream was written to (true) or not (false).</returns>
        public async Task<bool> StreamWriteAsync(string contents, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return StreamWrite(contents);
        }

        /// <summary>
        /// Writes a line to the stream.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>Whether the line was written (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool StreamWriteLine(string line)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a line to the stream asynchronously.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to write a line of contents to the stream. The boolean represents whether the line was written (true) or not (false).</returns>
        public async Task<bool> StreamWriteLineAsync(string line, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return StreamWriteLine(line);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return FullPath;
        }

        /// <summary>
        /// Writes contents to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode. Defaults to Truncate.</param>
        /// <returns>Whether the contents were written (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes contents to the file asynchronously.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode. Defaults to Truncate.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to write to the file. The boolean represents whether the contents were written (true) or not (false).</returns>
        public async Task<bool> WriteAllAsync(string contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAll(contents, writeMode);
        }

        /// <summary>
        /// Writes a line to the file.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="writeMode">The write mode. Defaults to Append.</param>
        /// <returns>Whether the line was written (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">Exception representing that this function is not implemented.</exception>
        public virtual bool WriteLine(string line, WriteMode writeMode = WriteMode.Append)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a line to the file asynchronously.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="writeMode">The write mode. Defaults to Append.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to write the line. The boolean represents whether the line was written (true) or not (false).</returns>
        public async Task<bool> WriteLineAsync(string line, WriteMode writeMode = WriteMode.Append, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteLine(line, writeMode);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && IsOpen)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                Stream.Dispose();

                disposedValue = true;
            }
        }

        /// <summary>
        /// Sets up the file.
        /// </summary>
        /// <param name="path">The path.</param>
        protected void SetupFile(string path)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            Path = System.IO.Path.GetDirectoryName(path);
            Extension = System.IO.Path.GetExtension(path);
            Stream = null;
        }

        #endregion Protected Methods
    }
}