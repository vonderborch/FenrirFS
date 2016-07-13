using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenrirFS
{
    public abstract class FSFolder
    {
        public static implicit operator string(FSFolder folder)
        {
            return folder.FullPath;
        }

        public FSFolder(string path)
        {

        }

        public FSFolder(string directory, string name, string extension)
        {

        }

        public string FullPath
        {
            get { return ""; }
        }
    }
}
