/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    /// <summary>
    /// Represents an abstract folder in FenrirFS.
    /// </summary>
    public abstract class AFolder : IDisposable, IEquatable<AFolder>
    {
        #region Protected Fields

        /// <summary>
        /// The disposed value
        /// </summary>
        protected bool disposedValue = false;

        #endregion Protected Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AFolder"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        protected AFolder(string path)
        {
            SetupAFolder(path);
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
        /// Gets the full path.
        /// </summary>
        /// <value>The full path.</value>
        public string FullPath
        {
            get { return System.IO.Path.Combine(Path, Name); }
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Gets the parent folder for the folder.
        /// </summary>
        /// <value>
        /// The parent folder.
        /// </value>
        public virtual string ParentFolder { get; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public virtual string Path { get; protected set; }

        /// <summary>
        /// Gets the root folder for the folder.
        /// </summary>
        /// <value>
        /// The root folder.
        /// </value>
        public virtual string RootFolder { get; }

        /// <summary>
        /// Gets the size of the folder, in bytes.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public virtual long Size { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Copies the folder to the destination.
        /// </summary>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the new folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder Copy(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies the folder to the destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the new folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder Copy(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies the folder to the destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the new folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder Copy(AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously copies the folder to the destination.
        /// </summary>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to copy the folder. The AFolder represents the new folder.</returns>
        public async Task<AFolder> CopyAsync(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(destinationName, folderCollisionOption, fileCollisionOption);
        }

        /// <summary>
        /// Asynchronously copies the folder to the destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to copy the folder. The AFolder represents the new folder.</returns>
        public async Task<AFolder> CopyAsync(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        /// <summary>
        /// Asynchronously copies the folder to the destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to copy the folder. The AFolder represents the new folder.</returns>
        public async Task<AFolder> CopyAsync(AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>An AFile representing the file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile CopyFile(string file, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>An AFile representing the file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile CopyFile(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>An AFile representing the file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile CopyFile(string file, AFolder destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies a file asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to copy a file. The AFile represents the file.</returns>
        public async Task<AFile> CopyFileAsync(string file, string destinationName, FileCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CopyFile(file, destinationName, collisionOption);
        }

        /// <summary>
        /// Copies a file asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to copy a file. The AFile represents the file.</returns>
        public async Task<AFile> CopyFileAsync(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CopyFile(file, destinationPath, destinationName, collisionOption);
        }

        /// <summary>
        /// Copies a file asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to copy a file. The AFile represents the file.</returns>
        public async Task<AFile> CopyFileAsync(string file, AFolder destinationPath, string destinationName, FileCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CopyFile(file, destinationPath, destinationName, collisionOption);
        }

        /// <summary>
        /// Copies a folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder CopyFolder(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies a folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder CopyFolder(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies a folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder CopyFolder(string folder, AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies a folder asynchronously.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to copy a folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> CopyFolderAsync(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CopyFolder(folder, destinationName, folderCollisionOption, fileCollisionOption);
        }

        /// <summary>
        /// Copies a folder asynchronously.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to copy a folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> CopyFolderAsync(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CopyFolder(folder, destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        /// <summary>
        /// Copies a folder asynchronously.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to copy a folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> CopyFolderAsync(string folder, AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CopyFolder(folder, destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        /// <summary>
        /// Creates a file in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>The new file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile CreateFile(string name, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a file in this directory asynchronously.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to create a file. The AFile represents the new file.</returns>
        public async Task<AFile> CreateFileAsync(string name, FileCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFile(name, collisionOption);
        }

        /// <summary>
        /// Creates a folder in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>The new folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder CreateFolder(string name, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a folder in this directory asynchronously.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to create a folder. The AFolder represents the new folder.</returns>
        public async Task<AFolder> CreateFolderAsync(string name, FolderCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFolder(name, collisionOption);
        }

        /// <summary>
        /// Deletes this folder.
        /// </summary>
        /// <returns>Whether this folder was deleted or not.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool Delete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes this folder asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to delete this folder. The boolean represents whether this folder was
        /// deleted or not.
        /// </returns>
        public async Task<bool> DeleteAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Delete();
        }

        /// <summary>
        /// Deletes a file in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Whether the file was deleted or not.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool DeleteFile(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a file in this directory asynchronously.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to delete a file. The boolean represents whether the file was deleted or not.
        /// </returns>
        public async Task<bool> DeleteFileAsync(string name, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return DeleteFile(name);
        }

        /// <summary>
        /// Deletes a folder in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Whether the folder was deleted or not.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool DeleteFolder(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a folder in this directory asynchronously.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to delete a folder. The boolean represents whether the folders was
        /// deleted or not.
        /// </returns>
        public async Task<bool> DeleteFolderAsync(string name, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return DeleteFolder(name);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj != null
                ? FullPath == obj.ToString()
                : false;
        }

        /// <summary>
        /// Checks whether the specified folder is equal to this folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>Whether the folders are equal (true) or not (false).</returns>
        public bool Equals(AFolder folder)
        {
            return folder.FullPath == FullPath;
        }

        /// <summary>
        /// Checks whether the item at the specified path exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The results of the existence check.</returns>
        public ExistenceCheckResult Exists(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            bool fileExists = FileExists(name);
            bool folderExists = FolderExists(name);

            if (fileExists && folderExists)
                return ExistenceCheckResult.FileAndFolderExists;
            else if (fileExists)
                return ExistenceCheckResult.FileExists;
            else if (folderExists)
                return ExistenceCheckResult.FolderExists;

            return ExistenceCheckResult.NotFound;
        }

        /// <summary>
        /// Checks if a file exists in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Whether the file exists (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool FileExists(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously checks if a file exists in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to check if the file exists. The boolean represents whether the file
        /// exists (true) or not (false).
        /// </returns>
        public async Task<bool> FileExistsAsync(string name, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return FileExists(name);
        }

        /// <summary>
        /// Checks if a folder exists in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Whether the folder exists (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool FolderExists(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously checks if a folder exists in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to check if the folder exists. The boolean represents whether the folder
        /// exists (true) or not (false).
        /// </returns>
        public async Task<bool> FolderExistsAsync(string name, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return FolderExists(name);
        }

        /// <summary>
        /// Gets the names of all files in this folder.
        /// </summary>
        /// <returns>A list of all file names in this folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual List<string> GetFileNames()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets the names of all files in this folder.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A string list task to get all the file names. The string list represents a list of all
        /// files in this folder.
        /// </returns>
        public async Task<List<string>> GetFileNamesAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFileNames();
        }

        /// <summary>
        /// Gets the files in this folder.
        /// </summary>
        /// <returns>A list of all files in this folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual List<AFile> GetFiles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets all the files in this folder.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A AFile list task to get all the files. The AFile list represents a list of all files in
        /// this folder.
        /// </returns>
        public async Task<List<AFile>> GetFilesAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFiles();
        }

        /// <summary>
        /// Gets the names of all folders in this folder.
        /// </summary>
        /// <returns>A list of all folders names in this folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual List<string> GetFolderNames()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets the names of all folders in this folder.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A string list task to get all the folder names. The string list represents a list of all
        /// folders in this folder.
        /// </returns>
        public async Task<List<string>> GetFolderNamesAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFolderNames();
        }

        /// <summary>
        /// Gets the folders in this folder.
        /// </summary>
        /// <returns>A AFolder list representing all folders in this folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual List<AFolder> GetFolders()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets the folders in this folder.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A AFolder list task to get all the folders. The AFolder list represents a list of all
        /// folders in this folder.
        /// </returns>
        public async Task<List<AFolder>> GetFoldersAsync(CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFolders();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return FullPath.GetHashCode();
        }

        /// <summary>
        /// Moves this folder to the specified destination.
        /// </summary>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Whether the folder was moved (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool Move(string destinationName, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves this folder to the specified destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Whether the folder was moved (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool Move(string destinationPath, string destinationName, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves this folder to the specified destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Whether the folder was moved (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool Move(AFolder destinationPath, string destinationName, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously moves this folder to the specified destination.
        /// </summary>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to move the folder. The boolean represents whether the folder was moved
        /// (true) or not (false).
        /// </returns>
        public async Task<bool> MoveAsync(string destinationName, FolderCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(destinationName, collisionOption);
        }

        /// <summary>
        /// Asynchronously moves this folder to the specified destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to move the folder. The boolean represents whether the folder was moved
        /// (true) or not (false).
        /// </returns>
        public async Task<bool> MoveAsync(string destinationPath, string destinationName, FolderCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(destinationPath, destinationName, collisionOption);
        }

        /// <summary>
        /// Asynchronously moves this folder to the specified destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to move the folder. The boolean represents whether the folder was moved
        /// (true) or not (false).
        /// </returns>
        public async Task<bool> MoveAsync(AFolder destinationPath, string destinationName, FolderCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(destinationPath, destinationName, collisionOption);
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile OpenFile(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the file asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to open the file. The AFile represents the file.</returns>
        public async Task<AFile> OpenFileAsync(string name, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenFile(name);
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder OpenFolder(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to get the folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> OpenFolderAsync(string name, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenFolder(name);
        }

        /// <summary>
        /// Renames the folder asynchronously.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to rename the folder. The boolean represents whether the folder was
        /// renamed (true) or not (false).
        /// </returns>
        public async Task<bool> RemameAsync(string name, FolderCollisionOption collisionOption, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Rename(name, collisionOption);
        }

        /// <summary>
        /// Renames the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Whether the folder was renamed (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool Rename(string name, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return FullPath;
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Sets up a folder.
        /// </summary>
        /// <param name="path">The path.</param>
        protected void SetupAFolder(string path)
        {
            Name = System.IO.Path.GetFileName(path);
            Path = System.IO.Path.GetDirectoryName(path);
        }

        #endregion Protected Methods
    }
}