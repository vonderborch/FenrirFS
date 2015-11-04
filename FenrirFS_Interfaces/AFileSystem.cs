/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    /// <summary>
    /// Represents an abstract file system in FenrirFS.
    /// </summary>
    public abstract class AFileSystem
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AFileSystem"/> class.
        /// </summary>
        protected AFileSystem()
        {
            DefaultEncoding = Encoding.UTF8;
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the default encoding.
        /// </summary>
        /// <value>The default encoding.</value>
        public Encoding DefaultEncoding { get; set; }

        /// <summary>
        /// Gets or sets the local storage folder.
        /// </summary>
        /// <value>The storage local.</value>
        public AFolder StorageLocal { get; protected set; }

        /// <summary>
        /// Gets or sets the roaming storage folder.
        /// </summary>
        /// <value>The storage roaming.</value>
        public AFolder StorageRoaming { get; protected set; }

        /// <summary>
        /// Gets or sets the user storage folder.
        /// </summary>
        /// <value>The storage user.</value>
        public AFolder StorageUser { get; set; }

        /// <summary>
        /// Gets or sets the working storage folder.
        /// </summary>
        /// <value>The storage working.</value>
        public AFolder StorageWorking { get; protected set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile CreateFile(string directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile CreateFile(AFolder directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile CreateFile(string path, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a file asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to create a new file. The AFile represents the file.</returns>
        public async Task<AFile> CreateFileAsync(string directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFile(directory, name, collisionOption);
        }

        /// <summary>
        /// Creates a file asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to create a new file. The AFile represents the file.</returns>
        public async Task<AFile> CreateFileAsync(AFolder directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFile(directory, name, collisionOption);
        }

        /// <summary>
        /// Creates a file asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to create a new file. The AFile represents the file.</returns>
        public async Task<AFile> CreateFileAsync(string path, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFile(path, collisionOption);
        }

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFolder representing the folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder CreateFolder(string directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFolder representing the folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder CreateFolder(AFolder directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFolder representing the folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder CreateFolder(string path, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a folder asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to create a new folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> CreateFolderAsync(string directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFolder(directory, name, collisionOption);
        }

        /// <summary>
        /// Creates a folder asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to create a new folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> CreateFolderAsync(AFolder directory, string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFolder(directory, name, collisionOption);
        }

        /// <summary>
        /// Creates a folder asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to create a new folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> CreateFolderAsync(string path, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return CreateFolder(path, collisionOption);
        }

        /// <summary>
        /// Checks whether the item at the specified path exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The results of the existence check.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual ExistenceCheckResult Exists(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously checks whether the item at the specified path exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// An ExistenceCheckResult task to check the existance of the path. The
        /// ExistanceCheckResult represents the results of the check.
        /// </returns>
        public async Task<ExistenceCheckResult> ExistsAsync(string path, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return Exists(path);
        }

        /// <summary>
        /// Checks if a file exists at the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Whether the file exists (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool FileExists(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously checks if a file exists at the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to check if the file exists. The boolean represents whether the file
        /// exists (true) or not (false).
        /// </returns>
        public async Task<bool> FileExistsAsync(string path, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return FileExists(path);
        }

        /// <summary>
        /// Checks if a folder exists at the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Whether the folder exists (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool FolderExists(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously checks if a folder exists at the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to check if the folder exists. The boolean represents whether the folder
        /// exists (true) or not (false).
        /// </returns>
        public async Task<bool> FolderExistsAsync(string path, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return FolderExists(path);
        }

        /// <summary>
        /// Generates a unique name for a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <returns>A new unique name for the file.</returns>
        public virtual string GenerateFileUniqueName(AFolder directory, string name, int iterations = 99)
        {
            string basename = Path.GetFileNameWithoutExtension(name);
            string ext = Path.GetExtension(name);
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < iterations; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!FileExists(Path.Combine(directory.ToString(), name)))
                            return name;
                        break;

                    default:
                        str.Clear();
                        str.Append(String.Format("{0} - ({1}).{2}", basename, i, ext));
                        if (!FileExists(Path.Combine(directory.ToString(), str.ToString())))
                            return str.ToString();
                        break;
                }
            }

            throw Exceptions.CanNotGenerateUniqueNameException(iterations);
        }

        /// <summary>
        /// Generates a unique name for a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <returns>A new unique name for the file.</returns>
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

        /// <summary>
        /// Generates a unique name for a file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <returns>A new unique name for the file.</returns>
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

        /// <summary>
        /// Generates a unique name for a file asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A string task to generate a unique file name. The string represents a new unique name
        /// for the file.
        /// </returns>
        public async Task<string> GenerateFileUniqueNameAsync(AFolder directory, string name, int iterations = 99, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GenerateFileUniqueName(directory, name, iterations);
        }

        /// <summary>
        /// Generates a unique name for a file asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A string task to generate a unique file name. The string represents a new unique name
        /// for the file.
        /// </returns>
        public async Task<string> GenerateFileUniqueNameAsync(string directory, string name, int iterations = 99, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GenerateFileUniqueName(directory, name, iterations);
        }

        /// <summary>
        /// Generates a unique name for a file asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A string task to generate a unique file name. The string represents a new unique name
        /// for the file.
        /// </returns>
        public async Task<string> GenerateFileUniqueNameAsync(string path, int iterations = 99, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GenerateFileUniqueName(path, iterations);
        }

        /// <summary>
        /// Generates a unique name for a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <returns>A new unique name for the folder.</returns>
        public virtual string GenerateFolderUniqueName(AFolder directory, string name, int iterations = 99)
        {
            string basename = Path.GetFileNameWithoutExtension(name);
            string ext = Path.GetExtension(name);
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < iterations; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!FolderExists(Path.Combine(directory.ToString(), name)))
                            return name;
                        break;

                    default:
                        str.Clear();
                        str.Append(String.Format("{0} - ({1})", basename, i));
                        if (!FolderExists(Path.Combine(directory.ToString(), str.ToString())))
                            return str.ToString();
                        break;
                }
            }

            throw Exceptions.CanNotGenerateUniqueNameException(iterations);
        }

        /// <summary>
        /// Generates a unique name for a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <returns>A new unique name for the folder.</returns>
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

        /// <summary>
        /// Generates a unique name for a folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <returns>A new unique name for the folder.</returns>
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

        /// <summary>
        /// Generates a unique name for a folder asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A string task to generate a unique folder name. The string represents a new unique name
        /// for the folder.
        /// </returns>
        public async Task<string> GenerateFolderUniqueNameAsync(AFolder directory, string name, int iterations = 99, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GenerateFolderUniqueName(directory, name, iterations);
        }

        /// <summary>
        /// Generates a unique name for a folder asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="name">The name.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A string task to generate a unique folder name. The string represents a new unique name
        /// for the folder.
        /// </returns>
        public async Task<string> GenerateFolderUniqueNameAsync(string directory, string name, int iterations = 99, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GenerateFolderUniqueName(directory, name, iterations);
        }

        /// <summary>
        /// Generates a unique name for a folder asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="iterations">The maximum iterations. Defaults to 99.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A string task to generate a unique folder name. The string represents a new unique name
        /// for the folder.
        /// </returns>
        public async Task<string> GenerateFolderUniqueNameAsync(string path, int iterations = 99, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return GenerateFolderUniqueName(path, iterations);
        }

        /// <summary>
        /// Gets a file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile OpenFile(string path, OpenMode openMode = OpenMode.Normal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="file">The file.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile OpenFile(string directory, string file, OpenMode openMode = OpenMode.Normal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a file.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="file">The file.</param>
        /// <param name="openMode">The open mode Defaults to Normal..</param>
        /// <returns>The file.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFile OpenFile(AFolder directory, string file, OpenMode openMode = OpenMode.Normal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens a file asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to open a file. The AFile represents the file.</returns>
        public async Task<AFile> OpenFileAsync(string path, OpenMode openMode, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenFile(path, openMode);
        }

        /// <summary>
        /// Opens a file asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="file">The file.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to open a file. The AFile represents the file.</returns>
        public async Task<AFile> OpenFileAsync(string directory, string file, OpenMode openMode, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenFile(directory, file, openMode);
        }

        /// <summary>
        /// Opens a file asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="file">The file.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFile task to open a file. The AFile represents the file.</returns>
        public async Task<AFile> OpenFileAsync(AFolder directory, string file, OpenMode openMode, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenFile(directory, file, openMode);
        }

        /// <summary>
        /// Gets a folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder OpenFolder(string path, OpenMode openMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder OpenFolder(string directory, string folder, OpenMode openMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a folder.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <returns>The folder.</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual AFolder OpenFolder(AFolder directory, string folder, OpenMode openMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens a folder asynchronously.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to open a folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> OpenFolderAsync(string path, OpenMode openMode, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenFolder(path, openMode);
        }

        /// <summary>
        /// Opens a folder asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to open a folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> OpenFolderAsync(string directory, string folder, OpenMode openMode, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenFolder(directory, folder, openMode);
        }

        /// <summary>
        /// Opens a folder asynchronously.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="openMode">The open mode. Defaults to Normal.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>An AFolder task to open a folder. The AFolder represents the folder.</returns>
        public async Task<AFolder> OpenFolderAsync(AFolder directory, string folder, OpenMode openMode, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return OpenFolder(directory, folder, openMode);
        }

        /// <summary>
        /// Sets the user's storage folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Whether the folder was set (true) or not (false).</returns>
        /// <exception cref="System.NotImplementedException">
        /// Exception representing that this function is not implemented.
        /// </exception>
        public virtual bool SetStorageUser(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously sets the user's storage folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="cancellationToken">The cancellation token. Defaults to null.</param>
        /// <returns>
        /// A boolean task to set the user's storage folder. The boolean represents whether the
        /// folder was set (true) or not (false).
        /// </returns>
        public async Task<bool> SetStorageUserAsync(string path, CancellationToken? cancellationToken = null)
        {
            await AwaitHelpers.CreateTaskScheduler(AwaitHelpers.CheckCancellationToken(cancellationToken));
            return SetStorageUser(path);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString() => "FenrirFS";

        #endregion Public Methods
    }
}