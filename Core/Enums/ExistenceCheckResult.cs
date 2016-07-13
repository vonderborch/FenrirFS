using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FenrirFS
{
    public enum ExistenceCheckResult
    {
        None = 0,

        FileExists = 1,

        FolderExists = 2,

        FileAndFolderExists = 3
    }
}
