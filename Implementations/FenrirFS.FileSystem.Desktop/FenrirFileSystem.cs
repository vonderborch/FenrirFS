/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

using System;
using System.IO;
using System.Text;

namespace FenrirFS.Desktop
{
    /// <summary>
    /// An implementation of an AFileSystem.
    /// </summary>
    public class FenrirFileSystem : AFileSystem
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FenrirFileSystem"/> class.
        /// </summary>
        public FenrirFileSystem()
        {
            DefaultEncoding = Encoding.Unicode;
            StorageLocal = new FenrirFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            StorageRoaming = new FenrirFolder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            StorageUser = new FenrirFolder(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            StorageWorking = new FenrirFolder(Environment.CurrentDirectory);
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the file.</returns>
        public override AFile CreateFile(string path, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileName(path);

            switch (collisionOption)
            {
                case FileCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FileExists(path))
                        return null;
                    break;

                case FileCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFileUniqueName(path);
                    break;
            }

            string file = System.IO.Path.Combine(directory, name);
            File.Create(file).Dispose();
            return new FenrirFile(file);
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the file.</returns>
        public override AFile CreateFile(string directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyCheck(directory, nameof(directory));
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            switch (collisionOption)
            {
                case FileCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FileExists(System.IO.Path.Combine(directory, name)))
                        return null;
                    break;

                case FileCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFileUniqueName(directory, name);
                    break;
            }

            string file = System.IO.Path.Combine(directory, name);
            File.Create(file).Dispose();
            return new FenrirFile(file);
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the file.</returns>
        public override AFile CreateFile(AFolder directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullCheck<AFolder>(directory, nameof(directory));
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            switch (collisionOption)
            {
                case FileCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FileExists(System.IO.Path.Combine(directory.FullPath, name)))
                        return null;
                    break;

                case FileCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFileUniqueName(directory.FullPath, name);
                    break;
            }

            string file = System.IO.Path.Combine(directory.FullPath, name);
            File.Create(file).Dispose();
            return new FenrirFile(file);
        }

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFolder representing the folder.</returns>
        public override AFolder CreateFolder(string path, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileName(path);

            switch (collisionOption)
            {
                case FileCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FolderExists(System.IO.Path.Combine(directory, name)))
                        return null;
                    break;

                case FileCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFileUniqueName(directory, name);
                    break;
            }

            string folder = System.IO.Path.Combine(directory, name);
            Directory.CreateDirectory(folder);
            return new FenrirFolder(folder);
        }

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFolder representing the folder.</returns>
        public override AFolder CreateFolder(string directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyCheck(directory, nameof(directory));
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            switch (collisionOption)
            {
                case FileCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FolderExists(System.IO.Path.Combine(directory, name)))
                        return null;
                    break;

                case FileCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFileUniqueName(directory, name);
                    break;
            }

