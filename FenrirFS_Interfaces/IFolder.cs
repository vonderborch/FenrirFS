using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    public interface IFolder : IDisposable, IEquatable<IFolder>
    {
        #region Public Properties

        string FullPath { get; }
        string Name { get; }
        string Path { get; }

        #endregion Public Properties

        #region Public Methods

        IFolder Copy(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption);

        IFolder Copy(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption);

        IFolder Copy(IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption);

        Task<IFolder> CopyAsync(string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken);

        Task<IFolder> CopyAsync(string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken);

        Task<IFolder> CopyAsync(IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken);

        IFile CopyFile(string file, string destinationName, FileCollisionOption collisionOption);

        IFile CopyFile(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption);

        IFile CopyFile(string file, IFolder destinationPath, string destinationName, FileCollisionOption collisionOption);

        Task<IFile> CopyFileAsync(string file, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        Task<IFile> CopyFileAsync(string file, string destinationPath, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        Task<IFile> CopyFileAsync(string file, IFolder destinationPath, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        IFolder CopyFolder(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption);

        IFolder CopyFolder(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption);

        IFolder CopyFolder(string folder, IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption);

        Task<IFolder> CopyFolderAsync(string folder, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken);

        Task<IFolder> CopyFolderAsync(string folder, string destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken);

        Task<IFolder> CopyFolderAsync(string folder, IFolder destinationPath, string destinationName, FolderCollisionOption folderCollisionOption, FileCollisionOption fileCollisionOption, CancellationToken cancellationToken);

        IFile CreateFile(string name, FileCollisionOption collisionOption);

        Task<IFile> CreateFileAsync(string name, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        IFolder CreateFolder(string name, FolderCollisionOption collisionOption);

        Task<IFolder> CreateFolderAsync(string name, FolderCollisionOption collisionOption, CancellationToken cancellationToken);

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

        IFolder GetFolder(string name);

        Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken);

        List<string> GetFolderNames();

        Task<List<string>> GetFolderNamesAsync(CancellationToken cancellationToken);

        List<IFolder> GetFolders();

        Task<List<IFolder>> GetFoldersAsync(CancellationToken cancellationToken);

        IFolder GetParentFolder();

        Task<IFolder> GetParentFolderAsync(CancellationToken cancellationToken);

        bool Move(string destinationName, FolderCollisionOption collisionOption);

        bool Move(string destinationPath, string destinationName, FolderCollisionOption collisionOption);

        bool Move(IFolder destinationPath, string destinationName, FolderCollisionOption collisionOption);

        Task<bool> MoveAsync(string destinationName, FolderCollisionOption collisionOption, CancellationToken cancellationToken);

        Task<bool> MoveAsync(string destinationPath, string destinationName, FolderCollisionOption collisionOption, CancellationToken cancellationToken);

        Task<bool> MoveAsync(IFolder destinationPath, string destinationName, FolderCollisionOption collisionOption, CancellationToken cancellationToken);

        Task<bool> RemameAsync(string name, FolderCollisionOption collisionOption, CancellationToken cancellationToken);

        bool Rename(string name, FolderCollisionOption collisionOption);

        #endregion Public Methods
    }
}