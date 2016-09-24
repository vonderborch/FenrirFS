using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS.Static
{
    public static class AsyncDirectory
    {
        public static async Task<bool> Copy(string source, string destination, FolderCollisionOption collisionOption = FolderCollisionOption.ThrowIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Copy(source, destination, collisionOption);
        }

        public static async Task<bool> Create(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Create(path);
        }

        public static async Task<FSFolder> CreateDirectory(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.CreateDirectory(path);
        }

        public static async Task<bool> Delete(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Delete(path);
        }

        public static async Task<IEnumerable<FSFolder>> EnumerateDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateDirectories(path, searchPattern, searchOption);
        }

        public static async Task<IEnumerable<FSFile>> EnumerateFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
        }

        public static async Task<IEnumerable<FSFileSystemEntry>> EnumerateFileSystemEntries(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateFileSystemEntries(path, searchPattern, searchOption);
        }

        public static async Task<IEnumerable<string>> EnumerateDirectoryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateDirectoryNames(path, searchPattern, searchOption);
        }

        public static async Task<IEnumerable<string>> EnumerateFileNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateFileNames(path, searchPattern, searchOption);
        }

        public static async Task<IEnumerable<string>> EnumerateFileSystemEntryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.EnumerateFileSystemEntryNames(path, searchPattern, searchOption);
        }

        public static async Task<ExistenceCheckResult> Exists(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Exists(path);
        }

        public static async Task<DateTime> GetCreationTime(string path, bool useUTC = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetCreationTime(path, useUTC);
        }

        public static async Task<DateTime> GetLastAccessedTime(string path, bool useUTC = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetLastAccessedTime(path, useUTC);
        }

        public static async Task<DateTime> GetLastModifiedTime(string path, bool useUTC = false, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetLastModifiedTime(path, useUTC);
        }

        public static async Task<FSFolder> GetCurrentDirectory(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetCurrentDirectory();
        }

        public static async Task<string> GetCurrentDirectoryFullPath(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetCurrentDirectoryFullPath();
        }

        public static async Task<List<FSFolder>> GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetDirectories(path, searchPattern, searchOption);
        }

        public static async Task<List<FSFile>> GetFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        public static async Task<List<FSFileSystemEntry>> GetFileSystemEntries(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetFileSystemEntries(path, searchPattern, searchOption);
        }

        public static async Task<List<string>> GetDirectoryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetDirectoryNames(path, searchPattern, searchOption);
        }

        public static async Task<List<string>> GetFileNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetFileNames(path, searchPattern, searchOption);
        }

        public static async Task<List<string>> GetFileSystemEntryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetFileSystemEntryNames(path, searchPattern, searchOption);
        }

        public static async Task<FSFolder> GetParentFolder(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetParentFolder(path);
        }

        public static async Task<string> GetParentFolderPath(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetParentFolderPath(path);
        }

        public static async Task<FSFolder> GetRootFolder(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetRootFolder(path);
        }

        public static async Task<string> GetRootFolderPath(string path, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetRootFolderPath(path);
        }

        public static async Task<List<FSFolder>> GetRootFolders(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetRootFolders();
        }

        public static async Task<List<string>> GetRootFolderNames(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Directory.GetRootFolderNames();
        }

        public static async Task<bool> Move(string source, string destination, FolderCollisionOption collisionOption = FolderCollisionOption.ThrowIfExists, CancellationToken? cancellationToken = null)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            await Tasks.ScheduleTask(cancellationToken);
            return Directory.Copy(source, destination, collisionOption);
        }
    }
}
