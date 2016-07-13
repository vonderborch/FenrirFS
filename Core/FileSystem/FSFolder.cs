using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO = System.IO;

namespace FenrirFS
{
    public abstract class FSFolder : FSFileSystemEntry, IDisposable, IEquatable<FSFolder>
    {
        public static implicit operator string(FSFolder folder)
        {
            return folder.FullPath;
        }

        public FSFolder(string path)
        {

        }

        public FSFolder(string directory, string name, string extension)
        {

        }

        public override string FullPath
        {
            get { return IO.Path.Combine(Path, Name); }
        }


        
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~FSFolder() {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            //   Dispose(false);
            // }

            // This code added to correctly implement the disposable pattern.

            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        public bool Equals(FSFolder other)
        {
            return FullPath == other.FullPath;
        }































        public abstract bool Copy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        public abstract bool CreateFile(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);
        public abstract bool CreateFolder(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        public abstract bool Delete();

        public abstract bool DeleteFile(string name);
        public abstract bool DeleteFolder(string name);

        public ExistenceCheckResult ItemExists(string name, SearchOption fileSearchOption = SearchOption.TopDirectoryOnly, SearchOption folderSearchOption = SearchOption.TopDirectoryOnly)
        {
            bool fileExists = FileExists(name, fileSearchOption);
            bool folderExists = FolderExists(name, folderSearchOption);

            if (fileExists && folderExists)
                return ExistenceCheckResult.FileAndFolderExists;
            else if (fileExists)
                return ExistenceCheckResult.FileExists;
            else if (folderExists)
                return ExistenceCheckResult.FolderExists;
            else
                return ExistenceCheckResult.None;
        }

        public abstract bool FileExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly);
        public abstract bool FolderExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly);

        public abstract List<string> GetFileNames();
        public abstract List<FSFile> GetFiles();

        public abstract FSFile GetFile(string name);

        public abstract List<string> GetFolderNames();
        public abstract List<FSFolder> GetFolders();
        public abstract FSFolder GetFolder(string name);
        public abstract FSFileSystemEntry GetFileSystemEntry(string name);

        public abstract List<string> GetFileSystemEntryNames();
        public abstract List<FSFileSystemEntry> GetFileSystemEntries();

        public abstract bool Rename(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        public abstract bool Move(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);
    }
}
