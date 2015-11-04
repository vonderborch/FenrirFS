using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    public abstract class AFolder : IDisposable, IEquatable<AFolder>
    {
        #region Protected Fields

        protected bool disposedValue = false;

        #endregion Protected Fields

        #region Protected Constructors

        protected AFolder(string path)
        {
            SetupAFolder(path);
        }

        #endregion Protected Constructors

        #region Public Properties

        public string FullPath
        {
            get { return System.IO.Path.Combine(Path, Name); }
        }

        public virtual string Name { get; protected set; }

        public virtual string Path { get; protected set; }

        #endregion Public Properties

        #region Public Methods

        public virtual AFolder Copy(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFolder Copy(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFolder Copy(AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<AFolder> CopyAsync(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Copy(destinationName, folderCollisionOption, fileCollisionOption);
        }

        public async Task<AFolder> CopyAsync(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Copy(destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public async Task<AFolder> CopyAsync(AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Copy(destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public virtual AFile CopyFile(string file, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFile CopyFile(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFile CopyFile(string file, AFolder destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<AFile> CopyFileAsync(string file, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFile(file, destinationName, collisionOption);
        }

        public async Task<AFile> CopyFileAsync(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFile(file, destinationPath, destinationName, collisionOption);
        }

        public async Task<AFile> CopyFileAsync(string file, AFolder destinationPath, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFile(file, destinationPath, destinationName, collisionOption);
        }

        public virtual AFolder CopyFolder(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFolder CopyFolder(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFolder CopyFolder(string folder, AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<AFolder> CopyFolderAsync(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFolder(folder, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public async Task<AFolder> CopyFolderAsync(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFolder(folder, destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public async Task<AFolder> CopyFolderAsync(string folder, AFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFolder(folder, destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public virtual AFile CreateFile(string name, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<AFile> CreateFileAsync(string name, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CreateFile(name, collisionOption);
        }

        public virtual AFolder CreateFolder(string name, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<AFolder> CreateFolderAsync(string name, FolderCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CreateFolder(name, collisionOption);
        }

        public virtual bool Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Delete();
        }

        public virtual bool DeleteFile(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFileAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return DeleteFile(name);
        }

        public virtual bool DeleteFolder(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFolderAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return DeleteFolder(name);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public override bool Equals(object obj)
        {
            return obj != null
                ? FullPath == obj.ToString()
                : false;
        }

        public bool Equals(AFolder folder)
        {
            return folder.FullPath == FullPath;
        }

        public virtual bool FileExists(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FileExistsAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return FileExists(name);
        }

        public virtual bool FolderExists(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FolderExistsAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return FolderExists(name);
        }

        public virtual AFile GetFile(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<AFile> GetFileAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFile(name);
        }

        public virtual List<string> GetFileNames()
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetFileNamesAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFileNames();
        }

        public virtual List<AFile> GetFiles()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AFile>> GetFilesAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFiles();
        }

        public virtual AFolder GetFolder(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<AFolder> GetFolderAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFolder(name);
        }

        public virtual List<string> GetFolderNames()
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetFolderNamesAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFolderNames();
        }

        public virtual List<AFolder> GetFolders()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AFolder>> GetFoldersAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFolders();
        }

        public virtual AFolder GetParentFolder()
        {
            throw new NotImplementedException();
        }

        public async Task<AFolder> GetParentFolderAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetParentFolder();
        }

        public virtual bool Move(string destinationName, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual bool Move(string destinationPath, string destinationName, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual bool Move(AFolder destinationPath, string destinationName, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MoveAsync(string destinationName, FolderCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Move(destinationName, collisionOption);
        }

        public async Task<bool> MoveAsync(string destinationPath, string destinationName, FolderCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Move(destinationPath, destinationName, collisionOption);
        }

        public async Task<bool> MoveAsync(AFolder destinationPath, string destinationName, FolderCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Move(destinationPath, destinationName, collisionOption);
        }

        public async Task<bool> RemameAsync(string name, FolderCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Rename(name, collisionOption);
        }

        public virtual bool Rename(string name, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return FullPath;
        }

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

                disposedValue = true;
            }
        }

        protected void SetupAFolder(string path)
        {
            Name = System.IO.Path.GetFileName(path);
            Path = System.IO.Path.GetDirectoryName(path);
        }

        #endregion Protected Methods
    }
}