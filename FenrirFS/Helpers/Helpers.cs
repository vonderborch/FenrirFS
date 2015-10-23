using System;
using System.Globalization;

namespace FenrirFS.Helpers
{
    /// <summary>
    /// Various helpers.
    /// </summary>
    public static class Helpers
    {
        #region Public Methods

        /// <summary>
        /// Gets the directory separator for the current platform.
        /// </summary>
        /// <value>The directory separator.</value>
        public static char DirectorySeparator
        {
            get
            {
#if NETFX_CORE
                return '\\';
#elif PORTABLE
                throw Exceptions.NotImplementedInCurrentFileSystemException();
#else
                return Path.DirectorySeparatorChar;
#endif
            }
        }

        /// <summary>
        /// Formats a string with the arguments.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>A formatted string.</returns>
        public static string Format(string format, params object[] arguments)
        {
            return string.Format(CultureInfo.CurrentCulture, format, arguments);
        }

        /// <summary>
        /// Returns whether a specified FileAccess and FileMode combination is valid.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>Whether the FileAccess and FileMode combination is valid.</returns>
        public static bool IsValidFileModeFileAccessOptions(FileAccess fileAccess, FileMode fileMode)
        {
            switch (fileAccess)
            {
                case FileAccess.None:
                    return false;

                case FileAccess.Read:
                    switch (fileMode)
                    {
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
        /// Gets the line separator for the current platform.
        /// </summary>
        /// <value>
        /// The line separator.
        /// </value>
        public static string LineSeparator
        {
            get { return Environment.NewLine; }
        }

        #endregion Public Methods
    }
}