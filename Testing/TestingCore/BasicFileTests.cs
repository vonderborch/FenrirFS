/*
 * This file is subject to the terms and conditions defined in the
 * license.txt file, which is part of this source code package.
 */

#if FS

using FenrirFS;
using NUnit.Framework;

#endif

namespace TestingCore
{
#if FS

    [TestFixture]
#endif

    public static class BasicFileTests
    {
        private const string TestFile1Name = "test1.txt";
        private const string TestFile2Name = "test2.txt";
        private const string TestFile3Name = "test3.txt";
        private const string TestFile4Name = "test4.txt";

        private const string File1Text1 = "Hello world! ";
        private const string File1Text2 = "Goodbye world!";
        private const string File2Text1 = "Testing files stuffs\nHopefully this works?";
        private const string File2Text2 = "This\nis\nSPARTA!!!\n";
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

            ///////// PASSED!
#if FS
            Assert.Pass("Passed");
#endif
        }

        private static void CreateFile()
        {
#if FS
            AFolder userPath = Fenrir.FileSystem.StorageUser;

            ////////// FILE 1 - Full path
            AFile file1 = Fenrir.FileSystem.CreateFile(FSHelpers.CombinePath(userPath.ToString(), TestFile1Name), FileCollisionOption.ReplaceExisting);
            Assert.AreEqual(true, Fenrir.FileSystem.FileExists(file1.FullPath), "CreateFile - File 1 failed! {0}", file1.FullPath);

            ////////// FILE 2 - Path + name
            AFile file2 = Fenrir.FileSystem.CreateFile(userPath.ToString(), TestFile2Name, FileCollisionOption.ReplaceExisting);
            Assert.AreEqual(true, Fenrir.FileSystem.FileExists(file2.FullPath), "CreateFile - File 2 failed! {0}", file2.FullPath);

            ////////// FILE 3 - Folder + name
            AFile file3 = Fenrir.FileSystem.CreateFile(userPath, TestFile3Name, FileCollisionOption.ReplaceExisting);
            Assert.AreEqual(true, Fenrir.FileSystem.FileExists(file3.FullPath), "CreateFile - File 3 failed! {0}", file3.FullPath);

            ////////// FILE 4 - Folder + name
            AFile file4 = Fenrir.FileSystem.CreateFile(userPath, TestFile4Name, FileCollisionOption.ReplaceExisting);
            Assert.AreEqual(true, Fenrir.FileSystem.FileExists(file4.FullPath), "CreateFile - File 4 failed! {0}", file4.FullPath);

#endif
        }

