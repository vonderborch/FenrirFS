﻿using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FenrirFS
{
    /// <summary>
    /// Represents an abstract file in FenrirFS, with some implementations for IFile.
    /// </summary>
    public abstract class AFile : IFile
    {
        #region Protected Fields

        protected bool disposedValue = false;
        protected Encoding encoding = null;

        #endregion Protected Fields

        #region Protected Constructors

        protected AFile(string path)
        {
            SetupIFile(path);
            Encoding = Encoding;
        }

        #endregion Protected Constructors

        #region Public Properties

        public Encoding Encoding
        {
            get
            {
                if (encoding != null)
                    return encoding;
                else if (!Fenrir.FileSystem.FileExists(FullPath))
                {
                    encoding = System.Text.Encoding.UTF8;
                }
                else
                {
                    bool close = false;
                    if (!IsOpen)
                    {
                        Open(FileAccess.Read, FileMode.OpenOrCreate);
                        close = true;
                    }

                    using (StreamReader stream = new StreamReader(Stream))
                    {
                        stream.Peek();
                        encoding = stream.CurrentEncoding;
                    }

                    if (close)
                        Close();
                }

                return encoding;
            }

            set
            {
                if (Fenrir.FileSystem.FileExists(FullPath))
                {
                    bool close = false;
                    if (!IsOpen)
                    {
                        Open(FileAccess.Read, FileMode.OpenOrCreate);
                        close = true;
                    }

                    using (StreamWriter stream = new StreamWriter(Stream, value)) { }
                    encoding = value;

                    if (close)
                        Close();
                }
                encoding = value;
            }
        }

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
                              String.Format("{0}.{1}", Name, Extension));
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

        public virtual bool ChangeExtension(string extension, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangeExtensionAsync(string extension, CollisionOption collisionOption, CancellationToken cancellationToken)
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
                return true;
            }

            return false;
        }

        public async Task<bool> CloseAsync(CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Close();
        }

        public virtual IFile Copy(string destination, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<IFile> CopyAsync(string destination, CollisionOption collisionOption, CancellationToken cancellationToken)
        {
            await AwaitHelpers.CreateTaskScheduler(cancellationToken);
            return Copy(destination, collisionOption);
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

        public bool Equals(IFile file)
        {
            return file.FullPath == FullPath;
        }

        public virtual bool Move(string destination, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MoveAsync(string destination, CollisionOption collisionOption, CancellationToken cancellationToken)
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

        public virtual bool Rename(string name, CollisionOption collisionOption)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RenameAsync(string name, CollisionOption collisionOption, CancellationToken cancellationToken)
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

        protected void SetupIFile(string path)
        {
            Name = System.IO.Path.GetFileName(path);
            Path = System.IO.Path.GetDirectoryName(path);
            Extension = System.IO.Path.GetExtension(path);
        }

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

        #endregion Protected Methods
    }
}