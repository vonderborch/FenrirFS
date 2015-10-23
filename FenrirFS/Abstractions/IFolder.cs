using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    public interface IFolder : IDisposable, IEquatable<IFolder>
    {
        #region Properties

        string FullPath { get; }
        string Name { get; set; }
        string Path { get; set; }

        #endregion Properties

        #region Methods

        IFile CreateFile(string name, CollisionOption collisionOption);

        Task<IFile> CreateFileAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken);

        IFolder CreateFolder(string name, CollisionOption collisionOption);

        Task<IFolder> CreateFolderAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken);

        bool Delete();

        Task<bool> DeleteAsync(CancellationToken cancellationToken);

        bool DeleteFile(string name);

        Task<bool> DeleteFileAsync(string name, CancellationToken cancellationToken);

        bool DeleteFolder(string name);

        Task<bool> DeleteFolderAsync(string name, CancellationToken cancellationToken);
        
        new void Dispose();

        bool FileExists(string name);

        Task<bool> FileExistsAsync(string name, CancellationToken cancellationToken);

        bool FolderExists(string name);

        Task<bool> FolderExistsAsync(string name, CancellationToken cancellationToken);

        IFile GetFile(string name);

        Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken);

        List<string> GetFileNames();

        Task<List<string>> GetFileNamesAsync(CancellationToken cancellationToken);

        List<IFile> GetFiles();

        Task<List<IFile>> GetFilesAsync(CancellationToken cancellationToken);

        AFolder GetFolder(string name);

        Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken);

        List<string> GetFolderNames();

        Task<List<string>> GetFolderNamesAsync(CancellationToken cancellationToken);

        List<IFolder> GetFolders();

        Task<List<IFolder>> GetFoldersAsync(CancellationToken cancellationToken);

        IFolder GetParentFolder();

        Task<IFolder> GetParentFolderAsync(CancellationToken cancellationToken);

        #endregion Methods
    }
}