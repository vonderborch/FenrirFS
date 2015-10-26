using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    public abstract class AFolder : IFolder
    {
        #region Protected Fields

        protected bool disposedValue = false;

        #endregion Protected Fields

        #region Protected Constructors

        protected AFolder(string path)
        {
            SetupIFolder(path);
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

        public virtual IFolder Copy(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFolder Copy(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFolder Copy(IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<IFolder> CopyAsync(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Copy(destinationName, folderCollisionOption, fileCollisionOption);
        }

        public async Task<IFolder> CopyAsync(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Copy(destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public async Task<IFolder> CopyAsync(IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Copy(destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public virtual IFile CopyFile(string file, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFile CopyFile(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFile CopyFile(string file, IFolder destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<IFile> CopyFileAsync(string file, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFile(file, destinationName, collisionOption);
        }

        public async Task<IFile> CopyFileAsync(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFile(file, destinationPath, destinationName, collisionOption);
        }

        public async Task<IFile> CopyFileAsync(string file, IFolder destinationPath, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFile(file, destinationPath, destinationName, collisionOption);
        }

        public virtual IFolder CopyFolder(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFolder CopyFolder(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFolder CopyFolder(string folder, IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<IFolder> CopyFolderAsync(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFolder(folder, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public async Task<IFolder> CopyFolderAsync(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFolder(folder, destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public async Task<IFolder> CopyFolderAsync(string folder, IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CopyFolder(folder, destinationPath, destinationName, folderCollisionOption, fileCollisionOption);
        }

        public virtual IFile CreateFile(string name, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<IFile> CreateFileAsync(string name, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CreateFile(name, collisionOption);
        }

        public virtual IFolder CreateFolder(string name, FolderCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<IFolder> CreateFolderAsync(string name, FolderCollisionOption collisionOption, CancellationToken cancellationToken)
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

        public bool Equals(IFolder folder)
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

        public virtual IFile GetFile(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken)
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

        public virtual List<IFile> GetFiles()
        {
            throw new NotImplementedException();
        }

        public async Task<List<IFile>> GetFilesAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFiles();
        }

        public virtual IFolder GetFolder(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken)
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

        public virtual List<IFolder> GetFolders()
        {
            throw new NotImplementedException();
        }

        public async Task<List<IFolder>> GetFoldersAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFolders();
        }

        public virtual IFolder GetParentFolder()
        {
            throw new NotImplementedException();
        }

        public async Task<IFolder> GetParentFolderAsync(CancellationToken cancellationToken)
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

        public virtual bool Move(IFolder destinationPath, string destinationName, FolderCollisionOption collisionOption)
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

        public async Task<bool> MoveAsync(IFolder destinationPath, string destinationName, FolderCollisionOption collisionOption, CancellationToken cancellationToken)
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

        protected void SetupIFolder(string path)
        {
            Name = System.IO.Path.GetFileName(path);
            Path = System.IO.Path.GetDirectoryName(path);
        }

        #endregion Protected Methods
    }
}