// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FS.cs
// Author           : vonderborch
// Created          : 07-12-2016
//
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="FS.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Access point for getting a File or Directory object.
// </summary>
//
// Changelog:
//            - 2.0.0 (09-24-2016) - Beta version.
//            - 2.0.0 (07-14-2016) - Added OpenMode parameter to GetFile and GetFolder functions and added a new version of both to handle both constructors.
//            - 2.0.0 (07-12-2016) - Initial version created.
// ***********************************************************************

using FenrirFS.FileSystem;
using System.Collections.Generic;
using IO = System.IO;

/// <summary>
/// The FenrirFS namespace.
/// </summary>
namespace FenrirFS
{
    /// <summary>
    /// Root File System class.
    /// </summary>
    public static class FS
    {
        #region Public Methods

        /// <summary>
        /// Determines whether a file system entry exists at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Whether a file or folder exists at the specified path.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static ExistenceCheckResult Exists(string path)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

#if IMPLEMENTATION
            // check if a file or folder with the name exists at the path and add the results.
            byte result = IO.File.Exists(path) ? (byte)1 : (byte)0;
            result += IO.Directory.Exists(path) ? (byte)2 : (byte)0;

            // return the result check depending on the results...
            switch (result)
            {
                case 3: return ExistenceCheckResult.FileAndFolderExists;
                case 2: return ExistenceCheckResult.FolderExists;
                case 1: return ExistenceCheckResult.FileExists;
                default: return ExistenceCheckResult.None;
            }
#else
            // if this is not an implementation, return that there are no results.
            return ExistenceCheckResult.None;
#endif
        }

        /// <summary>
        /// Gets or creates a file.
        /// </summary>
        /// <param name="path">The file's full path.</param>
        /// <param name="openMode">How to return the file if the file does not exist.</param>
        /// <returns>The desired file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FSFile GetFile(string path, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

#if CORE
            return new NullFile(path);
#elif IMPLEMENTATION
            if (!IO.File.Exists(path))
            {
                switch (openMode)
                {
                    case OpenMode.CreateIfDoesNotExist:
                        IO.File.Create(path).Close();
                        break;

                    case OpenMode.ReturnNullIfDoesNotExist:
                        return null;

                    case OpenMode.ThrowIfDoesNotExist:
                        throw new System.Exception("File does not exist!");
                }
            }
            return new FenrirFile(path);
#else
            throw new NotSupportedException("There is no File implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets or creates a file.
        /// </summary>
        /// <param name="path">The file's path.</param>
        /// <param name="name">The file's name.</param>
        /// <param name="extension">The file's extension.</param>
        /// <param name="openMode">How to return the file if the file does not exist.</param>
        /// <returns>The desired file.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FSFile GetFile(string path, string name, string extension, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Helpers.Validation.NotNullOrWhiteSpaceCheck(name, nameof(name));
            Helpers.Validation.NotNullOrWhiteSpaceCheck(extension, nameof(extension));

#if CORE
            return new NullFile(path, name, extension);
#elif IMPLEMENTATION
            var fullName = IO.Path.Combine(path, $"{name}{extension}");
            if (!IO.File.Exists(fullName))
            {
                switch (openMode)
                {
                    case OpenMode.CreateIfDoesNotExist:
                        IO.File.Create(fullName).Close();
                        break;

                    case OpenMode.ReturnNullIfDoesNotExist:
                        return null;

                    case OpenMode.ThrowIfDoesNotExist:
                        throw new System.Exception("File does not exist!");
                }
            }
            return new FenrirFile(path, name, extension);
#else
            throw new NotSupportedException("There is no File implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets or creates a directory.
        /// </summary>
        /// <param name="path">The directory's full path.</param>
        /// <param name="openMode">How to return the directory if the directory does not exist.</param>
        /// <returns>The desired directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FSDirectory GetDirectory(string path, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

#if CORE
            return new NullDirectory(path);
#elif IMPLEMENTATION
            if (!IO.Directory.Exists(path))
            {
                switch (openMode)
                {
                    case OpenMode.CreateIfDoesNotExist:
                        IO.Directory.CreateDirectory(path);
                        break;

                    case OpenMode.ReturnNullIfDoesNotExist:
                        return null;

                    case OpenMode.ThrowIfDoesNotExist:
                        throw new System.Exception("Directory does not exist!");
                }
            }
            return new FenrirDirectory(path);
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets or creates a directory.
        /// </summary>
        /// <param name="path">The directory's path.</param>
        /// <param name="name">The directory's path.</param>
        /// <param name="openMode">How to return the directory if the directory does not exist.</param>
        /// <returns>The desired directory.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FSDirectory GetDirectory(string path, string name, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Helpers.Validation.NotNullOrWhiteSpaceCheck(name, nameof(name));

#if CORE
            return new NullDirectory(path, name);
#elif IMPLEMENTATION
            var fullName = IO.Path.Combine(path, name);
            if (!IO.Directory.Exists(fullName))
            {
                switch (openMode)
                {
                    case OpenMode.CreateIfDoesNotExist:
                        IO.Directory.CreateDirectory(fullName);
                        break;

                    case OpenMode.ReturnNullIfDoesNotExist:
                        return null;

                    case OpenMode.ThrowIfDoesNotExist:
                        throw new System.Exception("File does not exist!");
                }
            }
            return new FenrirDirectory(path, name);
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets the current working directory of the application.
        /// </summary>
        /// <returns>The current working directory of the application.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FSDirectory GetCurrentDirectory()
        {
#if CORE
            return new NullDirectory("");
#elif IMPLEMENTATION
            return new FenrirDirectory(IO.Directory.GetCurrentDirectory());
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets the root directories of the system.
        /// </summary>
        /// <returns>A list of the system's root directories.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static List<FSDirectory> GetRootDirectories()
        {
#if CORE
            return new List<FSDirectory>();
#elif IMPLEMENTATION
            var roots = IO.Directory.GetLogicalDrives();
            var output = new List<FSDirectory>();
            for (int i = 0; i < roots.Length; i++)
                output.Add(new FenrirDirectory(roots[i]));

            return output;
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

        #endregion Public Methods
    }
}