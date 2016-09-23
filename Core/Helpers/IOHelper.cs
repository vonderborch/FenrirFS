using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO = System.IO;

namespace FenrirFS.Helpers
{
    public static class IOHelper
    {
#if IMPLEMENTATION
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

        public static IO.FileAttributes? ConvertAttributesToImplementation(FileAttributes attribute)
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
#endif

        public static bool IsValidModeCombination(FileAccess fileAccess, FileMode fileMode)
        {
            if (fileMode == FileMode.None)
                return false;

            switch (fileAccess)
            {
                case FileAccess.None:
                    return false;

                case FileAccess.Read:
                    switch (fileMode)
                    {
                        case FileMode.Truncate:
                        case FileMode.Append: return false;
                        default: return true;
                    }
                case FileAccess.ReadWrite:
                    return true;

                case FileAccess.Write:
                    return true;
            }

            return false;
        }

        public static string GenerateUniquePath(string path, bool isFile, RenameMode renameMode = RenameMode.TimeStampTicks, int maxAttempts = 10)
        {
            // end early if we don't have to do anything...
            if (FS.Exists(path) == ExistenceCheckResult.None)
                return path;

            // split up the path...
            var directory = IO.Path.GetDirectoryName(path);
            var name = IO.Path.GetFileNameWithoutExtension(path);
            var extension = isFile ? IO.Path.GetExtension(path) : "";

            // depending on the rename mode...
            if (renameMode == RenameMode.Integer)
            {
                for (int i = 0; i < maxAttempts; i++)
                {
                    var newPath = IO.Path.Combine(directory, $"{name}-{i}{extension}");
                    if (FS.Exists(newPath) == ExistenceCheckResult.None)
                        return newPath;
                }
            }
            else if (renameMode == RenameMode.TimeStampTicks)
            {
                for (int i = 0; i < maxAttempts; i++)
                {
                    var newPath = IO.Path.Combine(directory, $"{name}-{DateTime.Now.Ticks}{extension}");
                    if (FS.Exists(newPath) == ExistenceCheckResult.None)
                        return newPath;
                }
            }

            // if all else fails, return the original path
            return path;
        }
    }
}
