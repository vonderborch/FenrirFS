using System;
using System.Collections.Generic;
using IO = System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FenrirFS.Helpers;

namespace FenrirFS
{
    public class FenrirFile : FSFile
    {
        public FenrirFile(string path) : base(path) { }
        public FenrirFile(string path, string name, string extension) : base(path, name, extension) { }

        public override bool Exists
        {
            get { return IO.File.Exists(FullPath); }
        }

        public override bool ChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrEmptyCheck(extension, nameof(extension));

            if (!IsFileOpen)
            {
                string newFullPath = IO.Path.Combine(Path, $"{Name}.{extension}");
                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        newFullPath = IOHelper.GenerateUniquePath(newFullPath, true);
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        IO.File.Delete(newFullPath);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{newFullPath}] already exists!");
                }

                if (Exists)
                    IO.File.Move(this, newFullPath);
                FullPath = newFullPath;
                return true;
            }

            return false;
        }

        public override bool Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrEmptyCheck(destination, nameof(destination));

            bool exists = IO.File.Exists(destination);
            if (exists)
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, true);
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        IO.File.Delete(destination);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{destination}] already exists!");
                }
            }

            IO.File.Copy(this, destination);
            return true;
        }

        public override bool Delete()
        {
            if (!IsFileOpen)
            {
                if (Exists)
                {
                    IO.File.Delete(this);
                    return true;
                }
            }

            return false;
        }

        public override DateTime GetCreationTime(bool useUtc = false)
        {
            if (Exists)
            {
                return useUtc
                    ? IO.File.GetCreationTimeUtc(this)
                    : IO.File.GetCreationTime(this);
            }

            return DateTime.MinValue;
        }

        public override Encoding GetEncoding()
        {
            if (Exists)
            {
                using (IO.StreamReader reader = new IO.StreamReader(this, Encoding.UTF8, true))
                {
                    reader.Peek();
                    return reader.CurrentEncoding;
                }
            }

            return Encoding.Default;
        }

        public override FileAttributes GetFileAttributes()
        {
            if (Exists)
            {
                var attributes = IO.File.GetAttributes(this);
                return IOHelper.ConvertAttributes(attributes);
            }

            return FileAttributes.None;
        }

        public override long GetFileSize()
        {
            if (Exists)
            {
                return new IO.FileInfo(this).Length;
            }

            return long.MinValue;
        }

        public override DateTime GetLastAccessedTime(bool useUtc = false)
        {
            if (Exists)
            {
                return useUtc
                    ? IO.File.GetLastAccessTimeUtc(this)
                    : IO.File.GetLastAccessTime(this);
            }

            return DateTime.MinValue;
        }

        public override DateTime GetLastModifiedTime(bool useUtc = false)
        {
            if (Exists)
            {
                return useUtc
                    ? IO.File.GetLastWriteTimeUtc(this)
                    : IO.File.GetLastWriteTime(this);
            }

            return DateTime.MinValue;
        }

        public override bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (!IsFileOpen)
            {
                Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));
                
                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, true);
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        IO.File.Delete(destination);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{destination}] already exists!");
                }

                if (Exists)
                    IO.File.Move(this, destination);
                FullPath = destination;
                return true;
            }
            return false;
        }

        public override IO.Stream Open(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            if (Exists)
            {
                Close();

                if (!IOHelper.IsValidModeCombination(fileAccess, fileMode))
                    throw new Exception("Invalid File Access and File Mode parameter combination!");

                Stream = IO.File.Open(this, IOHelper.ConvertFileMode(fileMode), IOHelper.ConvertFileAccess(fileAccess));

                StreamFileAccessMode = fileAccess;
                StreamFileMode = fileMode;

                return Stream;
            }

            return null;
        }

        public override string ReadAll()
        {
            return Exists
                    ? IO.File.ReadAllText(this)
                    : "";
        }

        public override XDocument ReadAllAsXDocument()
        {
            if (Exists)
            {
                var contents = IO.File.ReadAllText(this);
                try
                {
                    return XDocument.Parse(contents);
                }
                catch
                {
                    return new XDocument();
                }
            }

            return new XDocument();
        }

        public override string[] ReadAllLines()
        {
            return Exists
                    ? IO.File.ReadAllLines(this)
                    : new string[0];
        }

        public override IEnumerable<string> ReadLine()
        {
            return Exists
                ? IO.File.ReadLines(this)
                : new List<string>();
        }

        public override bool RemoveAttribute(FileAttributes attribute)
        {
            if (!IsFileOpen && Exists)
            {
                var convertedAttribute = IOHelper.ConvertAttributesToImplementation(attribute);

                if (convertedAttribute != null)
                {
                    IO.File.SetAttributes(this, IO.File.GetAttributes(this) | ~(IO.FileAttributes)convertedAttribute);
                    return true;
                }
            }

            return false;
        }

        public override bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrEmptyCheck(name, nameof(name));

            if (!IsFileOpen)
            {
                string newFullPath = IO.Path.Combine(Path, $"{name}.{Extension}");
                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        newFullPath = IOHelper.GenerateUniquePath(newFullPath, true);
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        IO.File.Delete(newFullPath);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{newFullPath}] already exists!");
                }

                if (Exists)
                    IO.File.Move(FullPath, newFullPath);
                FullPath = newFullPath;
                return true;
            }

            return false;
        }

        public override bool SetEncoding(Encoding encoding)
        {
            Validation.NotNullCheck<Encoding>(encoding, nameof(encoding));

            if (!IsFileOpen && Exists)
            {
                using (var writer = new IO.StreamWriter(this, true, encoding))
                    writer.Write("");
            }

            return false;
        }

        public override bool SetFileAttributes(FileAttributes attribute)
        {
            if (!IsFileOpen && Exists)
            {
                var convertedAttribute = IOHelper.ConvertAttributesToImplementation(attribute);

                if (convertedAttribute != null)
                {
                    IO.File.SetAttributes(this, IO.File.GetAttributes(this) | (IO.FileAttributes)convertedAttribute);
                    return true;
                }
            }

            return false;
        }

        public override bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullCheck<string>(contents, nameof(contents));

            if (!IsFileOpen)
            {
                switch (writeMode)
                {
                    case WriteMode.Append:
                        if (FS.Exists(this) == ExistenceCheckResult.FileExists)
                            contents = $"{System.IO.File.ReadAllText(this, Encoding)}{contents}";
                        break;
                }
                System.IO.File.WriteAllText(this, contents, Encoding);

                return true;
            }

            return false;
        }

        public override bool WriteLine(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullCheck<string>(contents, nameof(contents));

            if (!IsFileOpen)
            {
                contents = $"{contents}{Environment.NewLine}";

                switch (writeMode)
                {
                    case WriteMode.Append:
                        if (FS.Exists(this) == ExistenceCheckResult.FileExists)
                            contents = $"{System.IO.File.ReadAllText(this, Encoding)}{contents}";
                        break;
                }
                System.IO.File.WriteAllText(this, contents, Encoding);

                return true;
            }

            return false;
        }
    }
}
