
#if FS

using FenrirFS;
using NUnit.Framework;
using System;
using System.IO;

#endif

namespace TestingCore
{
#if FS

    [TestFixture]
#endif

    public static class BasicFileTests
    {
        private const string CurrentDirectory = @"C:\Users\ricky\OneDrive\Personal Projects\FenrirFS\Testing\Desktop\bin\Debug\";

        private const string TestFile1Name = "test1.txt";
        private const string TestFile2Name = "test2.txt";
        private const string TestFile3Name = "test3.txt";
        private const string TestFile4Name = "test4.txt";

        private const string File1Text1 = "Hello world! ";
        private const string File1Text2 = "Goodbye world!";
        private const string File2Text1 = "Testing files stuffs\r\nHopefully this works?";
        private const string File2Text2 = "This\r\nis\r\nSPARTA!!!\r\n";
        private const string File3Text1 = "Carthago Delenda Est";
        private const string File3Text2 = "Carthago Delenda Est";
        private const string File3Text3 = "Carthago Delenda Est";
        private const string File4Text1 = "Lets see...";
        private const string File4Text2 = "If this works";

#if FS

        [Test]
#endif

        public static void TestSuite()
        {
            CreateFile();
            OpenFile();
            WriteToFile();
            ReadFile();
            DeleteFile();

            ///////// PASSED!
#if FS
            Assert.Pass("Passed");
#endif
        }

#if FS

        [Test]
#endif
        public static void CreateFile()
        {
#if FS
            var userPath = FS.GetDirectory(CurrentDirectory, OpenMode.CreateIfDoesNotExist);
            var file1Path = Path.Combine(userPath, TestFile1Name);
            var file2Path = Path.Combine(userPath, TestFile2Name);
            var file3Path = Path.Combine(userPath.ParentFolder, TestFile3Name);
            var file4Path = Path.Combine(userPath, TestFile4Name);

            ////////// FILE 1 - Full path
            var file1 = FS.GetFile(file1Path, OpenMode.CreateIfDoesNotExist);
            Assert.AreEqual(true, File.Exists(file1Path), "CreateFile - File 1 failed! {0}", file1.FullPath);

            ////////// FILE 2 - Path + Name + Extension
            var file2Name = Path.GetFileNameWithoutExtension(TestFile2Name);
            var file2Extension = Path.GetExtension(TestFile2Name);
            var file2 = FS.GetFile(userPath, file2Name, file2Extension, OpenMode.CreateIfDoesNotExist);
            Assert.AreEqual(true, File.Exists(file2Path), "CreateFile - File 2 failed! {0}", file2.FullPath);

            ////////// FILE 3 - Full path, Parent
            var file3 = FS.GetFile(file3Path, OpenMode.CreateIfDoesNotExist);
            Assert.AreEqual(true, File.Exists(file3Path), "CreateFile - File 3 failed! {0}", file3.FullPath);

            ////////// FILE 4 - Full path
            var file4 = FS.GetFile(file4Path, OpenMode.CreateIfDoesNotExist);
            Assert.AreEqual(true, File.Exists(file4Path), "CreateFile - File 4 failed! {0}", file4.FullPath);

#endif
        }

#if FS

