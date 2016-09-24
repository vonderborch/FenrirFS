using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenrirFS.Static
{
    public static class Directory
    {
        public static bool Copy(string source, string destination, FolderCollisionOption collisionOption = FolderCollisionOption.ThrowIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            if (Exists(source))
                return false;

            var folder = FS.GetDirectory(source, OpenMode.ThrowIfDoesNotExist);
            return folder.Copy(destination, collisionOption) != null;
        }

        public static bool Create(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            return FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist) != null;
        }

        public static FSFolder CreateDirectory(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            return FS.GetDirectory(path, OpenMode.CreateIfDoesNotExist);
        }

        public static bool Delete(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);

            return folder != null
                ? folder.Delete()
                : true;
        }

        public static IEnumerable<FSFolder> EnumerateDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFolders(searchPattern, searchOption)
                : null;
        }

        public static IEnumerable<FSFile> EnumerateFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFiles(searchPattern, searchOption)
                : null;
        }

        public static IEnumerable<FSFileSystemEntry> EnumerateFileSystemEntries(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileSystemEntries(searchPattern, searchOption)
                : null;
        }

        public static IEnumerable<string> EnumerateDirectoryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFolderNames(searchPattern, searchOption)
                : null;
        }

        public static IEnumerable<string> EnumerateFileNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileNames(searchPattern, searchOption)
                : null;
        }

        public static IEnumerable<string> EnumerateFileSystemEntryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileSystemEntryNames(searchPattern, searchOption)
                : null;
        }

        public static bool Exists(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            return FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist) != null;
        }

        public static DateTime GetCreationTime(string path, bool useUTC = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetCreationTime(useUTC)
                : DateTime.MinValue;
        }

        public static DateTime GetLastAccessedTime(string path, bool useUTC = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetLastAccessedTime(useUTC)
                : DateTime.MinValue;
        }

        public static DateTime GetLastModifiedTime(string path, bool useUTC = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetLastModifiedTime(useUTC)
                : DateTime.MinValue;
        }

        public static FSFolder GetCurrentDirectory()
        {
            return FS.GetCurrentDirectory();
        }

        public static string GetCurrentDirectoryFullPath()
        {
            return FS.GetCurrentDirectory();
        }

        public static List<FSFolder> GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFolders(searchPattern, searchOption)
                : null;
        }

        public static List<FSFile> GetFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFiles(searchPattern, searchOption)
                : null;
        }

        public static List<FSFileSystemEntry> GetFileSystemEntries(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileSystemEntries(searchPattern, searchOption)
                : null;
        }

        public static List<string> GetDirectoryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFolderNames(searchPattern, searchOption)
                : null;
        }

        public static List<string> GetFileNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileNames(searchPattern, searchOption)
                : null;
        }

        public static List<string> GetFileSystemEntryNames(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.All)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(searchPattern, nameof(searchPattern));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.GetFileSystemEntryNames(searchPattern, searchOption)
                : null;
        }

        public static FSFolder GetParentFolder(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.ParentFolder
                : null;
        }

        public static string GetParentFolderPath(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.ParentFolderPath
                : null;
        }

        public static FSFolder GetRootFolder(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.RootFolder
                : null;
        }

        public static string GetRootFolderPath(string path)
        {
            Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

            var folder = FS.GetDirectory(path, OpenMode.ReturnNullIfDoesNotExist);
            return folder != null
                ? folder.RootFolderPath
                : null;
        }

        public static List<FSFolder> GetRootFolders()
        {
            return FS.GetRootDirectories();
        }

        public static List<string> GetRootFolderNames()
        {
            var dirs = FS.GetRootDirectories();
            var output = new List<string>();
            for (int i = 0; i < dirs.Count; i++)
                output.Add(dirs[i]);
            return output;
        }

        public static bool Move(string source, string destination, FolderCollisionOption collisionOption = FolderCollisionOption.ThrowIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            if (Exists(source))
                return false;

            var folder = FS.GetDirectory(source, OpenMode.ThrowIfDoesNotExist);
            return folder.Move(destination, collisionOption);
        }
    }
}
