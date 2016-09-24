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

        public static async Task<bool> AppendAllLines(string file, IEnumerable<string> lines, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<IEnumerable<string>>(lines, nameof(lines));

            await Tasks.ScheduleTask(cancellationToken);
            return File.AppendAllLines(file, lines);
        }

        public static async Task<bool> AppendAllText(string file, string contents, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.AppendAllText(file, contents);
        }

        public static async Task<bool> Copy(string source, string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Copy(source, destination, collisionOption);
        }

        public static async Task<bool> Create(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Create(file, collisionOption);
        }

        public static async Task<FSFile> CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.CreateFile(file, collisionOption);
        }

        public static async Task<bool> Delete(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Delete(file);
        }

        public static async Task<bool> Exists(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Exists(file);
        }

        public static async Task<DateTime> GetCreationTime(string file, bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.GetCreationTime(file, useUtc);
        }

        public static async Task<FileAttributes> GetFileAttributes(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.GetFileAttributes(file);
        }

        public static async Task<DateTime> GetLastAccessedTime(string file, bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.GetLastAccessedTime(file, useUtc);
        }

        public static async Task<DateTime> GetLastModifiedTime(string file, bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.GetLastModifiedTime(file, useUtc);
        }

        public static async Task<bool> Move(string source, string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Move(source, destination, collisionOption);
        }

        public static async Task<IO.Stream> Open(string file, FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Open(file, fileAccess, fileMode);
        }

        public static async Task<IO.Stream> OpenRead(string file, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.OpenRead(file, fileMode);
        }

        public static async Task<IO.Stream> OpenReadWrite(string file, WriteMode writeMode = WriteMode.Truncate, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.OpenReadWrite(file, writeMode, fileMode);
        }

        public static async Task<IO.Stream> OpenWrite(string file, WriteMode writeMode = WriteMode.Truncate, FileMode fileMode = FileMode.OpenOrCreate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.OpenWrite(file, writeMode, fileMode);
        }

        public static async Task<byte[]> ReadAllBytes(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadAllBytes(file);
        }

        public static async Task<string[]> ReadAllLines(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadAllLines(file);
        }

        public static async Task<string> ReadAllText(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadAllText(file);
        }

        public static async Task<IEnumerable<string>> ReadLine(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadLine(file);
        }

        public static async Task<List<string>> ReadLinesList(string file, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            await Tasks.ScheduleTask(cancellationToken);
            return File.ReadLinesList(file);
        }

        public static async Task<bool> Replace(string source, string destination, string backup, bool overwriteBackup = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return File.Replace(source, destination, backup, overwriteBackup);
        }

        public static async Task<bool> WriteAllBytes(string file, byte[] contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<byte[]>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.WriteAllBytes(file, contents, writeMode);
        }

        public static async Task<bool> WriteAllLines(string file, string[] contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string[]>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.WriteAllLines(file, contents, writeMode);
        }

        public static async Task<bool> WriteAllLines(string file, IEnumerable<string> contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<IEnumerable<string>>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.WriteAllLines(file, contents, writeMode);
        }

        public static async Task<bool> WriteAllLines(string file, List<string> contents, WriteMode writeMode = WriteMode.Truncate, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<List<string>>(contents, nameof(contents));

            await Tasks.ScheduleTask(cancellationToken);
            return File.WriteAllLines(file, contents, writeMode);
        }

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