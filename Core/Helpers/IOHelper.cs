// ***********************************************************************
// Assembly         : FenrirFS
// Component        : IOHelper.cs
// Author           : vonderborch
// Created          : 09-22-2016
//
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="IOHelper.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines IO helper functions.
// </summary>
//
// Changelog:
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
using System;
using IO = System.IO;

namespace FenrirFS.Helpers
{
    /// <summary>
    /// Defines static functions to help with IO tasks.
    /// </summary>
    public static class IOHelper
    {
        #region Public Methods

        /// <summary>
        /// Attempts to generate a unique full path name, if required.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="isFile">if set to <c>true</c> [is file].</param>
        /// <param name="renameMode">The rename mode.</param>
        /// <param name="maxAttempts">The maximum attempts.</param>
        /// <returns>A full path name.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static string GenerateUniquePath(string path, bool isFile, RenameMode renameMode = RenameMode.TimeStampTicks, int maxAttempts = 10)
        {
            // end early if we don't have to do anything...
            if (FS.Exists(path) == ExistenceCheckResult.None)
                return path;

            // split up the path...
            var directory = IO.Path.GetDirectoryName(path);
            var name = IO.Path.GetFileNameWithoutExtension(path);
            var extension = isFile ? IO.Path.GetExtension(path) : "";

            // depending on the rename mode...
            if (renameMode == RenameMode.Integer)
            {
                for (int i = 0; i < maxAttempts; i++)
                {
                    var newPath = IO.Path.Combine(directory, $"{name}-{i}{extension}");
                    if (FS.Exists(newPath) == ExistenceCheckResult.None)
                        return newPath;
                }
            }
            else if (renameMode == RenameMode.TimeStampTicks)
            {
                for (int i = 0; i < maxAttempts; i++)
                {
                    var newPath = IO.Path.Combine(directory, $"{name}-{DateTime.Now.Ticks}{extension}");
                    if (FS.Exists(newPath) == ExistenceCheckResult.None)
                        return newPath;
                }
            }

            // if all else fails, return the original path
            return path;
        }

        /// <summary>
        /// Determines whether the combination of file access and file modes are valid or not
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns><c>true</c> if the combination is valid; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
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

        #endregion Public Methods
    }
}