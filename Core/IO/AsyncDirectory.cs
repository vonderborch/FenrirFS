// ***********************************************************************
// Assembly         : FenrirFS
// Component        : AsyncDirectory.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="AsyncDirectory.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the asynchronous Directory static class.
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

namespace FenrirFS.Static
{
    /// <summary>
    /// Exposes asynchronous static methods for creating, moving, and enumerating through directories and sub-directories.
    /// </summary>
    public static class AsyncDirectory
    {
        #region Public Methods

        /// <summary>
        /// Copies the directory at the source path to the destination path.
        /// </summary>
        /// <param name="source">The source directory full path.</param>
        /// <param name="destination">The destination directory full path.</param>
        /// <param name="collisionOption">The collision option if the destination directory already exists.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the copy succeeds; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<bool> Copy(string source, string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.ThrowIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Copy(source, destination, collisionOption);
        }

        /// <summary>
        /// Creates a directory at the specified path.
        /// </summary>
        /// <param name="path">The path to create the directory at.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the task succeeds; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<bool> Create(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Create(path);
        }

        /// <summary>
        /// Creates a directory at the specified path.
        /// </summary>
        /// <param name="path">The path to create the directory at.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The directory that was created</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<FSDirectory> CreateDirectory(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Deletes the directory at the specified path.
        /// </summary>
        /// <param name="path">The path of the directory to delete.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the delete succeeds; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<bool> Delete(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Delete(path);
        }

        /// <summary>
        /// Enumerates the sub-directories at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate sub-directories from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The enumeration of directories.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<IEnumerable<FSDirectory>> EnumerateDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateDirectories(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Enumerates the names sub-directories at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate sub-directories from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The enumeration of directory names.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<IEnumerable<string>> EnumerateDirectoryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateDirectoryNames(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Enumerates the file names at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate files from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The enumeration of file names.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<IEnumerable<string>> EnumerateFileNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateFileNames(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Enumerates the files at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate files from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The enumeration of files.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<IEnumerable<FSFile>> EnumerateFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Enumerates the file system entries at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate file system entries from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The enumeration of file system entries.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<IEnumerable<FSFileSystemEntry>> EnumerateFileSystemEntries(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateFileSystemEntries(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Enumerates the file system entry names at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate file system entries from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The enumeration of file system entry names.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<IEnumerable<string>> EnumerateFileSystemEntryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateFileSystemEntryNames(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Determines whether a directory exists at the specified path.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the directory exists; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<bool> Exists(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Exists(path);
        }

        /// <summary>
        /// Gets the creation time of a directory.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <param name="useUTC">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The creation time of the directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<DateTime> GetCreationTime(string path, bool useUTC = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetCreationTime(path, useUTC);
        }

        /// <summary>
        /// Gets the current working directory of the application.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The current working directory of the application.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<FSDirectory> GetCurrentDirectory(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Gets the path of the current working directory of the application.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The path of the current working directory of the application.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<string> GetCurrentDirectoryFullPath(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetCurrentDirectoryFullPath();
        }

        /// <summary>
        /// Gets a list of sub-directories at the path.
        /// </summary>
        /// <param name="path">The path to get sub-directories from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of sub-directories at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<List<FSDirectory>> GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetDirectories(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets a list of sub-directory names at the path.
        /// </summary>
        /// <param name="path">The path to get sub-directories from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of sub-directory names at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<List<string>> GetDirectoryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetDirectoryNames(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets a list of file names at the path.
        /// </summary>
        /// <param name="path">The path to get files from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of file names at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<List<string>> GetFileNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetFileNames(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets a list of files at the path.
        /// </summary>
        /// <param name="path">The path to get files from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of files at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<List<FSFile>> GetFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets a list of file system entries at the path.
        /// </summary>
        /// <param name="path">The path to get file system entries from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of file system entries at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<List<FSFileSystemEntry>> GetFileSystemEntries(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetFileSystemEntries(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets a list of file system entry names at the path.
        /// </summary>
        /// <param name="path">The path to get file system entries from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A list of file system entry names at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<List<string>> GetFileSystemEntryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetFileSystemEntryNames(path, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets the last accessed time of a directory.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <param name="useUTC">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The last accessed time of the directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<DateTime> GetLastAccessedTime(string path, bool useUTC = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetLastAccessedTime(path, useUTC);
        }

        /// <summary>
        /// Gets the last modified time of a directory.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <param name="useUTC">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The last modified time of the directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<DateTime> GetLastModifiedTime(string path, bool useUTC = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetLastModifiedTime(path, useUTC);
        }

        /// <summary>
        /// Gets the parent directory of the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The parent directory of the specified path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<FSDirectory> GetParentDirectory(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetParentDirectory(path);
        }

        /// <summary>
        /// Gets the parent directory full path of the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The parent directory full path of the specified path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<string> GetParentDirectoryPath(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetParentDirectoryPath(path);
        }

        /// <summary>
        /// Gets the root directory of the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The root directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<FSDirectory> GetRootDirectory(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetRootDirectory(path);
        }

        /// <summary>
        /// Gets the root directories of the system.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The root directories.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<List<FSDirectory>> GetRootDirectories(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetRootDirectories();
        }

        /// <summary>
        /// Gets the root directories names of the system.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The root directory names.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<List<string>> GetRootDirectoryNames(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetRootDirectoryNames();
        }

        /// <summary>
        /// Gets the root directory of the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The root directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<string> GetRootDirectoryPath(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetRootDirectoryPath(path);
        }

        /// <summary>
        /// Moves the directory from the specified source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if the destination already exists.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the move succeeds; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static async Task<bool> Move(string source, string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.ThrowIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Copy(source, destination, collisionOption);
        }

        #endregion Public Methods
    }
}