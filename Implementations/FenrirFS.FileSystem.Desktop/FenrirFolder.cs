/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace FenrirFS.Desktop
{
    /// <summary>
    /// An implementation of an AFolder.
    /// </summary>
    public class FenrirFolder : AFolder
    {
        #region Private Fields

        private DateTime lastCalculatedSize = DateTime.MinValue;
        private long size = 0;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FenrirFolder"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FenrirFolder(string path) : base(path)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        public override DateTime CreationTime
        {
            get { return Directory.GetCreationTime(FullPath); }
        }

        /// <summary>
        /// Gets the UTC creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        public override DateTime CreationTimeUtc
        {
            get { return Directory.GetCreationTimeUtc(FullPath); }
        }


        /// <summary>
        /// Gets the last accessed time.
        /// </summary>
        /// <value>
        /// The last accessed time.
        /// </value>
        public override DateTime LastAccessedTime
        {
            get { return Directory.GetLastAccessTime(FullPath); }
        }

        /// <summary>
        /// Gets the UTC last accessed time.
        /// </summary>
        /// <value>
        /// The last accessed time.
        /// </value>
        public override DateTime LastAccessedTimeUtc
        {
            get { return Directory.GetLastAccessTimeUtc(FullPath); }
        }

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        /// <value>
        /// The last modified time.
        /// </value>
        public override DateTime LastModifiedTime
        {
            get { return Directory.GetLastWriteTime(FullPath); }
        }

        /// <summary>
        /// Gets the UTC last modified time.
        /// </summary>
        /// <value>
        /// The last modified time.
        /// </value>
        public override DateTime LastModifiedTimeUtc
        {
            get { return Directory.GetLastWriteTimeUtc(FullPath); }
        }

        /// <summary>
        /// Gets the size of the folder, in bytes.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public override long Size
        {
            get
            {
                if (lastCalculatedSize == DateTime.MinValue ||
                    lastCalculatedSize != LastModifiedTime)
                {
                    var dir = new DirectoryInfo(FullPath);
                    size = 0;

                    // add the sizes of each of the files in the dir to the dir's total size
                    foreach (var file in dir.EnumerateFiles())
                        size += file.Length;

                    // add the sizes of each of the folders in the dir to the dir's total size
                    foreach (var folder in dir.EnumerateDirectories())
                    {
                        var fenrirFolder = new FenrirFolder(folder.FullName);
                        size += fenrirFolder.Size;
                        fenrirFolder.Dispose();
                    }

                    lastCalculatedSize = LastModifiedTime;
                }

                return size;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Copies the folder to the destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the new folder.</returns>
        public override AFolder Copy(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string destination = destinationPath;
            string path = System.IO.Path.Combine(destination, destinationName);

            switch (folderCollisionOption)
            {
                case FolderCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FolderExists(path))
                        return null;
                    break;

                case FolderCollisionOption.GenerateUniqueName:
                    destinationName = Fenrir.FileSystem.GenerateFileUniqueName(destination, destinationName);
                    path = System.IO.Path.Combine(destination, destinationName);
                    break;

                case FolderCollisionOption.OpenIfExists:
                    if (Fenrir.FileSystem.FolderExists(path))
                        return new FenrirFolder(path);
                    break;
            }

            Directory.CreateDirectory(path);

            List<string> files = this.GetFileNames();
            foreach (string str in files)
                CopyFile(str, destinationPath, str, fileCollisionOption);

            List<string> folders = this.GetFolderNames();
            foreach (string str in folders)
                CopyFolder(str, destinationPath, str, folderCollisionOption, fileCollisionOption);

            return new FenrirFolder(path);
        }

        /// <summary>
        /// Copies the folder to the destination.
        /// </summary>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the new folder.</returns>
        public override AFolder Copy(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string destination = System.IO.Path.GetDirectoryName(destinationName);
            string name = System.IO.Path.GetFileName(destinationName);
            string path = System.IO.Path.Combine(destination, name);

            switch (folderCollisionOption)
            {
                case FolderCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FolderExists(path))
                        return null;
                    break;

                case FolderCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFileUniqueName(destination, name);
                    path = System.IO.Path.Combine(destination, name);
                    break;

                case FolderCollisionOption.OpenIfExists:
                    if (Fenrir.FileSystem.FolderExists(path))
                        return new FenrirFolder(path);
                    break;
            }

            Directory.CreateDirectory(path);

            List<string> files = this.GetFileNames();
            foreach (string str in files)
                CopyFile(str, destination, str, fileCollisionOption);

            List<string> folders = this.GetFolderNames();
            foreach (string str in folders)
                CopyFolder(str, destination, str, folderCollisionOption, fileCollisionOption);

            return new FenrirFolder(path);
        }

        /// <summary>
        /// Copies the folder to the destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the new folder.</returns>
        public override AFolder Copy(AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullCheck<AFolder>(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string destination = destinationPath.FullPath;
            string path = System.IO.Path.Combine(destination, destinationName);

            switch (folderCollisionOption)
            {
                case FolderCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FolderExists(path))
                        return null;
                    break;

                case FolderCollisionOption.GenerateUniqueName:
                    destinationName = Fenrir.FileSystem.GenerateFileUniqueName(destination, destinationName);
                    path = System.IO.Path.Combine(destination, destinationName);
                    break;

                case FolderCollisionOption.OpenIfExists:
                    if (Fenrir.FileSystem.FolderExists(path))
                        return new FenrirFolder(path);
                    break;
            }

            Directory.CreateDirectory(path);

            List<string> files = this.GetFileNames();
            foreach (string str in files)
                CopyFile(str, destinationPath, str, fileCollisionOption);

            List<string> folders = this.GetFolderNames();
            foreach (string str in folders)
                CopyFolder(str, destinationPath, str, folderCollisionOption, fileCollisionOption);

            return new FenrirFolder(path);
        }

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>An AFile representing the file.</returns>
        public override AFile CopyFile(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, file);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                AFile newFile = new FenrirFile(path);
                return newFile.Copy(destinationPath, destinationName, collisionOption);
            }

            return null;
        }

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>An AFile representing the file.</returns>
        public override AFile CopyFile(string file, string destinationName, FileCollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, file);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                AFile newFile = new FenrirFile(path);
                return newFile.Copy(destinationName, collisionOption);
            }

            return null;
        }

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>An AFile representing the file.</returns>
        public override AFile CopyFile(string file, AFolder destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            Exceptions.NotNullCheck<AFolder>(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, file);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                AFile newFile = new FenrirFile(path);
                return newFile.Copy(destinationPath.FullPath, destinationName, collisionOption);
            }

            return null;
        }

        /// <summary>
        /// Copies a folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the file.</returns>
        public override AFolder CopyFolder(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, folder);

            if (Fenrir.FileSystem.FolderExists(path))
            {
                AFolder newFolder = new FenrirFolder(path);
                return newFolder.Copy(destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
            }

            return null;
        }

        /// <summary>
        /// Copies a folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the file.</returns>
        public override AFolder CopyFolder(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, folder);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                AFolder newFolder = new FenrirFolder(path);
                return newFolder.Copy(destinationName, folderCollisionOption, fileCollisionOption);
            }

            return null;
        }

        /// <summary>
        /// Copies a folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="folderCollisionOption">The folder collision option.</param>
        /// <param name="fileCollisionOption">The file collision option.</param>
        /// <returns>An AFolder representing the file.</returns>
        public override AFolder CopyFolder(string folder, AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullCheck<AFolder>(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, folder);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                AFolder newFolder = new FenrirFolder(path);
                return newFolder.Copy(destinationPath.FullPath, destinationName, folderCollisionOption, fileCollisionOption);
            }

            return null;
        }

        /// <summary>
        /// Creates a file in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>The new file.</returns>
        public override AFile CreateFile(string name, FileCollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string path = FullPath;

            switch (collisionOption)
            {
                case FileCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FileExists(System.IO.Path.Combine(path, name)))
                        return null;
                    break;

                case FileCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFileUniqueName(path, name);
                    break;
            }

            string file = System.IO.Path.Combine(path, name);
            File.Create(file);
            return new FenrirFile(file);
        }

        /// <summary>
        /// Creates a folder in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>The new folder.</returns>
        public override AFolder CreateFolder(string name, FolderCollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string path = FullPath;

            switch (collisionOption)
            {
                case FolderCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FolderExists(System.IO.Path.Combine(path, name)))
                        return null;
                    break;

                case FolderCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFolderUniqueName(path, name);
                    break;
            }

            string folder = System.IO.Path.Combine(path, name);
            Directory.CreateDirectory(folder);
            return new FenrirFolder(folder);
        }

        /// <summary>
        /// Deletes this folder.
        /// </summary>
        /// <returns>Whether this folder was deleted or not.</returns>
        public override bool Delete()
        {
            if (Fenrir.FileSystem.FolderExists(FullPath))
            {
                Directory.Delete(FullPath);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes a file in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Whether the file was deleted or not.</returns>
        public override bool DeleteFile(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);

            if (Fenrir.FileSystem.FileExists(fullName))
            {
                File.Delete(fullName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes a folder in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Whether the folder was deleted or not.</returns>
        public override bool DeleteFolder(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);

            if (Fenrir.FileSystem.FolderExists(fullName))
            {
                Directory.Delete(fullName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a file exists in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Whether the file exists (true) or not (false).</returns>
        public override bool FileExists(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);
            return Fenrir.FileSystem.FileExists(fullName);
        }

        /// <summary>
        /// Checks if a folder exists in this directory.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Whether the folder exists (true) or not (false).</returns>
        public override bool FolderExists(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);
            return Fenrir.FileSystem.FolderExists(fullName);
        }

        /// <summary>
        /// Gets the names of all files in this folder.
        /// </summary>
        /// <returns>A list of all file names in this folder.</returns>
        public override List<string> GetFileNames()
        {
            List<string> files = new List<string>();
            foreach (string file in Directory.EnumerateFiles(FullPath))
                files.Add(file);

            return files;
        }

        /// <summary>
        /// Gets the files in this folder.
        /// </summary>
        /// <returns>A list of all files in this folder.</returns>
        public override List<AFile> GetFiles()
        {
            List<AFile> files = new List<AFile>();
            foreach (string file in Directory.EnumerateFiles(FullPath))
                files.Add(new FenrirFile(file));

            return files;
        }

        /// <summary>
        /// Gets the names of all folders in this folder.
        /// </summary>
        /// <returns>A list of all folders names in this folder.</returns>
        public override List<string> GetFolderNames()
        {
            List<string> folders = new List<string>();
            foreach (string folder in Directory.EnumerateDirectories(FullPath))
                folders.Add(folder);

            return folders;
        }

        /// <summary>
        /// Gets the folders in this folder.
        /// </summary>
        /// <returns>A AFolder list representing all folders in this folder.</returns>
        public override List<AFolder> GetFolders()
        {
            List<AFolder> folders = new List<AFolder>();
            foreach (string folder in Directory.EnumerateDirectories(FullPath))
                folders.Add(new FenrirFolder(folder));

            return folders;
        }

        /// <summary>
        /// Gets the parent folder of this folder.
        /// </summary>
        /// <returns>The parent folder.</returns>
        public override AFolder GetParentFolder()
        {
            return new FenrirFolder(Path);
        }

        /// <summary>
        /// Moves this folder to the specified destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Whether the folder was moved (true) or not (false).</returns>
        public override bool Move(string destinationPath, string destinationName, FolderCollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, destinationName);
            string endPath = System.IO.Path.Combine(destinationPath, destinationName);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                switch (collisionOption)
                {
                    case FolderCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FolderExists(endPath))
                            return false;
                        break;

                    case FolderCollisionOption.GenerateUniqueName:
                        destinationName = Fenrir.FileSystem.GenerateFileUniqueName(destinationPath, destinationName);
                        endPath = System.IO.Path.Combine(destinationPath, destinationName);
                        break;

                    case FolderCollisionOption.OpenIfExists:
                        if (Fenrir.FileSystem.FolderExists(endPath))
                            return false;
                        break;
                }

                Directory.Move(path, endPath);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Moves this folder to the specified destination.
        /// </summary>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Whether the folder was moved (true) or not (false).</returns>
        public override bool Move(string destinationName, FolderCollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string name = System.IO.Path.GetFileName(destinationName);
            string path = System.IO.Path.Combine(FullPath, name);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                switch (collisionOption)
                {
                    case FolderCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FolderExists(destinationName))
                            return false;
                        break;

                    case FolderCollisionOption.GenerateUniqueName:
                        destinationName = Fenrir.FileSystem.GenerateFileUniqueName(destinationName);
                        break;

                    case FolderCollisionOption.OpenIfExists:
                        if (Fenrir.FileSystem.FolderExists(destinationName))
                            return false;
                        break;
                }

                Directory.Move(path, destinationName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves this folder to the specified destination.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Whether the folder was moved (true) or not (false).</returns>
        public override bool Move(AFolder destinationPath, string destinationName, FolderCollisionOption collisionOption)
        {
            Exceptions.NotNullCheck<AFolder>(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, destinationName);
            string endPath = System.IO.Path.Combine(destinationPath.FullPath, destinationName);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                switch (collisionOption)
                {
                    case FolderCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FolderExists(endPath))
                            return false;
                        break;

                    case FolderCollisionOption.GenerateUniqueName:
                        destinationName = Fenrir.FileSystem.GenerateFileUniqueName(destinationPath.FullPath, destinationName);
                        endPath = System.IO.Path.Combine(destinationPath.FullPath, destinationName);
                        break;

                    case FolderCollisionOption.OpenIfExists:
                        if (Fenrir.FileSystem.FolderExists(endPath))
                            return false;
                        break;
                }

                Directory.Move(path, endPath);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The file.</returns>
        public override AFile OpenFile(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);
            if (Fenrir.FileSystem.FileExists(fullName))
            {
                return new FenrirFile(fullName);
            }

            return null;
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The folder.</returns>
        public override AFolder OpenFolder(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);
            if (Fenrir.FileSystem.FolderExists(fullName))
            {
                return new FenrirFolder(fullName);
            }

            return null;
        }

        /// <summary>
        /// Renames the folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns>Whether the folder was renamed (true) or not (false).</returns>
        public override bool Rename(string name, FolderCollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string path = Path;

            switch (collisionOption)
            {
                case FolderCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FolderExists(System.IO.Path.Combine(path, name)))
                        return false;
                    break;

                case FolderCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFolderUniqueName(path, name);
                    break;

                case FolderCollisionOption.OpenIfExists:
                    if (Fenrir.FileSystem.FolderExists(System.IO.Path.Combine(path, name)))
                        return false;
                    break;
            }

            string folder = System.IO.Path.Combine(path, name);
            Directory.Move(FullPath, folder);
            Name = name;
            return true;
        }

        #endregion Public Methods
    }
}