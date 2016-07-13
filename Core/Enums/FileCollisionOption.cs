using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FenrirFS
{
    public enum FileCollisionOption
    {
        GenerateUniqueName = 0,
        
        ReplaceExisting = 1,
        
        FailIfExists = 2,

        ThrowIfExists = 3,

        OpenIfExists = 4
    }
}