        [Test]
#endif
        public static void OpenFile()
        {
#if FS
            var userPath = FS.GetDirectory(CurrentDirectory, OpenMode.CreateIfDoesNotExist);
            var file1Path = Path.Combine(userPath, TestFile1Name);
            var file2Path = Path.Combine(userPath, TestFile2Name);
            var file3Path = Path.Combine(userPath.ParentFolder, TestFile3Name);
            var file4Path = Path.Combine(userPath, TestFile4Name);

            ////////// FILE 1 - Full path
            var file1 = FS.GetFile(file1Path, OpenMode.ReturnNullIfDoesNotExist);
            Assert.AreEqual(true, file1 != null, "OpenFile - File 1 failed! {0}", file1.FullPath);

            ////////// FILE 2 - Path + Name + Extension
            var file2Name = Path.GetFileNameWithoutExtension(TestFile2Name);
            var file2Extension = Path.GetExtension(TestFile2Name);
            var file2 = FS.GetFile(userPath, file2Name, file2Extension, OpenMode.CreateIfDoesNotExist);
            Assert.AreEqual(true, file2 != null, "OpenFile - File 2 failed! {0}", file2.FullPath);

            ////////// FILE 3 - Directory, Parent
            var file3 = FS.GetFile(file3Path, OpenMode.ReturnNullIfDoesNotExist);
            Assert.AreEqual(true, file3 != null, "OpenFile - File 3 failed! {0}", file3.FullPath);

            ////////// FILE 4 - Full path
            var file4 = FS.GetFile(file4Path, OpenMode.ReturnNullIfDoesNotExist);
            Assert.AreEqual(true, file4 != null, "OpenFile - File 4 failed! {0}", file4.FullPath);

#endif
        }

#if FS

        [Test]
#endif
        public static void WriteToFile()
        {
#if FS
            var userPath = FS.GetDirectory(CurrentDirectory, OpenMode.CreateIfDoesNotExist);
            var file1Path = Path.Combine(userPath, TestFile1Name);
            var file2Path = Path.Combine(userPath, TestFile2Name);
            var file3Path = Path.Combine(userPath.ParentFolder, TestFile3Name);
            var file4Path = Path.Combine(userPath, TestFile4Name);

            ////////// FILE 1 - Full path
            var file1 = FS.GetFile(file1Path, OpenMode.ReturnNullIfDoesNotExist);
            Assert.AreEqual(true, file1.WriteAll(File1Text1, WriteMode.Append), "WriteToFile - File 1 failed 1! {0}", file1.FullPath);
            Assert.AreEqual(true, file1.WriteAll(File1Text2, WriteMode.Append), "WriteToFile - File 1 failed 2! {0}", file1.FullPath);

            ////////// FILE 2 - Path + Name + Extension
            var file2Name = Path.GetFileNameWithoutExtension(TestFile2Name);
            var file2Extension = Path.GetExtension(TestFile2Name);
            var file2 = FS.GetFile(userPath, file2Name, file2Extension, OpenMode.CreateIfDoesNotExist);
            Assert.AreEqual(true, file2.WriteAll(File2Text1, WriteMode.Truncate), "WriteToFile - File 2 failed 1! {0}", file2.FullPath);
            Assert.AreEqual(true, file2.WriteAll(File2Text2, WriteMode.Truncate), "WriteToFile - File 2 failed 2! {0}", file2.FullPath);

            ////////// FILE 3 - Directory, Parent
            var file3 = FS.GetFile(file3Path, OpenMode.ReturnNullIfDoesNotExist);
            Assert.AreEqual(true, file3.WriteLine(File3Text1, WriteMode.Append), "WriteToFile - File 3 failed 1! {0}", file3.FullPath);
            Assert.AreEqual(true, file3.WriteLine(File3Text2, WriteMode.Append), "WriteToFile - File 3 failed 2! {0}", file3.FullPath);
            Assert.AreEqual(true, file3.WriteLine(File3Text3, WriteMode.Append), "WriteToFile - File 3 failed 3! {0}", file3.FullPath);

            ////////// FILE 4 - Full path
            var file4 = FS.GetFile(file4Path, OpenMode.ReturnNullIfDoesNotExist);
            Assert.AreEqual(true, file4.WriteLine(File4Text1, WriteMode.Truncate), "WriteToFile - File 4 failed 1! {0}", file4.FullPath);
            Assert.AreEqual(true, file4.WriteLine(File4Text2, WriteMode.Truncate), "WriteToFile - File 4 failed 2! {0}", file4.FullPath);
#endif
        }

#if FS

