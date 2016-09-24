// ***********************************************************************
// Assembly         : FenrirFS
// Component        : ConversionHelpers.cs
// Author           : vonderborch
// Created          : 09-24-2016
// 
// Version          : 2.0.0
// Last Modified By : vonderborch
// Last Modified On : 09-24-2016
// ***********************************************************************
// <copyright file="ConversionHelpers.cs">
//		Copyright ©  2016
// </copyright>
// <summary>
//      Defines the helpers for conversions between System.IO objects and FenrirFS objects
// </summary>
//
// Changelog: 
//            - 2.0.0 (09-24-2016) - Beta version.
// ***********************************************************************
using System;
using IO = System.IO;

namespace FenrirFS.Helpers
{
    /// <summary>
    /// Defines static functions to help with conversions.
    /// </summary>
    public static class ConversionHelpers
    {
        #region Public Methods

        /// <summary>
        /// Converts the attributes from System.IO.FileAttributes to FenrirFS.FileAttributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns>FileAttributes.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static FileAttributes ConvertAttributes(IO.FileAttributes attributes)
        {
            FileAttributes output = FileAttributes.None;
            if ((attributes & IO.FileAttributes.Archive) == IO.FileAttributes.Archive)
                output = output | FileAttributes.Archive;

            if ((attributes & IO.FileAttributes.Compressed) == IO.FileAttributes.Compressed)
                output = output | FileAttributes.Compressed;

            if ((attributes & IO.FileAttributes.Device) == IO.FileAttributes.Device)
                output = output | FileAttributes.Device;

            if ((attributes & IO.FileAttributes.Directory) == IO.FileAttributes.Directory)
                output = output | FileAttributes.Directory;

            if ((attributes & IO.FileAttributes.Encrypted) == IO.FileAttributes.Encrypted)
                output = output | FileAttributes.Encrypted;

            if ((attributes & IO.FileAttributes.Hidden) == IO.FileAttributes.Hidden)
                output = output | FileAttributes.Hidden;

            if ((attributes & IO.FileAttributes.IntegrityStream) == IO.FileAttributes.IntegrityStream)
                output = output | FileAttributes.IntegrityStream;

            if ((attributes & IO.FileAttributes.Normal) == IO.FileAttributes.Normal)
                output = output | FileAttributes.Normal;

            if ((attributes & IO.FileAttributes.NoScrubData) == IO.FileAttributes.NoScrubData)
                output = output | FileAttributes.NoScrubData;

            if ((attributes & IO.FileAttributes.NotContentIndexed) == IO.FileAttributes.NotContentIndexed)
                output = output | FileAttributes.NotContentIndexed;

            if ((attributes & IO.FileAttributes.Offline) == IO.FileAttributes.Offline)
                output = output | FileAttributes.Offline;

            if ((attributes & IO.FileAttributes.ReadOnly) == IO.FileAttributes.ReadOnly)
                output = output | FileAttributes.ReadOnly;

            if ((attributes & IO.FileAttributes.ReparsePoint) == IO.FileAttributes.ReparsePoint)
                output = output | FileAttributes.ReparsePoint;

            if ((attributes & IO.FileAttributes.SparseFile) == IO.FileAttributes.SparseFile)
                output = output | FileAttributes.SparseFile;

            if ((attributes & IO.FileAttributes.System) == IO.FileAttributes.System)
                output = output | FileAttributes.System;

            if ((attributes & IO.FileAttributes.Temporary) == IO.FileAttributes.Temporary)
                output = output | FileAttributes.Temporary;

            return output & ~FileAttributes.None;
        }

        /// <summary>
        /// Converts an attribute to System.IO.FileAttributes from FenrirFS.FileAttributes.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns>The converted file attribute.</returns>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static IO.FileAttributes? ConvertAttributeToImplementation(FileAttributes attribute)
        {
            switch (attribute)
            {
                case FileAttributes.Archive: return IO.FileAttributes.Archive;
                case FileAttributes.Compressed: return IO.FileAttributes.Compressed;
                case FileAttributes.Device: return IO.FileAttributes.Device;
                case FileAttributes.Directory: return IO.FileAttributes.Directory;
                case FileAttributes.Encrypted: return IO.FileAttributes.Encrypted;
                case FileAttributes.Hidden: return IO.FileAttributes.Hidden;
                case FileAttributes.IntegrityStream: return IO.FileAttributes.IntegrityStream;
                case FileAttributes.Normal: return IO.FileAttributes.Normal;
                case FileAttributes.NoScrubData: return IO.FileAttributes.NoScrubData;
                case FileAttributes.NotContentIndexed: return IO.FileAttributes.NotContentIndexed;
                case FileAttributes.ReadOnly: return IO.FileAttributes.ReadOnly;
                case FileAttributes.ReparsePoint: return IO.FileAttributes.ReparsePoint;
                case FileAttributes.SparseFile: return IO.FileAttributes.SparseFile;
                case FileAttributes.System: return IO.FileAttributes.System;
                case FileAttributes.Temporary: return IO.FileAttributes.Temporary;
            }

            return null;
        }

        /// <summary>
        /// Converts the file access from FenrirFS.FileAccess to System.IO.FileAccess
        /// </summary>
        /// <param name="fileAccess">The file access.</param>
        /// <returns>The file accesss.</returns>
        /// <exception cref="Exception">Invalid FileAccess conversion!</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static IO.FileAccess ConvertFileAccess(FileAccess fileAccess)
        {
            switch (fileAccess)
            {
                case FileAccess.Read: return IO.FileAccess.Read;
                case FileAccess.ReadWrite: return IO.FileAccess.ReadWrite;
                case FileAccess.Write: return IO.FileAccess.Write;
                default: throw new Exception("Invalid FileAccess conversion!");
            }
        }

        /// <summary>
        /// Converts the file access from FenrirFS.FileMode to System.IO.FileMode
        /// </summary>
        /// <param name="fileMode">The file mode.</param>
        /// <returns>The file mode.</returns>
        /// <exception cref="Exception">Invalid FileMode conversion!</exception>
        ///  Changelog:
        ///             - 2.0.0 (09-24-2016) - Beta version.
        public static IO.FileMode ConvertFileMode(FileMode fileMode)
        {
            switch (fileMode)
            {
                case FileMode.Append: return IO.FileMode.Append;
                case FileMode.Create: return IO.FileMode.Create;
                case FileMode.CreateNew: return IO.FileMode.CreateNew;
                case FileMode.Open: return IO.FileMode.Open;
                case FileMode.OpenOrCreate: return IO.FileMode.OpenOrCreate;
                case FileMode.Truncate: return IO.FileMode.Truncate;
                default: throw new Exception("Invalid FileMode conversion!");
            }
        }

        #endregion Public Methods
    }
}