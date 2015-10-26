using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    public interface IFileSystem
    {
        #region Public Properties

        Encoding DefaultEncoding { get; set; }
        IFolder StorageLocal { get; }
        IFolder StorageRoaming { get; }
        IFolder StorageUser { get; set; }
        IFolder StorageWorking { get; }

        #endregion Public Properties

        #region Public Methods

        IFile CreateFile(string directory, string name, FileCollisionOption collisionOption);

        IFile CreateFile(IFolder directory, string name, FileCollisionOption collisionOption);

        IFile CreateFile(string path, FileCollisionOption collisionOption);

        Task<IFile> CreateFileAsync(string directory, string name, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        Task<IFile> CreateFileAsync(IFolder directory, string name, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        Task<IFile> CreateFileAsync(string path, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        IFolder CreateFolder(string directory, string name, FileCollisionOption collisionOption);

        IFolder CreateFolder(IFolder directory, string name, FileCollisionOption collisionOption);

        IFolder CreateFolder(string path, FileCollisionOption collisionOption);

        Task<IFolder> CreateFolderAsync(string directory, string name, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        Task<IFolder> CreateFolderAsync(IFolder directory, string name, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        Task<IFolder> CreateFolderAsync(string path, FileCollisionOption collisionOption, CancellationToken cancellationToken);

        ExistenceCheckResult Exists(string path);

        Task<ExistenceCheckResult> ExistsAsync(string path, CancellationToken cancellationToken);

        bool FileExists(string path);

        Task<bool> FileExistsAsync(string path, CancellationToken cancellationToken);

        bool FolderExists(string path);

        Task<bool> FolderExistsAsync(string path, CancellationToken cancellationToken);

        string GenerateFileUniqueName(string directory, string name, int iterations = 99);

        string GenerateFileUniqueName(string path, int iterations = 99);

        Task<string> GenerateFileUniqueNameAsync(string directory, string name, CancellationToken cancellationToken, int iterations = 99);

        Task<string> GenerateFileUniqueNameAsync(string path, CancellationToken cancellationToken, int iterations = 99);

        string GenerateFolderUniqueName(string directory, string name, int iterations = 99);

        string GenerateFolderUniqueName(string path, int iterations = 99);

        Task<string> GenerateFolderUniqueNameAsync(string directory, string name, CancellationToken cancellationToken, int iterations = 99);

        Task<string> GenerateFolderUniqueNameAsync(string path, CancellationToken cancellationToken, int iterations = 99);

        IFile GetFileFromPath(string path);

        Task<IFile> GetFileFromPathAsync(string path, CancellationToken cancellationToken);

        IFolder GetFolderFromPath(string path);

        Task<IFolder> GetFolderFromPathAsync(string path, CancellationToken cancellationToken);

        bool SetStorageUser(string path);

        Task<bool> SetStorageUserAsync(string path, CancellationToken cancellationToken);

        #endregion Public Methods
    }
}