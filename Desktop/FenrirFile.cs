using System;
using System.IO;
using System.Text;

namespace FenrirFS
{
    public class FenrirFile : AFile
    {
        #region Public Constructors

        public FenrirFile(string path) : base(path)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override bool ChangeExtension(string extension, CollisionOption collisionOption = CollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyCheck(extension, nameof(extension));

            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyException(extension, nameof(extension));

                string newPath = System.IO.Path.Combine(Path, Name, extension);
                switch (collisionOption)
                {
                    case CollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            return false;
                        break;

                    case CollisionOption.GenerateUniqueName:
                        newPath = Fenrir.FileSystem.GenerateFileUniqueName(newPath);
                        break;

                    case CollisionOption.ReplaceExisting:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            File.Delete(newPath);
                        break;
                }

                File.Copy(FullPath, newPath);
                File.Delete(FullPath);
                Extension = extension;
                return true;
            }

            return false;
        }

        public override IFile Copy(string destination, CollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(destination, nameof(destination));

            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyException(destination, nameof(destination));

                switch (collisionOption)
                {
                    case CollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(destination))
                            return null;
                        break;

                    case CollisionOption.GenerateUniqueName:
                        destination = Fenrir.FileSystem.GenerateFileUniqueName(destination);
                        break;
                }

                File.Copy(FullPath, destination, collisionOption == CollisionOption.ReplaceExisting);
                return new FenrirFile(destination);
            }

            return null;
        }

        public override bool Delete()
        {
            if (!IsOpen)
            {
                if (Fenrir.FileSystem.FileExists(FullPath))
                {
                    File.Delete(FullPath);
                    return true;
                }
            }

            return false;
        }

        public override bool Move(string destination, CollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(destination, nameof(destination));

            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyException(destination, nameof(destination));

                string newPath = System.IO.Path.Combine(destination, Name, Extension);
                switch (collisionOption)
                {
                    case CollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            return false;
                        break;

                    case CollisionOption.GenerateUniqueName:
                        newPath = Fenrir.FileSystem.GenerateFileUniqueName(newPath);
                        break;

                    case CollisionOption.ReplaceExisting:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            File.Delete(newPath);
                        break;
                }

                File.Move(FullPath, newPath);
                SetupFile(newPath);
                return true;
            }

            return false;
        }

        public override Stream Open(FileAccess fileAccess, FileMode fileMode)
        {
            // Close the current Stream, if its open
            Close();

            // Check for valid options
            if (Helpers.IsValidFileModeFileAccessOptions(fileAccess, fileMode))
                throw new Exception("Invalid File Access and File Mode parameters!");

            // Open a Stream with the options
            Stream = File.Open(FullPath, FenrirHelpers.FenrirFileModeToSystemFileMode(fileMode), FenrirHelpers.FenrirFileAccessToSystemFileAccess(fileAccess));

            // ensure encoding is set properly
            Encoding = Encoding;

            // Set FileAccess and FileMode
            FileAccess = fileAccess;
            FileMode = fileMode;

            return Stream;
        }

        public override string ReadAll()
        {
            return File.ReadAllText(FullPath);
        }

        public override bool Rename(string name, CollisionOption collisionOption)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyException(name, nameof(name));

                string newPath = System.IO.Path.Combine(Path, name, Extension);
                switch (collisionOption)
                {
                    case CollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            return false;
                        break;

                    case CollisionOption.GenerateUniqueName:
                        newPath = Fenrir.FileSystem.GenerateFileUniqueName(newPath);
                        break;

                    case CollisionOption.ReplaceExisting:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            File.Delete(newPath);
                        break;
                }

                File.Copy(FullPath, newPath);
                File.Delete(FullPath);
                Name = name;
                return true;
            }

            return false;
        }

        public override string StreamRead(int chars)
        {
            if (chars < 0)
                throw new ArgumentOutOfRangeException(nameof(chars));

            if (IsOpen && FileAccess != FileAccess.Write)
            {
                byte[] buffer = new byte[chars];

                Stream.Read(buffer, 0, chars);

                return Encoding.GetString(buffer);
            }

            return null;
        }

        public override string StreamReadAll()
        {
            if (IsOpen && FileAccess != FileAccess.Write)
            {
                byte[] buffer = new byte[Stream.Length];

                Stream.Read(buffer, 0, buffer.Length);

                return Encoding.GetString(buffer);
            }

            return null;
        }

        public override string StreamReadLine()
        {
            if (IsOpen && FileAccess != FileAccess.Write)
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

        public override bool StreamSetPosition(int position)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            if (IsOpen && position >= 0 && position < Stream.Length)
            {
                Stream.Position = position;
                return true;
            }

            return false;
        }

        public override bool StreamWrite(string contents)
        {
            Exceptions.NotNullOrEmptyCheck(contents, nameof(contents));

            if (IsOpen && FileAccess != FileAccess.Read)
            {
                byte[] buffer = new byte[contents.Length];

                buffer = Encoding.GetBytes(contents);

                Stream.Write(buffer, 0, buffer.Length);

                return true;
            }

            return false;
        }

        public override bool StreamWriteLine(string line)
        {
            Exceptions.NotNullOrEmptyCheck(line, nameof(line));

            if (IsOpen && FileAccess != FileAccess.Read)
            {
                line += Helpers.LineSeparator;
                byte[] buffer = new byte[line.Length];

                buffer = Encoding.GetBytes(line);

                Stream.Write(buffer, 0, buffer.Length);

                return true;
            }

            return false;
        }

        public override bool WriteAll(string contents, WriteMode writeMode)
        {
            Exceptions.NotNullOrEmptyCheck(contents, nameof(contents));

            if (!IsOpen)
            {
                switch (writeMode)
                {
                    case WriteMode.Append:
                        if (Fenrir.FileSystem.FileExists(FullPath))
                        {
                            contents = File.ReadAllText(FullPath, Encoding) + contents;
                        }

                        break;
                }
                File.WriteAllText(FullPath, contents, Encoding);

                return true;
            }

            return false;
        }

        public override bool WriteLine(string line, WriteMode writeMode)
        {
            Exceptions.NotNullOrEmptyCheck(line, nameof(line));

            if (!IsOpen)
            {
                line += Helpers.LineSeparator;

                switch (writeMode)
                {
                    case WriteMode.Append:
                        if (Fenrir.FileSystem.FileExists(FullPath))
                        {
                            line = File.ReadAllText(FullPath, Encoding) + line;
                        }

                        break;
                }
                File.WriteAllText(FullPath, line, Encoding);

                return true;
            }

            return false;
        }

        #endregion Public Methods
    }
}