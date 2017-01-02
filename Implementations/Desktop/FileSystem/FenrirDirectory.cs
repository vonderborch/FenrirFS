// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FenrirDirectory.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="FenrirDirectory.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the FenrirDirectory class, an implementation of a the FSDirectory class.
// </summary>
//
// Changelog: 
//            - 2.0.0 (01-01-2017) - Beta version. Fixed issue creating directories/files and specifying OpenIfExists.
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using IO = System.IO;

namespace FenrirFS
{
    /// <summary>
    /// Class FenrirDirectory.
    /// </summary>
    /// <seealso cref="FenrirFS.FSDirectory" />
    public class FenrirDirectory : FSDirectory
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FenrirDirectory"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FenrirDirectory(string path) : base(path)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FenrirDirectory"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        public FenrirDirectory(string path, string name) : base(path, name)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether [directory exists].
        /// </summary>
        /// <value><c>true</c> if [directory exists]; otherwise, <c>false</c>.</value>
        public override bool Exists
        {
            get { return IO.Directory.Exists(this); }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Copies the directory to the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override FSDirectory Copy(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            if (Exists)
            {
                Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

                switch (collisionOption)
                {
                    case DirectoryCollisionOption.OpenIfExists:
                        return FS.GetDirectory(destination, OpenMode.ThrowIfDoesNotExist);

                    case DirectoryCollisionOption.FailIfExists:
                        return null;

                    case DirectoryCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, false);
                        break;

                    case DirectoryCollisionOption.ReplaceExisting:
                        if (IO.Directory.Exists(destination))
                            IO.Directory.Delete(destination, true);
                        break;

                    case DirectoryCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A folder with the new path [{destination}] already exists!");
                }

                IO.Directory.CreateDirectory(destination);

                var files = GetFiles();
                for (int i = 0; i < files.Count; i++)
                {
                    try
                    {
                        var newPath = IO.Path.Combine(destination, $"{files[i].Name}{files[i].Extension}");
                        files[i].Copy(newPath, FileCollisionOption.FailIfExists);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Exception while copying a sub-directory!", ex);
                    }
                }

                var directories = GetDirectories();
                for (int i = 0; i < directories.Count; i++)
                {
                    try
                    {
                        var newPath = IO.Path.Combine(destination, directories[i].Name);
                        directories[i].Copy(newPath, DirectoryCollisionOption.FailIfExists);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Exception while copying a sub-directory!", ex);
                    }
                }

                return FS.GetDirectory(destination, OpenMode.ThrowIfDoesNotExist);
            }

            return null;
        }

        /// <summary>
        /// Creates a new file in the directory.
        /// </summary>
        /// <param name="file">The name for the file.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns>A file structure representing the new file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (01-01-2017) - Beta 3 Version. Fixed a bug that would throw an error if OpenIfExists was specified and the file did not exist.
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (!Exists)
                IO.Directory.CreateDirectory(this);

            string newFullPath = IO.Path.Combine(this, file);
            switch (collisionOption)
            {
                case FileCollisionOption.OpenIfExists:
                    return FS.GetFile(newFullPath, OpenMode.CreateIfDoesNotExist);

                case FileCollisionOption.FailIfExists:
                    return null;

                case FileCollisionOption.GenerateUniqueName:
                    newFullPath = IOHelper.GenerateUniquePath(newFullPath, true);
                    break;

                case FileCollisionOption.ReplaceExisting:
                    if (IO.File.Exists(newFullPath))
                        IO.File.Delete(newFullPath);
                    break;

                case FileCollisionOption.ThrowIfExists:
                    throw new IO.IOException($"A file with the new path [{newFullPath}] already exists!");
            }

