// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FenrirFile.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="FenrirFile.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the FenrirFile class, an implementation of a the FSFile class.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using IO = System.IO;

namespace FenrirFS
{
    /// <summary>
    /// Defines the implementation version of an FSFile.
    /// </summary>
    /// <seealso cref="FenrirFS.FSFile" />
    public class FenrirFile : FSFile
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FenrirFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FenrirFile(string path) : base(path)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FenrirFile"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        /// <param name="extension">The extension.</param>
        public FenrirFile(string path, string name, string extension) : base(path, name, extension)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether the [file exists].
        /// </summary>
        /// <value><c>true</c> if the [file exists]; otherwise, <c>false</c>.</value>
        public override bool Exists
        {
            get { return IO.File.Exists(FullPath); }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Changes the extension of the file.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <param name="collisionOption">The collision option, if another file already exists.</param>
        /// <returns><c>true</c> if the extension change succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool ChangeExtension(string extension, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrEmptyCheck(extension, nameof(extension));

            if (!IsFileOpen)
            {
                string newFullPath = IO.Path.Combine(Path, $"{Name}.{extension}");
                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;

                    case FileCollisionOption.GenerateUniqueName:
                        newFullPath = IOHelper.GenerateUniquePath(newFullPath, true);
                        break;

                    case FileCollisionOption.ReplaceExisting:
                        if (IO.File.Exists(newFullPath))
                            IO.File.Delete(newFullPath);
                        break;

                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{newFullPath}] already exists!");
                }

                if (Exists)
                    IO.File.Move(this, newFullPath);
                FullPath = newFullPath;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Copies the file to the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Copy(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrEmptyCheck(destination, nameof(destination));

            bool exists = IO.File.Exists(destination);
            if (exists)
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;

                    case FileCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, true);
                        break;

