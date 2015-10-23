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
        string Name { get; }
        string Path { get; }

        #endregion Properties

        #region Methods

        IFile CreateFile(string name, CollisionOption collisionOption);

        Task<IFile> CreateFileAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken);

        IFolder CreateFolder(string name, CollisionOption collisionOption);

        Task<IFolder> CreateFolderAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken);



        IFolder Copy(string destinationName, CollisionOption collisionOption);

        IFolder Copy(string destinationPath, string destinationName, CollisionOption collisionOption);

        IFolder Copy(IFolder destinationPath, string destinationName, CollisionOption collisionOption);

        IFile CopyFile(string destinationName, CollisionOption collisionOption);

        IFile CopyFile(string destinationPath, string destinationName, CollisionOption collisionOption);

        IFile CopyFile(IFolder destinationPath, string destinationName, CollisionOption collisionOption);

        IFolder CopyFolder(string destinationName, CollisionOption collisionOption);

        IFolder CopyFolder(string destinationPath, string destinationName, CollisionOption collisionOption);

        IFolder CopyFolder(IFolder destinationPath, string destinationName, CollisionOption collisionOption);

        bool Move(string destinationName, CollisionOption collisionOption);

        bool Move(string destinationPath, string destinationName, CollisionOption collisionOption);

        bool Move(IFolder destinationPath, string destinationName, CollisionOption collisionOption);





        Task<IFolder> CopyAsync(string destinationName, CollisionOption collisionOption);

        Task<IFolder> CopyAsync(string destinationPath, string destinationName, CollisionOption collisionOption);

        Task<IFolder> CopyAsync(IFolder destinationPath, string destinationName, CollisionOption collisionOption);

        Task<IFile> CopyFileAsync(string destinationName, CollisionOption collisionOption);

        Task<IFile> CopyFileAsync(string destinationPath, string destinationName, CollisionOption collisionOption);

        Task<IFile> CopyFileAsync(IFolder destinationPath, string destinationName, CollisionOption collisionOption);

        Task<IFolder> CopyFolderAsync(string destinationName, CollisionOption collisionOption);

        Task<IFolder> CopyFolderAsync(string destinationPath, string destinationName, CollisionOption collisionOption);

        Task<IFolder> CopyFolderAsync(IFolder destinationPath, string destinationName, CollisionOption collisionOption);

        Task<bool> MoveAsync(string destinationName, CollisionOption collisionOption);

        Task<bool> MoveAsync(string destinationPath, string destinationName, CollisionOption collisionOption);

        Task<bool> MoveAsync(IFolder destinationPath, string destinationName, CollisionOption collisionOption);



        bool Delete();

        Task<bool> DeleteAsync(CancellationToken cancellationToken);

        bool DeleteFile(string name);

        Task<bool> DeleteFileAsync(string name, CancellationToken cancellationToken);

        bool DeleteFolder(string name);

        Task<bool> DeleteFolderAsync(string name, CancellationToken cancellationToken);
        
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

        bool Rename(string name, CollisionOption collisionOption);

        Task<bool> RemameAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken);

        #endregion Methods
    }
}