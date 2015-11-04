using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FenrirFS.Desktop
{
    public class FenrirFolder : AFolder
    {
        #region Public Constructors

        public FenrirFolder(string path) : base(path)
        {
        }

        #endregion Public Constructors

        #region Public Methods

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

        public override bool Move(IFolder destinationPath, string destinationName, FolderCollisionOption collisionOption)
        {
            Exceptions.NotNullCheck<IFolder>(destinationPath, nameof(destinationPath));
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

        public override IFolder CopyFolder(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, folder);

            if (Fenrir.FileSystem.FolderExists(path))
            {
                IFolder newFolder = new FenrirFolder(path);
                return newFolder.Copy(destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
            }

            return null;
        }

        public override IFolder CopyFolder(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, folder);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                IFolder newFolder = new FenrirFolder(path);
                return newFolder.Copy(destinationName, folderCollisionOption, fileCollisionOption);
            }

            return null;
        }

        public override IFolder CopyFolder(string folder, IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullCheck<IFolder>(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, folder);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                IFolder newFolder = new FenrirFolder(path);
                return newFolder.Copy(destinationPath.FullPath, destinationName, folderCollisionOption, fileCollisionOption);
            }

            return null;
        }

        public override IFile CopyFile(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, file);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                IFile newFile = new FenrirFile(path);
                return newFile.Copy(destinationPath, destinationName, collisionOption);
            }

            return null;
        }

        public override IFile CopyFile(string file, string destinationName, FileCollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, file);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                IFile newFile = new FenrirFile(path);
                return newFile.Copy(destinationName, collisionOption);
            }

            return null;
        }

        public override IFile CopyFile(string file, IFolder destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            Exceptions.NotNullCheck<IFolder>(destinationPath, nameof(destinationPath));
            Exceptions.NotNullOrEmptyCheck(Name, nameof(destinationName));

            string path = System.IO.Path.Combine(FullPath, file);
            if (Fenrir.FileSystem.FolderExists(path))
            {
                IFile newFile = new FenrirFile(path);
                return newFile.Copy(destinationPath.FullPath, destinationName, collisionOption);
            }

            return null;
        }

        public override IFolder Copy(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
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

        public override IFolder Copy(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
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

        public override IFolder Copy(IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            Exceptions.NotNullCheck<IFolder>(destinationPath, nameof(destinationPath));
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

        public override IFile CreateFile(string name, FileCollisionOption collisionOption)
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

        public override IFolder CreateFolder(string name, FolderCollisionOption collisionOption)
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