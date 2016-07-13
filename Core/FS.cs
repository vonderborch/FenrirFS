using FenrirFS.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FenrirFS
{
    public static class FS
    {
        public static FSFile GetFile()
        {
#if CORE
            return null;
#elif IMPLEMENTATION
            return null;
#else
            throw new NotSupportedException("There is no File implementation on the current platform!");
#endif
        }

        public static FSFolder GetFolder()
        {
#if CORE
            return null;
#elif IMPLEMENTATION
            return null;
#else
            throw new NotSupportedException("There is no Folder implementation on the current platform!");
#endif
        }
    }
}
