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
            Name = IO.Path.GetFileNameWithoutExtension(path);
            Path = IO.Path.GetDirectoryName(path);

            FileSystemEntryType = FileSystemEntryType.Directory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSFolder"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        public FSFolder(string path, string name)
        {
            Name = name;
            Path = path;

            FileSystemEntryType = FileSystemEntryType.Directory;
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
        public async Task<FSFolder> AsyncCopy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Copy(destination);
        }

        /// <summary>
        /// Asynchronouses the create file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<FSFile> AsyncCreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateFile(file);
        }

        /// <summary>
        /// Asynchronouses the create folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<FSFolder> AsyncCreateFolder(string folder, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateFolder(folder);
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
        /// <param name="file">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncDeleteFile(string file, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteFile(file);
        }

        /// <summary>
        /// Asynchronouses the delete folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncDeleteFolder(string folder, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteFolder(folder);
        }

        /// <summary>
        /// Asynchronouses the file exists.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncFileExists(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return FileExists(file, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronouses the folder exists.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<bool> AsyncFolderExists(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return FolderExists(folder, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronouses the get file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;FSFile&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<FSFile> AsyncGetFile(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFile(file, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronouses the get file names.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<string>> AsyncGetFileNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken ? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileNames(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronouses the get files.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;FSFile&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<FSFile>> AsyncGetFiles(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFiles(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronouses the get file system entries.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;FSFileSystemEntry&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<FSFileSystemEntry>> AsyncGetFileSystemEntries(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntries(searchPattern, searchOption);
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
        public async Task<FSFileSystemEntry> AsyncGetFileSystemEntry(string name, bool returnFileOverFolder = true, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntry(name, returnFileOverFolder, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronouses the get file system entry names.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<string>> AsyncGetFileSystemEntryNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntryNames(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronouses the get folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;FSFolder&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<FSFolder> AsyncGetFolder(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolder(folder, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronouses the get folder names.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<string>> AsyncGetFolderNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolderNames(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronouses the get folders.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;List&lt;FSFolder&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<List<FSFolder>> AsyncGetFolders(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolders(searchPattern, searchOption);
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
        public async Task<ExistenceCheckResult> AsyncItemExists(string name, SearchOption fileSearchOption = SearchOption.TopDirectoryOnly, SearchOption folderSearchOption = SearchOption.TopDirectoryOnly, bool fileIgnoreCase = true, bool folderIgnoreCase = true, CancellationToken? cancellationToken = null)
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
        public abstract FSFolder Copy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract FSFolder CreateFolder(string folder, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

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
        /// <param name="file">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool DeleteFile(string file);

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract bool DeleteFolder(string folder);

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
        /// <param name="file">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public bool FileExists(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            return GetFile(file, searchOption, ignoreCase) != null;
        }

        /// <summary>
        /// Folders the exists.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public bool FolderExists(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            return GetFolder(folder, searchOption, ignoreCase) != null;
        }
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <returns>FSFile.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public FSFile GetFile(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            if (Exists)
            {
                if (ignoreCase)
                    file = file.ToLowerInvariant();

                if (searchOption != SearchOption.SubDirectoriesOnly)
                {
                    var topFiles = GetFiles();

                    for (int i = 0; i < topFiles.Count; i++)
                    {
                        var name = ignoreCase
                            ? topFiles[i].FullPath.ToLowerInvariant()
                            : topFiles[i];
                        if (file == name)
                            return topFiles[i];
                    }
                }

                if (searchOption != SearchOption.TopDirectoryOnly)
                {
                    var directories = GetFolders();

                    for (int i = 0; i < directories.Count; i++)
                    {
                        var output = directories[i].GetFile(file, SearchOption.All, ignoreCase);
                        if (output != null)
                            return output;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the file names.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public List<string> GetFileNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            var entries = InternalGetFileSystemEntries(true, false, searchPattern, searchOption);
            var results = new List<string>();

            for (int i = 0; i < entries.Count; i++)
                results.Add(entries[i]);

            return results;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <returns>List&lt;FSFile&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public List<FSFile> GetFiles(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            var entries = InternalGetFileSystemEntries(true, false, searchPattern, searchOption);
            var results = new List<FSFile>();

            for (int i = 0; i < entries.Count; i++)
                results.Add((FSFile)entries[i]);

            return results;
        }

        /// <summary>
        /// Gets the file system entries.
        /// </summary>
        /// <returns>List&lt;FSFileSystemEntry&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public List<FSFileSystemEntry> GetFileSystemEntries(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            return InternalGetFileSystemEntries(true, true, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets the file system entry.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="returnFileOverFolder">if set to <c>true</c> [return file over folder].</param>
        /// <returns>FSFileSystemEntry.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public FSFileSystemEntry GetFileSystemEntry(string name, bool returnFileOverFolder = true, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            FSFile file = GetFile(name, searchOption, ignoreCase);
            FSFolder folder = GetFolder(name, searchOption, ignoreCase);

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
        public List<string> GetFileSystemEntryNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            var entries = InternalGetFileSystemEntries(true, false, searchPattern, searchOption);
            var results = new List<string>();

            for (int i = 0; i < entries.Count; i++)
                results.Add(entries[i]);

            return results;
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <returns>FSFolder.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public FSFolder GetFolder(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            if (Exists)
            {
                if (ignoreCase)
                    folder = folder.ToLowerInvariant();

                var directories = GetFolders();

                for (int i = 0; i < directories.Count; i++)
                {
                    if (searchOption != SearchOption.SubDirectoriesOnly)
                    {
                        var name = ignoreCase
                            ? directories[i].FullPath.ToLowerInvariant()
                            : directories[i];
                        if (folder == name)
                            return directories[i];
                    }

                    var output = directories[i].GetFolder(folder, SearchOption.All, ignoreCase);
                    if (output != null)
                        return output;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the folder names.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public List<string> GetFolderNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            var entries = InternalGetFileSystemEntries(false, true, searchPattern, searchOption);
            var results = new List<string>();

            for (int i = 0; i < entries.Count; i++)
                results.Add(entries[i]);

            return results;
        }

        /// <summary>
        /// Gets the folders.
        /// </summary>
        /// <returns>List&lt;FSFolder&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public List<FSFolder> GetFolders(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            var entries = InternalGetFileSystemEntries(false, true, searchPattern, searchOption);
            var results = new List<FSFolder>();

            for (int i = 0; i < entries.Count; i++)
                results.Add((FSFolder)entries[i]);

            return results;
        }

        /// <summary>
        /// Items the exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fileSearchOption">The file search option.</param>
        /// <param name="folderSearchOption">The folder search option.</param>
        /// <returns>ExistenceCheckResult.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public ExistenceCheckResult ItemExists(string name, SearchOption fileSearchOption = SearchOption.TopDirectoryOnly, SearchOption folderSearchOption = SearchOption.TopDirectoryOnly, bool fileIgnoreCase = true, bool folderIgnoreCase = true)
        {
            bool fileExists = FileExists(name, fileSearchOption, fileIgnoreCase);
            bool folderExists = FolderExists(name, folderSearchOption, folderIgnoreCase);

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


        protected abstract List<FSFileSystemEntry> InternalGetFileSystemEntries(bool grabFiles, bool grabDirectories, string searchPattern = "*", SearchOption searchOption = SearchOption.All);
    }
}