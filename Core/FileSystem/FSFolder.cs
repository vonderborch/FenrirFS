// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FSFolder.cs
// Author           : vonderborch
// Created          : 07-12-2016
// 
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="FSFolder.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      An abstract class representing a folder.
// </summary>
//
// Changelog: 
//            - 1.0.0 (07-12-2016) - Initial version created.
// ***********************************************************************
using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IO = System.IO;

namespace FenrirFS
{
    /// <summary>
    /// Class FSFolder.
    /// </summary>
    /// <seealso cref="FenrirFS.FSFileSystemEntry" />
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="System.IEquatable{FenrirFS.FSFolder}" />
    public abstract class FSFolder : FSFileSystemEntry, IDisposable, IEquatable<FSFolder>
    {
        #region Private Fields

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FSFolder"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FSFolder(string path)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSFolder"/> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        public FSFolder(string directory, string name)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <value>The full path.</value>
        public override string FullPath
        {
            get { return IO.Path.Combine(Path, Name); }
            protected set
            {
                Path = IO.Path.GetDirectoryName(value);
                Name = IO.Path.GetFileNameWithoutExtension(value);
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="FSFolder"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The result of the conversion.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public static implicit operator string(FSFolder folder)
        {
            return folder.FullPath;
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
        public async Task<bool> AsyncCopy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Copy(destination);
        }

        /// <summary>
        /// Asynchronouses the create file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncCreateFile(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateFile(name);
        }

        /// <summary>
        /// Asynchronouses the create folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncCreateFolder(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateFolder(name);
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
        /// Asynchronouses the delete file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncDeleteFile(string name, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteFile(name);
        }

        /// <summary>
        /// Asynchronouses the delete folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncDeleteFolder(string name, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteFolder(name);
        }

        /// <summary>
        /// Asynchronouses the file exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncFileExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return FileExists(name, searchOption);
        }

        /// <summary>
        /// Asynchronouses the folder exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncFolderExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return FolderExists(name, searchOption);
        }

        /// <summary>
        /// Asynchronouses the get file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;FSFile&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<FSFile> AsyncGetFile(string name, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFile(name);
        }

        /// <summary>
        /// Asynchronouses the get file names.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<string>> AsyncGetFileNames(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileNames();
        }

        /// <summary>
        /// Asynchronouses the get files.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;FSFile&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<FSFile>> AsyncGetFiles(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFiles();
        }

        /// <summary>
        /// Asynchronouses the get file system entries.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;FSFileSystemEntry&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<FSFileSystemEntry>> AsyncGetFileSystemEntries(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntries();
        }

        /// <summary>
        /// Asynchronouses the get file system entry.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="returnFileOverFolder">if set to <c>true</c> [return file over folder].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;FSFileSystemEntry&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<FSFileSystemEntry> AsyncGetFileSystemEntry(string name, bool returnFileOverFolder = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntry(name, returnFileOverFolder);
        }

        /// <summary>
        /// Asynchronouses the get file system entry names.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<string>> AsyncGetFileSystemEntryNames(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntryNames();
        }

        /// <summary>
        /// Asynchronouses the get folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;FSFolder&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<FSFolder> AsyncGetFolder(string name, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolder(name);
        }

        /// <summary>
        /// Asynchronouses the get folder names.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<string>> AsyncGetFolderNames(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolderNames();
        }

        /// <summary>
        /// Asynchronouses the get folders.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;FSFolder&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<FSFolder>> AsyncGetFolders(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolders();
        }

        /// <summary>
        /// Asynchronouses the item exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fileSearchOption">The file search option.</param>
        /// <param name="folderSearchOption">The folder search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;ExistenceCheckResult&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<ExistenceCheckResult> AsyncItemExists(string name, SearchOption fileSearchOption = SearchOption.TopDirectoryOnly, SearchOption folderSearchOption = SearchOption.TopDirectoryOnly, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ItemExists(name, fileSearchOption, folderSearchOption);
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
        public async Task<bool> AsyncMove(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Move(destination);
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
        public async Task<bool> AsyncRename(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Rename(Name);
        }

        /// <summary>
        /// Copies the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool Copy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool CreateFile(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool CreateFolder(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool Delete();

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool DeleteFile(string name);

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool DeleteFolder(string name);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public void Dispose()
        {
            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~FSFolder() {
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
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public bool Equals(FSFolder other)
        {
            return FullPath == other.FullPath;
        }

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool FileExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Folders the exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool FolderExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>FSFile.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract FSFile GetFile(string name);

        /// <summary>
        /// Gets the file names.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract List<string> GetFileNames();

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <returns>List&lt;FSFile&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract List<FSFile> GetFiles();

        /// <summary>
        /// Gets the file system entries.
        /// </summary>
        /// <returns>List&lt;FSFileSystemEntry&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public List<FSFileSystemEntry> GetFileSystemEntries()
        {
            var files = GetFiles();
            var folders = GetFolders();

            var merged = new List<FSFileSystemEntry>(files);
            for (int i = 0; i < folders.Count; i++)
                merged.Add(folders[i]);

            return merged;
        }

        /// <summary>
        /// Gets the file system entry.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="returnFileOverFolder">if set to <c>true</c> [return file over folder].</param>
        /// <returns>FSFileSystemEntry.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public FSFileSystemEntry GetFileSystemEntry(string name, bool returnFileOverFolder = true)
        {
            FSFile file = GetFile(name);
            FSFolder folder = GetFolder(name);

            if (file != null && folder != null)
            {
                if (returnFileOverFolder)
                    return file;
                else
                    return folder;
            }
            else if (file != null)
                return file;
            else if (folder != null)
                return folder;
            else
                return null;
        }

        /// <summary>
        /// Gets the file system entry names.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public List<string> GetFileSystemEntryNames()
        {
            var files = GetFileNames();
            var folders = GetFolderNames();

            for (int i = 0; i < folders.Count; i++)
                files.Add(folders[i]);

            return files;
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>FSFolder.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract FSFolder GetFolder(string name);

        /// <summary>
        /// Gets the folder names.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract List<string> GetFolderNames();

        /// <summary>
        /// Gets the folders.
        /// </summary>
        /// <returns>List&lt;FSFolder&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract List<FSFolder> GetFolders();

        /// <summary>
        /// Items the exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fileSearchOption">The file search option.</param>
        /// <param name="folderSearchOption">The folder search option.</param>
        /// <returns>ExistenceCheckResult.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public ExistenceCheckResult ItemExists(string name, SearchOption fileSearchOption = SearchOption.TopDirectoryOnly, SearchOption folderSearchOption = SearchOption.TopDirectoryOnly)
        {
            bool fileExists = FileExists(name, fileSearchOption);
            bool folderExists = FolderExists(name, folderSearchOption);

            if (fileExists && folderExists)
                return ExistenceCheckResult.FileAndFolderExists;
            else if (fileExists)
                return ExistenceCheckResult.FileExists;
            else if (folderExists)
                return ExistenceCheckResult.FolderExists;
            else
                return ExistenceCheckResult.None;
        }

        /// <summary>
        /// Moves the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool Move(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        /// <summary>
        /// Renames the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool Rename(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

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
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}