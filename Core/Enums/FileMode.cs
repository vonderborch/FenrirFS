using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FenrirFS
{
    public enum FileMode
    {
        Append = 3,
        
        Create = 1,
        
        CreateNew = 2,
        
        Open = 4,
        
        OpenOrCreate = 5,
        
        Truncate = 0,
        
        None = -1
    }
}
