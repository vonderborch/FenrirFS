using FenrirFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingCore;

namespace TestingDesktop
{
    class Program
    {
        static void Main(string[] args)
        {
            //string userPath = Fenrir.FileSystem.StorageUser.FullPath;
            //File.Create(userPath + "\\test.txt").Dispose();

            FileTests.TestSuite();

        }
    }
}
