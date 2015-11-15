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

namespace FenrirFS
{
    /// <summary>
    /// Provides methods for interacting with files.
    /// </summary>
    public static class File
    {
        #region Public Methods

        /// <summary>
        /// Appends lines to a file. If the file doesn't exist, this method creates the file, writes the lines to the it, and then closes the it.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents to append.</param>
        /// <returns>Whether the write was successful or not.</returns>
        public static bool AppendAllLines(string file, IEnumerable<string> contents)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));
            Exceptions.NotNullException<IEnumerable<string>>(contents, nameof(contents));

            bool result = false;
            using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.Normal))
            {
                var str = new StringBuilder();
                foreach (var line in contents)
                    str.AppendLine(line);

                result = f.WriteAll(str.ToString(), WriteMode.Append);
            }

            return result;
        }

        /// <summary>
        /// Appends lines to a file. If the file doesn't exist, this method creates the file, writes the lines to the it, and then closes the it.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <returns>Whether the write was successful or not.</returns>
        public static bool AppendAllLines(AFile file, IEnumerable<string> contents)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));
            Exceptions.NotNullException<IEnumerable<string>>(contents, nameof(contents));

            if (!file.IsOpen)
            {
                var str = new StringBuilder();
                foreach (var line in contents)
                    str.AppendLine(line);

                return file.WriteAll(str.ToString(), WriteMode.Append);
            }

            return false;
        }

        /// <summary>
        /// Asynchronously appends lines to a file. If the file doesn't exist, this method creates the file, writes the lines to the it, and then closes the it.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to append lines to the file. The boolean represents whether the write was successful or not.</returns>
        public static async Task<bool> AppendAllLinesAsync(string file, IEnumerable<string> contents, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return AppendAllLines(file, contents);
        }

        /// <summary>
        /// Asynchronously appends lines to a file. If the file doesn't exist, this method creates the file, writes the lines to the it, and then closes the it.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to append lines to the file. The boolean represents whether the write was successful or not.</returns>
        public static async Task<bool> AppendAllLinesAsync(AFile file, IEnumerable<string> contents, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return AppendAllLines(file, contents);
        }

        /// <summary>
        /// Appends text to a file. If the file doesn't exist, this method creates the file, writes the text to the it, and then closes the it.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <returns>Whether the write was successful or not.</returns>
        public static bool AppendAllText(string file, string contents)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));
            Exceptions.NotNullException<string>(contents, nameof(contents));

            bool result = false;
            using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.Normal))
                result = f.WriteAll(contents, WriteMode.Append);

            return result;
        }

        /// <summary>
        /// Appends text to a file. If the file doesn't exist, this method creates the file, writes the text to the it, and then closes the it.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <returns>Whether the write was successful or not.</returns>
        public static bool AppendAllText(AFile file, string contents)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));
            Exceptions.NotNullException<string>(contents, nameof(contents));

            return file.IsOpen
                ? false
                : file.WriteAll(contents, WriteMode.Append);
        }

        /// <summary>
        /// Asynchronously appends text to a file. If the file doesn't exist, this method creates the file, writes the text to the it, and then closes the it.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to append lines to the file. The boolean represents whether the write was successful or not.</returns>
        public static async Task<bool> AppendAllTextAsync(string file, string contents, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return AppendAllText(file, contents);
        }
        
        /// <summary>
        /// Asynchronously appends text to a file. If the file doesn't exist, this method creates the file, writes the text to the it, and then closes the it.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>A boolean task to append lines to the file. The boolean represents whether the write was successful or not.</returns>
        public static async Task<bool> AppendAllTextAsync(AFile file, string contents, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return AppendAllText(file, contents);
        }

        /// <summary>
        /// Opens the file for appending. If the file does not exist, this method will create the file and then open it for appending.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A Stream for appending text to the file.</returns>
        public static Stream AppendText(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.Normal))
                return f.Open(FileAccess.Write, FileMode.Append);
        }

        /// <summary>
        /// Opens the file for appending. If the file does not exist, this method will create the file and then open it for appending.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A Stream for appending text to the file.</returns>
        public static Stream AppendText(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.Open(FileAccess.Write, FileMode.Append);
        }

        /// <summary>
        /// Asynchronously opens the file for appending. If the file does not exist, this method will create the file and then open it for appending.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file for appending. The Stream is for appending text to the file.</returns>
        public static async Task<Stream> AppendTextAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return AppendText(file);
        }

        /// <summary>
        /// Asynchronously opens the file for appending. If the file does not exist, this method will create the file and then open it for appending.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file for appending. The Stream is for appending text to the file.</returns>
        public static async Task<Stream> AppendTextAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return AppendText(file);
        }

        /// <summary>
        /// Copies the file at the source to the destination. 
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite]. By default, is set to false (do not overwrite the destination file).</param>
        /// <returns>Whether the copy was successful or not.</returns>
        public static bool Copy(string source, string destination, bool overwrite = false)
        {
            Exceptions.NotNullOrEmptyException(source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));

            if ((!overwrite && Fenrir.FileSystem.FileExists(destination)) || !Fenrir.FileSystem.FileExists(source))
                return false;

            AFile d = null;
            using (var s = Fenrir.FileSystem.OpenFile(source, OpenMode.FailIfDoesNotExist))
                d = s.Copy(destination, FileCollisionOption.ReplaceExisting);

            return d != null;
        }

        /// <summary>
        /// Copies the file at the source to the destination. 
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite]. By default, is set to false (do not overwrite the destination file).</param>
        /// <returns>Whether the copy was successful or not.</returns>
        public static bool Copy(AFile source, string destination, bool overwrite = false)
        {
            Exceptions.NotNullException<AFile>(source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));

            if ((!overwrite && Fenrir.FileSystem.FileExists(destination)) || !Fenrir.FileSystem.FileExists(source.FullPath))
                return false;

            return source.Copy(destination, FileCollisionOption.ReplaceExisting) != null;
        }

        /// <summary>
        /// Copies the file at the source to the destination. 
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite]. By default, is set to false (do not overwrite the destination file).</param>
        /// <returns>Whether the copy was successful or not.</returns>
        public static bool Copy(AFile source, AFile destination, bool overwrite = false)
        {
            Exceptions.NotNullException<AFile>(source, nameof(source));
            Exceptions.NotNullException<AFile>(destination, nameof(destination));

            if ((!overwrite && Fenrir.FileSystem.FileExists(destination.FullPath)) || !Fenrir.FileSystem.FileExists(source.FullPath))
                return false;

            return source.Copy(destination.FullPath, FileCollisionOption.ReplaceExisting) != null;
        }

        /// <summary>
        /// Asynchronously copies the file at the source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite]. By default, is set to false (do not overwrite the destination file).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to copy the file. The boolean represents whether the copy was successful or not.</returns>
        public static async Task<bool> CopyAsync(string source, string destination, bool overwrite = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(source, destination, overwrite);
        }

        /// <summary>
        /// Asynchronously copies the file at the source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite]. By default, is set to false (do not overwrite the destination file).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to copy the file. The boolean represents whether the copy was successful or not.</returns>
        public static async Task<bool> CopyAsync(AFile source, string destination, bool overwrite = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(source, destination, overwrite);
        }

        /// <summary>
        /// Asynchronously copies the file at the source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite]. By default, is set to false (do not overwrite the destination file).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to copy the file. The boolean represents whether the copy was successful or not.</returns>
        public static async Task<bool> CopyAsync(AFile source, AFile destination, bool overwrite = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Copy(source, destination, overwrite);
        }

        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Returns whether the file was created or not.</returns>
        public static bool Create(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            return Fenrir.FileSystem.CreateFile(file, collisionOption) != null;
        }

        /// <summary>
        /// Asynchronously creates the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to create the file. The boolean represents whether the file was created or not.</returns>
        public static async Task<bool> CreateAsync(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Create(file, collisionOption);
        }

        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Returns an AFile representing the new file.</returns>
        public static AFile CreateAFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            return Fenrir.FileSystem.CreateFile(file, collisionOption);
        }

        /// <summary>
        /// Asynchronously creates the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An AFile task to create the file. The AFile represents the new file.</returns>
        public static async Task<AFile> CreateAFileAsync(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateAFile(file, collisionOption);
        }

        /// <summary>
        /// Creates and opens the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Returns the file stream of the new file.</returns>
        public static Stream CreateAndOpen(string file, FileAccess fileAccess = FileAccess.Write, FileMode fileMode = FileMode.Open, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            return Fenrir.FileSystem.CreateFile(file, collisionOption).Open(fileAccess, fileMode);
        }

        /// <summary>
        /// Asynchronously creates and opens the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to create and open the file. The Stream is the file's stream.</returns>
        public static async Task<Stream> CreateAndOpenAsync(string file, FileAccess fileAccess = FileAccess.Write, FileMode fileMode = FileMode.Open, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateAndOpen(file, fileAccess, fileMode, collisionOption);
        }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Whether the file was deleted or not.</returns>
        public static bool Delete(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));
            
            return Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist).Delete();
        }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Whether the file was deleted or not.</returns>
        public static bool Delete(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.Delete();
        }

        /// <summary>
        /// Asynchronously deletes the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to delete the file. The boolean represents whether the file was deleted or not.</returns>
        public static async Task<bool> DeleteAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Delete(file);
        }

        /// <summary>
        /// Asynchronously deletes the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to delete the file. The boolean represents whether the file was deleted or not.</returns>
        public static async Task<bool> DeleteAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Delete(file);
        }

        /// <summary>
        /// Returns whether the specified file exists.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Whether the file exists or not.</returns>
        public static bool Exists(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            return Fenrir.FileSystem.FileExists(file);
        }

        /// <summary>
        /// Returns whether the specified file exists.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Whether the file exists or not.</returns>
        public static bool Exists(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return Fenrir.FileSystem.FileExists(file.FullPath);
        }

        /// <summary>
        /// Asynchronously returns whether the specified file exists.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to check file existence. The boolean represents whether the file exists or not.</returns>
        public static async Task<bool> ExistsAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Exists(file);
        }

        /// <summary>
        /// Asynchronously returns whether the specified file exists.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to check file existence. The boolean represents whether the file exists or not.</returns>
        public static async Task<bool> ExistsAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Exists(file);
        }

        /// <summary>
        /// Gets the creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was created.</returns>
        public static DateTime GetCreationTime(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));
            
            if (Fenrir.FileSystem.FileExists(file))
            {
                var time = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist))
                    time = f.CreationTime;

                return time;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was created.</returns>
        public static DateTime GetCreationTime(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.CreationTime;
        }

        /// <summary>
        /// Asynchronously gets the creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the creation time. The time represents the time the file was created.</returns>
        public static async Task<DateTime> GetCreationTimeAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetCreationTime(file);
        }

        /// <summary>
        /// Asynchronously gets the creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the creation time. The time represents the time the file was created.</returns>
        public static async Task<DateTime> GetCreationTimeAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetCreationTime(file);
        }

        /// <summary>
        /// Gets the UTC creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was created.</returns>
        public static DateTime GetCreationTimeUtc(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            if (Fenrir.FileSystem.FileExists(file))
            {
                var time = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist))
                    time = f.CreationTimeUtc;

                return time;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the UTC creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was created.</returns>
        public static DateTime GetCreationTimeUtc(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.CreationTimeUtc;
        }

        /// <summary>
        /// Asynchronously gets the UTC creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the creation time. The time represents the time the file was created.</returns>
        public static async Task<DateTime> GetCreationTimeUtcAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetCreationTimeUtc(file);
        }

        /// <summary>
        /// Asynchronously gets the UTC creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the creation time. The time represents the time the file was created.</returns>
        public static async Task<DateTime> GetCreationTimeUtcAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetCreationTimeUtc(file);
        }

        /// <summary>
        /// Gets the last access time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was last accessed.</returns>
        public static DateTime GetLastAccessTime(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            if (Fenrir.FileSystem.FileExists(file))
            {
                var time = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist))
                    time = f.LastAccessedTime;

                return time;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the last access time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was last accessed.</returns>
        public static DateTime GetLastAccessTime(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.LastAccessedTime;
        }

        /// <summary>
        /// Asynchronously gets the last access time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last access time. The time represents the time the file was last access.</returns>
        public static async Task<DateTime> GetLastAccessTimeAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastAccessTime(file);
        }

        /// <summary>
        /// Asynchronously gets the last access time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last access time. The time represents the time the file was last access.</returns>
        public static async Task<DateTime> GetLastAccessTimeAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastAccessTime(file);
        }

        /// <summary>
        /// Gets the UTC last access time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was last accessed.</returns>
        public static DateTime GetLastAccessTimeUtc(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            if (Fenrir.FileSystem.FileExists(file))
            {
                var time = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist))
                    time = f.LastAccessedTimeUtc;

                return time;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the UTC last access time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was last accessed.</returns>
        public static DateTime GetLastAccessTimeUtc(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.LastAccessedTimeUtc;
        }

        /// <summary>
        /// Asynchronously gets the UTC last access time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last access time. The time represents the time the file was last access.</returns>
        public static async Task<DateTime> GetLastAccessTimeUtcAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastAccessTimeUtc(file);
        }

        /// <summary>
        /// Asynchronously gets the UTC last access time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last access time. The time represents the time the file was last access.</returns>
        public static async Task<DateTime> GetLastAccessTimeUtcAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastAccessTimeUtc(file);
        }

        /// <summary>
        /// Gets the last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was last accessed.</returns>
        public static DateTime GetLastModifiedTime(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            if (Fenrir.FileSystem.FileExists(file))
            {
                var time = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist))
                    time = f.LastModifiedTime;

                return time;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was last accessed.</returns>
        public static DateTime GetLastModifiedTime(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.LastModifiedTime;
        }

        /// <summary>
        /// Asynchronously gets the last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last modified time. The time represents the time the file was last modified.</returns>
        public static async Task<DateTime> GetLastModifiedTimeAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastModifiedTime(file);
        }

        /// <summary>
        /// Asynchronously gets the last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last modified time. The time represents the time the file was last modified.</returns>
        public static async Task<DateTime> GetLastModifiedTimeAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastModifiedTime(file);
        }

        /// <summary>
        /// Gets the UTC last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was last accessed.</returns>
        public static DateTime GetLastModifiedTimeUtc(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            if (Fenrir.FileSystem.FileExists(file))
            {
                var time = DateTime.MinValue;
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist))
                    time = f.LastModifiedTimeUtc;

                return time;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the UTC last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The time the file was last accessed.</returns>
        public static DateTime GetLastModifiedTimeUtc(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.LastModifiedTimeUtc;
        }

        /// <summary>
        /// Asynchronously gets the UTC last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last modified time. The time represents the time the file was last modified.</returns>
        public static async Task<DateTime> GetLastModifiedTimeUtcAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastModifiedTimeUtc(file);
        }

        /// <summary>
        /// Asynchronously gets the UTC last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A DateTime task to get the last modified time. The time represents the time the file was last modified.</returns>
        public static async Task<DateTime> GetLastModifiedTimeUtcAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GetLastModifiedTimeUtc(file);
        }

        /// <summary>
        /// Moves the specified file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <returns>Whether the file was moved or not.</returns>
        public static bool Move(string source, string destination, bool overwrite = false)
        {
            Exceptions.NotNullOrEmptyException(source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));

            if ((!overwrite && Fenrir.FileSystem.FileExists(destination)) || !Fenrir.FileSystem.FileExists(source))
                return false;
            
            bool moved = false;
            using (var f = Fenrir.FileSystem.OpenFile(source, OpenMode.FailIfDoesNotExist))
                moved = f.Move(destination, FileCollisionOption.ReplaceExisting);

            return moved;
        }

        /// <summary>
        /// Moves the specified file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <returns>Whether the file was moved or not.</returns>
        public static bool Move(AFile source, string destination, bool overwrite = false)
        {
            Exceptions.NotNullException<AFile>(source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));

            if ((!overwrite && Fenrir.FileSystem.FileExists(destination)) || !Fenrir.FileSystem.FileExists(source.FullPath))
                return false;

            return source.Move(destination, FileCollisionOption.ReplaceExisting);
        }

        /// <summary>
        /// Moves the specified file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <returns>Whether the file was moved or not.</returns>
        public static bool Move(AFile source, AFile destination, bool overwrite = false)
        {
            Exceptions.NotNullException<AFile>(source, nameof(source));
            Exceptions.NotNullException<AFile>(destination, nameof(destination));

            if ((!overwrite && Fenrir.FileSystem.FileExists(destination.FullPath)) || !Fenrir.FileSystem.FileExists(source.FullPath))
                return false;

            return source.Move(destination.FullPath, FileCollisionOption.ReplaceExisting);
        }

        /// <summary>
        /// Asynchronously moves the specified file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to move the file. The boolean represents whether the file was moved or not.</returns>
        public static async Task<bool> MoveAsync(string source, string destination, bool overwrite = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(source, destination, overwrite);
        }

        /// <summary>
        /// Asynchronously moves the specified file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to move the file. The boolean represents whether the file was moved or not.</returns>
        public static async Task<bool> MoveAsync(AFile source, string destination, bool overwrite = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(source, destination, overwrite);
        }

        /// <summary>
        /// Asynchronously moves the specified file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to move the file. The boolean represents whether the file was moved or not.</returns>
        public static async Task<bool> MoveAsync(AFile source, AFile destination, bool overwrite = false, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Move(source, destination, overwrite);
        }

        /// <summary>
        /// Opens the specified file with the file access.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The stream representing the opened file.</returns>
        public static Stream Open(string file, FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            if (Fenrir.FileSystem.FileExists(file))
                return Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist).Open(fileAccess, fileMode);

            return null;
        }

        /// <summary>
        /// Opens the specified file with the file access.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The stream representing the opened file.</returns>
        public static Stream Open(AFile file, FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.Open(fileAccess, fileMode);
        }

        /// <summary>
        /// Asynchronously opens the specified file with the file access.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file. The stream represents the opened file.</returns>
        public static async Task<Stream> OpenAsync(string file, FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Open(file, fileAccess, fileMode);
        }

        /// <summary>
        /// Asynchronously opens the specified file with the file access.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file. The stream represents the opened file.</returns>
        public static async Task<Stream> OpenAsync(AFile file, FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Open(file, fileAccess, fileMode);
        }

        /// <summary>
        /// Opens the specified file for reading.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The stream representing the opened file.</returns>
        public static Stream OpenRead(string file, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            if (Fenrir.FileSystem.FileExists(file))
                return Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist).Open(FileAccess.Read, fileMode);

            return null;
        }

        /// <summary>
        /// Opens the specified file for reading.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The stream representing the opened file.</returns>
        public static Stream OpenRead(AFile file, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.Open(FileAccess.Read, fileMode);
        }

        /// <summary>
        /// Asynchronously opens the specified file for reading.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file for reading. The stream represents the opened file.</returns>
        public static async Task<Stream> OpenReadAsync(string file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenRead(file, fileMode);
        }

        /// <summary>
        /// Asynchronously opens the specified file for reading.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file for reading. The stream represents the opened file.</returns>
        public static async Task<Stream> OpenReadAsync(AFile file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenRead(file, fileMode);
        }

        /// <summary>
        /// Opens the specified file for reading and writing.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The stream representing the opened file.</returns>
        public static Stream OpenReadWrite(string file, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            if (Fenrir.FileSystem.FileExists(file))
                return Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist).Open(FileAccess.ReadWrite, fileMode);

            return null;
        }

        /// <summary>
        /// Opens the specified file for reading and writing.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The stream representing the opened file.</returns>
        public static Stream OpenReadWrite(AFile file, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.Open(FileAccess.ReadWrite, fileMode);
        }

        /// <summary>
        /// Asynchronously opens the specified file for reading and writing.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file for reading and writing. The stream represents the opened file.</returns>
        public static async Task<Stream> OpenReadWriteAsync(string file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenReadWrite(file, fileMode);
        }

        /// <summary>
        /// Asynchronously opens the specified file for reading and writing.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file for reading and writing. The stream represents the opened file.</returns>
        public static async Task<Stream> OpenReadWriteAsync(AFile file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenReadWrite(file, fileMode);
        }

        /// <summary>
        /// Opens the specified file for writing.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The stream representing the opened file.</returns>
        public static Stream OpenWrite(string file, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            if (Fenrir.FileSystem.FileExists(file))
                return Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist).Open(FileAccess.Write, fileMode);

            return null;
        }

        /// <summary>
        /// Opens the specified file for writing.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The stream representing the opened file.</returns>
        public static Stream OpenWrite(AFile file, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.Open(FileAccess.Write, fileMode);
        }

        /// <summary>
        /// Asynchronously opens the specified file for writing.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file for writing. The stream represents the opened file.</returns>
        public static async Task<Stream> OpenWriteAsync(string file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenWrite(file, fileMode);
        }

        /// <summary>
        /// Asynchronously opens the specified file for writing.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Stream task to open the file for writing. The stream represents the opened file.</returns>
        public static async Task<Stream> OpenWriteAsync(AFile file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenWrite(file, fileMode);
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a byte array.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A byte array representing the contents of the file.</returns>
        public static byte[] ReadAllBytes(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            var output = new byte[0];
            if (Fenrir.FileSystem.FileExists(file))
            {
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.Normal))
                {
                    if (!f.IsOpen)
                        f.Open(FileAccess.Read, FileMode.Open);

                    long length = f.Stream.Length;
                    long position = 0;
                    output = new byte[length];
                    var tmp = new byte[0];
                    while (length > 0)
                    {
                        var tmpLength = (int)(Int32.MaxValue - length);
                        if (tmpLength < 0)
                            tmpLength = (int)length;
                        tmp = new byte[tmpLength];

                        f.Stream.Read(tmp, 0, tmpLength);
                        for (int i = 0; i < tmpLength; i++)
                            output[position++] = tmp[i];

                        length -= tmpLength;
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a byte array.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A byte array representing the contents of the file.</returns>
        public static byte[] ReadAllBytes(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            var output = new byte[0];

            if (!file.IsOpen)
                file.Open(FileAccess.Read, FileMode.Open);

            long length = file.Stream.Length;
            long position = 0;
            output = new byte[length];
            var tmp = new byte[0];
            while (length > 0)
            {
                var tmpLength = (int)(Int32.MaxValue - length);
                if (tmpLength < 0)
                    tmpLength = (int)length;
                tmp = new byte[tmpLength];

                file.Stream.Read(tmp, 0, tmpLength);
                for (int i = 0; i < tmpLength; i++)
                    output[position++] = tmp[i];

                length -= tmpLength;
            }

            return output;
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a byte array.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A byte array task to read the file. The byte array represents the contents of the file.</returns>
        public static async Task<byte[]> ReadAllBytesAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadAllBytes(file);
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a byte array.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A byte array task to read the file. The byte array represents the contents of the file.</returns>
        public static async Task<byte[]> ReadAllBytesAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadAllBytes(file);
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a string array, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A string array representing the contents of the file.</returns>
        public static string[] ReadAllLines(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            var output = new string[0];
            if (Fenrir.FileSystem.FileExists(file))
            {
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.Normal))
                    output = f.ReadAllLines();
            }

            return output;
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a string array, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A string array representing the contents of the file.</returns>
        public static string[] ReadAllLines(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.ReadAllLines();
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a string array, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string array task to read all lines in the file. The string array represents the contents of the file.</returns>
        public static async Task<string[]> ReadAllLinesAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadAllLines(file);
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a string array, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string array task to read all lines in the file. The string array represents the contents of the file.</returns>
        public static async Task<string[]> ReadAllLinesAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadAllLines(file);
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a string.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A string representing the contents of the file.</returns>
        public static string ReadAllText(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            var output = String.Empty;
            if (Fenrir.FileSystem.FileExists(file))
            {
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.Normal))
                    output = f.ReadAll();
            }

            return output;
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a string.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A string representing the contents of the file.</returns>
        public static string ReadAllText(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.ReadAll();
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a string.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string task to read the file. The string represents the contents of the file.</returns>
        public static async Task<string> ReadAllTextAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadAllText(file);
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a string.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string task to read the file. The string represents the contents of the file.</returns>
        public static async Task<string> ReadAllTextAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadAllText(file);
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a string enumberable, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A string enumberable representing the contents of the file.</returns>
        public static IEnumerable<string> ReadLines(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            var output = new string[0];
            if (Fenrir.FileSystem.FileExists(file))
            {
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.Normal))
                    output = f.ReadAllLines();
            }

            return output;
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a string enumberable, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A string enumberable representing the contents of the file.</returns>
        public static IEnumerable<string> ReadLines(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return file.ReadAllLines();
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a string enumberable, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string enumberable task to read all lines in the file. The string enumberable represents the contents of the file.</returns>
        public static async Task<IEnumerable<string>> ReadLinesAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadLines(file);
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a string enumberable, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string enumberable task to read all lines in the file. The string enumberable represents the contents of the file.</returns>
        public static async Task<IEnumerable<string>> ReadLinesAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadLines(file);
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a string list, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A string list representing the contents of the file.</returns>
        public static List<string> ReadLinesList(string file)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));

            var output = new string[0];
            if (Fenrir.FileSystem.FileExists(file))
            {
                using (var f = Fenrir.FileSystem.OpenFile(file, OpenMode.Normal))
                    output = f.ReadAllLines();
            }

            return new List<string>(output);
        }

        /// <summary>
        /// Reads the entire file and returns the contents as a string list, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>A string list representing the contents of the file.</returns>
        public static List<string> ReadLinesList(AFile file)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));

            return new List<string>(file.ReadAllLines());
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a string list, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string list task to read all lines in the file. The string list represents the contents of the file.</returns>
        public static async Task<List<string>> ReadLinesListAsync(string file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadLinesList(file);
        }

        /// <summary>
        /// Asynchronously reads the entire file and returns the contents as a string list, with each item representing a different line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A string list task to read all lines in the file. The string list represents the contents of the file.</returns>
        public static async Task<List<string>> ReadLinesListAsync(AFile file, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return ReadLinesList(file);
        }

        /// <summary>
        /// Replaces the contents of the destination file (if it exists) with the contents of the source file, making a backup of the original destination file (if it exists). If the file doesn't exist, this copies the source file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="overwriteBackup">if set to <c>true</c>, will overwrite the backup file if it already exists.</param>
        /// <returns>Whether the replace was successful or not.</returns>
        public static bool Replace(string source, string destination, string backup, bool overwriteBackup = true)
        {
            Exceptions.NotNullOrEmptyException(source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));
            Exceptions.NotNullOrEmptyException(backup, nameof(backup));

            if (Fenrir.FileSystem.FileExists(source))
            {
                Copy(destination, backup, overwriteBackup);

                return Copy(source, destination, true);
            }

            return false;
        }

        /// <summary>
        /// Replaces the contents of the destination file (if it exists) with the contents of the source file, making a backup of the original destination file (if it exists). If the file doesn't exist, this copies the source file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="overwriteBackup">if set to <c>true</c>, will overwrite the backup file if it already exists.</param>
        /// <returns>Whether the replace was successful or not.</returns>
        public static bool Replace(AFile source, string destination, string backup, bool overwriteBackup = true)
        {
            Exceptions.NotNullException<AFile> (source, nameof(source));
            Exceptions.NotNullOrEmptyException(destination, nameof(destination));
            Exceptions.NotNullOrEmptyException(backup, nameof(backup));
            
            Copy(destination, backup, overwriteBackup);

            return Copy(source, destination, true);
        }

        /// <summary>
        /// Replaces the contents of the destination file (if it exists) with the contents of the source file, making a backup of the original destination file (if it exists). If the file doesn't exist, this copies the source file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="overwriteBackup">if set to <c>true</c>, will overwrite the backup file if it already exists.</param>
        /// <returns>Whether the replace was successful or not.</returns>
        public static bool Replace(AFile source, AFile destination, string backup, bool overwriteBackup = true)
        {
            Exceptions.NotNullException<AFile>(source, nameof(source));
            Exceptions.NotNullException<AFile>(destination, nameof(destination));
            Exceptions.NotNullOrEmptyException(backup, nameof(backup));

            Copy(destination, backup, overwriteBackup);

            return Copy(source, destination, true);
        }

        /// <summary>
        /// Replaces the contents of the destination file (if it exists) with the contents of the source file, making a backup of the original destination file (if it exists). If the file doesn't exist, this copies the source file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="overwriteBackup">if set to <c>true</c>, will overwrite the backup file if it already exists.</param>
        /// <returns>Whether the replace was successful or not.</returns>
        public static bool Replace(AFile source, AFile destination, AFile backup, bool overwriteBackup = true)
        {
            Exceptions.NotNullException<AFile>(source, nameof(source));
            Exceptions.NotNullException<AFile>(destination, nameof(destination));
            Exceptions.NotNullException<AFile>(backup, nameof(backup));

            Copy(destination, backup, overwriteBackup);

            return Copy(source, destination, true);
        }

        /// <summary>
        /// Asynchronously replaces the contents of the destination file (if it exists) with the contents of the source file, making a backup of the original destination file (if it exists). If the file doesn't exist, this copies the source file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="overwriteBackup">if set to <c>true</c>, will overwrite the backup file if it already exists.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to do the replace. The boolean represents whether the replace was successful or not.</returns>
        public static async Task<bool> ReplaceAsync(string source, string destination, string backup, bool overwriteBackup = true, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Replace(source, destination, backup, overwriteBackup);
        }

        /// <summary>
        /// Asynchronously replaces the contents of the destination file (if it exists) with the contents of the source file, making a backup of the original destination file (if it exists). If the file doesn't exist, this copies the source file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="overwriteBackup">if set to <c>true</c>, will overwrite the backup file if it already exists.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to do the replace. The boolean represents whether the replace was successful or not.</returns>
        public static async Task<bool> ReplaceAsync(AFile source, string destination, string backup, bool overwriteBackup = true, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Replace(source, destination, backup, overwriteBackup);
        }

        /// <summary>
        /// Asynchronously replaces the contents of the destination file (if it exists) with the contents of the source file, making a backup of the original destination file (if it exists). If the file doesn't exist, this copies the source file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="overwriteBackup">if set to <c>true</c>, will overwrite the backup file if it already exists.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to do the replace. The boolean represents whether the replace was successful or not.</returns>
        public static async Task<bool> ReplaceAsync(AFile source, AFile destination, string backup, bool overwriteBackup = true, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Replace(source, destination, backup, overwriteBackup);
        }

        /// <summary>
        /// Asynchronously replaces the contents of the destination file (if it exists) with the contents of the source file, making a backup of the original destination file (if it exists). If the file doesn't exist, this copies the source file to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="overwriteBackup">if set to <c>true</c>, will overwrite the backup file if it already exists.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to do the replace. The boolean represents whether the replace was successful or not.</returns>
        public static async Task<bool> ReplaceAsync(AFile source, AFile destination, AFile backup, bool overwriteBackup = true, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Replace(source, destination, backup, overwriteBackup);
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllBytes(string file, byte[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));
            Exceptions.NotNullException<byte[]>(contents, nameof(contents));

            if (Fenrir.FileSystem.FileExists(file))
            {
                var f = Fenrir.FileSystem.CreateFile(file, FileCollisionOption.FailIfExists);

                return f.WriteAll(Convert.ToString(contents), writeMode);
            }
            else
            {
                var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist);

                return f.WriteAll(Convert.ToString(contents), writeMode);
            }
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllBytes(AFile file, byte[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));
            Exceptions.NotNullException<byte[]>(contents, nameof(contents));
            
            return file.WriteAll(Convert.ToString(contents), writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write contents to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllBytesAsync(string file, byte[] contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllBytes(file, contents, writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write contents to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllBytesAsync(AFile file, byte[] contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllBytes(file, contents, writeMode);
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllLines(string file, string[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));
            Exceptions.NotNullException<string[]>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            if (Fenrir.FileSystem.FileExists(file))
            {
                var f = Fenrir.FileSystem.CreateFile(file, FileCollisionOption.FailIfExists);

                return f.WriteAll(str.ToString(), writeMode);
            }
            else
            {
                var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist);

                return f.WriteAll(str.ToString(), writeMode);
            }
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllLines(AFile file, string[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));
            Exceptions.NotNullException<string[]>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            return file.WriteAll(str.ToString(), writeMode);
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllLines(string file, IEnumerable<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));
            Exceptions.NotNullException<IEnumerable<string>>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            if (Fenrir.FileSystem.FileExists(file))
            {
                var f = Fenrir.FileSystem.CreateFile(file, FileCollisionOption.FailIfExists);

                return f.WriteAll(str.ToString(), writeMode);
            }
            else
            {
                var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist);

                return f.WriteAll(str.ToString(), writeMode);
            }
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllLines(AFile file, IEnumerable<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));
            Exceptions.NotNullException<IEnumerable<string>>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            return file.WriteAll(str.ToString(), writeMode);
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllLines(string file, List<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));
            Exceptions.NotNullException<List<string>>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            if (Fenrir.FileSystem.FileExists(file))
            {
                var f = Fenrir.FileSystem.CreateFile(file, FileCollisionOption.FailIfExists);

                return f.WriteAll(str.ToString(), writeMode);
            }
            else
            {
                var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist);

                return f.WriteAll(str.ToString(), writeMode);
            }
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllLines(AFile file, List<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));
            Exceptions.NotNullException<List<string>>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            return file.WriteAll(str.ToString(), writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllLinesAsync(string file, string[] contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllLines(file, contents, writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllLinesAsync(AFile file, string[] contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllLines(file, contents, writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllLinesAsync(string file, IEnumerable<string> contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllLines(file, contents, writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllLinesAsync(AFile file, IEnumerable<string> contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllLines(file, contents, writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllLinesAsync(string file, List<string> contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllLines(file, contents, writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllLinesAsync(AFile file, List<string> contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllLines(file, contents, writeMode);
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllText(string file, string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullOrEmptyException(file, nameof(file));
            Exceptions.NotNullException<string>(contents, nameof(contents));

            if (Fenrir.FileSystem.FileExists(file))
            {
                var f = Fenrir.FileSystem.CreateFile(file, FileCollisionOption.FailIfExists);

                return f.WriteAll(contents, writeMode);
            }
            else
            {
                var f = Fenrir.FileSystem.OpenFile(file, OpenMode.FailIfDoesNotExist);

                return f.WriteAll(contents, writeMode);
            }
        }

        /// <summary>
        /// Writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns>Whether the contents were written or not.</returns>
        public static bool WriteAllText(AFile file, string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullException<AFile>(file, nameof(file));
            Exceptions.NotNullException<string>(contents, nameof(contents));

            return file.WriteAll(contents, writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllTextAsync(string file, string contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllText(file, contents, writeMode);
        }

        /// <summary>
        /// Asynchronously writes content to the file. If the file does not exist, this method will create the file, write to the file, and then close the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A boolean task to write to the file. The boolean represents whether the contents were written or not.</returns>
        public static async Task<bool> WriteAllTextAsync(AFile file, string contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return WriteAllText(file, contents, writeMode);
        }

        #endregion Public Methods
    }
}