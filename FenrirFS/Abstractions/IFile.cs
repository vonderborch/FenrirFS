using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    /// <summary>
    /// Represents a file in FenrirFS.
    /// </summary>
    public interface IFile : IDisposable, IEquatable<IFile>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the encoding for the file.
        /// </summary>
        /// <value>
        /// The encoding.
        /// </value>
        Encoding Encoding { get; set; }

        /// <summary>
        /// Gets a value indicating whether the Stream is at the end-of-stream.
        /// </summary>
        /// <value><c>true</c> if end-of-stream; otherwise, <c>false</c>.</value>
        bool EOS { get; }

        /// <summary>
        /// Gets the file's extension.
        /// </summary>
        /// <value>The extension.</value>
        string Extension { get; }

        /// <summary>
        /// Gets the file access of the current stream.
        /// </summary>
        /// <value>The file access.</value>
        FileAccess FileAccess { get; }

        /// <summary>
        /// Gets the file mode of the current stream.
        /// </summary>
        /// <value>The file mode.</value>
        FileMode FileMode { get; }

        /// <summary>
        /// Gets the full path to the file.
        /// </summary>
        /// <value>The full path.</value>
        string FullPath { get; }

        /// <summary>
        /// Gets a value indicating whether the Stream is open.
        /// </summary>
        /// <value><c>true</c> if this instance is open; otherwise, <c>false</c>.</value>
        bool IsOpen { get; }

        /// <summary>
        /// Gets the file's name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the file's path.
        /// </summary>
        /// <value>The path.</value>
        string Path { get; }

        /// <summary>
        /// Gets the file's Stream, or null if it a stream isn't opened.
        /// </summary>
        /// <value>The stream.</value>
        Stream Stream { get; }

        #endregion Properties

        #region Methods

        bool ChangeExtension(string extension, CollisionOption collisionOption);

        Task<bool> ChangeExtensionAsync(string extension, CollisionOption collisionOption, CancellationToken cancellationToken);

        bool Clear();

        Task<bool> ClearAsync(CancellationToken cancellationToken);

        bool Close();

        Task<bool> CloseAsync(CancellationToken cancellationToken);

        IFile Copy(string destination, CollisionOption collisionOption);

        Task<IFile> CopyAsync(string destination, CollisionOption collisionOption, CancellationToken cancellationToken);

        bool Delete();

        Task<bool> DeleteAsync(CancellationToken cancellationToken);

        bool Move(string destination, CollisionOption collisionOption);

        Task<bool> MoveAsync(string destination, CollisionOption collisionOption, CancellationToken cancellationToken);

        Stream Open(FileAccess fileAccess, FileMode fileMode);

        Task<Stream> OpenAsync(FileAccess fileAccess, FileMode fileMode, CancellationToken cancellationToken);

        string ReadAll();

        Task<string> ReadAllAsync(CancellationToken cancellationToken);

        bool Rename(string name, CollisionOption collisionOption);

        Task<bool> RenameAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken);

        string StreamRead(int chars);

        string StreamReadAll();

        Task<string> StreamReadAllAsync(CancellationToken cancellationToken);

        Task<string> StreamReadAsync(int chars, CancellationToken cancellationToken);

        string StreamReadLine();

        Task<string> StreamReadLineAsync(CancellationToken cancellationToken);

        bool StreamSetPosition(int position);

        Task<bool> StreamSetPositionAsync(int position, CancellationToken cancellationToken);

        bool StreamWrite(string contents);

        Task<bool> StreamWriteAsync(string contents, CancellationToken cancellationToken);

        bool StreamWriteLine(string line);

        Task<bool> StreamWriteLineAsync(string line, CancellationToken cancellationToken);

        bool WriteAll(string contents, WriteMode writeMode);

        Task<bool> WriteAllAsync(string contents, WriteMode writeMode, CancellationToken cancellationToken);

        bool WriteLine(string line, WriteMode writeMode);

        Task<bool> WriteLineAsync(string line, WriteMode writeMode, CancellationToken cancellationToken);

        #endregion Methods
    }
}