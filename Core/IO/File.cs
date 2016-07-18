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
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        file = GenerateUniqueFileName(file);
                        break;
                    case FileCollisionOption.OpenIfExists:
                        return true;
                    case FileCollisionOption.ReplaceExisting:
                        Delete(file);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"File [{file}] already exists!");
                }
            }

            return FS.GetFile(file, OpenMode.CreateIfDoesNotExist) != null;
        }

        public static FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (Exists(file))
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        return null;
                    case FileCollisionOption.GenerateUniqueName:
                        file = GenerateUniqueFileName(file);
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        Delete(file);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"File [{file}] already exists!");
                }
            }

            return FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
        }

        public static bool Delete(string file)
        {
            return FS.GetFile(file, OpenMode.CreateIfDoesNotExist).Delete();
        }

        public static bool Exists(string  file)
        {
            return FS.GetFile(file, OpenMode.ReturnNullIfDoesNotExist) != null;
        }

        public static DateTime GetCreationTime(string file, bool useUtc = false)
        {
            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetCreationTime(useUtc);
        }

        public static DateTime GetLastAccessedTime(string file, bool useUtc = false)
        {
            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetLastAccessedTime(useUtc);
        }

        public static DateTime GetLastModifiedTime(string file, bool useUtc = false)
        {
            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetLastModifiedTime(useUtc);
        }

        public static Encoding GetFileEncoding(string file)
        {
            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetEncoding();
        }

        public static FileAttributes GetFileAttributes(string file)
        {
            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetFileAttributes();
        }

        public static bool Move(string source, string destination, bool overwrite = false)
        {
            if (Exists(destination))
            {
                if (overwrite)
                    Delete(destination);
                else
                    return false;
            }

            return FS.GetFile(source, OpenMode.ThrowIfDoesNotExist).Move(destination, FileCollisionOption.ThrowIfExists);
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











        public static string GenerateUniqueFileName(string file, int maxNumberOfTries = 9, RenameMode renameMode = RenameMode.TimeStamp)
        {
            string path = IO.Path.GetDirectoryName(file);
            string extension = IO.Path.GetExtension(file);
            string name = IO.Path.GetFileNameWithoutExtension(file);

            int tries = 0;
            while (Exists(file) && tries < maxNumberOfTries)
            {
                string newName;
                switch (renameMode)
                {
                    case RenameMode.Integer:
                        newName = $"{name} - {tries}";
                        break;
                    case RenameMode.TimeStamp:
                        newName = $"{name} - {DateTime.Now.Ticks}";
                        break;
                    default:
                        newName = name;
                        break;
                }

                file = IO.Path.Combine(path, $"{newName}.{extension}");
            }

            return file;
        }
    }
}
