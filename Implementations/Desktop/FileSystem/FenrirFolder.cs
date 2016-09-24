using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IO = System.IO;

namespace FenrirFS
{
    public class FenrirFolder : FSFolder
    {
        public FenrirFolder(string path) : base(path) { }
        public FenrirFolder(string path, string name) : base(path, name) { }

        public override bool Exists
        {
            get { return IO.Directory.Exists(this); }
        }

        public override FSFolder Copy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            if (Exists)
            {
                Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

                switch (collisionOption)
                {
                    case FolderCollisionOption.OpenIfExists:
                        return FS.GetDirectory(destination, OpenMode.ThrowIfDoesNotExist);
                    case FolderCollisionOption.FailIfExists:
                        return null;
                    case FolderCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, false);
                        break;
                    case FolderCollisionOption.ReplaceExisting:
                        if (IO.Directory.Exists(destination))
                            IO.Directory.Delete(destination, true);
                        break;
                    case FolderCollisionOption.ThrowIfExists:
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

                var directories = GetFolders();
                for (int i = 0; i < directories.Count; i++)
                {
                    try
                    {
                        var newPath = IO.Path.Combine(destination, directories[i].Name);
                        directories[i].Copy(newPath, FolderCollisionOption.FailIfExists);
                    }
                    catch(Exception ex)
                    {
                        throw new Exception("Exception while copying a sub-directory!", ex);
                    }
                }

                return FS.GetDirectory(destination, OpenMode.ThrowIfDoesNotExist);
            }

            return null;
        }

        public override FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (!Exists)
                IO.Directory.CreateDirectory(this);

            string newFullPath = IO.Path.Combine(Path, file);
            switch (collisionOption)
            {
                case FileCollisionOption.OpenIfExists:
                    return FS.GetFile(newFullPath, OpenMode.ThrowIfDoesNotExist);
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

        public override FSFolder CreateFolder(string folder, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(folder, nameof(folder));

            if (!Exists)
                IO.Directory.CreateDirectory(this);

            string newFullPath = IO.Path.Combine(Path, folder);
            switch (collisionOption)
            {
                case FolderCollisionOption.OpenIfExists:
                    return FS.GetDirectory(newFullPath, OpenMode.ThrowIfDoesNotExist);
                case FolderCollisionOption.FailIfExists:
                    return null;
                case FolderCollisionOption.GenerateUniqueName:
                    newFullPath = IOHelper.GenerateUniquePath(newFullPath, false);
                    break;
                case FolderCollisionOption.ReplaceExisting:
                    if (IO.Directory.Exists(newFullPath))
                        IO.Directory.Delete(newFullPath);
                    break;
                case FolderCollisionOption.ThrowIfExists:
                    throw new IO.IOException($"A folder with the new path [{newFullPath}] already exists!");
            }

            return FS.GetDirectory(newFullPath, OpenMode.CreateIfDoesNotExist);
        }

        public override bool Delete()
        {
            if (Exists)
            {
                IO.Directory.Delete(this);
                return true;
            }
            return false;
        }

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

        public override bool DeleteFolder(string folder)
        {
            Validation.NotNullOrWhiteSpaceCheck(folder, nameof(folder));
            if (!Exists) return false;

            if (IO.Directory.Exists(folder))
            {
                IO.Directory.Delete(folder);
                return true;
            }
            return false;
        }

        public override DateTime GetCreationTime(bool useUtc = false)
        {
            return !Exists
                ? DateTime.MinValue
                : useUtc
                    ? IO.Directory.GetCreationTimeUtc(this)
                    : IO.Directory.GetCreationTime(this);
        }

        public override DateTime GetLastAccessedTime(bool useUtc = false)
        {
            return !Exists
                ? DateTime.MinValue
                : useUtc
                    ? IO.Directory.GetLastAccessTimeUtc(this)
                    : IO.Directory.GetLastAccessTime(this);
        }

        public override DateTime GetLastModifiedTime(bool useUtc = false)
        {
            return !Exists
                ? DateTime.MinValue
                : useUtc
                    ? IO.Directory.GetLastWriteTimeUtc(this)
                    : IO.Directory.GetLastWriteTime(this);
        }

        public override bool Move(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            if (Exists)
            {
                Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

                switch (collisionOption)
                {
                    case FolderCollisionOption.OpenIfExists:
                    case FolderCollisionOption.FailIfExists:
                        return false;
                    case FolderCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, false);
                        break;
                    case FolderCollisionOption.ReplaceExisting:
                        if (IO.Directory.Exists(destination))
                            IO.Directory.Delete(destination);
                        break;
                    case FolderCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A folder with the new path [{destination}] already exists!");
                }

                IO.Directory.Move(this, destination);
                FullPath = destination;
                return true;
            }

            return false;
        }

        public override bool Rename(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            if (Exists)
            {
                Validation.NotNullOrWhiteSpaceCheck(name, nameof(name));

                string newFullPath = IO.Path.Combine(Path, name);
                switch (collisionOption)
                {
                    case FolderCollisionOption.OpenIfExists:
                    case FolderCollisionOption.FailIfExists:
                        return false;
                    case FolderCollisionOption.GenerateUniqueName:
                        newFullPath = IOHelper.GenerateUniquePath(newFullPath, false);
                        break;
                    case FolderCollisionOption.ReplaceExisting:
                        if (IO.Directory.Exists(newFullPath))
                            IO.Directory.Delete(newFullPath);
                        break;
                    case FolderCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A folder with the new path [{newFullPath}] already exists!");
                }

                IO.Directory.Move(this, newFullPath);
                FullPath = newFullPath;
                return true;
            }

            return false;
        }

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
                    var newFolder = new FenrirFolder(folder);
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
    }
}