            string folder = System.IO.Path.Combine(directory, name);
            Directory.CreateDirectory(folder);
            return new FenrirFolder(folder);
        }

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFolder representing the folder.</returns>
        public override AFolder CreateFolder(AFolder directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullCheck<AFolder>(directory, nameof(directory));
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            switch (collisionOption)
            {
                case FileCollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FolderExists(System.IO.Path.Combine(directory.FullPath, name)))
                        return null;
                    break;

                case FileCollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFileUniqueName(directory.FullPath, name);
                    break;
            }

            string folder = System.IO.Path.Combine(directory.FullPath, name);
            Directory.CreateDirectory(folder);
            return new FenrirFolder(folder);
        }

        /// <summary>
        /// Checks whether the item at the specified path exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The results of the existence check.</returns>
        public override ExistenceCheckResult Exists(string path)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            bool fileExists = File.Exists(path);
            bool folderExists = Directory.Exists(path);

            if (fileExists && folderExists)
                return ExistenceCheckResult.FileAndFolderExists;
            else if (fileExists)
                return ExistenceCheckResult.FileExists;
            else if (folderExists)
                return ExistenceCheckResult.FolderExists;

            return ExistenceCheckResult.NotFound;
        }

        /// <summary>
        /// Checks if a file exists at the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Whether the file exists (true) or not (false).</returns>
        public override bool FileExists(string path)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            return File.Exists(path);
        }

        /// <summary>
        /// Checks if a folder exists at the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Whether the folder exists (true) or not (false).</returns>
        public override bool FolderExists(string path)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            return Directory.Exists(path);
        }

        /// <summary>
        /// Gets a file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The file.</returns>
        public override AFile OpenFile(string path, OpenMode openMode)
        {
            if (!Fenrir.FileSystem.FileExists(path))
            {
                switch (openMode)
                {
                    case OpenMode.Normal:
                        Fenrir.FileSystem.CreateFile(path, FileCollisionOption.FailIfExists);
                        break;

                    case OpenMode.FailIfDoesNotExist:
                        throw new FileNotFoundException(String.Format("File {0} does not exist!", path));
                }
            }

            return new FenrirFile(path);
        }

        /// <summary>
        /// Gets a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="file">The file.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The file.</returns>
        public override AFile OpenFile(string directory, string file, OpenMode openMode)
        {
            string path = FSHelpers.CombinePath(directory, file);

            if (!Fenrir.FileSystem.FileExists(path))
            {
                switch (openMode)
                {
                    case OpenMode.Normal:
                        Fenrir.FileSystem.CreateFile(path, FileCollisionOption.FailIfExists);
                        break;

                    case OpenMode.FailIfDoesNotExist:
                        throw new FileNotFoundException(String.Format("File {0} does not exist!", path));
                }
            }

            return new FenrirFile(path);
        }

        /// <summary>
        /// Gets a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="file">The file.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The file.</returns>
        public override AFile OpenFile(AFolder directory, string file, OpenMode openMode)
        {
            string path = FSHelpers.CombinePath(directory.ToString(), file);

            if (!Fenrir.FileSystem.FileExists(path))
            {
                switch (openMode)
                {
                    case OpenMode.Normal:
                        Fenrir.FileSystem.CreateFile(path, FileCollisionOption.FailIfExists);
                        break;

                    case OpenMode.FailIfDoesNotExist:
                        throw new FileNotFoundException(String.Format("File {0} does not exist!", path));
                }
            }

            return new FenrirFile(path);
        }

        /// <summary>
        /// Gets a folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The folder.</returns>
        public override AFolder OpenFolder(string path, OpenMode openMode)
        {
            if (!Fenrir.FileSystem.FolderExists(path))
            {
                switch (openMode)
                {
                    case OpenMode.Normal:
                        Fenrir.FileSystem.CreateFolder(path, FileCollisionOption.FailIfExists);
                        break;

                    case OpenMode.FailIfDoesNotExist:
                        throw new FileNotFoundException(String.Format("Folder {0} does not exist!", path));
                }
            }

            return new FenrirFolder(path);
        }

        /// <summary>
        /// Gets a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The folder.</returns>
        public override AFolder OpenFolder(string directory, string folder, OpenMode openMode)
        {
            string path = FSHelpers.CombinePath(directory, folder);

            if (!Fenrir.FileSystem.FolderExists(path))
            {
                switch (openMode)
                {
                    case OpenMode.Normal:
                        Fenrir.FileSystem.CreateFolder(path, FileCollisionOption.FailIfExists);
                        break;

                    case OpenMode.FailIfDoesNotExist:
                        throw new FileNotFoundException(String.Format("Folder {0} does not exist!", path));
                }
            }

            return new FenrirFolder(path);
        }

        /// <summary>
        /// Gets a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The folder.</returns>
        public override AFolder OpenFolder(AFolder directory, string folder, OpenMode openMode)
        {
            string path = FSHelpers.CombinePath(directory.ToString(), folder);

            if (!Fenrir.FileSystem.FolderExists(path))
            {
                switch (openMode)
                {
                    case OpenMode.Normal:
                        Fenrir.FileSystem.CreateFolder(path, FileCollisionOption.FailIfExists);
                        break;

                    case OpenMode.FailIfDoesNotExist:
                        throw new FileNotFoundException(String.Format("Folder {0} does not exist!", path));
                }
            }

            return new FenrirFolder(path);
        }

        /// <summary>
        /// Sets the user's storage folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Whether the folder was set (true) or not (false).</returns>
        public override bool SetStorageUser(string path)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            if (Fenrir.FileSystem.FolderExists(path))
            {
                StorageUser = new FenrirFolder(path);
                return true;
            }

            return false;
        }

        #endregion Public Methods
    }
}