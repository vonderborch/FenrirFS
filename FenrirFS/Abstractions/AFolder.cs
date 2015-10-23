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

        protected AFolder(string name)
        {
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

        public virtual AFile CreateFile(string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AFile> CreateFileAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual AFolder CreateFolder(string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AFolder> CreateFolderAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual bool Delete()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual bool DeleteFile(string name)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> DeleteFileAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual bool DeleteFolder(string name)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> DeleteFolderAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above. GC.SuppressFinalize(this);
        }

        public virtual bool FileExists(string name)
        {
            AFile file = GetFile(name);
            return Fenrir.FileSystem.FileExists(name);
        }

        public virtual async Task<bool> FileExistsAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            AFile file = GetFile(name);
            return Fenrir.FileSystem.FileExists(name);
        }

        public virtual bool FolderExists(string name)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> FolderExistsAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual AFile GetFile(string name)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AFile> GetFileAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual List<string> GetFileNames()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<string>> GetFileNamesAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual List<AFile> GetFiles()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<AFile>> GetFilesAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual AFolder GetFolder(string name)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AFolder> GetFolderAsync(string name, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual List<string> GetFolderNames()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<string>> GetFolderNamesAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual List<AFolder> GetFolders()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<AFolder>> GetFoldersAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual AFolder GetParentFolder()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AFolder> GetParentFolderAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        #endregion Public Methods

        // To detect redundant calls

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

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free
        //       unmanaged resources. ~AFolder() { // Do not change this code. Put cleanup code in
        // Dispose(bool disposing) above. Dispose(false); }
    }
}