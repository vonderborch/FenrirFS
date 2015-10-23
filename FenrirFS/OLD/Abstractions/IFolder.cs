using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLD.FenrirFS
{
    public interface IFolder
    {
        #region Public Properties
        
        /// <summary>
        /// Gets the full path of the file.
        /// </summary>
        /// <value>The full path.</value>
        string FullPath { get; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the path of the file.
        /// </summary>
        /// <value>The path.</value>
        string Path { get; }

        #endregion Public Properties

        IFile CreateFile(string name, CollisionOption option);
        IFile CreateFileAsync(string name, CollisionOption option, CancellationToken cancellationToken);

        IFolder CreateFolder(string name, CollisionOption option);

        IFile GetFile(string name);

        List<IFile> GetFiles();

        IFolder GetFolder(string name);
        IFolder GetParentFolder();
        List<IFolder> GetFolders();

        bool FileExists(string name);
        bool FolderExists(string name);

        bool Delete();
    }
}
