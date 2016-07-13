using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using IO = System.IO;

namespace FenrirFS
{
    public abstract class FSFile : IDisposable, IEquatable<FSFile>
    {
        public static implicit operator string(FSFile file)
        {
            return file.FullPath;
        }

        public FSFile(string path)
        {
            Name = IO.Path.GetFileNameWithoutExtension(path);
            Path = IO.Path.GetDirectoryName(path);
            Extension = IO.Path.GetExtension(path);
        }

        public FSFile(string path, string name, string extension)
        {
            Name = name;
            Path = path;
            Extension = extension;
        }

        public FileAccess StreamFileAccessMode { get; private set; }
        public FileMode StreamFileMode { get; private set; }

        public FileAttributes FileAttributes
        {
            get { return GetFileAttributes(); }
        }

        public string ParentFolderPath
        {
            get { return IO.Path.GetDirectoryName(Path); }
        }

        public FSFolder ParentFolder
        {
            get { return FS.GetFolder(IO.Path.GetDirectoryName(Path)); }
        }

        public string RootFolderPath
        {
            get { return IO.Path.GetPathRoot(Path); }
        }

        public FSFolder RootFolder
        {
            get { return FS.GetFolder(IO.Path.GetPathRoot(Path)); }
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
        public string Extension { get; private set; }

        public string FullPath
        {
            get { return IO.Path.Combine(Path, $"{Name}.{Extension}"); }
        }

        public IO.Stream Stream { get; private set; }

        public bool IsFileOpen
        {
            get { return Stream != null; }
        }

        public bool EOS
        {
            get
            {
                return IsFileOpen
                    ? Stream.Length - 1 == Stream.Position
                    : false;
            }
        }
        
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~FSFile() {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            //   Dispose(false);
            // }

            // This code added to correctly implement the disposable pattern.

            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        public bool Equals(FSFile other)
        {
            return this.FullPath == other.FullPath;
        }

        public override bool Equals(object obj)
        {
            return obj != null
                ? FullPath == obj.ToString()
                : false;
        }

        public override int GetHashCode()
        {
            return FullPath.GetHashCode();
        }

        public override string ToString()
        {
            return FullPath;
        }

        public Encoding Encoding
        {
            get { return GetEncoding(); }
        }

        public long FileSize
        {
            get { return GetFileSize(); }
        }

        public DateTime CreationTime
        {
            get { return GetCreationTime(false); }
        }

        public DateTime CreationTimeUtc
        {
            get { return GetCreationTime(true); }
        }

        public DateTime LastAccessedTime
        {
            get { return GetLastAccessedTime(false); }
        }

        public DateTime LastAccessedTimeUtc
        {
            get { return GetLastAccessedTime(true); }
        }

        public DateTime LastModifiedTime
        {
            get { return GetLastModifiedTime(false); }
        }

        public DateTime LastModifiedTimeUtc
        {
            get { return GetLastModifiedTime(true); }
        }


        public abstract FileAttributes GetFileAttributes();

        public abstract bool SetFileAttributes(FileAttributes attributes);

        public abstract long GetFileSize();

        public abstract DateTime GetCreationTime(bool useUtc = false);
        public abstract DateTime GetLastAccessedTime(bool useUtc = false);

        public abstract DateTime GetLastModifiedTime(bool useUtc = false);

        public abstract Encoding GetEncoding();

        public abstract bool SetEncoding(Encoding encoding);

        public abstract bool ChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        public bool Clear()
        {
            return false;
        }

        public abstract bool Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        public abstract bool Delete();

        public abstract bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        public abstract string ReadAll();

        public abstract XDocument ReadAllAsXDocument();

        public abstract string[] ReadAllLines();

        public abstract IEnumerable<string> ReadLine();

        public abstract bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists);

        public abstract bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate);

        public abstract bool WriteLine(string contents, WriteMode writeMode = WriteMode.Truncate);











        public bool Close()
        {
            if (IsFileOpen)
            {
                try
                {
                    switch (StreamFileAccessMode)
                    {
                        case FileAccess.ReadWrite:
                        case FileAccess.Write:
                            Stream.Flush();
                            break;
                    }

                    Stream.Dispose();
                    StreamFileAccessMode = FileAccess.None;
                    StreamFileMode = FileMode.None;
                    Stream = null;
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public abstract IO.Stream Open(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate);

        public string StreamRead(int chars)
        {
            if (!IsFileOpen && StreamFileAccessMode != FileAccess.Write)
            {
                byte[] buffer = new byte[chars];

                Stream.Read(buffer, 0, chars);

                return Encoding.GetString(buffer, 0, chars);
            }

            return null;
        }

        public string StreamReadAll()
        {
            if (IsFileOpen && StreamFileAccessMode != FileAccess.Write)
            {
                byte[] buffer = new byte[Stream.Length];

                Stream.Read(buffer, 0, buffer.Length);

                return Encoding.GetString(buffer, 0, buffer.Length);
            }

            return null;
        }

        public string StreamReadLine()
        {
            if (IsFileOpen && StreamFileAccessMode != FileAccess.Write)
            {
                StringBuilder str = new StringBuilder();

                int value = -1;
                do
                {
                    value = Stream.ReadByte();
                    if (value != -1)
                    {
                        char c = Convert.ToChar((byte)value);
                        str.Append(c);

                        switch (c)
                        {
                            case '\n': value = -1; break;
                            case '\r':
                                value = Stream.ReadByte();
                                char nc = Convert.ToChar((byte)value);
                                str.Append(nc);
                                if (nc == '\n')
                                    value = -1;
                                break;
                        }
                    }
                } while (value != -1);

                return str.ToString();
            }

            return null;
        }

        public bool StreamSetPosition(int position)
        {
            if (IsFileOpen && position >= 0 && position < Stream.Length)
            {
                Stream.Position = position;

                return true;
            }

            return false;
        }

        public bool StreamWrite(string contents)
        {
            if (IsFileOpen && StreamFileAccessMode != FileAccess.Read)
            {
                byte[] buffer = new byte[contents.Length];

                buffer = Encoding.GetBytes(contents);

                Stream.Write(buffer, 0, buffer.Length);

                return true;
            }

            return false;
        }

        public bool StreamWriteLine(string contents)
        {
            if (IsFileOpen && StreamFileAccessMode != FileAccess.Read && contents.Length == int.MaxValue)
            {
                byte[] buffer = new byte[contents.Length + 1];

                buffer = Encoding.GetBytes($"{contents}{Environment.NewLine}");

                Stream.Write(buffer, 0, buffer.Length);

                return true;
            }

            return false;
        }
    }
}
