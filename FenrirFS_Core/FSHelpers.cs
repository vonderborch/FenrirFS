using System;
using System.Globalization;
using System.Text;

namespace FenrirFS
{
    /// <summary>
    /// Various helper functions.
    /// </summary>
    public static class FSHelpers
    {
        #region Public Properties

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
                return System.IO.Path.DirectorySeparatorChar;
#endif
            }
        }

        /// <summary>
        /// Gets the line separator for the current platform.
        /// </summary>
        /// <value>The line separator.</value>
        public static string LineSeparator
        {
            get { return Environment.NewLine; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Combines string parts into a path.
        /// </summary>
        /// <param name="parts">A string to combine.</param>
        /// <returns>A combined path string.</returns>
        public static string CombinePath(params string[] parts)
        {
            if (parts.Length > 0)
            {
                StringBuilder str = new StringBuilder();

                int length = parts.Length - 1;
                for (int i = 0; i < parts.Length; i++)
                {
                    str.Append(parts[i]);
                    if (i < length)
                    {
                        str.Append(DirectorySeparator);
                    }
                }

                return str.ToString();
            }

            return "";
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

        #endregion Public Methods
    }
}