        private static void OpenFile()
        {
#if FS
            AFolder userPath = Fenrir.FileSystem.StorageUser;
            string path;

            ////////// FILE 1 - Full path
            path = FSHelpers.CombinePath(userPath.ToString(), TestFile1Name);
            AFile file1 = Fenrir.FileSystem.OpenFile(FSHelpers.CombinePath(userPath.ToString(), TestFile1Name), OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(path, file1.ToString(), "OpenFile - File 1 failed! {0}", file1.FullPath);

            ////////// FILE 2 - Full path
            path = FSHelpers.CombinePath(userPath.ToString(), TestFile2Name);
            AFile file2 = Fenrir.FileSystem.OpenFile(path, OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(path, file2.ToString(), "OpenFile - File 2 failed! {0}", file2.FullPath);

            ////////// FILE 3 - Directory + name
            path = FSHelpers.CombinePath(userPath.ToString(), TestFile3Name);
            AFile file3 = Fenrir.FileSystem.OpenFile(userPath.ToString(), TestFile3Name, OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(path, file3.ToString(), "OpenFile - File 3 failed! {0}", file3.FullPath);

            ////////// FILE 4 - Folder + name
            path = FSHelpers.CombinePath(userPath.ToString(), TestFile4Name);
            AFile file4 = Fenrir.FileSystem.OpenFile(userPath, TestFile4Name, OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(path, file4.ToString(), "OpenFile - File 4 failed! {0}", file4.FullPath);

#endif
        }

        private static void WriteToFile()
        {
#if FS
            AFolder userPath = Fenrir.FileSystem.StorageUser;

            ////////// FILE 1 - WriteAll append
            AFile file1 = Fenrir.FileSystem.OpenFile(FSHelpers.CombinePath(userPath.ToString(), TestFile1Name), OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(true, file1.WriteAll(File1Text1, WriteMode.Append), "WriteToFile - File 1 failed 1! {0}", file1.FullPath);
            Assert.AreEqual(true, file1.WriteAll(File1Text2, WriteMode.Append), "WriteToFile - File 1 failed 2! {0}", file1.FullPath);

            ////////// FILE 2 - WriteAll truncate
            AFile file2 = Fenrir.FileSystem.OpenFile(userPath.ToString(), TestFile2Name, OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(true, file2.WriteAll(File2Text1, WriteMode.Truncate), "WriteToFile - File 2 failed 1! {0}", file2.FullPath);
            Assert.AreEqual(true, file2.WriteAll(File2Text2, WriteMode.Truncate), "WriteToFile - File 2 failed 2! {0}", file2.FullPath);

            ////////// FILE 3 - WriteLine Append
            AFile file3 = Fenrir.FileSystem.OpenFile(userPath, TestFile3Name, OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(true, file3.WriteLine(File3Text1, WriteMode.Append), "WriteToFile - File 3 failed 1! {0}", file3.FullPath);
            Assert.AreEqual(true, file3.WriteLine(File3Text2, WriteMode.Append), "WriteToFile - File 3 failed 2! {0}", file3.FullPath);
            Assert.AreEqual(true, file3.WriteLine(File3Text3, WriteMode.Append), "WriteToFile - File 3 failed 3! {0}", file3.FullPath);

            ////////// FILE 4 - WriteLine Truncate
            AFile file4 = Fenrir.FileSystem.OpenFile(userPath, TestFile4Name, OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(true, file4.WriteLine(File4Text1, WriteMode.Truncate), "WriteToFile - File 4 failed 1! {0}", file4.FullPath);
            Assert.AreEqual(true, file4.WriteLine(File4Text2, WriteMode.Truncate), "WriteToFile - File 4 failed 2! {0}", file4.FullPath);

#endif
        }

        private static void ReadFile()
        {
#if FS
            AFolder userPath = Fenrir.FileSystem.StorageUser;
            string output;

            ////////// FILE 1 - WriteAll append
            output = File1Text1 + File1Text2;
            AFile file1 = Fenrir.FileSystem.OpenFile(FSHelpers.CombinePath(userPath.ToString(), TestFile1Name), OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(output, file1.ReadAll(), "ReadFile - File 1 failed! {0}", file1.FullPath);

            ////////// FILE 2 - WriteAll truncate
            output = File2Text2;
            AFile file2 = Fenrir.FileSystem.OpenFile(userPath.ToString(), TestFile2Name, OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(output, file2.ReadAll(), "ReadFile - File 2 failed! {0}", file2.FullPath);

            ////////// FILE 3 - WriteLine Append
            output = File3Text1 + FSHelpers.LineSeparator + File3Text2 + FSHelpers.LineSeparator + File3Text3 + FSHelpers.LineSeparator;
            AFile file3 = Fenrir.FileSystem.OpenFile(userPath, TestFile3Name, OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(output, file3.ReadAll(), "ReadFile - File 3 failed! {0}", file3.FullPath);

            ////////// FILE 4 - WriteLine Truncate
            output = File4Text2 + FSHelpers.LineSeparator;
            AFile file4 = Fenrir.FileSystem.OpenFile(userPath, TestFile4Name, OpenMode.FailIfDoesNotExist);
            Assert.AreEqual(output, file4.ReadAll(), "ReadFile - File 4 failed! {0}", file4.FullPath);

#endif
        }
    }
}