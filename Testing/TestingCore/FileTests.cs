using FenrirFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingCore
{
    public static class FileTests
    {
        public static bool CreateFile()
        {
            string userPath = Fenrir.FileSystem.StorageUser.FullPath;
            Fenrir.FileSystem.CreateFile(userPath + "\\test.txt", FileCollisionOption.ReplaceExisting);
            Fenrir.FileSystem.CreateFile(userPath, "test2.txt", FileCollisionOption.ReplaceExisting);
            Fenrir.FileSystem.CreateFile(Fenrir.FileSystem.GetFolderFromPath(userPath), "test3.txt", FileCollisionOption.ReplaceExisting);
            return false;
        }
    }
}