            return FS.GetFile(newFullPath, OpenMode.CreateIfDoesNotExist);
        }

        /// <summary>
        /// Creates a new directory in the directory.
        /// </summary>
        /// <param name="directory">The name for the directory.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns>A directory structure representing the new directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (01-01-2017) - Beta 3 Version. Fixed a bug that would throw an error if OpenIfExists was specified and the directory did not exist.
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override FSDirectory CreateDirectory(string directory, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(directory, nameof(directory));

            if (!Exists)
                IO.Directory.CreateDirectory(this);

            string newFullPath = IO.Path.Combine(this, directory);
            switch (collisionOption)
            {
                case DirectoryCollisionOption.OpenIfExists:
                    return FS.GetDirectory(newFullPath, OpenMode.CreateIfDoesNotExist);

                case DirectoryCollisionOption.FailIfExists:
                    return null;

                case DirectoryCollisionOption.GenerateUniqueName:
                    newFullPath = IOHelper.GenerateUniquePath(newFullPath, false);
                    break;

                case DirectoryCollisionOption.ReplaceExisting:
                    if (IO.Directory.Exists(newFullPath))
                        IO.Directory.Delete(newFullPath);
                    break;

                case DirectoryCollisionOption.ThrowIfExists:
                    throw new IO.IOException($"A folder with the new path [{newFullPath}] already exists!");
            }

            return FS.GetDirectory(newFullPath, OpenMode.CreateIfDoesNotExist);
        }

        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <returns><c>true</c> if the directory was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Delete()
        {
            if (Exists)
            {
                IO.Directory.Delete(this);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes a file in the directory.
        /// </summary>
        /// <param name="file">The name of the file to delete.</param>
        /// <returns><c>true</c> if the file was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool DeleteFile(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            if (!Exists) return false;

            if (IO.File.Exists(file))
            {
                IO.File.Delete(file);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes a directory in the directory.
        /// </summary>
        /// <param name="directory">The name of the directory to delete.</param>
        /// <returns><c>true</c> if the directory was deleted, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool DeleteDirectory(string directory)
        {
            Validation.NotNullOrWhiteSpaceCheck(directory, nameof(directory));
            if (!Exists) return false;

            if (IO.Directory.Exists(directory))
            {
                IO.Directory.Delete(directory);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the creation time of the directory.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The creation time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetCreationTime(bool useUtc = false)
        {
            return !Exists
                ? DateTime.MinValue
                : useUtc
                    ? IO.Directory.GetCreationTimeUtc(this)
                    : IO.Directory.GetCreationTime(this);
        }

        /// <summary>
        /// Gets the last accessed time of the directory.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last accessed time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetLastAccessedTime(bool useUtc = false)
        {
            return !Exists
                ? DateTime.MinValue
                : useUtc
                    ? IO.Directory.GetLastAccessTimeUtc(this)
                    : IO.Directory.GetLastAccessTime(this);
        }

        /// <summary>
        /// Gets the last modified time of the directory.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last modified time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetLastModifiedTime(bool useUtc = false)
        {
            return !Exists
                ? DateTime.MinValue
                : useUtc
                    ? IO.Directory.GetLastWriteTimeUtc(this)
                    : IO.Directory.GetLastWriteTime(this);
        }

        /// <summary>
        /// Moves the directory to the specified specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Move(string destination, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            if (Exists)
            {
                Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

                switch (collisionOption)
                {
                    case DirectoryCollisionOption.OpenIfExists:
                    case DirectoryCollisionOption.FailIfExists:
                        return false;

                    case DirectoryCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, false);
                        break;

                    case DirectoryCollisionOption.ReplaceExisting:
                        if (IO.Directory.Exists(destination))
                            IO.Directory.Delete(destination);
                        break;

                    case DirectoryCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A folder with the new path [{destination}] already exists!");
                }

                IO.Directory.Move(this, destination);
                FullPath = destination;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Renames the directory to the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option if a file with the new name already exists.</param>
        /// <returns><c>true</c> if the rename was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Rename(string name, DirectoryCollisionOption collisionOption = DirectoryCollisionOption.FailIfExists)
        {
            if (Exists)
            {
                Validation.NotNullOrWhiteSpaceCheck(name, nameof(name));

                string newFullPath = IO.Path.Combine(this, name);
                switch (collisionOption)
                {
                    case DirectoryCollisionOption.OpenIfExists:
                    case DirectoryCollisionOption.FailIfExists:
                        return false;

                    case DirectoryCollisionOption.GenerateUniqueName:
                        newFullPath = IOHelper.GenerateUniquePath(newFullPath, false);
                        break;

                    case DirectoryCollisionOption.ReplaceExisting:
                        if (IO.Directory.Exists(newFullPath))
                            IO.Directory.Delete(newFullPath);
                        break;

                    case DirectoryCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A folder with the new path [{newFullPath}] already exists!");
                }

                IO.Directory.Move(this, newFullPath);
                FullPath = newFullPath;
                return true;
            }

            return false;
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Internal method to get file system entries.
        /// </summary>
        /// <param name="grabFiles">if set to <c>true</c> [grabs files].</param>
        /// <param name="grabDirectories">if set to <c>true</c> [grabs directories].</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>A list of file system entries matching the desired parameters.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        protected override List<FSFileSystemEntry> InternalGetFileSystemEntries(bool grabFiles, bool grabDirectories, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            var filesAndFolders = new List<FSFileSystemEntry>();

            if (Exists)
            {
                // search top directory...
                if (searchOption != SearchOption.SubDirectoriesOnly)
                {
                    if (grabFiles)
                    {
                        foreach (var file in IO.Directory.EnumerateFiles(this))
                        {
                            if (Regex.IsMatch(file, searchPattern))
                                filesAndFolders.Add(new FenrirFile(file));
                        }
                    }
                }

                // return early if we don't need to do anything with any directories...
                if (searchOption == SearchOption.TopDirectoryOnly && !grabDirectories)
                    return filesAndFolders;

                foreach (var folder in IO.Directory.EnumerateDirectories(this))
                {
                    var newFolder = new FenrirDirectory(folder);
                    if (grabDirectories && Regex.IsMatch(folder, searchPattern))
                        filesAndFolders.Add(newFolder);

                    if (searchOption != SearchOption.TopDirectoryOnly)
                    {
                        var result = newFolder.InternalGetFileSystemEntries(grabFiles, grabDirectories, searchPattern, SearchOption.All);

                        for (int i = 0; i < result.Count; i++)
                            filesAndFolders.Add(result[i]);
                    }
                }
            }

            return filesAndFolders;
        }

        #endregion Protected Methods
    }
}