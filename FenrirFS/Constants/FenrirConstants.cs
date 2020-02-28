using System;
using System.Collections.Generic;
using System.Text;

namespace FenrirFS
{
    public class FenrirConstants
    {
        private static readonly FenrirConstants instance = new FenrirConstants();

        private FenrirConstants()
        {
        }

        public static FenrirConstants Instance => instance;

        public string DirectoryNameUniquePathTimestampFormat { get; set; } = "yyyy-MM-dd_hh-mm-ss-fff";

        public string FileNameUniquePathTimestampFormat { get; set; } = "yyyy-MM-dd_hh-mm-ss-fff";

        public string UniquePathNameFormat { get; set; } = "{0}-{1}";
    }
}
