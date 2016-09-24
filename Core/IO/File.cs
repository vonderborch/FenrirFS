// ***********************************************************************
// Assembly         : FenrirFS
// Component        : File.cs
// Author           : vonderborch
// Created          : 09-22-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="File.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the File static class.
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta Version.
// ***********************************************************************
using FenrirFS.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using IO = System.IO;

namespace FenrirFS.Static
{
    /// <summary>
    /// Provides static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of FileStream objects.
    /// </summary>
    public static class File
    {
        #region Public Methods

        /// <summary>
        /// Appends all lines in the enumerable to the file.
        /// </summary>
        /// <param name="file">The file to append to.</param>
        /// <param name="lines">The lines to append.</param>
        /// <returns><c>true</c> if the append succeeded, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool AppendAllLines(string file, IEnumerable<string> lines)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<IEnumerable<string>>(lines, nameof(lines));

            bool result = false;
            using (var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist))
            {
                var str = new StringBuilder();
                foreach (var line in lines)
                    str.AppendLine(line);
                result = f.WriteAll(str.ToString(), WriteMode.Append);
            }

            return result;
        }

        /// <summary>
        /// Appends the contents in the enumerable to the file.
        /// </summary>
        /// <param name="file">The file to append to.</param>
        /// <param name="contents">The contents to append.</param>
        /// <returns><c>true</c> if the append succeeded, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool AppendAllText(string file, string contents)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string>(contents, nameof(contents));

            bool result = false;
            using (var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist))
                result = f.WriteAll(contents, WriteMode.Append);

            return result;
        }

        /// <summary>
        /// Copies the file at the source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if a file at the destination already exists.</param>
        /// <returns><c>true</c> if the copy succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool Copy(string source, string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            if (Exists(destination))
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, true);
                        break;
                    case FileCollisionOption.OpenIfExists:
                        return true;
                    case FileCollisionOption.ReplaceExisting:
                        Delete(destination);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"File [{destination}] already exists!");
                }
            }

            bool result = false;
            using (var s = FS.GetFile(source, OpenMode.ThrowIfDoesNotExist))
                result = s.Copy(destination, FileCollisionOption.ThrowIfExists);

            return result;
        }

        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="file">The full path of the file.</param>
        /// <param name="collisionOption">The collision option if the file already exists.</param>
        /// <returns><c>true</c> if the creation succeeds, <c>false</c> otherwise.</returns>
        /// <exception cref="IO.IOException">File [{file}] already exists.</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool Create(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        file = IOHelper.GenerateUniquePath(file, true);
                        break;
                    case FileCollisionOption.OpenIfExists:
                        return true;
                    case FileCollisionOption.ReplaceExisting:
                        Delete(file);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"File [{file}] already exists!");
                }
            }

            return FS.GetFile(file, OpenMode.CreateIfDoesNotExist) != null;
        }

        /// <summary>
        /// Creates the specified file.
        /// </summary>
        /// <param name="file">The full path of the file.</param>
        /// <param name="collisionOption">The collision option if the file already exists.</param>
        /// <returns>The created file.</returns>
        /// <exception cref="IO.IOException">File [{file}] already exists.</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static FSFile CreateFile(string file, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        return null;
                    case FileCollisionOption.GenerateUniqueName:
                        file = IOHelper.GenerateUniquePath(file, true);
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        Delete(file);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"File [{file}] already exists!");
                }
            }

            return FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
        }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="file">The file to delete.</param>
        /// <returns><c>true</c> if the deletion succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool Delete(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.CreateIfDoesNotExist).Delete();
        }

        /// <summary>
        /// Determines whether a file exists at the specified path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns><c>true</c> if the file exists, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool Exists(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ReturnNullIfDoesNotExist) != null;
        }

        /// <summary>
        /// Gets the creation time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The creation time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static DateTime GetCreationTime(string file, bool useUtc = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetCreationTime(useUtc);
        }

        /// <summary>
        /// Gets the attributes of the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The attributes of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static FileAttributes GetFileAttributes(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetFileAttributes();
        }

        /// <summary>
        /// Gets the encoding of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The encoding of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static Encoding GetFileEncoding(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetEncoding();
        }

        /// <summary>
        /// Gets the last accessed time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last accessed time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static DateTime GetLastAccessedTime(string file, bool useUtc = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetLastAccessedTime(useUtc);
        }

        /// <summary>
        /// Gets the last modified time of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="useUtc">if set to <c>true</c> [use UTC].</param>
        /// <returns>The last modified time of the file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static DateTime GetLastModifiedTime(string file, bool useUtc = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).GetLastModifiedTime(useUtc);
        }

        /// <summary>
        /// Moves the file at the specified source to the destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="collisionOption">The collision option to use if the destination already exists.</param>
        /// <returns><c>true</c> if the move succeeds, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool Move(string source, string destination, FileCollisionOption collisionOption = FileCollisionOption.FailIfExists)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));

            if (Exists(destination))
            {
                switch (collisionOption)
                {
                    case FileCollisionOption.FailIfExists:
                        return false;
                    case FileCollisionOption.GenerateUniqueName:
                        destination = IOHelper.GenerateUniquePath(destination, true);
                        break;
                    case FileCollisionOption.ReplaceExisting:
                        Delete(destination);
                        break;
                    case FileCollisionOption.ThrowIfExists:
                        throw new IO.IOException($"File [{destination}] already exists!");
                }
            }

            return FS.GetFile(source, OpenMode.ThrowIfDoesNotExist).Move(destination, FileCollisionOption.ThrowIfExists);
        }

        /// <summary>
        /// Opens the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>IO.Stream.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static IO.Stream Open(string file, FileAccess fileAccess = FileAccess.ReadWrite, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
                return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).Open(fileAccess, fileMode);

            return null;
        }

        /// <summary>
        /// Opens the read.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>IO.Stream.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static IO.Stream OpenRead(string file, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
                return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).Open(FileAccess.Read, fileMode);

            return null;
        }

        /// <summary>
        /// Opens the read write.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>IO.Stream.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static IO.Stream OpenReadWrite(string file, WriteMode writeMode = WriteMode.Truncate, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
                return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).Open(FileAccess.ReadWrite, fileMode);

            return null;
        }

        /// <summary>
        /// Opens the write.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>IO.Stream.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static IO.Stream OpenWrite(string file, WriteMode writeMode = WriteMode.Truncate, FileMode fileMode = FileMode.OpenOrCreate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
                return FS.GetFile(file, OpenMode.ThrowIfDoesNotExist).Open(FileAccess.Write, fileMode);

            return null;
        }

        /// <summary>
        /// Reads all bytes.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>System.Byte[].</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static byte[] ReadAllBytes(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            var output = new byte[0];
            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                {
                    if (!f.IsFileOpen)
                        f.Open(FileAccess.Read, FileMode.Open);

                    long length = f.Stream.Length;
                    long position = 0;
                    output = new byte[length];
                    var tmp = new byte[0];
                    while (length > 0)
                    {
                        var tmpLength = (int)(Int32.MaxValue - length);
                        if (tmpLength < 0)
                            tmpLength = (int)length;
                        tmp = new byte[tmpLength];

                        f.Stream.Read(tmp, 0, tmpLength);
                        for (int i = 0; i < tmpLength; i++)
                            output[position++] = tmp[i];

                        length -= tmpLength;
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Reads all lines.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>System.String[].</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static string[] ReadAllLines(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            var output = new string[0];
            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                    output = f.ReadAllLines();
            }

            return output;
        }

        /// <summary>
        /// Reads all text.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>System.String.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static string ReadAllText(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            var output = String.Empty;
            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                    output = f.ReadAll();
            }

            return output;
        }

        /// <summary>
        /// Reads the line.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static IEnumerable<string> ReadLine(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                    f.ReadLine();
            }

            return null;
        }

        /// <summary>
        /// Reads the lines list.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static List<string> ReadLinesList(string file)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));

            var output = new string[0];
            if (Exists(file))
            {
                using (var f = FS.GetFile(file, OpenMode.ThrowIfDoesNotExist))
                    output = f.ReadAllLines();
            }

            return new List<string>(output);
        }

        /// <summary>
        /// Replaces the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="backup">The backup.</param>
        /// <param name="overwriteBackup">if set to <c>true</c> [overwrite backup].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool Replace(string source, string destination, string backup, bool overwriteBackup = false)
        {
            Validation.NotNullOrWhiteSpaceCheck(source, nameof(source));
            Validation.NotNullOrWhiteSpaceCheck(destination, nameof(destination));
            Validation.NotNullOrWhiteSpaceCheck(backup, nameof(backup));

            if (Exists(source))
            {
                Copy(destination, backup, overwriteBackup ? FileCollisionOption.ReplaceExisting : FileCollisionOption.FailIfExists);

                return Copy(source, destination, FileCollisionOption.ReplaceExisting);
            }

            return false;
        }

        /// <summary>
        /// Writes all bytes.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool WriteAllBytes(string file, byte[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<byte[]>(contents, nameof(contents));

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(Convert.ToString(contents), writeMode);
        }

        /// <summary>
        /// Writes all lines.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool WriteAllLines(string file, string[] contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string[]>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(str.ToString(), writeMode);
        }

        /// <summary>
        /// Writes all lines.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool WriteAllLines(string file, IEnumerable<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<IEnumerable<string>>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(str.ToString(), writeMode);
        }

        /// <summary>
        /// Writes all lines.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool WriteAllLines(string file, List<string> contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<List<string>>(contents, nameof(contents));

            var str = new StringBuilder();
            foreach (var line in contents)
                str.AppendLine(line);

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(str.ToString(), writeMode);
        }

        /// <summary>
        /// Writes all text.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="writeMode">The write mode.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta Version.
        public static bool WriteAllText(string file, string contents, WriteMode writeMode = WriteMode.Truncate)
        {
            Validation.NotNullOrWhiteSpaceCheck(file, nameof(file));
            Validation.NotNullCheck<string>(contents, nameof(contents));

            var f = FS.GetFile(file, OpenMode.CreateIfDoesNotExist);
            return f.WriteAll(contents, writeMode);
        }

        #endregion Public Methods
    }
}