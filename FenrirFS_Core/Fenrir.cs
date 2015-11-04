/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

namespace FenrirFS
{
    /// <summary>
    /// Provides access to the implementation of <see cref="AFileSystem"/> for the current platform.
    /// </summary>
    public static class Fenrir
    {
        #region Private Fields

        /// <summary>
        /// The AFileSystem instance
        /// </summary>
        private static AFileSystem instance;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Gets the current <see cref="AFileSystem"/>.
        /// </summary>
        /// <value>
        /// The file system.
        /// </value>
        public static AFileSystem FileSystem
        {
            get { return instance ?? (instance = CreateFileSystem()); }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates the correct implementation for the file system, based on the current platform.
        /// </summary>
        /// <returns></returns>
        private static AFileSystem CreateFileSystem()
        {
#if FS
            return new FenrirFS.Desktop.FenrirFileSystem();
#else
            return null;
#endif
        }

        #endregion Private Methods
    }
}