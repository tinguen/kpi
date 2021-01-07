using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IIG.PasswordHashingUtils;

namespace Lab._3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEmptyStringHash() {
            Assert.IsNotNull(PasswordHasher.GetHash(""));
            Assert.AreNotEqual(PasswordHasher.GetHash(""), "");
        }

        [TestMethod]
        public void TestGetHash()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("asd"));
            Assert.AreNotEqual(PasswordHasher.GetHash("asd"), "");
        }

        [TestMethod]
        public void TestNumberGetHash()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("123"));
            Assert.AreNotEqual(PasswordHasher.GetHash("123"), "");
        }

        [TestMethod]
        public void TestSpacesGetHash()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("   "));
            Assert.AreNotEqual(PasswordHasher.GetHash("   "), "");
        }

        [TestMethod]
        public void TestCaseSensitivityGetHash()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("hello"), PasswordHasher.GetHash("HELLO"));
            Assert.AreEqual(PasswordHasher.GetHash("hello"), PasswordHasher.GetHash("hello"));
        }

        [TestMethod]
        public void TestUniqueGetHash()
        {
            Assert.AreNotEqual(PasswordHasher.GetHash("hello"), PasswordHasher.GetHash("abcd123"));
        }

        [TestMethod]
        public void TestGetHashUkrainian()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("раоапо"));
            Assert.AreNotEqual(PasswordHasher.GetHash("раоапо"), "");
        }

        [TestMethod]
        public void TestGetHashChinese()
        {
            Assert.IsNotNull(PasswordHasher.GetHash("汉语"));
            Assert.AreNotEqual(PasswordHasher.GetHash("汉语"), "");
        }

        [TestMethod]
        public void TestInitIncorrectSaltValue()
        {
            string wrongSalt1 = "";
            string wrongSalt2 = null;
            string hash = PasswordHasher.GetHash("汉语");
            PasswordHasher.Init(wrongSalt1, 65521);
            Assert.AreEqual(hash, PasswordHasher.GetHash("汉语"));
            PasswordHasher.Init(wrongSalt2, 65521);
            Assert.AreEqual(hash, PasswordHasher.GetHash("汉语"));
        }

        [TestMethod]
        public void TestInitSalt()
        {
            string hash = PasswordHasher.GetHash("汉语");
            PasswordHasher.Init("somesalt", 65521);
            Assert.AreNotEqual(hash, PasswordHasher.GetHash("汉语"));
        }

        [TestMethod]
        public void TestInitIncorrectAdler32Value()
        {
            string hash = PasswordHasher.GetHash("汉语");
            PasswordHasher.Init("some salt", 0);
            Assert.AreEqual(hash, PasswordHasher.GetHash("汉语"));
        }

        [TestMethod]
        public void TestInitAdler32()
        {
            string hash = PasswordHasher.GetHash("汉语");
            PasswordHasher.Init("some salt", 1000);
            Assert.AreNotEqual(hash, PasswordHasher.GetHash("汉语"));
        }

        [TestMethod]
        public void TestGetHashIncorrectSaltValue()
        {
            string wrongSalt1 = "";
            string wrongSalt2 = null;
            string hash = PasswordHasher.GetHash("汉语");
            Assert.AreEqual(hash, PasswordHasher.GetHash("汉语", wrongSalt1, 65521));
            Assert.AreEqual(hash, PasswordHasher.GetHash("汉语", wrongSalt2, 65521));
        }

        [TestMethod]
        public void TestGetHashSalt()
        {
            string hash = PasswordHasher.GetHash("汉语");
            Assert.AreNotEqual(hash, PasswordHasher.GetHash("汉语", "somesalt", 65521));
        }

        [TestMethod]
        public void TestGetHashIncorrectAdler32Value()
        {
            string hash = PasswordHasher.GetHash("汉语");
            Assert.AreEqual(hash, PasswordHasher.GetHash("汉语", "some salt", 0));
        }

        [TestMethod]
        public void TestGetHashAdler32()
        {
            string hash = PasswordHasher.GetHash("汉语");
            Assert.AreNotEqual(hash, PasswordHasher.GetHash("汉语", "some salt", 1000));
        }
    }
}
