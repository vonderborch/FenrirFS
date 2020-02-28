using System;
using System.Collections.Generic;
using System.Text;
using FenrirFS.FileSystem;

namespace FenrirFS.IO
{
    class Path
    {
        public static string GetDirectoryName(string path)
        {
            throw new NotImplementedException();
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            throw new NotImplementedException();
        }

        public static string GetExtension(string path)
        {
            throw new NotImplementedException();
        }

        public static string Combine(params string[] pathStrings)
        {
            throw new NotImplementedException();
        }

        public static string CombineFileSystemEntryPath(string name, string extension, params string[] pathStrings)
        {
            Array.Resize(ref pathStrings, pathStrings.Length + 1);
            extension = extension[0] == '.' ? extension : $".{extension}";
            pathStrings[pathStrings.Length - 1] = $"{name}{extension}";
            return Combine(pathStrings);
        }

        public static ExistenceCheckResult Exists(string newPath)
        {
            throw new NotImplementedException();
        }

        public static FSFile GetFile(string fullPath, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            throw new NotImplementedException();
        }

        public static FSFile GetFile(string directoryPath, string fileName, string extension, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            throw new NotImplementedException();
        }

        public static FSDirectory GetDirectory(string fullPath, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            throw new NotImplementedException();
        }

        public static FSDirectory GetDirectory(string directoryPath, string directoryName, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            throw new NotImplementedException();
        }

        public static FSDirectory GetCurrentWorkingDirectory()
        {
            throw new NotImplementedException();
        }

        public static List<FSDirectory> GetSystemRootDirectories()
        {
            throw new NotImplementedException();
        }
    }
}
