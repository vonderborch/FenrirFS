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
        /// <param name="directory">The directory.</param>
        /// <returns>The result of the conversion.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static implicit operator string(FSDirectory directory)
        {
            return directory.FullPath;
        }

        /// <summary>
        /// Copies the directory to the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<FSDirectory> AsyncCopy(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Copy(destination);
        }

        /// <summary>
        /// Creates a new file in the directory.
        /// </summary>
        /// <param name="file">The name for the file.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A file structure representing the new file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<FSFile> AsyncCreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateFile(file);
        }

        /// <summary>
        /// Creates a new directory in the directory.
        /// </summary>
        /// <param name="directory">The name for the directory.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A directory structure representing the new directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<FSDirectory> AsyncCreateDirectory(string directory, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateDirectory(directory);
        }

        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the directory was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncDelete(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Delete();
        }

        /// <summary>
        /// Deletes a file in the directory.
        /// </summary>
        /// <param name="file">The name of the file to delete.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the file was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncDeleteFile(string file, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteFile(file);
        }

        /// <summary>
        /// Deletes a directory in the directory.
        /// </summary>
        /// <param name="directory">The name of the directory to delete.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the directory was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncDeleteDirectory(string directory, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteDirectory(directory);
        }

        /// <summary>
        /// Determines if the file exists in the directory.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the file exists, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<bool> AsyncFileExists(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return FileExists(file, searchOption, ignoreCase);
        }

        /// <summary>
        /// Determines if the directory exists in the directory.
        /// </summary>
        /// <param name="directory">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the directory exists, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<bool> AsyncDirectoryExists(string directory, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DirectoryExists(directory, searchOption, ignoreCase);
        }

        /// <summary>
        /// Returns the file with the name, if it exists in the directory and with the specified search option.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A file structure representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<FSFile> AsyncGetFile(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFile(file, searchOption, ignoreCase);
        }

        /// <summary>
        /// Returns a list of all files with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of file names of matching files.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<string>> AsyncGetFileNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileNames(searchPattern, searchOption);
        }

        /// <summary>
        /// Returns a list of all files with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of files of matching files.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<FSFile>> AsyncGetFiles(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFiles(searchPattern, searchOption);
        }

        /// <summary>
        /// Returns a list of all file system entries with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of file system entries of matching file system entries.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<FSFileSystemEntry>> AsyncGetFileSystemEntries(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntries(searchPattern, searchOption);
        }

        /// <summary>
        /// Returns a file system entry matching the search parameters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="returnFileOverFolder">Whether to prefer a file (true) or a folder (false).</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A file system entry matching the parameters.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<FSFileSystemEntry> AsyncGetFileSystemEntry(string name, bool returnFileOverFolder = true, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntry(name, returnFileOverFolder, searchOption, ignoreCase);
        }

        /// <summary>
        /// Returns a list of all file system entry names with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of file system entry names of matching file system entries.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<string>> AsyncGetFileSystemEntryNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntryNames(searchPattern, searchOption);
        }

        /// <summary>
        /// Returns the directory with the name, if it exists in the directory and with the specified search option.
        /// </summary>
        /// <param name="directory">The name.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A directory structure representing the directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<FSDirectory> AsyncGetDirectory(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetDirectory(folder, searchOption, ignoreCase);
        }

        /// <summary>
        /// Returns a list of all directory names with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of directory names of matching directories.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<string>> AsyncGetDirectoryNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetDirectoryNames(searchPattern, searchOption);
        }

        /// <summary>
        /// Returns a list of all directories with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of directories of matching directories.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<List<FSDirectory>> AsyncGetDirectories(string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetDirectories(searchPattern, searchOption);
        }

        /// <summary>
        /// Determines whether a file system entry exists that matches the search parameters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fileSearchOption">The file search option.</param>
        /// <param name="folderSearchOption">The folder search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The existence check result for the search.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public async Task<ExistenceCheckResult> AsyncItemExists(string name, SearchOption fileSearchOption = SearchOption.TopDirectoryOnly, SearchOption folderSearchOption = SearchOption.TopDirectoryOnly, bool fileIgnoreCase = true, bool folderIgnoreCase = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ItemExists(name, fileSearchOption, folderSearchOption);
        }

        /// <summary>
        /// Moves the directory to the specified specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncMove(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Move(destination);
        }

        /// <summary>
        /// Renames the directory to the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option if a file with the new name already exists.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the rename was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<bool> AsyncRename(string name, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Rename(Name);
        }

        /// <summary>
        /// Copies the directory to the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract FSDirectory Copy(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists);

        /// <summary>
        /// Creates a new file in the directory.
        /// </summary>
        /// <param name="file">The name for the file.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns>A file structure representing the new file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        /// <summary>
        /// Creates a new directory in the directory.
        /// </summary>
        /// <param name="directory">The name for the directory.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns>A directory structure representing the new directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract FSDirectory CreateDirectory(string directory, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists);

        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <returns><c>true</c> if the directory was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool Delete();

        /// <summary>
        /// Deletes a file in the directory.
        /// </summary>
        /// <param name="file">The name of the file to delete.</param>
        /// <returns><c>true</c> if the file was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool DeleteFile(string file);

        /// <summary>
        /// Deletes a directory in the directory.
        /// </summary>
        /// <param name="directory">The name of the directory to delete.</param>
        /// <returns><c>true</c> if the directory was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool DeleteDirectory(string directory);

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
        /// Whether the directories point to the same source.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if the directories match, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public bool Equals(FSDirectory other)
        {
            return FullPath == other.FullPath;
        }

        /// <summary>
        /// Determines if the file exists in the directory.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns><c>true</c> if the file exists, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public bool FileExists(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            return GetFile(file, searchOption, ignoreCase) != null;
        }

        /// <summary>
        /// Determines if the directory exists in the directory.
        /// </summary>
        /// <param name="directory">The name.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns><c>true</c> if the directory exists, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public bool DirectoryExists(string directory, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            return GetDirectory(directory, searchOption, ignoreCase) != null;
        }

        /// <summary>
        /// Returns the file with the name, if it exists in the directory and with the specified search option.
        /// </summary>
        /// <param name="file">The name.</param>
        /// <returns>A file structure representing the file.</returns>
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
                    var directories = GetDirectories();

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
        /// Returns a list of all files with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of file names of matching files.</returns>
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
        /// Returns a list of all files with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of files of matching files.</returns>
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
        /// Returns a list of all file system entries with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of file system entries of matching file system entries.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public List<FSFileSystemEntry> GetFileSystemEntries(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            return InternalGetFileSystemEntries(true, true, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns a file system entry matching the search parameters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="returnFileOverFolder">Whether to prefer a file (true) or a folder (false).</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A file system entry matching the parameters.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public FSFileSystemEntry GetFileSystemEntry(string name, bool returnFileOverFolder = true, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            FSFile file = GetFile(name, searchOption, ignoreCase);
            FSDirectory folder = GetDirectory(name, searchOption, ignoreCase);

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
        /// Returns a list of all file system entry names with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of file system entry names of matching file system entries.</returns>
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
        /// Returns the directory with the name, if it exists in the directory and with the specified search option.
        /// </summary>
        /// <param name="directory">The name.</param>
        /// <returns>A directory structure representing the directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public FSDirectory GetDirectory(string directory, SearchOption searchOption = SearchOption.TopDirectoryOnly, bool ignoreCase = true)
        {
            if (Exists)
            {
                if (ignoreCase)
                    directory = directory.ToLowerInvariant();

                var directories = GetDirectories();

                for (int i = 0; i < directories.Count; i++)
                {
                    if (searchOption != SearchOption.SubDirectoriesOnly)
                    {
                        var name = ignoreCase
                            ? directories[i].FullPath.ToLowerInvariant()
                            : directories[i];
                        if (directory == name)
                            return directories[i];
                    }

                    var output = directories[i].GetDirectory(directory, SearchOption.All, ignoreCase);
                    if (output != null)
                        return output;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a list of all directory names with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of directory names of matching directories.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public List<string> GetDirectoryNames(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            var entries = InternalGetFileSystemEntries(false, true, searchPattern, searchOption);
            var results = new List<string>();

            for (int i = 0; i < entries.Count; i++)
                results.Add(entries[i]);

            return results;
        }

        /// <summary>
        /// Returns a list of all directories with a name matching the search parameters.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of directories of matching directories.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public List<FSDirectory> GetDirectories(string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            var entries = InternalGetFileSystemEntries(false, true, searchPattern, searchOption);
            var results = new List<FSDirectory>();

            for (int i = 0; i < entries.Count; i++)
                results.Add((FSDirectory)entries[i]);

            return results;
        }

        /// <summary>
        /// Determines whether a file system entry exists that matches the search parameters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fileSearchOption">The file search option.</param>
        /// <param name="folderSearchOption">The folder search option.</param>
        /// <returns>The existence check result for the search.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public ExistenceCheckResult ItemExists(string name, SearchOption fileSearchOption = SearchOption.TopDirectoryOnly, SearchOption folderSearchOption = SearchOption.TopDirectoryOnly, bool fileIgnoreCase = true, bool folderIgnoreCase = true)
        {
            bool fileExists = FileExists(name, fileSearchOption, fileIgnoreCase);
            bool folderExists = DirectoryExists(name, folderSearchOption, folderIgnoreCase);

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
        /// Moves the directory to the specified specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract bool Move(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists);

        /// <summary>
        /// Renames the directory to the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option if a file with the new name already exists.</param>
        /// <returns><c>true</c> if the rename was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
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