                    case FileCollisionOption.ReplaceExisting:
                        if (IO.File.Exists(destination))
                            IO.File.Delete(destination);
                        break;

                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{destination}] already exists!");
                }
            }

            IO.File.Copy(this, destination);
            return true;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <returns><c>true</c> if the deletion succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Delete()
        {
            if (!IsFileOpen)
            {
                if (Exists)
                {
                    IO.File.Delete(this);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the creation time of the file.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The creation time.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetCreationTime(bool useUtc = false)
        {
            return !Exists
                ? DateTime.MinValue
                : useUtc
                    ? IO.File.GetCreationTimeUtc(this)
                    : IO.File.GetCreationTime(this);
        }

        /// <summary>
        /// Gets the encoding of the file.
        /// </summary>
        /// <returns>The encoding of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override Encoding GetEncoding()
        {
            if (Exists)
            {
                try
                {
                    using (IO.StreamReader reader = new IO.StreamReader(this, Encoding.UTF8, true))
                    {
                        reader.Peek();
                        encoding = reader.CurrentEncoding;
                        encodingHasSet = true;
                    }

                    return encoding;
                }
                catch
                {
                    encoding = Encoding.Default;
                    encodingHasSet = true;
                }
            }

            return Encoding.Default;
        }

        /// <summary>
        /// Gets the file attributes.
        /// </summary>
        /// <returns>The file attributes.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override FileAttributes GetFileAttributes()
        {
            if (Exists)
            {
                var attributes = IO.File.GetAttributes(this);
                return ConversionHelpers.ConvertAttributes(attributes);
            }

            return FileAttributes.None;
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <returns>The size of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override long GetFileSize()
        {
            if (Exists)
            {
                return new IO.FileInfo(this).Length;
            }

            return long.MinValue;
        }

        /// <summary>
        /// Gets the last accessed time of the file.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last accessed time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetLastAccessedTime(bool useUtc = false)
        {
            return !Exists
                ? DateTime.MinValue
                : useUtc
                    ? IO.File.GetLastAccessTimeUtc(this)
                    : IO.File.GetLastAccessTime(this);
        }

        /// <summary>
        /// Gets the last modified time of the file.
        /// </summary>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last modified time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override DateTime GetLastModifiedTime(bool useUtc = false)
        {
            return !Exists
                ? DateTime.MinValue
                : useUtc
                    ? IO.File.GetLastWriteTimeUtc(this)
                    : IO.File.GetLastWriteTime(this);
        }

        /// <summary>
        /// Moves the file to the specified specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a collision occurs.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Move(string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            if (!IsFileOpen)
            {
                Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;

                    case FileCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, true);
                        break;

                    case FileCollisionOption.ReplaceExisting:
                        if (IO.File.Exists(destination))
                            IO.File.Delete(destination);
                        break;

                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{destination}] already exists!");
                }

                if (Exists)
                    IO.File.Move(this, destination);
                FullPath = destination;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Opens the file with the specified access and file mode.
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>A stream representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override IO.Stream Open(FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            if (Exists)
            {
                Close();

                if (!IOHelper.IsValidModeCombination(fileAccess, fileMode))
                    throw new Exception("Invalid File Access and File Mode parameter combination!");

                Stream = IO.File.Open(this, ConversionHelpers.ConvertFileMode(fileMode), ConversionHelpers.ConvertFileAccess(fileAccess));

                StreamFileAccessMode = fileAccess;
                StreamFileMode = fileMode;

                return Stream;
            }

            return null;
        }

        /// <summary>
        /// Reads all the contents of the file.
        /// </summary>
        /// <returns>The contents of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override string ReadAll()
        {
            return Exists
                    ? IO.File.ReadAllText(this)
                    : "";
        }

        /// <summary>
        /// Reads the contents of the file as an XDocument.
        /// </summary>
        /// <returns>An XDocument representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override XDocument ReadAllAsXDocument()
        {
            if (Exists)
            {
                var contents = IO.File.ReadAllText(this);
                try
                {
                    return XDocument.Parse(contents);
                }
                catch
                {
                    return new XDocument();
                }
            }

            return new XDocument();
        }

        /// <summary>
        /// Reads the contents of the file and returns it as a string array.
        /// </summary>
        /// <returns>A string array representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override string[] ReadAllLines()
        {
            return Exists
                    ? IO.File.ReadAllLines(this)
                    : new string[0];
        }

        /// <summary>
        /// Reads the contents of the file and returns a string enumerable.
        /// </summary>
        /// <returns>An enumerable representing the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override IEnumerable<string> ReadLines()
        {
            return Exists
                ? IO.File.ReadLines(this)
                : new List<string>();
        }

        /// <summary>
        /// Removes an attribute from the file.
        /// </summary>
        /// <param name="attribute">The attribute to remove.</param>
        /// <returns><c>true</c> if the attribute was removed, <c>false</c> otherwise.</returns>
        /// <exception cref="NotImplementedException"></exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Initial version.
        public override bool RemoveAttribute(FileAttributes attribute)
        {
            if (!IsFileOpen && Exists)
            {
                var convertedAttribute = ConversionHelpers.ConvertAttributeToImplementation(attribute);

                if (convertedAttribute != null)
                {
                    IO.File.SetAttributes(this, IO.File.GetAttributes(this) | ~(IO.FileAttributes)convertedAttribute);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Renames the file to the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="collisionOption">The collision option if a file with the new name already exists.</param>
        /// <returns><c>true</c> if the rename was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool Rename(string name, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrEmptyCheck(name, nameof(name));

            if (!IsFileOpen)
            {
                string newFullPath = IO.Path.Combine(Path, $"{name}.{Extension}");
                switch (collisionOption)
                {
                    case FileCollisionOption.OpenIfExists:
                    case FileCollisionOption.FailIfExists:
                        return false;

                    case FileCollisionOption.GenerateUniqueName:
                        newFullPath = IOHelper.GenerateUniquePath(newFullPath, true);
                        break;

                    case FileCollisionOption.ReplaceExisting:
                        if (IO.File.Exists(newFullPath))
                            IO.File.Delete(newFullPath);
                        break;

                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"A file with the new path [{newFullPath}] already exists!");
                }

                if (Exists)
                    IO.File.Move(FullPath, newFullPath);
                FullPath = newFullPath;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the encoding of the file.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns><c>true</c> if the encoding change was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool SetEncoding(Encoding encoding)
        {
            Validation.NotNullCheck<Encoding>(encoding, nameof(encoding));

            if (!IsFileOpen && Exists)
            {
                using (var writer = new IO.StreamWriter(this, true, encoding))
                    writer.Write("");

                this.encoding = encoding;
                encodingHasSet = true;
            }

            return false;
        }

        /// <summary>
        /// Adds an attribute to the file.
        /// </summary>
        /// <param name="attribute">The attribute to add.</param>
        /// <returns><c>true</c> if the attribute was added, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool SetFileAttribute(FileAttributes attribute)
        {
            if (!IsFileOpen && Exists)
            {
                var convertedAttribute = ConversionHelpers.ConvertAttributeToImplementation(attribute);

                if (convertedAttribute != null)
                {
                    IO.File.SetAttributes(this, IO.File.GetAttributes(this) | (IO.FileAttributes)convertedAttribute);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Writes the contents to the file.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode to use.</param>
        /// <returns><c>true</c> if the write was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool WriteAll(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullCheck<string>(contents, nameof(contents));

            if (!IsFileOpen)
            {
                switch (writeMode)
                {
                    case WriteMode.Append:
                        if (FS.Exists(this) == ExistenceCheckResult.FileExists)
                            contents = IO.File.ReadAllText(this, Encoding) + contents;
                        break;
                }
                IO.File.WriteAllText(this, contents, Encoding);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes the contents to the file and add a new line at the end.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode to use.</param>
        /// <returns><c>true</c> if the write was successful, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public override bool WriteLine(string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullCheck<string>(contents, nameof(contents));

            if (!IsFileOpen)
            {
                contents = $"{contents}{Environment.NewLine}";

                switch (writeMode)
                {
                    case WriteMode.Append:
                        if (FS.Exists(this) == ExistenceCheckResult.FileExists)
                            contents = IO.File.ReadAllText(this, Encoding) + contents;
                        break;
                }
                System.IO.File.WriteAllText(this, contents, Encoding);

                return true;
            }

            return false;
        }

        #endregion Public Methods
    }
}