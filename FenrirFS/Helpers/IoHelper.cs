// ***********************************************************************
// Assembly         : FenrirFS
// Component        : IoHelper.cs
// Author           : Christian Webber
// Created          : 2016-09-22
//
// Version          : 3.0.0
// Last Modified By : Christian Webber
// Last Modified On : 2020-02-27
// ***********************************************************************
// <copyright file="IoHelper.cs">
//     Copyright © 2020
// </copyright>
// <summary>
//     Defines various IO helper functions.
// </summary>
//
// Changelog:
//            - 3.0.0 (2020-02-27) - Re-organized and renamed. Improved GenerateUniquePath method.
//            - 2.0.0 (2016-09-24) - Beta version.
// ***********************************************************************

using System;
using FenrirFS.IO;

namespace FenrirFS.Helpers
{
    /// <summary>
    /// Defines static functions to help with IO tasks.
    /// </summary>
    public static class IoHelper
    {
        /// <summary>
        /// Attempts to generate a unique full path name, if required.
        /// </summary>
        /// <param name="path">The path to generate a unique name for.</param>
        /// <param name="type">The type of the File System Entry.</param>
        /// <param name="renameMode">The rename mode.</param>
        /// <param name="maxAttempts">The maximum number of attempts.</param>
        /// <returns>A unique path, or null if we failed to generate a unique path.</returns>
        public static string GenerateUniquePath(string path, FileSystemEntryType type, RenameMode renameMode = RenameMode.TimeStampTicks, int maxAttempts = 10)
        {
            // end early if we don't have to do anything...
            if (Path.Exists(path) == ExistenceCheckResult.None)
                return path;

            // split up the path...
            var directory = IO.Path.GetDirectoryName(path);
            var name = IO.Path.GetFileNameWithoutExtension(path);
            var extension = type == FileSystemEntryType.File || type == FileSystemEntryType.SymbolicFile
                ? IO.Path.GetExtension(path)
                : string.Empty;

            // attempt to find a new name for the file system entry...
            for (var i = 0; i < maxAttempts; i++)
            {
                var newPath = InternalGetUniquePath(directory, name, extension, i, renameMode, type);
                if (Path.Exists(newPath) == ExistenceCheckResult.None)
                    return newPath;
            }

            return null;
        }

        /// <summary>
        /// Determines whether the combination of file access and file modes are valid or not
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns><c>true</c> if the combination is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidModeCombination(FileAccess fileAccess, FileMode fileMode)
        {
            if (fileMode == FileMode.None)
                return false;

            switch (fileAccess)
            {
                case FileAccess.None:
                    return false;

                case FileAccess.Read:
                    switch (fileMode)
                    {
                        case FileMode.Truncate:
                        case FileMode.Append: return false;
                        default: return true;
                    }
                case FileAccess.ReadWrite:
                    return true;

                case FileAccess.Write:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Generates a new path for the specified existing path, based on the rename mode specified.
        /// </summary>
        /// <param name="directory">The directory path.</param>
        /// <param name="name">The base name</param>
        /// <param name="extension">The extension, if applicable.</param>
        /// <param name="attempt">The attempt number that we are at.</param>
        /// <param name="renameMode">The rename mode specified.</param>
        /// <param name="type">The type of the File System Entry.</param>
        /// <returns>The next unique path to attempt.</returns>
        private static string InternalGetUniquePath(string directory, string name, string extension, int attempt, RenameMode renameMode, FileSystemEntryType type)
        {
            var uniquePart = string.Empty;
            var format = renameMode == RenameMode.TimeStamp || renameMode == RenameMode.Integer
                ? string.Empty
                : type == FileSystemEntryType.File || type == FileSystemEntryType.SymbolicFile
                    ? FenrirConstants.Instance.FileNameUniquePathTimestampFormat
                    : FenrirConstants.Instance.DirectoryNameUniquePathTimestampFormat;

            switch (renameMode)
            {
                case RenameMode.Integer:
                    uniquePart = attempt.ToString();
                    break;

                case RenameMode.TimeStamp:
                    uniquePart = DateTime.Now.ToString(format);
                    break;

                case RenameMode.TimeStampUtc:
                    uniquePart = DateTime.UtcNow.ToString(format);
                    break;

                case RenameMode.TimeStampTicks:
                    uniquePart = DateTime.Now.Ticks.ToString();
                    break;
            }

            return IO.Path.CombineFileSystemEntryPath(string.Format(FenrirConstants.Instance.UniquePathNameFormat, name, uniquePart), extension, directory);
        }
    }
}
