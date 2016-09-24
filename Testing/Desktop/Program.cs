using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FenrirFS;
using TestingCore;
using System.IO;

namespace Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = FS.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");

            Console.ReadKey();

            BasicFileTests.TestSuite();

            Console.ReadKey();
        }
    }
}
