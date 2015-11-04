using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    /// <summary>
    /// Represents an abstract file in FenrirFS, with some implementations for AFile.
    /// </summary>
    public abstract class AFile : IDisposable, IEquatable<AFile>
    {
        #region Protected Fields

        protected bool disposedValue = false;
        protected Encoding encoding = null;

        #endregion Protected Fields

        #region Protected Constructors

        protected AFile(string path)
        {
            SetupFile(path);
            var enc = Encoding;
        }

        #endregion Protected Constructors

        #region Public Properties

        public virtual Encoding Encoding { get; set; }

        public bool EOS
        {
            get
            {
                return IsOpen
                    ? Stream.Length == Stream.Position
                    : false;
            }
        }

        public virtual string Extension { get; protected set; }
        public virtual FileAccess FileAccess { get; protected set; }
        public virtual FileMode FileMode { get; protected set; }

        public string FullPath
        {
            get
            {
                return System.IO.Path.Combine(Path,
                              String.Format("{0}{1}", Name, Extension));
            }
        }

        public bool IsOpen
        {
            get { return Stream != null; }
        }

        public virtual string Name { get; protected set; }
        public virtual string Path { get; protected set; }
        public virtual Stream Stream { get; protected set; }

        #endregion Public Properties

        #region Public Methods

        public virtual bool ChangeExtension(string extension, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangeExtensionAsync(string extension, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return ChangeExtension(extension, collisionOption);
        }

        public bool Clear()
        {
            if (!IsOpen)
                return WriteAll("", WriteMode.Truncate);
            return false;
        }

        public async Task<bool> ClearAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Clear();
        }

        public bool Close()
        {
            if (IsOpen)
            {
                switch (FileAccess)
                {
                    case FileAccess.ReadWrite:
                    case FileAccess.Write:
                        Stream.Flush();
                        break;
                }

                Stream.Dispose();
                FileAccess = FileAccess.None;
                FileMode = FileMode.None;
                Stream = null;
                return true;
            }

            return false;
        }

        public async Task<bool> CloseAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Close();
        }

        public virtual AFile Copy(string destination, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public virtual AFile Copy(string destinationPath, string destinationName, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<AFile> CopyAsync(string destination, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Copy(destination, collisionOption);
        }

        public async Task<AFile> CopyAsync(string destinationPath, string destinationName, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Copy(destinationPath, destinationName, collisionOption);
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

        public void Dispose()
        {
            //GC.SuppressFinalize(this);
            Dispose(true);
        }

        public override bool Equals(object obj)
        {
            return obj != null
                ? FullPath == obj.ToString()
                : false;
        }

        public bool Equals(AFile file)
        {
            return file.FullPath == FullPath;
        }

        public virtual bool Move(string destination, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MoveAsync(string destination, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Move(destination, collisionOption);
        }

        public virtual Stream Open(FileAccess fileAccess, FileMode fileMode)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> OpenAsync(FileAccess fileAccess, FileMode fileMode, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Open(fileAccess, FileMode);
        }

        public virtual string ReadAll()
        {
            throw new NotImplementedException();
        }

        public async Task<string> ReadAllAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return ReadAll();
        }

        public virtual bool Rename(string name, FileCollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RenameAsync(string name, FileCollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Rename(name, collisionOption);
        }

        public virtual string StreamRead(int chars)
        {
            throw new NotImplementedException();
        }

        public virtual string StreamReadAll()
        {
            throw new NotImplementedException();
        }

        public async Task<string> StreamReadAllAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return StreamReadAll();
        }

        public async Task<string> StreamReadAsync(int chars, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return StreamRead(chars);
        }

        public virtual string StreamReadLine()
        {
            throw new NotImplementedException();
        }

        public async Task<string> StreamReadLineAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return StreamReadLine();
        }

        public virtual bool StreamSetPosition(int position)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> StreamSetPositionAsync(int position, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return StreamSetPosition(position);
        }

        public virtual bool StreamWrite(string contents)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> StreamWriteAsync(string contents, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return StreamWrite(contents);
        }

        public virtual bool StreamWriteLine(string line)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> StreamWriteLineAsync(string line, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return StreamWriteLine(line);
        }

        public override string ToString()
        {
            return FullPath;
        }

        public virtual bool WriteAll(string contents, WriteMode writeMode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> WriteAllAsync(string contents, WriteMode writeMode, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return WriteAll(contents, writeMode);
        }

        public virtual bool WriteLine(string line, WriteMode writeMode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> WriteLineAsync(string line, WriteMode writeMode, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return WriteLine(line, writeMode);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && IsOpen)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                Stream.Dispose();

                disposedValue = true;
            }
        }

        protected void SetupFile(string path)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            Path = System.IO.Path.GetDirectoryName(path);
            Extension = System.IO.Path.GetExtension(path);
            Stream = null;
        }

        #endregion Protected Methods
    }
}