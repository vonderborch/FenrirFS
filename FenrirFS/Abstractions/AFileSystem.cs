using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    public abstract class AFileSystem : IFileSystem
    {
        #region Protected Constructors

        protected AFileSystem()
        {
            DefaultEncoding = Encoding.UTF8;
        }

        #endregion Protected Constructors

        #region Public Properties

        public Encoding DefaultEncoding { get; set; }
        public IFolder StorageLocal { get; protected set; }
        public IFolder StorageRoaming { get; protected set; }
        public IFolder StorageUser { get; set; }
        public IFolder StorageWorking { get; protected set; }

        #endregion Public Properties

        #region Public Methods

        public virtual IFile CreateFile(string directory, string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFile CreateFile(IFolder directory, string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFile CreateFile(string path, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<IFile> CreateFileAsync(string directory, string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CreateFile(directory, name, collisionOption);
        }

        public async Task<IFile> CreateFileAsync(IFolder directory, string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CreateFile(directory, name, collisionOption);
        }

        public async Task<IFile> CreateFileAsync(string path, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CreateFile(path, collisionOption);
        }

        public virtual IFolder CreateFolder(string directory, string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFolder CreateFolder(IFolder directory, string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual IFolder CreateFolder(string path, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<IFolder> CreateFolderAsync(string directory, string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CreateFolder(directory, name, collisionOption);
        }

        public async Task<IFolder> CreateFolderAsync(IFolder directory, string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CreateFolder(directory, name, collisionOption);
        }

        public async Task<IFolder> CreateFolderAsync(string path, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return CreateFolder(path, collisionOption);
        }

        public virtual ExistenceCheckResult Exists(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<ExistenceCheckResult> ExistsAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Exists(path);
        }

        public virtual bool FileExists(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FileExistsAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return FileExists(path);
        }

        public virtual bool FolderExists(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FolderExistsAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return FolderExists(path);
        }

        public virtual string GenerateFileUniqueName(string directory, string name, int iterations = 99)
        {
            string basename = Path.GetFileNameWithoutExtension(name);
            string ext = Path.GetExtension(name);
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < iterations; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!FileExists(Path.Combine(directory, name)))
                            return name;
                        break;

                    default:
                        str.Clear();
                        str.Append(String.Format("{0} - ({1}).{2}", basename, i, ext));
                        if (!FileExists(Path.Combine(directory, str.ToString())))
                            return str.ToString();
                        break;
                }
            }

            throw Exceptions.CanNotGenerateUniqueNameException(iterations);
        }

        public virtual string GenerateFileUniqueName(string path, int iterations = 99)
        {
            string directory = Path.GetDirectoryName(path);
            string basename = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < iterations; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!FileExists(path))
                            return basename + "." + ext;
                        break;

                    default:
                        str.Clear();
                        str.Append(String.Format("{0} - ({1}).{2}", basename, i, ext));
                        if (!FileExists(Path.Combine(directory, str.ToString())))
                            return str.ToString();
                        break;
                }
            }

            throw Exceptions.CanNotGenerateUniqueNameException(iterations);
        }

        public async Task<string> GenerateFileUniqueNameAsync(string directory, string name, CancellationToken cancellationToken, int iterations = 99)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GenerateFileUniqueName(directory, name, iterations);
        }

        public async Task<string> GenerateFileUniqueNameAsync(string path, CancellationToken cancellationToken, int iterations = 99)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GenerateFileUniqueName(path, iterations);
        }

        public virtual string GenerateFolderUniqueName(string directory, string name, int iterations = 99)
        {
            string basename = Path.GetFileNameWithoutExtension(name);
            string ext = Path.GetExtension(name);
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < iterations; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!FolderExists(Path.Combine(directory, name)))
                            return name;
                        break;

                    default:
                        str.Clear();
                        str.Append(String.Format("{0} - ({1})", basename, i));
                        if (!FolderExists(Path.Combine(directory, str.ToString())))
                            return str.ToString();
                        break;
                }
            }

            throw Exceptions.CanNotGenerateUniqueNameException(iterations);
        }

        public virtual string GenerateFolderUniqueName(string path, int iterations = 99)
        {
            string directory = Path.GetDirectoryName(path);
            string basename = Path.GetFileNameWithoutExtension(path);
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < iterations; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!FolderExists(path))
                            return basename;
                        break;

                    default:
                        str.Clear();
                        str.Append(String.Format("{0} - ({1})", basename, i));
                        if (!FolderExists(Path.Combine(directory, str.ToString())))
                            return str.ToString();
                        break;
                }
            }

            throw Exceptions.CanNotGenerateUniqueNameException(iterations);
        }

        public async Task<string> GenerateFolderUniqueNameAsync(string directory, string name, CancellationToken cancellationToken, int iterations = 99)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GenerateFolderUniqueName(directory, name, iterations);
        }

        public async Task<string> GenerateFolderUniqueNameAsync(string path, CancellationToken cancellationToken, int iterations = 99)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GenerateFolderUniqueName(path, iterations);
        }

        public virtual IFile GetFileFromPath(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<IFile> GetFileFromPathAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFileFromPath(path);
        }

        public virtual IFolder GetFolderFromPath(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<IFolder> GetFolderFromPathAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return GetFolderFromPath(path);
        }

        public virtual bool SetStorageUser(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetStorageUserAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return SetStorageUser(path);
        }

        public override string ToString() => "FenrirFS";

        #endregion Public Methods
    }
}