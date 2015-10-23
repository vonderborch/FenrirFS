using FenrirFS.Helpers;
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
        }

        #endregion Protected Constructors

        #region Public Properties

        public AFolder StorageExecutable { get; protected set; }
        public AFolder StorageLocal { get; protected set; }
        public AFolder StorageRoaming { get; protected set; }
        public AFolder StorageUser { get; set; }

        #endregion Public Properties

        #region Public Methods

        public virtual AFile CreateFile(string directory, string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFile CreateFile(AFolder directory, string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFile CreateFile(string path, CollisionOption collisionOption)
        {
            throw new NotImplementedException("Not implemented!");
        }

        public virtual async Task<AFile> CreateFileAsync(string directory, string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual async Task<AFile> CreateFileAsync(AFolder directory, string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException("Not implemented!");
        }

        public virtual async Task<AFile> CreateFileAsync(string path, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual AFolder CreateFolder(string directory, string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFolder CreateFolder(AFolder directory, string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFolder CreateFolder(string path, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AFolder> CreateFolderAsync(string directory, string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual async Task<AFolder> CreateFolderAsync(AFolder directory, string name, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual async Task<AFolder> CreateFolderAsync(string path, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual ExistenceCheckResult Exists(string path)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<ExistenceCheckResult> ExistsAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual bool FileExists(string path)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> FileExistsAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual bool FolderExists(string path)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> FolderExistsAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public string GenerateFileUniqueName(string directory, string name, int iterations = 99)
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

        public string GenerateFileUniqueName(string path, int iterations = 99)
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

        public async Task<string> GenerateFileUniqueNameAsync(string path, CancellationToken cancellationToken, int iterations = 99)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);

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

        public string GenerateFolderUniqueName(string directory, string name, int iterations = 99)
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

        public string GenerateFolderUniqueName(string path, int iterations = 99)
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

        public async Task<string> GenerateFolderUniqueNameAsync(string path, CancellationToken cancellationToken, int iterations = 99)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);

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

        public virtual AFile GetFileFromPath(string path)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AFile> GetFileFromPathAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        public virtual AFolder GetFolderFromPath(string path)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AFolder> GetFolderFromPathAsync(string path, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}