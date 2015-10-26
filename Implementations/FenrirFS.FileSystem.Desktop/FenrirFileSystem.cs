using System;
using System.IO;
using System.Text;

namespace FenrirFS.Desktop
{
    public class FenrirFileSystem : AFileSystem
    {
        #region Public Constructors

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

        public override IFile CreateFile(string path, FileCollisionOption collisionOption)
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

        public override IFile CreateFile(string directory, string name, FileCollisionOption collisionOption)
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

        public override IFile CreateFile(IFolder directory, string name, FileCollisionOption collisionOption)
        {
            Exceptions.NotNullCheck<IFolder>(directory, nameof(directory));
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

        public override IFolder CreateFolder(string path, FileCollisionOption collisionOption)
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

        public override IFolder CreateFolder(string directory, string name, FileCollisionOption collisionOption)
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

        public override IFolder CreateFolder(IFolder directory, string name, FileCollisionOption collisionOption)
        {
            Exceptions.NotNullCheck<IFolder>(directory, nameof(directory));
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

        public override bool FileExists(string path)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            return File.Exists(path);
        }

        public override bool FolderExists(string path)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            return Directory.Exists(path);
        }

        public override IFile GetFileFromPath(string path)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            if (Fenrir.FileSystem.FileExists(path))
            {
                return new FenrirFile(path);
            }

            return null;
        }

        public override IFolder GetFolderFromPath(string path)
        {
            Exceptions.NotNullOrEmptyCheck(path, nameof(path));

            if (Fenrir.FileSystem.FolderExists(path))
            {
                return new FenrirFolder(path);
            }

            return null;
        }

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