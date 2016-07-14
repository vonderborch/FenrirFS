using System;
using System.Collections.Generic;
using IO = System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            string newFullPath = IO.Path.Combine(Path, $"{Name}.{Extension}");
            bool exists = IO.File.Exists(newFullPath);
            if (exists)
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        IO.File.Delete(newFullPath);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{newFullPath}] already exists!");
                }
            }

            IO.File.Move(FullPath, newFullPath);
            return true;
        }

        public override bool Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            bool exists = IO.File.Exists(destination);
            if (exists)
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        IO.File.Delete(destination);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{destination}] already exists!");
                }
            }

            IO.File.Copy(FullPath, destination);
            return true;
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override DateTime GetCreationTime(bool useUtc = false)
        {
            throw new NotImplementedException();
        }

        public override Encoding GetEncoding()
        {
            throw new NotImplementedException();
        }

        public override FileAttributes GetFileAttributes()
        {
            throw new NotImplementedException();
        }

        public override long GetFileSize()
        {
            throw new NotImplementedException();
        }

        public override DateTime GetLastAccessedTime(bool useUtc = false)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetLastModifiedTime(bool useUtc = false)
        {
            throw new NotImplementedException();
        }

        public override bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        public override IO.Stream Open(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            throw new NotImplementedException();
        }

        public override string ReadAll()
        {
            throw new NotImplementedException();
        }

        public override XDocument ReadAllAsXDocument()
        {
            throw new NotImplementedException();
        }

        public override string[] ReadAllLines()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> ReadLine()
        {
            throw new NotImplementedException();
        }

        public override bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        public override bool SetEncoding(Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public override bool SetFileAttributes(FileAttributes attributes)
        {
            throw new NotImplementedException();
        }

        public override bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            throw new NotImplementedException();
        }

        public override bool WriteLine(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            throw new NotImplementedException();
        }
    }
}
