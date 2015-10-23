using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLD.FenrirFS
{
    public interface IFileSystem
    {
        IFolder StorageLocal { get; }
        IFolder StorageRoaming { get; }
        IFolder StorageExecutable { get; }
        IFolder StorageUser { get; set; }

        IFile GetFileFromPath(string path);
        IFolder GetFolderFromPath(string path);

        IFile CreateFile(string directory, string name, CollisionOption option);
        IFile CreateFile(IFolder directory, string name, CollisionOption option);
        IFolder CreateFolder(string directory, string name, CollisionOption option);
        IFolder CreateFolder(IFolder directory, string name, CollisionOption option);

        bool FileExists(string path);
        bool FolderExists(string path);
        ExistenceCheckResult Exists(string path);


    }
}
