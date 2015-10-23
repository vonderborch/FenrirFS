using FenrirFS.Helpers;
using System;

namespace OLD.FenrirFS
{
    public static class FileSystemCore
    {
        private static Lazy<IFileSystem> instance = new Lazy<IFileSystem>(() => CreateFileSystem(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static IFileSystem CurrentFileSystem
        {
            get
            {
                IFileSystem output = instance.Value;
                if (output == null)
                    throw Exceptions.NotImplementedInReferenceAssembly();

                return output;
            }
        }

        private static IFileSystem CreateFileSystem()
        {
#if UNIVERSAL || WINDOWS_PHONE
            return null;
#elif DESKTOP
            return Desktop.ImplementationFileSystem();
#else
            return null;
#endif
        }
    }
}
