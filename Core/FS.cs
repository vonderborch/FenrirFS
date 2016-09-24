// ***********************************************************************
// Assembly         : FenrirFS
// Component        : FS.cs
// Author           : vonderborch
// Created          : 07-12-2016
// 
// Version          : 1.1.0
// Last Modified By : vonderborch
// Last Modified On : 07-14-2016
// ***********************************************************************
// <copyright file="FS.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Access point for getting a File or Folder object.
// </summary>
//
// Changelog: 
//            - 1.1.0 (07-14-2016) - Added OpenMode parameter to GetFile and GetFolder functions and added a new version of both to handle both constructors.
//            - 1.0.0 (07-12-2016) - Initial version created.
// ***********************************************************************
using FenrirFS.FileSystem;
using FenrirFS.Static;
using System.Collections.Generic;
using IO = System.IO;

namespace FenrirFS
{
    /// <summary>
    /// Class FS.
    /// </summary>
    public static class FS
    {
        #region Public Methods

        public static ExistenceCheckResult Exists(string path)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

#if IMPLEMENTATION
            byte result = IO.File.Exists(path) ? (byte)1 : (byte)0;
            result += IO.Directory.Exists(path) ? (byte)2 : (byte)0;

            switch (result)
            {
                case 3: return ExistenceCheckResult.FileAndFolderExists;
                case 2: return ExistenceCheckResult.FolderExists;
                case 1: return ExistenceCheckResult.FileExists;
                default: return ExistenceCheckResult.None;
            }
#else
            return ExistenceCheckResult.None;
#endif
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode.</param>
        /// <returns>FenrirFS.FSFile.</returns>
        ///  Changelog:
        ///             - 1.1.0 (07-14-2016) - Added OpenMode parameter
        ///             - 1.0.0 (07-12-2016) - Initial version.
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
        /// Gets the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="openMode">The open mode.</param>
        /// <returns>FSFile.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-14-2016) - Initial version.
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
        /// Gets the folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode.</param>
        /// <returns>FSFolder.</returns>
        ///  Changelog:
        ///             - 1.1.0 (07-14-2016) - Added OpenMode parameter
        ///             - 1.0.0 (07-12-2016) - Initial version.
        public static FSFolder GetDirectory(string path, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));

#if CORE
            return new NullFolder(path);
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
            return new FenrirFolder(path);
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="openMode">The open mode.</param>
        /// <returns>FSFolder.</returns>
        ///  Changelog:
        ///             - 1.0.0 (07-14-2016) - Initial version.
        public static FSFolder GetFolder(string path, string name, OpenMode openMode = OpenMode.CreateIfDoesNotExist)
        {
            Helpers.Validation.NotNullOrWhiteSpaceCheck(path, nameof(path));
            Helpers.Validation.NotNullOrWhiteSpaceCheck(name, nameof(name));

#if CORE
            return new NullFolder(path, name);
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
            return new FenrirFolder(path, name);
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }


        public static FSFolder GetCurrentDirectory()
        {
#if CORE
            return new NullFolder("");
#elif IMPLEMENTATION
            return new FenrirFolder(IO.Directory.GetCurrentDirectory());
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

        /// <summary>
        /// Gets the root directories.
        /// </summary>
        /// <returns>FSFolder.</returns>
        ///  Changelog:
        ///             - 1.0.0 (09-22-2016) - Initial version.
        public static List<FSFolder> GetRootDirectories()
        {
#if CORE
            return new List<FSFolder>();
#elif IMPLEMENTATION
            var roots = IO.Directory.GetLogicalDrives();
            var output = new List<FSFolder>();
            for (int i = 0; i < roots.Length; i++)
                output.Add(new FenrirFolder(roots[i]));

            return output;
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }

#endregion Public Methods
    }
}