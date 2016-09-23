// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FSFileSystemEntry.cs
// Author           : vonderborch
// Created          : 07-13-2016
//
// Version          : 1.0.0
// Last Modified By : vonderborch
// Last Modified On : 07-13-2016
// ***********************************************************************
// <copyright file="FSFileSystemEntry.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the abstract FSFileSystemEntry class.
// </summary>
//
// Changelog:
//            - 1.0.0 (07-13-2016) - Initial version created.
// ***********************************************************************
using FenrirFS.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using IO = System.IO;

namespace FenrirFS
{
    /// <summary>
    /// Class FSFileSystemEntry.
    /// </summary>
    public abstract class FSFileSystemEntry
    {
        #region Public Properties

        /// <summary>
        /// Gets the creation time.
        /// </summary>
        /// <value>The creation time.</value>
        public DateTime CreationTime
        {
            get { return GetCreationTime(false); }
        }

        /// <summary>
        /// Gets the creation time UTC.
        /// </summary>
        /// <value>The creation time UTC.</value>
        public DateTime CreationTimeUtc
        {
            get { return GetCreationTime(true); }
        }

        /// <summary>
        /// Gets a value indicating whether [file exists].
        /// </summary>
        /// <value><c>true</c> if [file exists]; otherwise, <c>false</c>.</value>
        public abstract bool Exists { get; }

        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        /// <value>The full path.</value>
        public abstract string FullPath
        {
            get; protected set;
        }

        /// <summary>
        /// Gets the last accessed time.
        /// </summary>
        /// <value>The last accessed time.</value>
        public DateTime LastAccessedTime
        {
            get { return GetLastAccessedTime(false); }
        }

        /// <summary>
        /// Gets the last accessed time UTC.
        /// </summary>
        /// <value>The last accessed time UTC.</value>
        public DateTime LastAccessedTimeUtc
        {
            get { return GetLastAccessedTime(true); }
        }

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        /// <value>The last modified time.</value>
        public DateTime LastModifiedTime
        {
            get { return GetLastModifiedTime(false); }
        }

        /// <summary>
        /// Gets the last modified time UTC.
        /// </summary>
        /// <value>The last modified time UTC.</value>
        public DateTime LastModifiedTimeUtc
        {
            get { return GetLastModifiedTime(true); }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the parent folder.
        /// </summary>
        /// <value>The parent folder.</value>
        public FSFolder ParentFolder
        {
            get { return FS.GetFolder(IO.Path.GetDirectoryName(Path)); }
        }

        /// <summary>
        /// Gets the parent folder path.
        /// </summary>
        /// <value>The parent folder path.</value>
        public string ParentFolderPath
        {
            get { return IO.Path.GetDirectoryName(Path); }
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; protected set; }

        /// <summary>
        /// Gets the root folder.
        /// </summary>
        /// <value>The root folder.</value>
        public FSFolder RootFolder
        {
            get { return FS.GetFolder(IO.Path.GetPathRoot(Path)); }
        }

        /// <summary>
        /// Gets the root folder path.
        /// </summary>
        /// <value>The root folder path.</value>
        public string RootFolderPath
        {
            get { return IO.Path.GetPathRoot(Path); }
        }

        #endregion Public Properties

        #region Public Methods
        
        /// <summary>
        /// Asynchronouses the get creation time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;DateTime&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<DateTime> AsyncGetCreationTime(bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetCreationTime(useUtc);
        }

        /// <summary>
        /// Asynchronouses the get last accessed time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;DateTime&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<DateTime> AsyncGetLastAccessedTime(bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetLastAccessedTime(useUtc);
        }

        /// <summary>
        /// Asynchronouses the get last modified time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;DateTime&gt;.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public async Task<DateTime> AsyncGetLastModifiedTime(bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetLastModifiedTime(useUtc);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="FSFile"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="file">The item.</param>
        /// <returns>The result of the conversion.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public static implicit operator string(FSFileSystemEntry item)
        {
            return item.FullPath;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public override bool Equals(object obj)
        {
            return obj != null
                ? FullPath == obj.ToString()
                : false;
        }

        /// <summary>
        /// Gets the creation time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>DateTime.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract DateTime GetCreationTime(bool useUtc = false);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override int GetHashCode()
        {
            return FullPath.GetHashCode();
        }

        /// <summary>
        /// Gets the last accessed time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>DateTime.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract DateTime GetLastAccessedTime(bool useUtc = false);

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>DateTime.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public abstract DateTime GetLastModifiedTime(bool useUtc = false);

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-13-2016) - Initial version.
        public override string ToString()
        {
            return FullPath;
        }

        #endregion Public Methods
    }
}