        [Test]
#endif
        public static void ReadFile()
        {
#if FS
            var userPath = FS.GetDirectory(CurrentDirectory, OpenMode.CreateIfDoesNotExist);
            var file1Path = Path.Combine(userPath, TestFile1Name);
            var file2Path = Path.Combine(userPath, TestFile2Name);
            var file3Path = Path.Combine(userPath.ParentFolder, TestFile3Name);
            var file4Path = Path.Combine(userPath, TestFile4Name);
            string output;

            ////////// FILE 1 - Full path
            output = File1Text1 + File1Text2;
            var file1 = FS.GetFile(file1Path, OpenMode.ReturnNullIfDoesNotExist);
            Assert.AreEqual(output, file1.ReadAll(), "ReadFile - File 1 failed! {0}", file1.FullPath);

            ////////// FILE 2 - Path + Name + Extension
            output = File2Text2;
            var file2Name = Path.GetFileNameWithoutExtension(TestFile2Name);
            var file2Extension = Path.GetExtension(TestFile2Name);
            var file2 = FS.GetFile(userPath, file2Name, file2Extension, OpenMode.CreateIfDoesNotExist);
            Assert.AreEqual(output, file2.ReadAll(), "ReadFile - File 2 failed! {0}", file2.FullPath);

            ////////// FILE 3 - Directory, Parent
            output = File3Text1 + Environment.NewLine + File3Text2 + Environment.NewLine + File3Text3 + Environment.NewLine;
            var file3 = FS.GetFile(file3Path, OpenMode.ReturnNullIfDoesNotExist);
            Assert.AreEqual(output, file3.ReadAll(), "ReadFile - File 3 failed! {0}", file3.FullPath);

            ////////// FILE 4 - Full path
            output = File4Text2 + Environment.NewLine;
            var file4 = FS.GetFile(file4Path, OpenMode.ReturnNullIfDoesNotExist);
            Assert.AreEqual(output, file4.ReadAll(), "ReadFile - File 4 failed! {0}", file4.FullPath);
#endif
        }

#if FS
        [Test]
#endif
        public static void DeleteFile()
        {
#if FS
            var userPath = FS.GetDirectory(CurrentDirectory, OpenMode.CreateIfDoesNotExist);
            var file1Path = Path.Combine(userPath, TestFile1Name);
            var file2Path = Path.Combine(userPath, TestFile2Name);
            var file3Path = Path.Combine(userPath.ParentFolder, TestFile3Name);
            var file4Path = Path.Combine(userPath, TestFile4Name);
            bool output;

            ////////// FILE 1 - Full path
            var file1 = FS.GetFile(file1Path, OpenMode.ReturnNullIfDoesNotExist);
            output = !file1.Delete();
            Assert.AreEqual(File.Exists(file1), output, "DeleteFile - File 1 failed! {0}", file1.FullPath);

            ////////// FILE 2 - Path + Name + Extension
            var file2Name = Path.GetFileNameWithoutExtension(TestFile2Name);
            var file2Extension = Path.GetExtension(TestFile2Name);
            var file2 = FS.GetFile(userPath, file2Name, file2Extension, OpenMode.CreateIfDoesNotExist);
            output = !file2.Delete();
            Assert.AreEqual(File.Exists(file2), output, "DeleteFile - File 2 failed! {0}", file2.FullPath);

            ////////// FILE 3 - Directory, Parent
            var file3 = FS.GetFile(file3Path, OpenMode.ReturnNullIfDoesNotExist);
            output = !file3.Delete();
            Assert.AreEqual(File.Exists(file3), output, "DeleteFile - File 3 failed! {0}", file3.FullPath);

            ////////// FILE 4 - Full path
            var file4 = FS.GetFile(file4Path, OpenMode.ReturnNullIfDoesNotExist);
            output = !file4.Delete();
            Assert.AreEqual(File.Exists(file4), output, "DeleteFile - File 4 failed! {0}", file4.FullPath);
#endif
        }
    }
}