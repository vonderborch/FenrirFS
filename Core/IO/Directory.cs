// ***********************************************************************
// Assembly         : FenrirFS
// Component        : Directory.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="Directory.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the Directory static class.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
using FenrirFS.Helpers;
using System;
using System.Collections.Generic;

namespace FenrirFS.Static
{
    /// <summary>
    /// Exposes static methods for creating, moving, and enumerating through directories and sub-directories.
    /// </summary>
    public static class Directory
    {
        #region Public Methods

        /// <summary>
        /// Copies the directory at the source path to the destination path.
        /// </summary>
        /// <param name="source">The source directory full path.</param>
        /// <param name="destination">The destination directory full path.</param>
        /// <param name="collisionOption">The collision option if the destination directory already exists.</param>
        /// <returns><c>true</c> if the copy succeeds; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static bool Copy(string source, string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.ThrowIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            if (Exists(source))
                return false;

            var folder = FS.GetDirectory(source, OpenMode.ThrowIfDoesNotExist);
            return folder.Copy(destination, collisionOption) != null;
        }

        /// <summary>
        /// Creates a directory at the specified path.
        /// </summary>
        /// <param name="path">The path to create the directory at.</param>
        /// <returns><c>true</c> if the task succeeds; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static bool Create(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            return FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist) != null;
        }

        /// <summary>
        /// Creates a directory at the specified path.
        /// </summary>
        /// <param name="path">The path to create the directory at.</param>
        /// <returns>The directory that was created</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FSDirectory CreateDirectory(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            return FS.GetDirectory(path, OpenMode.CreateIfDoesNotExist);
        }

        /// <summary>
        /// Deletes the directory at the specified path.
        /// </summary>
        /// <param name="path">The path of the directory to delete.</param>
        /// <returns><c>true</c> if the delete succeeds; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static bool Delete(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);

            return folder != null
                ? folder.Delete()
                : true;
        }

        /// <summary>
        /// Enumerates the sub-directories at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate sub-directories from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>The enumeration of directories.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static IEnumerable<FSDirectory> EnumerateDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFolders(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Enumerates the names sub-directories at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate sub-directories from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>The enumeration of directory names.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static IEnumerable<string> EnumerateDirectoryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFolderNames(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Enumerates the file names at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate files from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>The enumeration of file names.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static IEnumerable<string> EnumerateFileNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileNames(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Enumerates the files at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate files from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>The enumeration of files.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static IEnumerable<FSFile> EnumerateFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFiles(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Enumerates the file system entries at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate file system entries from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>The enumeration of file system entries.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static IEnumerable<FSFileSystemEntry> EnumerateFileSystemEntries(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileSystemEntries(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Enumerates the file system entry names at the specified path.
        /// </summary>
        /// <param name="path">The path to enumerate file system entries from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>The enumeration of file system entry names.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static IEnumerable<string> EnumerateFileSystemEntryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileSystemEntryNames(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Determines whether a directory exists at the specified path.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns><c>true</c> if the directory exists; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static bool Exists(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            return FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist) != null;
        }

        /// <summary>
        /// Gets the creation time of a directory.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <param name="useUTC">if set to <c>true</c> [use UTC].</param>
        /// <returns>The creation time of the directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static DateTime GetCreationTime(string path, bool useUTC = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetCreationTime(useUTC)
                : DateTime.MinValue;
        }

        /// <summary>
        /// Gets the current working directory of the application.
        /// </summary>
        /// <returns>The current working directory of the application.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FSDirectory GetCurrentDirectory()
        {
            return FS.GetCurrentDirectory();
        }

        /// <summary>
        /// Gets the path of the current working directory of the application.
        /// </summary>
        /// <returns>The path of the current working directory of the application.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static string GetCurrentDirectoryFullPath()
        {
            return FS.GetCurrentDirectory();
        }

        /// <summary>
        /// Gets a list of sub-directories at the path.
        /// </summary>
        /// <param name="path">The path to get sub-directories from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of sub-directories at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static List<FSDirectory> GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFolders(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Gets a list of sub-directory names at the path.
        /// </summary>
        /// <param name="path">The path to get sub-directories from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of sub-directory names at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static List<string> GetDirectoryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFolderNames(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Gets a list of file names at the path.
        /// </summary>
        /// <param name="path">The path to get files from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of file names at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static List<string> GetFileNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileNames(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Gets a list of files at the path.
        /// </summary>
        /// <param name="path">The path to get files from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of files at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static List<FSFile> GetFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFiles(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Gets a list of file system entries at the path.
        /// </summary>
        /// <param name="path">The path to get file system entries from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of file system entries at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static List<FSFileSystemEntry> GetFileSystemEntries(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileSystemEntries(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Gets a list of file system entry names at the path.
        /// </summary>
        /// <param name="path">The path to get file system entries from.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of file system entry names at the path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static List<string> GetFileSystemEntryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileSystemEntryNames(searchPattern, searchOption)
                : null;
        }

        /// <summary>
        /// Gets the last accessed time of a directory.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <param name="useUTC">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last accessed time of the directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static DateTime GetLastAccessedTime(string path, bool useUTC = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetLastAccessedTime(useUTC)
                : DateTime.MinValue;
        }

        /// <summary>
        /// Gets the last modified time of a directory.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <param name="useUTC">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last modified time of the directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static DateTime GetLastModifiedTime(string path, bool useUTC = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetLastModifiedTime(useUTC)
                : DateTime.MinValue;
        }

        /// <summary>
        /// Gets the parent directory of the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The parent directory of the specified path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FSDirectory GetParentDirectory(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.ParentFolder
                : null;
        }

        /// <summary>
        /// Gets the parent directory full path of the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The parent directory full path of the specified path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static string GetParentDirectoryPath(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.ParentFolderPath
                : null;
        }

        /// <summary>
        /// Gets the root directories of the system.
        /// </summary>
        /// <returns>The root directories.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static List<FSDirectory> GetRootDirectories()
        {
            return FS.GetRootDirectories();
        }

        /// <summary>
        /// Gets the root directory of the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The root directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FSDirectory GetRootDirectory(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.RootFolder
                : null;
        }

        /// <summary>
        /// Gets the root directories names of the system.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The root directory names.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static List<string> GetRootDirectoryNames()
        {
            var dirs = FS.GetRootDirectories();
            var output = new List<string>();
            for (int i = 0; i < dirs.Count; i++)
                output.Add(dirs[i]);
            return output;
        }

        /// <summary>
        /// Gets the root directory of the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The root directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static string GetRootDirectoryPath(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.RootFolderPath
                : null;
        }

        /// <summary>
        /// Moves the directory from the specified source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if the destination already exists.</param>
        /// <returns><c>true</c> if the move succeeds; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static bool Move(string source, string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.ThrowIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            if (Exists(source))
                return false;

            var folder = FS.GetDirectory(source, OpenMode.ThrowIfDoesNotExist);
            return folder.Move(destination, collisionOption);
        }

        #endregion Public Methods
    }
}