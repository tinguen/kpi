using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using IIG.BinaryFlag;
using IIG.FileWorker;

namespace Lab4
{
    [TestClass]
    public class UnitTest2
    {
        string testFile = "./testFile.txt";
        string filledFile = "./filledFile.txt";
        string readAllFile = "./readAllFile.txt";

        [TestCleanup]
        public void TestCleanup()
        {
            File.WriteAllText(testFile, "");
        }

        [TestMethod]
        public void TestGetFlagTrue()
        {
            string expected = "True";

            MultipleBinaryFlag flag = new MultipleBinaryFlag(44, true);

            Assert.IsTrue(BaseFileWorker.Write(flag.GetFlag().ToString(), testFile));
            Assert.AreEqual(expected, File.ReadAllText(testFile));
        }

        [TestMethod]
        public void TestGetFlagFalse()
        {
            string expected = "False";

            MultipleBinaryFlag flag = new MultipleBinaryFlag(44, false);

            Assert.IsTrue(BaseFileWorker.Write(flag.GetFlag().ToString(), testFile));
            Assert.AreEqual(expected, File.ReadAllText(testFile));
        }

        [TestMethod]
        public void TestTryWriteTrue()
        {
            string expected = "True";

            MultipleBinaryFlag flag = new MultipleBinaryFlag(44, true);

            Assert.IsTrue(BaseFileWorker.TryWrite(flag.GetFlag().ToString(), testFile));
            Assert.AreEqual(expected, File.ReadAllText(testFile));
        }

        [TestMethod]
        public void TestTryWriteFalse()
        {
            string expected = "False";

            MultipleBinaryFlag flag = new MultipleBinaryFlag(44, false);

            Assert.IsTrue(BaseFileWorker.TryWrite(flag.GetFlag().ToString(), testFile));
            Assert.AreEqual(expected, File.ReadAllText(testFile));
        }

        [TestMethod]
        public void TestReadLines()
        {
            MultipleBinaryFlag[] binaryFlags = { new MultipleBinaryFlag(2, true),
                new MultipleBinaryFlag(2, false) };

            string[] results = BaseFileWorker.ReadLines(filledFile);

            Assert.AreEqual(binaryFlags.Length, results.Length);

            for (int i = 0; i < binaryFlags.Length; i++)
            {
                Assert.AreEqual(binaryFlags[i].GetFlag().ToString(), results[i]);
            }
        }

        [TestMethod]
        public void TestGetFlagFalseAfterSetFlag()
        {
            string expected = "True";

            MultipleBinaryFlag flag = new MultipleBinaryFlag(44, false);

            for (uint i = 0; i < 44; i++)
            {
                flag.SetFlag(i);
            }

            Assert.IsTrue(BaseFileWorker.Write(flag.GetFlag().ToString(), testFile));
            Assert.AreEqual(expected, File.ReadAllText(testFile));
        }

        [TestMethod]
        public void TestGetFlagFalseAfteResetFlag()
        {
            string expected = "False";

            MultipleBinaryFlag flag = new MultipleBinaryFlag(100, true);

            flag.ResetFlag(10);

            Assert.IsTrue(BaseFileWorker.Write(flag.GetFlag().ToString(), testFile));
            Assert.AreEqual(expected, File.ReadAllText(testFile));
        }

        [TestMethod]
        public void TestReadAll()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(5, true);
            Assert.AreEqual(flag.GetFlag().ToString(), BaseFileWorker.ReadAll(readAllFile));

        }

        [TestMethod]
        public void TestReadlAllNotEqualGetFlag()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(5, true);
            flag.ResetFlag(3);
            Assert.AreNotEqual(flag.GetFlag().ToString(), BaseFileWorker.ReadAll(readAllFile));
        }
    }
}
