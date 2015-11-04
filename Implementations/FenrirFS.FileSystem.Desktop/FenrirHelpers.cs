/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

using System;

namespace FenrirFS.Desktop
{
    /// <summary>
    /// Various helpers for the desktop implementation.
    /// </summary>
    public static class FenrirHelpers
    {
        #region Public Methods

        /// <summary>
        /// Converts a Fenrir.FileAccess object to a System.IO.FileAccess object.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <returns>The file access.</returns>
        /// <exception cref="System.Exception">Unsupported FenrirFS FileAccess!</exception>
        public static System.IO.FileAccess FenrirFileAccessToSystemFileAccess(FileAccess fileAccess)
        {
            switch (fileAccess)
            {
                case FileAccess.Read:
                    return System.IO.FileAccess.Read;

                case FileAccess.Write:
                    return System.IO.FileAccess.Write;

                case FileAccess.ReadWrite:
                    return System.IO.FileAccess.ReadWrite;

                default:
                    throw new Exception("Unsupported FenrirFS FileAccess!");
            }
        }

        /// <summary>
        /// Converts a Fenrir.FileMode object to a System.IO.FileMode object.
        /// </summary>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The file mode.</returns>
        /// <exception cref="System.ArgumentException">Unsupported FenrirFS FileMode!</exception>
        public static System.IO.FileMode FenrirFileModeToSystemFileMode(FileMode fileMode)
        {
            switch (fileMode)
            {
                case FileMode.Append:
                    return System.IO.FileMode.Append;

                case FileMode.Create:
                    return System.IO.FileMode.Create;

                case FileMode.CreateNew:
                    return System.IO.FileMode.CreateNew;

                case FileMode.Open:
                    return System.IO.FileMode.Open;

                case FileMode.OpenOrCreate:
                    return System.IO.FileMode.OpenOrCreate;

                case FileMode.Truncate:
                    return System.IO.FileMode.Truncate;

                default:
                    throw new ArgumentException("Unsupported FenrirFS Filemode!");
            }
        }

        #endregion Public Methods
    }
}