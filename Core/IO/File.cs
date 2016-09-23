using FenrirFS.Helpers;
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
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<IEnumerable<string>>(lines, nameof(lines));

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
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string>(contents, nameof(contents));

            bool result = false;
            using (var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist))
                result = f.WriteAll(contents, WriteMode.Append);

            return result;
        }

        public static bool Copy(string source, string destination, bool overwrite = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            if ((!overwrite && Exists(destination)) || !Exists(source))
                return false;

            bool result = false;
            using (var s = FS.GetFile(source, OpenMode.ThrowIfDoesNotExist))
                result = s.Copy(destination, FileCollisionOption.ThrowIfExists);

            return result;
        }

        public static bool Create(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        file = IOHelper.GenerateUniquePath(file, true);
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
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        return null;
                    case FileCollisionOption.GenerateUniqueName:
                        file = IOHelper.GenerateUniquePath(file, true);
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
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.CreateIfDoesNotExist).Delete();
        }

        public static bool Exists(string  file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ReturnNullIfDoesNotExist) != null;
        }

        public static DateTime GetCreationTime(string file, bool useUtc = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetCreationTime(useUtc);
        }

        public static DateTime GetLastAccessedTime(string file, bool useUtc = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetLastAccessedTime(useUtc);
        }

        public static DateTime GetLastModifiedTime(string file, bool useUtc = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetLastModifiedTime(useUtc);
        }

        public static Encoding GetFileEncoding(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetEncoding();
        }

        public static FileAttributes GetFileAttributes(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetFileAttributes();
        }

        public static bool Move(string source, string destination, bool overwrite = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

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
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            var output = new byte[0];
            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                {
                    if (!f.IsFileOpen)
                        f.Open(FileAccess.Read, FileMode.Open);

                    long length = f.Stream.Length;
                    long position = 0;
                    output = new byte[length];
                    var tmp = new byte[0];
                    while (length > 0)
                    {
                        var tmpLength = (int)(Int32.MaxValue - length);
                        if (tmpLength < 0)
                            tmpLength = (int)length;
                        tmp = new byte[tmpLength];

                        f.Stream.Read(tmp, 0, tmpLength);
                        for (int i = 0; i < tmpLength; i++)
                            output[position++] = tmp[i];

                        length -= tmpLength;
                    }
                }
            }

            return output;
        }

        public static string[] ReadAllLines(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            var output = new string[0];
            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                    output = f.ReadAllLines();
            }

            return output;
        }

        public static string ReadAllText(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            var output = String.Empty;
            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                    output = f.ReadAll();
            }

            return output;
        }

        public static IEnumerable<string> ReadLine(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            
            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                    f.ReadLine();
            }

            return null;
        }

        public static List<string> ReadLinesList(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            var output = new string[0];
            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                    output = f.ReadAllLines();
            }

            return new List<string>(output);
        }

        public static bool Replace(string source, string destination, string backup, bool overwriteBackup = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));
            Validation.NotNullOrWhiteSpaceCheck(backup, nameof(backup));

            if (Exists(source))
            {
                Copy(destination, backup, overwriteBackup);

                return Copy(source, destination, true);
            }

            return false;
        }

        public static bool WriteAllBytes(string file, byte[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<byte[]>(contents, nameof(contents));

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(Convert.ToString(contents), writeMode);
        }

        public static bool WriteAllLines(string file, string[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string[]>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(str.ToString(), writeMode);
        }

        public static bool WriteAllLines(string file, IEnumerable<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<IEnumerable<string>>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(str.ToString(), writeMode);
        }

        public static bool WriteAllLines(string file, List<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<List<string>>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(str.ToString(), writeMode);
        }

        public static bool WriteAllText(string file, string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string>(contents, nameof(contents));

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(contents, writeMode);
        }















        
        public static IO.Stream Open(string file, FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
                return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).Open(fileAccess, fileMode);

            return null;
        }

        public static IO.Stream OpenRead(string file, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
                return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).Open(FileAccess.Read, fileMode);

            return null;
        }

        public static IO.Stream OpenReadWrite(string file, WriteMode writeMode = WriteMode.Truncate, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
                return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).Open(FileAccess.ReadWrite, fileMode);

            return null;
        }

        public static IO.Stream OpenWrite(string file, WriteMode writeMode = WriteMode.Truncate, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
                return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).Open(FileAccess.Write, fileMode);

            return null;
        }






        
    }
}
