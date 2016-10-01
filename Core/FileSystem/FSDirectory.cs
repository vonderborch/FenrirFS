// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FSFolder.cs
// Author           : vonderborch
// Created          : 07-12-2016
//
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="FSFolder.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      An abstract class representing a folder.
// </summary>
//
// Changelog:
//            - 2.0.0 (09-24-2016) - Beta version.
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
    /// An abstract representation of a directory.
    /// </summary>
    /// <seealso cref="FenrirFS.FSFileSystemEntry" />
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="System.IEquatable{FenrirFS.FSDirectory}" />
    public abstract class FSDirectory : FSFileSystemEntry, IDisposable, IEquatable<FSDirectory>
    {
        #region Private Fields

        /// <summary>
        /// The disposed value
        /// </summary>
        private bool disposedValue = false;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FSDirectory"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FSDirectory(string path)
        {
            Name = IO.Path.GetFileNameWithoutExtension(path);
            Path = IO.Path.GetDirectoryName(path);

            FileSystemEntryType = FileSystemEntryType.Directory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSDirectory"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        public FSDirectory(string path, string name)
        {
            Name = name;
            Path = path;

            FileSystemEntryType = FileSystemEntryType.Directory;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the full path of the directory.
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
        /// Performs an implicit conversion from <see cref="FSDirectory"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The result of the conversion.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static implicit operator string(FSDirectory folder)
        {
            return folder.FullPath;
        }

        /// <summary>
        /// Asynchronously the copy.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<FSDirectory> AsyncCopy(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Copy(destination);
        }

        /// <summary>
        /// Asynchronously the create file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<FSFile> AsyncCreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateFile(file);
        }

        /// <summary>
        /// Asynchronously the create folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<FSDirectory> AsyncCreateFolder(string folder, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateFolder(folder);
        }

        /// <summary>
        /// Asynchronously the delete.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<bool> AsyncDelete(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Delete();
        }

        /// <summary>
        /// Asynchronously the delete file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<bool> AsyncDeleteFile(string file, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteFile(file);
        }

        /// <summary>
        /// Asynchronously the delete folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<bool> AsyncDeleteFolder(string folder, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteFolder(folder);
        }

        /// <summary>
        /// Asynchronously the file exists.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<bool> AsyncFileExists(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return FileExists(file, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronously the folder exists.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<bool> AsyncFolderExists(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return FolderExists(folder, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronously the get file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;FSFile&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<FSFile> AsyncGetFile(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFile(file, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronously the get file names.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<string>> AsyncGetFileNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileNames(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously the get files.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;List&lt;FSFile&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<FSFile>> AsyncGetFiles(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFiles(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously the get file system entries.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;List&lt;FSFileSystemEntry&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<FSFileSystemEntry>> AsyncGetFileSystemEntries(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntries(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously the get file system entry.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="returnFileOverFolder">if set to <c>true</c> [return file over folder].</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;FSFileSystemEntry&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<FSFileSystemEntry> AsyncGetFileSystemEntry(string name, bool returnFileOverFolder = true, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntry(name, returnFileOverFolder, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronously the get file system entry names.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<string>> AsyncGetFileSystemEntryNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntryNames(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously the get folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;FSFolder&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<FSDirectory> AsyncGetFolder(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolder(folder, searchOption, ignoreCase);
        }

        /// <summary>
        /// Asynchronously the get folder names.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;List&lt;System.String&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<string>> AsyncGetFolderNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolderNames(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously the get folders.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;List&lt;FSFolder&gt;&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<FSDirectory>> AsyncGetFolders(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolders(searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously the item exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fileSearchOption">The file search option.</param>
        /// <param name="folderSearchOption">The folder search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;ExistenceCheckResult&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<ExistenceCheckResult> AsyncItemExists(string name, SearchOption fileSearchOption = SearchOption.TopDirectoryOnly, SearchOption folderSearchOption = SearchOption.TopDirectoryOnly, bool fileIgnoreCase = true, bool folderIgnoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ItemExists(name, fileSearchOption, folderSearchOption);
        }

        /// <summary>
        /// Asynchronously the move.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<bool> AsyncMove(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Move(destination);
        }

        /// <summary>
        /// Asynchronously the rename.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<bool> AsyncRename(string name, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public abstract FSDirectory Copy(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists);

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public abstract FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public abstract FSDirectory CreateFolder(string folder, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists);

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public abstract bool Delete();

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public abstract bool DeleteFile(string file);

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="folder">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public abstract bool DeleteFolder(string folder);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public bool Equals(FSDirectory other)
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public FSFileSystemEntry GetFileSystemEntry(string name, bool returnFileOverFolder = true, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            FSFile file = GetFile(name, searchOption, ignoreCase);
            FSDirectory folder = GetFolder(name, searchOption, ignoreCase);

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
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public FSDirectory GetFolder(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public List<FSDirectory> GetFolders(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            var entries = InternalGetFileSystemEntries(false, true, searchPattern, searchOption);
            var results = new List<FSDirectory>();

            for (int i = 0; i < entries.Count; i++)
                results.Add((FSDirectory)entries[i]);

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
        ///             - 2.0.0 (09-24-2016) - Beta version.
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
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public abstract bool Move(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists);

        /// <summary>
        /// Renames the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public abstract bool Rename(string name, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists);

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
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

        /// <summary>
        /// Internal method to get file system entries.
        /// </summary>
        /// <param name="grabFiles">if set to <c>true</c> [grabs files].</param>
        /// <param name="grabDirectories">if set to <c>true</c> [grabs directories].</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of file system entries matching the desired parameters.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        protected abstract List<FSFileSystemEntry> InternalGetFileSystemEntries(bool grabFiles, bool grabDirectories, string searchPattern = "*", SearchOption searchOption = SearchOption.All);

        #endregion Protected Methods
    }
}