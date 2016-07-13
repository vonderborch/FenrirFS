using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IO = System.IO;

namespace FenrirFS
{
    public abstract class FSFolder : FSFileSystemEntry, IDisposable, IEquatable<FSFolder>
    {
        #region Private Fields

        private bool disposedValue = false;

        #endregion Private Fields

        #region Public Constructors

        public FSFolder(string path)
        {
        }

        public FSFolder(string directory, string name, string extension)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override string FullPath
        {
            get { return IO.Path.Combine(Path, Name); }
        }

        #endregion Public Properties

        #region Public Methods

        public static implicit operator string(FSFolder folder)
        {
            return folder.FullPath;
        }

        public async Task<bool> AsyncCopy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Copy(destination);
        }

        public async Task<bool> AsyncCreateFile(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateFile(name);
        }

        public async Task<bool> AsyncCreateFolder(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return CreateFolder(name);
        }

        public async Task<bool> AsyncDelete(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Delete();
        }

        public async Task<bool> AsyncDeleteFile(string name, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteFile(name);
        }

        public async Task<bool> AsyncDeleteFolder(string name, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return DeleteFolder(name);
        }

        public async Task<bool> AsyncFileExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return FileExists(name, searchOption);
        }

        public async Task<bool> AsyncFolderExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return FolderExists(name, searchOption);
        }

        public async Task<FSFile> AsyncGetFile(string name, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFile(name);
        }

        public async Task<List<string>> AsyncGetFileNames(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileNames();
        }

        public async Task<List<FSFile>> AsyncGetFiles(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFiles();
        }

        public async Task<List<FSFileSystemEntry>> AsyncGetFileSystemEntries(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntries();
        }

        public async Task<FSFileSystemEntry> AsyncGetFileSystemEntry(string name, bool returnFileOverFolder = true, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntry(name, returnFileOverFolder);
        }

        public async Task<List<string>> AsyncGetFileSystemEntryNames(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFileSystemEntryNames();
        }

        public async Task<FSFolder> AsyncGetFolder(string name, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolder(name);
        }

        public async Task<List<string>> AsyncGetFolderNames(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolderNames();
        }

        public async Task<List<FSFolder>> AsyncGetFolders(CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return GetFolders();
        }

        public async Task<ExistenceCheckResult> AsyncItemExists(string name, SearchOption fileSearchOption = SearchOption.TopDirectoryOnly, SearchOption folderSearchOption = SearchOption.TopDirectoryOnly, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return ItemExists(name, fileSearchOption, folderSearchOption);
        }

        public async Task<bool> AsyncMove(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Move(destination);
        }

        public async Task<bool> AsyncRename(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await Tasks.ScheduleTask(cancellationToken);
            return Rename(Name);
        }

        public abstract bool Copy(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        public abstract bool CreateFile(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        public abstract bool CreateFolder(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        public abstract bool Delete();

        public abstract bool DeleteFile(string name);

        public abstract bool DeleteFolder(string name);

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

        public abstract bool FileExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly);

        public abstract bool FolderExists(string name, SearchOption searchOption = SearchOption.TopDirectoryOnly);

        public abstract FSFile GetFile(string name);

        public abstract List<string> GetFileNames();

        public abstract List<FSFile> GetFiles();

        public List<FSFileSystemEntry> GetFileSystemEntries()
        {
            var files = GetFiles();
            var folders = GetFolders();

            var merged = new List<FSFileSystemEntry>(files);
            for (int i = 0; i < folders.Count; i++)
                merged.Add(folders[i]);

            return merged;
        }

        public FSFileSystemEntry GetFileSystemEntry(string name, bool returnFileOverFolder = true)
        {
            FSFile file = GetFile(name);
            FSFolder folder = GetFolder(name);

            if (file != null && folder != null)
            {
                if (returnFileOverFolder)
                    return file;
                else
                    return folder;
            }
            else if (file != null)
                return file;
            else if (folder != null)
                return folder;
            else
                return null;
        }

        public List<string> GetFileSystemEntryNames()
        {
            var files = GetFileNames();
            var folders = GetFolderNames();

            for (int i = 0; i < folders.Count; i++)
                files.Add(folders[i]);

            return files;
        }

        public abstract FSFolder GetFolder(string name);

        public abstract List<string> GetFolderNames();

        public abstract List<FSFolder> GetFolders();

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

        public abstract bool Move(string destination, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        public abstract bool Rename(string name, FolderCollisionOption collisionOption = FolderCollisionOption.FailIfExists);

        #endregion Public Methods

        #region Protected Methods

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

        #endregion Protected Methods
    }
}