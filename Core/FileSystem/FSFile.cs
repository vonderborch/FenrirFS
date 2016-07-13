using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get { return null; }
            set { }
        }











    }
}
