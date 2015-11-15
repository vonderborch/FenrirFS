/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace FenrirFS.Desktop
{
    /// <summary>
    /// An implementation of an AFile.
    /// </summary>
    public class FenrirFile : AFile
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FenrirFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FenrirFile(string path) : base(path)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        public override DateTime CreationTime
        {
            get { return System.IO.File.GetCreationTime(FullPath); }
        }

        /// <summary>
        /// Gets the UTC creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        public override DateTime CreationTimeUtc
        {
            get { return System.IO.File.GetCreationTimeUtc(FullPath); }
        }

        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        /// <value>
        /// The encoding.
        /// </value>
        public override Encoding Encoding
        {
            get
            {
                if (encoding != null)
                    return encoding;
                else if (!Fenrir.FileSystem.FileExists(FullPath))
                {
                    encoding = Fenrir.FileSystem.DefaultEncoding;
                }
                else
                {
                    if (!IsOpen)
                    {
                        var str = Open(FileAccess.Read, FileMode.OpenOrCreate);

                        using (StreamReader stream = new StreamReader(str))
                        {
                            stream.Peek();
                            encoding = stream.CurrentEncoding;
                        }

                        Close();
                    }
                    else
                    {
                        using (StreamReader stream = new StreamReader(Stream))
                        {
                            stream.Peek();
                            encoding = stream.CurrentEncoding;
                        }
                    }
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
                        Open(FileAccess.ReadWrite, FileMode.OpenOrCreate);
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

        /// <summary>
        /// Gets or sets the file attributes.
        /// </summary>
        /// <value>
        /// The file attributes.
        /// </value>
        public override FileAttributes FileAttributes
        {
            get { return FenrirHelpers.SystemFileAttributesToFenrirFileAttributes(System.IO.File.GetAttributes(FullPath)); }

            set { System.IO.File.SetAttributes(FullPath, FenrirHelpers.FenrirFileAttributesToSystemFileAttributes(value)); }
        }

        /// <summary>
        /// Gets the last accessed time.
        /// </summary>
        /// <value>
        /// The last accessed time.
        /// </value>
        public override DateTime LastAccessedTime
        {
            get { return System.IO.File.GetLastAccessTime(FullPath); }
        }

        /// <summary>
        /// Gets the UTC last accessed time.
        /// </summary>
        /// <value>
        /// The last accessed time.
        /// </value>
        public override DateTime LastAccessedTimeUtc
        {
            get { return System.IO.File.GetLastAccessTimeUtc(FullPath); }
        }

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        /// <value>
        /// The last modified time.
        /// </value>
        public override DateTime LastModifiedTime
        {
            get { return System.IO.File.GetLastWriteTime(FullPath); }
        }

        /// <summary>
        /// Gets the UTC last modified time.
        /// </summary>
        /// <value>
        /// The last modified time.
        /// </value>
        public override DateTime LastModifiedTimeUtc
        {
            get { return System.IO.File.GetLastWriteTimeUtc(FullPath); }
        }

        /// <summary>
        /// Gets the parent folder for the file.
        /// </summary>
        /// <value>
        /// The parent folder.
        /// </value>
        public override string ParentFolder
        {
            get { return System.IO.Path.GetDirectoryName(FullPath); }
        }

        /// <summary>
        /// Gets the root folder for the file.
        /// </summary>
        /// <value>
        /// The root folder.
        /// </value>
        public override string RootFolder
        {
            get { return System.IO.Path.GetPathRoot(FullPath); }
        }

        /// <summary>
        /// Gets the size of the file, in bytes.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public override long Size
        {
            get { return new FileInfo(FullPath).Length; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Changes the extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <param name="collisionOption">The collision option.</param>
        /// <returns></returns>
        public override bool ChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyCheck(extension, nameof(extension));
            
            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyException(extension, nameof(extension));

                string newPath = System.IO.Path.Combine(Path, Name, extension);
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            return false;
                        break;

                    case FileCollisionOption.GenerateUniqueName:
                        newPath = Fenrir.FileSystem.GenerateFileUniqueName(newPath);
                        break;

                    case FileCollisionOption.ReplaceExisting:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            System.IO.File.Delete(newPath);
                        break;
                }

                System.IO.File.Copy(FullPath, newPath);
                System.IO.File.Delete(FullPath);
                Extension = extension;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Copies the System.IO.File.
        /// </summary>
        /// <param name="destination">The full path of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the copied System.IO.File.</returns>
        public override AFile Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyException(destination, nameof(destination));

                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(destination))
                            return null;
                        break;

                    case FileCollisionOption.GenerateUniqueName:
                        destination = Fenrir.FileSystem.GenerateFileUniqueName(destination);
                        break;
                }

                System.IO.File.Copy(FullPath, destination, collisionOption == FileCollisionOption.ReplaceExisting);
                return new FenrirFile(destination);
            }

            return null;
        }

        /// <summary>
        /// Copies the System.IO.File.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the copied System.IO.File.</returns>
        public override AFile Copy(AFolder destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (!IsOpen)
            {
                Exceptions.NotNullCheck<AFolder>(destinationPath, nameof(destinationPath));
                Exceptions.NotNullOrEmptyCheck(destinationName, nameof(destinationName));

                string destination = System.IO.Path.Combine(destinationPath.ToString(), destinationName);

                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(destination))
                            return null;
                        break;

                    case FileCollisionOption.GenerateUniqueName:
                        destination = Fenrir.FileSystem.GenerateFileUniqueName(destination);
                        break;
                }

                System.IO.File.Copy(FullPath, destination, collisionOption == FileCollisionOption.ReplaceExisting);
                return new FenrirFile(destination);
            }

            return null;
        }

        /// <summary>
        /// Copies the System.IO.File.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>An AFile representing the copied System.IO.File.</returns>
        public override AFile Copy(string destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyCheck(destinationPath, nameof(destinationPath));
                Exceptions.NotNullOrEmptyCheck(destinationName, nameof(destinationName));

                string destination = System.IO.Path.Combine(destinationPath, destinationName);

                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(destination))
                            return null;
                        break;

                    case FileCollisionOption.GenerateUniqueName:
                        destination = Fenrir.FileSystem.GenerateFileUniqueName(destination);
                        break;
                }

                System.IO.File.Copy(FullPath, destination, collisionOption == FileCollisionOption.ReplaceExisting);
                return new FenrirFile(destination);
            }

            return null;
        }

        /// <summary>
        /// Deletes the System.IO.File.
        /// </summary>
        /// <returns>Whether the file was deleted (true) or not (false).</returns>
        public override bool Delete()
        {
            if (!IsOpen)
            {
                if (Fenrir.FileSystem.FileExists(FullPath))
                {
                    System.IO.File.Delete(FullPath);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Moves the System.IO.File.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>Whether the file was moved or not.</returns>
        public override bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyException(destination, nameof(destination));

                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(destination))
                            return false;
                        break;

                    case FileCollisionOption.GenerateUniqueName:
                        destination = Fenrir.FileSystem.GenerateFileUniqueName(destination);
                        break;

                    case FileCollisionOption.ReplaceExisting:
                        if (Fenrir.FileSystem.FileExists(destination))
                            System.IO.File.Delete(destination);
                        break;
                }

                System.IO.File.Move(FullPath, destination);
                SetupFile(destination);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves the System.IO.File.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>Whether the file was moved or not.</returns>
        public override bool Move(AFolder destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (!IsOpen)
            {
                Exceptions.NotNullCheck<AFolder>(destinationPath, nameof(destinationPath));
                Exceptions.NotNullOrEmptyException(destinationName, nameof(destinationName));

                string newPath = System.IO.Path.Combine(destinationPath.ToString(), destinationName);
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            return false;
                        break;

                    case FileCollisionOption.GenerateUniqueName:
                        newPath = Fenrir.FileSystem.GenerateFileUniqueName(newPath);
                        break;

                    case FileCollisionOption.ReplaceExisting:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            System.IO.File.Delete(newPath);
                        break;
                }

                System.IO.File.Move(FullPath, newPath);
                SetupFile(newPath);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves the System.IO.File.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>Whether the file was moved or not.</returns>
        public override bool Move(string destinationPath, string destinationName, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyException(destinationPath, nameof(destinationPath));
                Exceptions.NotNullOrEmptyException(destinationName, nameof(destinationName));

                string newPath = System.IO.Path.Combine(destinationPath, destinationName);
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            return false;
                        break;

                    case FileCollisionOption.GenerateUniqueName:
                        newPath = Fenrir.FileSystem.GenerateFileUniqueName(newPath);
                        break;

                    case FileCollisionOption.ReplaceExisting:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            System.IO.File.Delete(newPath);
                        break;
                }

                System.IO.File.Move(FullPath, newPath);
                SetupFile(newPath);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Opens the file to the Stream.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>A reference to the Stream.</returns>
        /// <exception cref="System.Exception">Invalid File Access and File Mode parameters!</exception>
        public override Stream Open(FileAccess fileAccess, FileMode fileMode)
        {
            // Close the current Stream, if its open
            Close();

            // Check for valid options
            if (!FSHelpers.IsValidFileModeFileAccessOptions(fileAccess, fileMode))
                throw new Exception("Invalid File Access and File Mode parameters!");

            // Open a Stream with the options
            //var str = System.IO.File.Open(FullPath, FenrirHelpers.FenrirFileModeToSystemFileMode(fileMode), FenrirHelpers.FenrirFileAccessToSystemFileAccess(fileAccess));
            Stream = System.IO.File.Open(FullPath, FenrirHelpers.FenrirFileModeToSystemFileMode(fileMode), FenrirHelpers.FenrirFileAccessToSystemFileAccess(fileAccess));

            // Set FileAccess and FileMode
            FileAccessMode = fileAccess;
            FileMode = fileMode;

            return Stream;
        }

        /// <summary>
        /// Reads all the contents of the System.IO.File.
        /// </summary>
        /// <returns>A string representing the contents of the System.IO.File.</returns>
        public override string ReadAll()
        {
            return System.IO.File.ReadAllText(FullPath);
        }

        /// <summary>
        /// Reads all as the contents of the file as an XDocument.
        /// </summary>
        /// <returns>An XDocument representing the contents of the System.IO.File.</returns>
        public override XDocument ReadAllAsXElement()
        {
            string contents = System.IO.File.ReadAllText(FullPath);

            return XDocument.Parse(contents);
        }

        /// <summary>
        /// Reads all lines in the System.IO.File.
        /// </summary>
        /// <returns>An array of strings, each item representing a line in the System.IO.File.</returns>
        public override string[] ReadAllLines()
        {
            return System.IO.File.ReadAllLines(FullPath);
        }

        /// <summary>
        /// Reads a line.
        /// </summary>
        /// <returns>A string representing a line in the System.IO.File.</returns>
        public override IEnumerable<string> ReadLine()
        {
            return System.IO.File.ReadLines(FullPath);
        }

        /// <summary>
        /// Renames the System.IO.File.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option. Defaults to FailIfExists.</param>
        /// <returns>Whether the file was renamed (true) or not (false).</returns>
        public override bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Exceptions.NotNullOrEmptyCheck(name, nameof(name));

            if (!IsOpen)
            {
                Exceptions.NotNullOrEmptyException(name, nameof(name));

                string newPath = System.IO.Path.Combine(Path, name, Extension);
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            return false;
                        break;

                    case FileCollisionOption.GenerateUniqueName:
                        newPath = Fenrir.FileSystem.GenerateFileUniqueName(newPath);
                        break;

                    case FileCollisionOption.ReplaceExisting:
                        if (Fenrir.FileSystem.FileExists(newPath))
                            System.IO.File.Delete(newPath);
                        break;
                }

                System.IO.File.Copy(FullPath, newPath);
                System.IO.File.Delete(FullPath);
                Name = name;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads from the stream.
        /// </summary>
        /// <param name="chars">The number of chars to read.</param>
        /// <returns>A string representing the read characters.</returns>
        public override string StreamRead(int chars)
        {
            if (chars < 0)
                throw new ArgumentOutOfRangeException(nameof(chars));

            if (IsOpen && FileAccessMode != FileAccess.Write)
            {
                byte[] buffer = new byte[chars];

                Stream.Read(buffer, 0, chars);

                return Encoding.GetString(buffer);
            }

            return null;
        }

        /// <summary>
        /// Reads all contents from the stream.
        /// </summary>
        /// <returns>A string representing all the contents in the stream.</returns>
        public override string StreamReadAll()
        {
            if (IsOpen && FileAccessMode != FileAccess.Write)
            {
                byte[] buffer = new byte[Stream.Length];

                Stream.Read(buffer, 0, buffer.Length);

                return Encoding.GetString(buffer);
            }

            return null;
        }

        /// <summary>
        /// Reads a line from the stream.
        /// </summary>
        /// <returns>A string representing a line.</returns>
        public override string StreamReadLine()
        {
            if (IsOpen && FileAccessMode != FileAccess.Write)
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

        /// <summary>
        /// Sets the position of the stream.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>Whether the position was set (true) or not (false).</returns>
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

        /// <summary>
        /// Writes contents to the stream.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns>Whether the stream was written to (true) or not (false).</returns>
        public override bool StreamWrite(string contents)
        {
            Exceptions.NotNullOrEmptyCheck(contents, nameof(contents));

            if (IsOpen && FileAccessMode != FileAccess.Read)
            {
                byte[] buffer = new byte[contents.Length];

                buffer = Encoding.GetBytes(contents);

                Stream.Write(buffer, 0, buffer.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes a line to the stream.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>Whether the line was written (true) or not (false).</returns>
        public override bool StreamWriteLine(string line)
        {
            Exceptions.NotNullOrEmptyCheck(line, nameof(line));

            if (IsOpen && FileAccessMode != FileAccess.Read)
            {
                line += FSHelpers.LineSeparator;
                byte[] buffer = new byte[line.Length];

                buffer = Encoding.GetBytes(line);

                Stream.Write(buffer, 0, buffer.Length);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes contents to the System.IO.File.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode. Defaults to Truncate.</param>
        /// <returns>Whether the contents were written (true) or not (false).</returns>
        public override bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Exceptions.NotNullOrEmptyCheck(contents, nameof(contents));

            if (!IsOpen)
            {
                switch (writeMode)
                {
                    case WriteMode.Append:
                        if (Fenrir.FileSystem.FileExists(FullPath))
                        {
                            contents = System.IO.File.ReadAllText(FullPath, Encoding) + contents;
                        }

                        break;
                }
                System.IO.File.WriteAllText(FullPath, contents, Encoding);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes a line to the System.IO.File.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="writeMode">The write mode. Defaults to Append.</param>
        /// <returns>Whether the line was written (true) or not (false).</returns>
        public override bool WriteLine(string line, WriteMode writeMode = WriteMode.Append)
        {
            Exceptions.NotNullOrEmptyCheck(line, nameof(line));

            if (!IsOpen)
            {
                line += FSHelpers.LineSeparator;

                switch (writeMode)
                {
                    case WriteMode.Append:
                        if (Fenrir.FileSystem.FileExists(FullPath))
                        {
                            line = System.IO.File.ReadAllText(FullPath, Encoding) + line;
                        }

                        break;
                }
                System.IO.File.WriteAllText(FullPath, line, Encoding);

                return true;
            }

            return false;
        }

        #endregion Public Methods
    }
}