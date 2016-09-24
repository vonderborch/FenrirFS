// ***********************************************************************
// Assembly         : FenrirFS
// Component        : AsyncFile.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="AsyncFile.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the asynchronous File static class.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta Version.
// ***********************************************************************
using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IO = System.IO;

namespace FenrirFS.Static
{
    /// <summary>
    /// Provides asynchronous static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of FileStream objects.
    /// </summary>
    public static class AsyncFile
    {
        #region Public Methods

        /// <summary>
        /// Appends all lines in the enumerable to the file.
        /// </summary>
        /// <param name="file">The file to append to.</param>
        /// <param name="lines">The lines to append.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the append succeeded, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> AppendAllLines(string file, IEnumerable<string> lines, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<IEnumerable<string>>(lines, nameof(lines));

            await Tasks.ScheduleTask(cancellationToken);
            return File.AppendAllLines(file, lines);
        }

        /// <summary>
        /// Appends the contents in the enumerable to the file.
        /// </summary>
        /// <param name="file">The file to append to.</param>
        /// <param name="contents">The contents to append.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the append succeeded, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> AppendAllText(string file, string contents, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.AppendAllText(file, contents);
        }

        /// <summary>
        /// Copies the file at the source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a file at the destination already exists.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> Copy(string source, string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Copy(source, destination, collisionOption);
        }

        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="file">The full path of the file.</param>
        /// <param name="collisionOption">The collision option if the file already exists.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the creation succeeds, <c>false</c> otherwise.</returns>
        /// <exception cref="IO.IOException">File [{file}] already exists.</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> Create(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Create(file, collisionOption);
        }

        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="file">The full path of the file.</param>
        /// <param name="collisionOption">The collision option if the file already exists.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The created file.</returns>
        /// <exception cref="IO.IOException">File [{file}] already exists.</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<FSFile> CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.CreateFile(file, collisionOption);
        }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="file">The file to delete.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the deletion succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> Delete(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Delete(file);
        }

        /// <summary>
        /// Determines whether a file exists at the specified path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the file exists, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> Exists(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Exists(file);
        }

        /// <summary>
        /// Gets the creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The creation time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<DateTime> GetCreationTime(string file, bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.GetCreationTime(file, useUtc);
        }

        /// <summary>
        /// Gets the attributes of the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The attributes of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<FileAttributes> GetFileAttributes(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.GetFileAttributes(file);
        }

        /// <summary>
        /// Gets the last accessed time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The last accessed time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<DateTime> GetLastAccessedTime(string file, bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.GetLastAccessedTime(file, useUtc);
        }

        /// <summary>
        /// Gets the last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The last modified time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<DateTime> GetLastModifiedTime(string file, bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.GetLastModifiedTime(file, useUtc);
        }

        /// <summary>
        /// Moves the file at the specified source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if the destination already exists.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> Move(string source, string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Move(source, destination, collisionOption);
        }

        /// <summary>
        /// Opens a stream for the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileAccess">The file access mode.</param>
        /// <param name="fileMode">The file open mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The stream for the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<IO.Stream> Open(string file, FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Open(file, fileAccess, fileMode);
        }

        /// <summary>
        /// Opens a stream for reading the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file open mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The stream for the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<IO.Stream> OpenRead(string file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.OpenRead(file, fileMode);
        }

        /// <summary>
        /// Opens a stream for reading and writing the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The stream for the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<IO.Stream> OpenReadWrite(string file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.OpenReadWrite(file, fileMode);
        }

        /// <summary>
        /// Opens a stream for writing the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>The stream for the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<IO.Stream> OpenWrite(string file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.OpenWrite(file, fileMode);
        }

        /// <summary>
        /// Reads all content from the file and returns it as a byte array.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A byte array representing the contents of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<byte[]> ReadAllBytes(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadAllBytes(file);
        }

        /// <summary>
        /// Reads all content from a file and returns it as a string array (line-separated).
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A string array representing the contents of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<string[]> ReadAllLines(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadAllLines(file);
        }

        /// <summary>
        /// Reads all content from a file and returns it as a string.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>A string representing the contents of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<string> ReadAllText(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadAllText(file);
        }

        /// <summary>
        /// Reads lines from a file and returns them as a enumerable.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>An enumerable representing the contents of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<IEnumerable<string>> ReadLines(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadLines(file);
        }

        /// <summary>
        /// Reads lines from a file and returns them as a list.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns>An list representing the contents of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<List<string>> ReadLinesList(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadLinesList(file);
        }

        /// <summary>
        /// Replaces the file at the destination with the specified source, optionally creating a backup file of the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="createBackup">if set to <c>true</c> [creates a backup].</param>
        /// <param name="overwriteBackup">if set to <c>true</c> [overwrite backup].</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the replacement succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> Replace(string source, string destination, string backup, bool createBackup = true, bool overwriteBackup = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Replace(source, destination, backup, overwriteBackup);
        }

        /// <summary>
        /// Writes the contents represented by the byte array to the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the write succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> WriteAllBytes(string file, byte[] contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<byte[]>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.WriteAllBytes(file, contents, writeMode);
        }

        /// <summary>
        /// Writes the contents represented by the string array to the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the write succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> WriteAllLines(string file, string[] contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string[]>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.WriteAllLines(file, contents, writeMode);
        }

        /// <summary>
        /// Writes the contents represented by the string enumerable to the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the write succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> WriteAllLines(string file, IEnumerable<string> contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<IEnumerable<string>>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.WriteAllLines(file, contents, writeMode);
        }

        /// <summary>
        /// Writes the contents represented by the string list to the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the write succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> WriteAllLines(string file, List<string> contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<List<string>>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.WriteAllLines(file, contents, writeMode);
        }

        /// <summary>
        /// Writes the contents represented by the string to the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="cancellationToken">The cancellation token, defaults to null.</param>
        /// <returns><c>true</c> if the write succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static async Task<bool> WriteAllText(string file, string contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.WriteAllText(file, contents, writeMode);
        }

        #endregion Public Methods
    }
}