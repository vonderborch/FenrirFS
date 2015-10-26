using System;

namespace FenrirFS.Desktop
{
    public static class FenrirHelpers
    {
        public static System.IO.FileMode FenrirFileModeToSystemFileMode(FileMode fileMode)
        {
            switch (fileMode)
            {
                case FileMode.Append:
                    return System.IO.FileMode.Append;
                case FileMode.Create:
                    return System.IO.FileMode.Create;
                case FileMode.CreateNew:
                    return System.IO.FileMode.CreateNew;
                case FileMode.Open:
                    return System.IO.FileMode.Open;
                case FileMode.OpenOrCreate:
                    return System.IO.FileMode.OpenOrCreate;
                case FileMode.Truncate:
                    return System.IO.FileMode.Truncate;
                default:
                    throw new ArgumentException("Unsupported FenrirFS Filemode!");
            }
        }

        public static System.IO.FileAccess FenrirFileAccessToSystemFileAccess(FileAccess fileAccess)
        {
            switch (fileAccess)
            {
                case FileAccess.Read:
                    return System.IO.FileAccess.Read;
                case FileAccess.Write:
                    return System.IO.FileAccess.Write;
                case FileAccess.ReadWrite:
                    return System.IO.FileAccess.ReadWrite;
                default:
                    throw new Exception("No valid exception!");
            }
        }
    }
}
