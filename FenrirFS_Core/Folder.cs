/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    /// <summary>
    /// Provides methods for interacting with folders.
    /// </summary>
    public static class Folder
    {
        #region Public Methods

        /// <summary>
        /// Copies the specified source folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <param name="overwriteItems">if set to <c>true</c> overwrites any existing items with the same name in the destination folder.</param>
        /// <returns>Whether the folder was copied or not.</returns>
        public static bool Copy(string source, string destination, bool overwriteFolder = false, bool overwriteItems = false)
        {
            Exceptions.NotNullOrEmptyException(source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));

            if ((!overwriteFolder && Fenrir.FileSystem.FolderExists(destination)) || !Fenrir.FileSystem.FolderExists(source))
                return false;

            FileCollisionOption itemCollision;
            if (overwriteItems)
                itemCollision = FileCollisionOption.ReplaceExisting;
            else
                itemCollision = FileCollisionOption.FailIfExists;

            AFolder d = null;
            using (var s = Fenrir.FileSystem.OpenFolder(source, OpenMode.FailIfDoesNotExist))
                d = s.Copy(destination, FolderCollisionOption.ReplaceExisting, itemCollision);

            return d != null;
        }

        /// <summary>
        /// Copies the specified source folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <param name="overwriteItems">if set to <c>true</c> overwrites any existing items with the same name in the destination folder.</param>
        /// <returns>Whether the folder was copied or not.</returns>
        public static bool Copy(AFolder source, string destination, bool overwriteFolder = false, bool overwriteItems = false)
        {
            Exceptions.NotNullException<AFolder>(source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));

            if (!overwriteFolder && Fenrir.FileSystem.FolderExists(destination))
                return false;

            FileCollisionOption itemCollision;
            if (overwriteItems)
                itemCollision = FileCollisionOption.ReplaceExisting;
            else
                itemCollision = FileCollisionOption.FailIfExists;
            
            return source.Copy(destination, FolderCollisionOption.ReplaceExisting, itemCollision) != null;
        }

        /// <summary>
        /// Copies the specified source folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <param name="overwriteItems">if set to <c>true</c> overwrites any existing items with the same name in the destination folder.</param>
        /// <returns>Whether the folder was copied or not.</returns>
        public static bool Copy(AFolder source, AFolder destination, bool overwriteFolder = false, bool overwriteItems = false)
        {
            Exceptions.NotNullException<AFolder>(source, nameof(source));
            Exceptions.NotNullException<AFolder>(destination, nameof(destination));

            if (!overwriteFolder && Fenrir.FileSystem.FolderExists(destination.FullPath))
                return false;

            FileCollisionOption itemCollision;
            if (overwriteItems)
                itemCollision = FileCollisionOption.ReplaceExisting;
            else
                itemCollision = FileCollisionOption.FailIfExists;

            return source.Copy(destination.FullPath, FolderCollisionOption.ReplaceExisting, itemCollision) != null;
        }

        /// <summary>
        /// Asynchronously copies the specified source folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <param name="overwriteItems">if set to <c>true</c> overwrites any existing items with the same name in the destination folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to copy the folder. The boolean represents whether the folder was copied or not.</returns>
        public static async Task<bool> CopyAsync(string source, string destination, bool overwriteFolder = false, bool overwriteItems = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(source, destination, overwriteFolder, overwriteItems);
        }

        /// <summary>
        /// Asynchronously copies the specified source folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <param name="overwriteItems">if set to <c>true</c> overwrites any existing items with the same name in the destination folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to copy the folder. The boolean represents whether the folder was copied or not.</returns>
        public static async Task<bool> CopyAsync(AFolder source, string destination, bool overwriteFolder = false, bool overwriteItems = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(source, destination, overwriteFolder, overwriteItems);
        }

        /// <summary>
        /// Asynchronously copies the specified source folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <param name="overwriteItems">if set to <c>true</c> overwrites any existing items with the same name in the destination folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to copy the folder. The boolean represents whether the folder was copied or not.</returns>
        public static async Task<bool> CopyAsync(AFolder source, AFolder destination, bool overwriteFolder = false, bool overwriteItems = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(source, destination, overwriteFolder, overwriteItems);
        }

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>Whether the folder was created or not.</returns>
        public static bool CreateFolder(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            try { return Fenrir.FileSystem.CreateFolder(folder, FileCollisionOption.FailIfExists) != null; }
            catch { return false; }
        }

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>Whether the folder was created or not.</returns>
        public static bool CreateFolder(AFolder folder)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            try { return Fenrir.FileSystem.CreateFolder(folder.FullPath, FileCollisionOption.FailIfExists) != null; }
            catch { return false; }
        }

        /// <summary>
        /// Asynchronously creates the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to create the folder. The boolean represents whether the folder was created or not.</returns>
        public static async Task<bool> CreateFolderAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFolder(folder);
        }

        /// <summary>
        /// Asynchronously creates the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to create the folder. The boolean represents whether the folder was created or not.</returns>
        public static async Task<bool> CreateFolderAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFolder(folder);
        }

        /// <summary>
        /// Deletes the specified folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>Whether the folder was deleted or not.</returns>
        public static bool Delete(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            if (Fenrir.FileSystem.FolderExists(folder))
            {
                bool output = false;

                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.Normal))
                    output = f.Delete();

                return output;
            }

            return true;
        }

        /// <summary>
        /// Deletes the specified folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>Whether the folder was deleted or not.</returns>
        public static bool Delete(AFolder folder)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            return folder.Delete();
        }

        /// <summary>
        /// Asynchronously deletes the specified folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to delete the folder. The boolean represents whether the folder was deleted or not.</returns>
        public static async Task<bool> DeleteAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Delete(folder);
        }

        /// <summary>
        /// Asynchronously deletes the specified folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to delete the folder. The boolean represents whether the folder was deleted or not.</returns>
        public static async Task<bool> DeleteAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Delete(folder);
        }

        /// <summary>
        /// Returns an enumeration of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string enumerable representing the names of all files in the folder.</returns>
        public static IEnumerable<string> EnumerateFiles(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            return InternalGetFiles(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an enumeration of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string enumerable representing the names of all files in the folder.</returns>
        public static IEnumerable<string> EnumerateFiles(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            return InternalGetFiles(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an enumeration of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable task to enumerate through the files. The string enumerable represents the names of all files in the folder.</returns>
        public static async Task<IEnumerable<string>> EnumerateFilesAsync(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return EnumerateFiles(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an enumeration of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable task to enumerate through the files. The string enumerable represents the names of all files in the folder.</returns>
        public static async Task<IEnumerable<string>> EnumerateFilesAsync(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return EnumerateFiles(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an enumeration of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string enumerable representing the names of all files and folders in the folder.</returns>
        public static IEnumerable<string> EnumerateFileSystemEntries(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            return InternalGetFileSystemEntries(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an enumeration of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string enumerable representing the names of all files and folders in the folder.</returns>
        public static IEnumerable<string> EnumerateFileSystemEntries(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            return InternalGetFileSystemEntries(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an enumeration of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable task to enumerate through the files and folders. The string enumerable represents the names of all files and folders in the folder.</returns>
        public static async Task<IEnumerable<string>> EnumerateFileSystemEntriesAsync(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return EnumerateFileSystemEntries(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an enumeration of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable task to enumerate through the files and folders. The string enumerable represents the names of all files and folders in the folder.</returns>
        public static async Task<IEnumerable<string>> EnumerateFileSystemEntriesAsync(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return EnumerateFileSystemEntries(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an enumeration of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string enumerable representing the names of all folders in the folder.</returns>
        public static IEnumerable<string> EnumerateFolders(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            return InternalGetFolders(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an enumeration of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string enumerable representing the names of all folders in the folder.</returns>
        public static IEnumerable<string> EnumerateFolders(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            return InternalGetFolders(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an enumeration of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable task to enumerate through the folders. The string enumerable represents the names of all folders in the folder.</returns>
        public static async Task<IEnumerable<string>> EnumerateFoldersAsync(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return EnumerateFolders(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an enumeration of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An enumerable task to enumerate through the folders. The string enumerable represents the names of all folders in the folder.</returns>
        public static async Task<IEnumerable<string>> EnumerateFoldersAsync(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return EnumerateFolders(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns whether the folder exists or not.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>Whether the folder exists or not.</returns>
        public static bool Exists(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));
            return Fenrir.FileSystem.FolderExists(folder);
        }

        /// <summary>
        /// Returns whether the folder exists or not.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>Whether the folder exists or not.</returns>
        public static bool Exists(AFolder folder)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));
            return Fenrir.FileSystem.FolderExists(folder.FullPath);
        }

        /// <summary>
        /// Asynchronously returns whether the folder exists or not.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to check if the folder exists. The boolean represents whether the folder exists or not.</returns>
        public static async Task<bool> ExistsAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Exists(folder);
        }

        /// <summary>
        /// Asynchronously returns whether the folder exists or not.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to check if the folder exists. The boolean represents whether the folder exists or not.</returns>
        public static async Task<bool> ExistsAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Exists(folder);
        }

        /// <summary>
        /// Gets the creation time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The creation time of the folder.</returns>
        public static DateTime GetCreationTime(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));
            
            if (Fenrir.FileSystem.FolderExists(folder))
            {
                DateTime output = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.FailIfDoesNotExist))
                    output = f.CreationTime;

                return output;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the creation time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The creation time of the folder.</returns>
        public static DateTime GetCreationTime(AFolder folder)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));
            
            return folder.CreationTime;
        }

        /// <summary>
        /// Asynchronously gets the creation time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the creation time. The DateTime represents the creation time of the folder.</returns>
        public static async Task<DateTime> GetCreationTimeAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetCreationTime(folder);
        }

        /// <summary>
        /// Asynchronously gets the creation time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the creation time. The DateTime represents the creation time of the folder.</returns>
        public static async Task<DateTime> GetCreationTimeAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetCreationTime(folder);
        }

        /// <summary>
        /// Gets the UTC creation time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The creation time of the folder.</returns>
        public static DateTime GetCreationTimeUtc(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            if (Fenrir.FileSystem.FolderExists(folder))
            {
                DateTime output = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.FailIfDoesNotExist))
                    output = f.CreationTimeUtc;

                return output;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the UTC creation time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The creation time of the folder.</returns>
        public static DateTime GetCreationTimeUtc(AFolder folder)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            return folder.CreationTimeUtc;
        }

        /// <summary>
        /// Asynchronously gets the UTC creation time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the creation time. The DateTime represents the creation time of the folder.</returns>
        public static async Task<DateTime> GetCreationTimeUtcAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetCreationTimeUtc(folder);
        }

        /// <summary>
        /// Asynchronously gets the UTC creation time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the creation time. The DateTime represents the creation time of the folder.</returns>
        public static async Task<DateTime> GetCreationTimeUtcAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetCreationTimeUtc(folder);
        }

        /// <summary>
        /// Returns an string array of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string array representing the names of all files in the folder.</returns>
        public static string[] GetFiles(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFiles(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries.ToArray();
        }

        /// <summary>
        /// Returns an string array of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string array representing the names of all files in the folder.</returns>
        public static string[] GetFiles(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFiles(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries.ToArray();
        }

        /// <summary>
        /// Asynchronously returns an string array of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string array task to get the files. The string array represents the names of all files in the folder.</returns>
        public static async Task<string[]> GetFilesAsync(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFiles(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an string array of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string array task to get the files. The string array represents the names of all files in the folder.</returns>
        public static async Task<string[]> GetFilesAsync(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFiles(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an string list of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string array representing the names of all files in the folder.</returns>
        public static List<string> GetFilesList(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFiles(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries;
        }

        /// <summary>
        /// Returns an string list of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string list representing the names of all files in the folder.</returns>
        public static List<string> GetFilesList(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFiles(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries;
        }

        /// <summary>
        /// Asynchronously returns an string list of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string list task to get the files. The string list represents the names of all files in the folder.</returns>
        public static async Task<List<string>> GetFilesListAsync(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFilesList(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an string list of all files in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string list task to get the files. The string list represents the names of all files in the folder.</returns>
        public static async Task<List<string>> GetFilesListAsync(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFilesList(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an string array of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string array representing the names of all files and folders in the folder.</returns>
        public static string[] GetFileSystemEntries(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFileSystemEntries(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries.ToArray();
        }

        /// <summary>
        /// Returns an string array of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string array representing the names of all files and folders in the folder.</returns>
        public static string[] GetFileSystemEntries(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFileSystemEntries(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries.ToArray();
        }

        /// <summary>
        /// Asynchronously returns an string array of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string array task to get the files and folders. The string array represents the names of all files and folders in the folder.</returns>
        public static async Task<string[]> GetFileSystemEntriesAsync(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFileSystemEntries(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an string array of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string array task to get the files and folders. The string array represents the names of all files and folders in the folder.</returns>
        public static async Task<string[]> GetFileSystemEntriesAsync(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFileSystemEntries(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an string list of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string list representing the names of all files and folders in the folder.</returns>
        public static List<string> GetFileSystemEntriesList(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFileSystemEntries(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries;
        }

        /// <summary>
        /// Returns an string list of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string list representing the names of all files and folders in the folder.</returns>
        public static List<string> GetFileSystemEntriesList(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFileSystemEntries(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries;
        }

        /// <summary>
        /// Asynchronously returns an string list of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string list task to get the files and folders. The string list represents the names of all files and folders in the folder.</returns>
        public static async Task<List<string>> GetFileSystemEntriesListAsync(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFileSystemEntriesList(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an string list of all files and folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string list task to get the files and folders. The string list represents the names of all files and folders in the folder.</returns>
        public static async Task<List<string>> GetFileSystemEntriesListAsync(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFileSystemEntriesList(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an string array of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string array representing the names of all folders in the folder.</returns>
        public static string[] GetFolders(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFolders(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries.ToArray();
        }

        /// <summary>
        /// Returns an string array of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string array representing the names of all folders in the folder.</returns>
        public static string[] GetFolders(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFolders(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries.ToArray();
        }

        /// <summary>
        /// Asynchronously returns an string array of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string array task to get the folders. The string array represents the names of all folders in the folder.</returns>
        public static async Task<string[]> GetFoldersAsync(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFolders(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an string array of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string array task to get the folders. The string array represents the names of all folders in the folder.</returns>
        public static async Task<string[]> GetFoldersAsync(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFolders(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Returns an string list of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string list representing the names of all folders in the folder.</returns>
        public static List<string> GetFoldersList(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFolders(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries;
        }

        /// <summary>
        /// Returns an string list of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A string list representing the names of all folders in the folder.</returns>
        public static List<string> GetFoldersList(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            var entries = new List<string>();

            foreach (var entry in InternalGetFolders(folder, searchPattern, searchOption))
                entries.Add(entry);

            return entries;
        }

        /// <summary>
        /// Asynchronously returns an string list of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string list task to get the folders. The string list represents the names of all folders in the folder.</returns>
        public static async Task<List<string>> GetFoldersListAsync(string folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFoldersList(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Asynchronously returns an string list of all folders in the folder that match the search pattern and are within the search option choosen.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string list task to get the folders. The string list represents the names of all folders in the folder.</returns>
        public static async Task<List<string>> GetFoldersListAsync(AFolder folder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetFoldersList(folder, searchPattern, searchOption);
        }

        /// <summary>
        /// Gets the last accessed time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The last accessed time of the folder.</returns>
        public static DateTime GetLastAccessedTime(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            if (Fenrir.FileSystem.FolderExists(folder))
            {
                DateTime output = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.FailIfDoesNotExist))
                    output = f.LastAccessedTime;

                return output;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the last accessed time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The last accessed time of the folder.</returns>
        public static DateTime GetLastAccessedTime(AFolder folder)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            return folder.LastAccessedTime;
        }

        /// <summary>
        /// Asynchronously gets the UTClast accessed time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last accessed time. The DateTime represents the last accessed time of the folder.</returns>
        public static async Task<DateTime> GetLastAccessedTimeAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastAccessedTime(folder);
        }

        /// <summary>
        /// Asynchronously gets the last accessed time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last accessed time. The DateTime represents the last accessed time of the folder.</returns>
        public static async Task<DateTime> GetLastAccessedTimeAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastAccessedTime(folder);
        }

        /// <summary>
        /// Gets the UTC last accessed time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The last accessed time of the folder.</returns>
        public static DateTime GetLastAccessedTimeUtc(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            if (Fenrir.FileSystem.FolderExists(folder))
            {
                DateTime output = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.FailIfDoesNotExist))
                    output = f.LastAccessedTimeUtc;

                return output;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the UTC last accessed time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The last accessed time of the folder.</returns>
        public static DateTime GetLastAccessedTimeUtc(AFolder folder)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            return folder.LastAccessedTimeUtc;
        }

        /// <summary>
        /// Asynchronously gets the UTC last accessed time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last accessed time. The DateTime represents the last accessed time of the folder.</returns>
        public static async Task<DateTime> GetLastAccessedTimeUtcAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastAccessedTimeUtc(folder);
        }

        /// <summary>
        /// Asynchronously gets the UTC last accessed time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last accessed time. The DateTime represents the last accessed time of the folder.</returns>
        public static async Task<DateTime> GetLastAccessedTimeUtcAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastAccessedTimeUtc(folder);
        }

        /// <summary>
        /// Gets the last modified time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The last modified time of the folder.</returns>
        public static DateTime GetLastModifiedTime(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            if (Fenrir.FileSystem.FolderExists(folder))
            {
                DateTime output = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.FailIfDoesNotExist))
                    output = f.LastModifiedTime;

                return output;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the last modified time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The last modified time of the folder.</returns>
        public static DateTime GetLastModifiedTime(AFolder folder)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            return folder.LastModifiedTime;
        }

        /// <summary>
        /// Asynchronously gets the last modified time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last modified time. The DateTime represents the last modified time of the folder.</returns>
        public static async Task<DateTime> GetLastModifiedTimeAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastModifiedTime(folder);
        }

        /// <summary>
        /// Asynchronously gets the last modified time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last modified time. The DateTime represents the last modified time of the folder.</returns>
        public static async Task<DateTime> GetLastModifiedTimeAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastModifiedTime(folder);
        }

        /// <summary>
        /// Gets the UTC last modified time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The last modified time of the folder.</returns>
        public static DateTime GetLastModifiedTimeUtc(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            if (Fenrir.FileSystem.FolderExists(folder))
            {
                DateTime output = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.FailIfDoesNotExist))
                    output = f.LastModifiedTimeUtc;

                return output;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the UTC last modified time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The last modified time of the folder.</returns>
        public static DateTime GetLastModifiedTimeUtc(AFolder folder)
        {
            Exceptions.NotNullException<AFolder>(folder, nameof(folder));

            return folder.LastModifiedTimeUtc;
        }

        /// <summary>
        /// Asynchronously gets the UTC last modified time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last modified time. The DateTime represents the last modified time of the folder.</returns>
        public static async Task<DateTime> GetLastModifiedTimeUtcAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastModifiedTimeUtc(folder);
        }

        /// <summary>
        /// Asynchronously gets the UTC last modified time of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last modified time. The DateTime represents the last modified time of the folder.</returns>
        public static async Task<DateTime> GetLastModifiedTimeUtcAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastModifiedTimeUtc(folder);
        }

        /// <summary>
        /// Gets the parent folder of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The parent folder.</returns>
        public static string GetParentFolder(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            if (Fenrir.FileSystem.FolderExists(folder))
            {
                string output = String.Empty;
                using (var s = Fenrir.FileSystem.OpenFolder(folder, OpenMode.FailIfDoesNotExist))
                    output = s.ParentFolder;

                return output;
            }

            return String.Empty;
        }

        /// <summary>
        /// Gets the parent folder of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The parent folder.</returns>
        public static string GetParentFolder(AFolder folder)
        {
            return folder.ParentFolder;
        }

        /// <summary>
        /// Asynchronously gets the parent folder of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string task to get the parent folder. The string is the parent folder.</returns>
        public static async Task<string> GetParentFolderAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetParentFolder(folder);
        }

        /// <summary>
        /// Asynchronously gets the parent folder of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string task to get the parent folder. The string is the parent folder.</returns>
        public static async Task<string> GetParentFolderAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetParentFolder(folder);
        }

        /// <summary>
        /// Gets the root folder of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The root folder.</returns>
        public static string GetRootFolder(string folder)
        {
            Exceptions.NotNullOrEmptyException(folder, nameof(folder));

            if (Fenrir.FileSystem.FolderExists(folder))
            {
                string output = String.Empty;
                using (var s = Fenrir.FileSystem.OpenFolder(folder, OpenMode.FailIfDoesNotExist))
                    output = s.RootFolder;

                return output;
            }

            return String.Empty;
        }

        /// <summary>
        /// Gets the root folder of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>The root folder.</returns>
        public static string GetRootFolder(AFolder folder)
        {
            return folder.RootFolder;
        }

        /// <summary>
        /// Asynchronously gets the root folder of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string task to get the root folder. The string is the root folder.</returns>
        public static async Task<string> GetRootFolderAsync(string folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetRootFolder(folder);
        }

        /// <summary>
        /// Asynchronously gets the root folder of the folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string task to get the root folder. The string is the root folder.</returns>
        public static async Task<string> GetRootFolderAsync(AFolder folder, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetRootFolder(folder);
        }

        /// <summary>
        /// Moves the specified folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <returns>Whether the folder was moved or not.</returns>
        public static bool Move(string source, string destination, bool overwriteFolder = false)
        {
            Exceptions.NotNullOrEmptyException(source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));

            if ((!overwriteFolder && Fenrir.FileSystem.FolderExists(destination)) || !Fenrir.FileSystem.FolderExists(source))
                return false;
            
            bool output = false;
            using (var s = Fenrir.FileSystem.OpenFolder(source, OpenMode.FailIfDoesNotExist))
                output = s.Move(destination, FolderCollisionOption.ReplaceExisting);

            return output;
        }

        /// <summary>
        /// Moves the specified folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <returns>Whether the folder was moved or not.</returns>
        public static bool Move(AFolder source, string destination, bool overwriteFolder = false)
        {
            Exceptions.NotNullException<AFolder>(source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));

            if (!overwriteFolder && Fenrir.FileSystem.FolderExists(destination))
                return false;
            
            return source.Move(destination, FolderCollisionOption.ReplaceExisting);
        }

        /// <summary>
        /// Moves the specified folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <returns>Whether the folder was moved or not.</returns>
        public static bool Move(AFolder source, AFolder destination, bool overwriteFolder = false)
        {
            Exceptions.NotNullException<AFolder>(source, nameof(source));
            Exceptions.NotNullException<AFolder>(destination, nameof(destination));

            if (!overwriteFolder && Fenrir.FileSystem.FolderExists(destination.FullPath))
                return false;
            
            return source.Move(destination.FullPath, FolderCollisionOption.ReplaceExisting);
        }

        /// <summary>
        /// Asynchronously moves the specified folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to move the folder. The boolean represents whether the folder was moved or not.</returns>
        public static async Task<bool> MoveAsync(string source, string destination, bool overwriteFolder = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(source, destination, overwriteFolder);
        }

        /// <summary>
        /// Asynchronously moves the specified folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to move the folder. The boolean represents whether the folder was moved or not.</returns>
        public static async Task<bool> MoveAsync(AFolder source, string destination, bool overwriteFolder = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(source, destination, overwriteFolder);
        }

        /// <summary>
        /// Asynchronously moves the specified folder to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwriteFolder">if set to <c>true</c> overwrites the destination folder if it exists.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to move the folder. The boolean represents whether the folder was moved or not.</returns>
        public static async Task<bool> MoveAsync(AFolder source, AFolder destination, bool overwriteFolder = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(source, destination, overwriteFolder);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Internal function to get files.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>An enumerable representing the files.</returns>
        private static IEnumerable<string> InternalGetFiles(string folder, string searchPattern, SearchOption searchOption)
        {
            if (Fenrir.FileSystem.FolderExists(folder))
            {
                var files = new List<string>();

                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.Normal))
                {
                    if (searchOption != SearchOption.SubDirectoriesOnly)
                    {
                        var topFiles = f.GetFileNames();

                        foreach (var file in topFiles)
                        {
                            if (Regex.IsMatch(file, searchPattern))
                                files.Add(file);
                        }
                    }

                    if (searchOption != SearchOption.TopDirectoryOnly)
                    {
                        var folders = f.GetFolderNames();
                        foreach (var subFolder in folders)
                        {
                            var subFiles = EnumerateFiles(subFolder, searchPattern, SearchOption.AllDirectories);
                            foreach (var file in subFiles)
                            {
                                if (Regex.IsMatch(file, searchPattern))
                                    files.Add(file);
                            }
                        }
                    }
                }

                return files;
            }

            return new string[0];
        }

        /// <summary>
        /// Internal function to get files.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>An enumerable representing the files.</returns>
        private static IEnumerable<string> InternalGetFiles(AFolder folder, string searchPattern, SearchOption searchOption)
        {
            var files = new List<string>();

            if (searchOption != SearchOption.SubDirectoriesOnly)
            {
                var topFiles = folder.GetFileNames();

                foreach (var file in topFiles)
                {
                    if (Regex.IsMatch(file, searchPattern))
                        files.Add(file);
                }
            }

            if (searchOption != SearchOption.TopDirectoryOnly)
            {
                var folders = folder.GetFolderNames();
                foreach (var subFolder in folders)
                {
                    var subFiles = EnumerateFiles(subFolder, searchPattern, SearchOption.AllDirectories);
                    foreach (var file in subFiles)
                    {
                        if (Regex.IsMatch(file, searchPattern))
                            files.Add(file);
                    }
                }
            }

            return files;
        }

        /// <summary>
        /// Internal function to get file system entries.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>An enumerable representing the file system entries.</returns>
        private static IEnumerable<string> InternalGetFileSystemEntries(string folder, string searchPattern, SearchOption searchOption)
        {
            if (Fenrir.FileSystem.FolderExists(folder))
            {
                var filesAndFolders = new List<string>();

                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.Normal))
                {
                    if (searchOption != SearchOption.SubDirectoriesOnly)
                    {
                        var topFiles = f.GetFileNames();
                        foreach (var file in topFiles)
                        {
                            if (Regex.IsMatch(file, searchPattern))
                                filesAndFolders.Add(file);
                        }

                        var topFolders = f.GetFileNames();
                        foreach (var topFolder in topFolders)
                        {
                            if (Regex.IsMatch(topFolder, searchPattern))
                                filesAndFolders.Add(topFolder);
                        }
                    }

                    var folders = f.GetFolderNames();
                    foreach (var subFolder in folders)
                    {
                        if (Regex.IsMatch(subFolder, searchPattern))
                            filesAndFolders.Add(subFolder);
                        if (searchOption != SearchOption.TopDirectoryOnly)
                        {
                            var subFiles = EnumerateFileSystemEntries(subFolder, searchPattern, SearchOption.AllDirectories);
                            foreach (var file in subFiles)
                            {
                                if (Regex.IsMatch(file, searchPattern))
                                    filesAndFolders.Add(file);
                            }
                        }
                    }
                }

                return filesAndFolders;
            }

            return new string[0];
        }

        /// <summary>
        /// Internal function to get file system entries.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>An enumerable representing the file system entries.</returns>
        private static IEnumerable<string> InternalGetFileSystemEntries(AFolder folder, string searchPattern, SearchOption searchOption)
        {
            var filesAndFolders = new List<string>();

            if (searchOption != SearchOption.SubDirectoriesOnly)
            {
                var topFiles = folder.GetFileNames();
                foreach (var file in topFiles)
                {
                    if (Regex.IsMatch(file, searchPattern))
                        filesAndFolders.Add(file);
                }

                var topFolders = folder.GetFileNames();
                foreach (var topFolder in topFolders)
                {
                    if (Regex.IsMatch(topFolder, searchPattern))
                        filesAndFolders.Add(topFolder);
                }
            }

            var folders = folder.GetFolderNames();
            foreach (var subFolder in folders)
            {
                if (Regex.IsMatch(subFolder, searchPattern))
                    filesAndFolders.Add(subFolder);
                if (searchOption != SearchOption.TopDirectoryOnly)
                {
                    var subFiles = EnumerateFileSystemEntries(subFolder, searchPattern, SearchOption.AllDirectories);
                    foreach (var file in subFiles)
                    {
                        if (Regex.IsMatch(file, searchPattern))
                            filesAndFolders.Add(file);
                    }
                }
            }

            return filesAndFolders;
        }

        /// <summary>
        /// Internal function to get folders.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>An enumerable representing the folders.</returns>
        private static IEnumerable<string> InternalGetFolders(string folder, string searchPattern, SearchOption searchOption)
        {
            if (Fenrir.FileSystem.FolderExists(folder))
            {
                var folders = new List<string>();

                using (var f = Fenrir.FileSystem.OpenFolder(folder, OpenMode.Normal))
                {
                    var subFolders = f.GetFolderNames();
                    foreach (var subFolder in subFolders)
                    {
                        if (Regex.IsMatch(subFolder, searchPattern))
                            folders.Add(subFolder);
                        if (searchOption != SearchOption.TopDirectoryOnly)
                        {
                            var subFolderFolders = EnumerateFolders(subFolder, searchPattern, SearchOption.AllDirectories);
                            foreach (var tmp in subFolderFolders)
                            {
                                if (Regex.IsMatch(tmp, searchPattern))
                                    folders.Add(tmp);
                            }
                        }
                    }
                }

                return folders;
            }

            return new string[0];
        }

        /// <summary>
        /// Internal function to get folders.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>An enumerable representing the folders.</returns>
        private static IEnumerable<string> InternalGetFolders(AFolder folder, string searchPattern, SearchOption searchOption)
        {
            var folders = new List<string>();

            var subFolders = folder.GetFolderNames();
            foreach (var subFolder in subFolders)
            {
                if (Regex.IsMatch(subFolder, searchPattern))
                    folders.Add(subFolder);
                if (searchOption != SearchOption.TopDirectoryOnly)
                {
                    var subFolderFolders = EnumerateFolders(subFolder, searchPattern, SearchOption.AllDirectories);
                    foreach (var tmp in subFolderFolders)
                    {
                        if (Regex.IsMatch(tmp, searchPattern))
                            folders.Add(tmp);
                    }
                }
            }

            return folders;
        }

        #endregion Private Methods
    }
}