// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FSFileSystemEntry.cs
// Author           : vonderborch
// Created          : 07-13-2016
//
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="FSFileSystemEntry.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the abstract FSFileSystemEntry class.
// </summary>
//
// Changelog:
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
using FenrirFS.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using IO = System.IO;

namespace FenrirFS
{
    /// <summary>
    /// An abstract representation of a File System Entry.
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
        /// Gets the UTC creation time.
        /// </summary>
        /// <value>The UTC creation time.</value>
        public DateTime CreationTimeUtc
        {
            get { return GetCreationTime(true); }
        }

        /// <summary>
        /// Gets a value indicating whether the entry exists or not.
        /// </summary>
        /// <value><c>true</c> if [entry exists]; otherwise, <c>false</c>.</value>
        public abstract bool Exists { get; }

        /// <summary>
        /// Gets the type of the file system entry.
        /// </summary>
        /// <value>The type of the file system entry.</value>
        public FileSystemEntryType FileSystemEntryType { get; protected set; }

        /// <summary>
        /// Gets the full path of the entry.
        /// </summary>
        /// <value>The full path.</value>
        public abstract string FullPath { get; protected set; }

        /// <summary>
        /// Gets the last accessed time.
        /// </summary>
        /// <value>The last accessed time.</value>
        public DateTime LastAccessedTime
        {
            get { return GetLastAccessedTime(false); }
        }

        /// <summary>
        /// Gets the UTC last accessed time.
        /// </summary>
        /// <value>The UTC last accessed time.</value>
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
        /// Gets the UTC last modified time.
        /// </summary>
        /// <value>The UTC last modified time.</value>
        public DateTime LastModifiedTimeUtc
        {
            get { return GetLastModifiedTime(true); }
        }

        /// <summary>
        /// Gets the name of the entry.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the parent folder of the entry.
        /// </summary>
        /// <value>The parent folder.</value>
        public FSDirectory ParentFolder
        {
            get { return FS.GetDirectory(IO.Path.GetDirectoryName(Path)); }
        }

        /// <summary>
        /// Gets the parent folder path of the entry.
        /// </summary>
        /// <value>The parent folder path.</value>
        public string ParentFolderPath
        {
            get { return IO.Path.GetDirectoryName(Path); }
        }

        /// <summary>
        /// Gets the path of the entry.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; protected set; }

        /// <summary>
        /// Gets the root folder of the entry.
        /// </summary>
        /// <value>The root folder.</value>
        public FSDirectory RootFolder
        {
            get { return FS.GetDirectory(IO.Path.GetPathRoot(Path)); }
        }

        /// <summary>
        /// Gets the root folder path of the entry.
        /// </summary>
        /// <value>The root folder path.</value>
        public string RootFolderPath
        {
            get { return IO.Path.GetPathRoot(Path); }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="FSFile"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="file">The item.</param>
        /// <returns>The result of the conversion.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static implicit operator string(FSFileSystemEntry item)
        {
            return item.FullPath;
        }

        /// <summary>
        /// Asynchronously gets the creation time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The creation time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<DateTime> AsyncGetCreationTime(bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetCreationTime(useUtc);
        }

        /// <summary>
        /// Asynchronously gets the last accessed time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The last accessed time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<DateTime> AsyncGetLastAccessedTime(bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetLastAccessedTime(useUtc);
        }

        /// <summary>
        /// Asynchronously gets the last modified time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The last modified time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public async Task<DateTime> AsyncGetLastModifiedTime(bool useUtc = false, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetLastModifiedTime(useUtc);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
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
        /// <returns>The creation time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract DateTime GetCreationTime(bool useUtc = false);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override int GetHashCode()
        {
            return FullPath.GetHashCode();
        }

        /// <summary>
        /// Gets the last accessed time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last accessed time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract DateTime GetLastAccessedTime(bool useUtc = false);

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last modified time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public abstract DateTime GetLastModifiedTime(bool useUtc = false);

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override string ToString()
        {
            return FullPath;
        }

        #endregion Public Methods
    }
}