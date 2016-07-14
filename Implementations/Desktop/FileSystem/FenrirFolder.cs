using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool Copy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        public override bool CreateFile(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        public override bool CreateFolder(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override bool DeleteFile(string name)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteFolder(string name)
        {
            throw new NotImplementedException();
        }

        public override bool FileExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            throw new NotImplementedException();
        }

        public override bool FolderExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetCreationTime(bool useUtc = false)
        {
            throw new NotImplementedException();
        }

        public override FSFile GetFile(string name)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetFileNames()
        {
            throw new NotImplementedException();
        }

        public override List<FSFile> GetFiles()
        {
            throw new NotImplementedException();
        }

        public override FSFolder GetFolder(string name)
        {
            throw new NotImplementedException();
        }

        public override List<string> GetFolderNames()
        {
            throw new NotImplementedException();
        }

        public override List<FSFolder> GetFolders()
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

        public override bool Move(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        public override bool Rename(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }
    }
}
