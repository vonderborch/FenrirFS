namespace FenrirFS
{
    /// <summary>
    /// Provides access to the implementation of <see cref="IFileSystem"/> for the current platform.
    /// </summary>
    public static class Fenrir
    {
        #region Private Fields

        /// <summary>
        /// The IFileSystem instance
        /// </summary>
        private static IFileSystem instance;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Gets the current <see cref="IFileSystem"/>.
        /// </summary>
        /// <value>
        /// The file system.
        /// </value>
        public static IFileSystem FileSystem
        {
            get { return instance ?? (instance = CreateFileSystem()); }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Creates the correct implementation for the file system, based on the current platform.
        /// </summary>
        /// <returns></returns>
        private static IFileSystem CreateFileSystem()
        {
#if UNIVERSAL || WINDOWS_PHONE
            return null;
#elif DESKTOP
            return Desktop.ImplementationFileSystem();
#else
            return null;
#endif
        }

        #endregion Private Methods
    }
}