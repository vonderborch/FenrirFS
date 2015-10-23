using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FenrirFS
{
    public class FenrirFolder : AFolder
    {
        #region Public Constructors

        public FenrirFolder(string path) : base(path)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override IFile CreateFile(string name, CollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string path = FullPath;

            switch (collisionOption)
            {
                case CollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FileExists(System.IO.Path.Combine(path, name)))
                        return null;
                    break;

                case CollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFileUniqueName(path, name);
                    break;
            }

            string file = System.IO.Path.Combine(path, name);
            File.Create(file);
            return new FenrirFile(file);
        }

        public override IFolder CreateFolder(string name, CollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string path = FullPath;

            switch (collisionOption)
            {
                case CollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FolderExists(System.IO.Path.Combine(path, name)))
                        return null;
                    break;

                case CollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFolderUniqueName(path, name);
                    break;
            }

            string folder = System.IO.Path.Combine(path, name);
            Directory.CreateDirectory(folder);
            return new FenrirFolder(folder);
        }

        public override bool Delete()
        {
            if (Fenrir.FileSystem.FolderExists(FullPath))
            {
                Directory.Delete(FullPath);
                return true;
            }

            return false;
        }

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

        public override bool FileExists(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);
            return Fenrir.FileSystem.FileExists(fullName);
        }

        public override bool FolderExists(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);
            return Fenrir.FileSystem.FolderExists(fullName);
        }

        public override IFile GetFile(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);
            if (Fenrir.FileSystem.FileExists(fullName))
            {
                return new FenrirFile(fullName);
            }

            return null;
        }

        public override List<string> GetFileNames()
        {
            List<string> files = new List<string>();
            foreach (string file in Directory.EnumerateFiles(FullPath))
                files.Add(file);

            return files;
        }

        public override List<IFile> GetFiles()
        {
            List<IFile> files = new List<IFile>();
            foreach (string file in Directory.EnumerateFiles(FullPath))
                files.Add(new FenrirFile(file));

            return files;
        }

        public override AFolder GetFolder(string name)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string fullName = System.IO.Path.Combine(Path, name);
            if (Fenrir.FileSystem.FolderExists(fullName))
            {
                return new FenrirFolder(fullName);
            }

            return null;
        }

        public override List<string> GetFolderNames()
        {
            List<string> folders = new List<string>();
            foreach (string folder in Directory.EnumerateDirectories(FullPath))
                folders.Add(folder);

            return folders;
        }

        public override List<IFolder> GetFolders()
        {
            List<IFolder> folders = new List<IFolder>();
            foreach (string folder in Directory.EnumerateDirectories(FullPath))
                folders.Add(new FenrirFolder(folder));

            return folders;
        }

        public override IFolder GetParentFolder()
        {
            return new FenrirFolder(Path);
        }

        public override bool Rename(string name, CollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            string path = Path;

            switch (collisionOption)
            {
                case CollisionOption.FailIfExists:
                    if (Fenrir.FileSystem.FileExists(System.IO.Path.Combine(path, name)))
                        return false;
                    break;

                case CollisionOption.GenerateUniqueName:
                    name = Fenrir.FileSystem.GenerateFolderUniqueName(path, name);
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