using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO = System.IO;

namespace FenrirFS.Static
{
    public static class File
    {
        public static bool AppendAllLines(string file, IEnumerable<string> lines)
        {
            bool result = false;
            using (var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist))
            {
                var str = new StringBuilder();
                foreach (var line in lines)
                    str.AppendLine(line);
                result = f.WriteAll(str.ToString(), WriteMode.Append);
            }

            return result;
        }

        public static bool AppendAllText(string file, string contents)
        {
            bool result = false;
            using (var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist))
                result = f.WriteAll(contents, WriteMode.Append);

            return result;
        }

        public static bool Copy(string source, string destination, bool overwrite = false)
        {
            if ((!overwrite && Exists(destination)) || !Exists(source))
                return false;

            bool result = false;
            using (var s = FS.GetFile(source, OpenMode.ThrowIfDoesNotExist))
                result = s.Copy(destination, FileCollisionOption.ThrowIfExists);

            return result;
        }

        public static bool Create(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (Exists(file))
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        break;
                    case FileCollisionOption.GenerateUniqueName:
                        break;
                    case FileCollisionOption.OpenIfExists:
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        break;
                }
            }

            return FS.GetFile(file, OpenMode.CreateIfDoesNotExist) != null;
        }

        public static FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            return null;
        }

        public static bool Delete(string file)
        {
            return false;
        }

        public static bool Exists(string  file)
        {
            return false;
        }

        public static DateTime GetCreationTime(string file, bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        public static DateTime GetLastAccessedTime(string file, bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        public static DateTime GetLastModifiedTime(string file, bool useUtc = false)
        {
            return DateTime.MinValue;
        }

        public static FileAttributes GetFileAttributes(string file)
        {
            return FileAttributes.Offline;
        }

        public static bool Move(string source, string destination, bool overwrite = false)
        {
            return false;
        }

        public static byte[] ReadAllBytes(string file)
        {
            return null;
        }

        public static string[] ReadAllLines(string file)
        {
            return null;
        }

        public static string ReadAllText(string file)
        {
            return null;
        }

        public static IEnumerable<string> ReadLine(string file)
        {
            return null;
        }

        public static List<string> ReadLinesList(string file)
        {
            return null;
        }

        public static bool Replace(string source, string destination, string backup, bool overwriteBackup = false)
        {
            return false;
        }

        public static bool WriteAllBytes(string file, byte[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            return false;
        }

        public static bool WriteAllLines(string file, string[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            return false;
        }

        public static bool WriteAllLines(string file, IEnumerable<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            return false;
        }

        public static bool WriteAllLines(string file, List<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            return false;
        }

        public static bool WriteAllText(string file, string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            return false;
        }















        
        public static IO.Stream Open(string file, FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            return null;
        }

        public static IO.Stream OpenRead(string file, FileMode fileMode = FileMode.OpenOrCreate)
        {
            return null;
        }

        public static IO.Stream OpenReadWrite(string file, WriteMode writeMode = WriteMode.Truncate, FileMode fileMode = FileMode.OpenOrCreate)
        {
            return null;
        }

        public static IO.Stream OpenWrite(string file, WriteMode writeMode = WriteMode.Truncate, FileMode fileMode = FileMode.OpenOrCreate)
        {
            return null;
        }


    }
}
