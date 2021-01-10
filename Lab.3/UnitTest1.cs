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
            string empty_string = "";

            Assert.IsNotNull(PasswordHasher.GetHash(empty_string));
            Assert.AreNotEqual(PasswordHasher.GetHash(empty_string), "");
        }

        [TestMethod]
        public void TestGetHash()
        {
            string password = "asd";
            Assert.IsNotNull(PasswordHasher.GetHash(password));
            Assert.AreNotEqual(PasswordHasher.GetHash(password), "");
        }

        [TestMethod]
        public void TestNumberGetHash()
        {
            string number_password = "123";
            Assert.IsNotNull(PasswordHasher.GetHash(number_password));
            Assert.AreNotEqual(PasswordHasher.GetHash(number_password), "");
        }

        [TestMethod]
        public void TestSpacesGetHash()
        {
            string space_password = "   ";
            Assert.IsNotNull(PasswordHasher.GetHash(space_password));
            Assert.AreNotEqual(PasswordHasher.GetHash(space_password), "");
        }

        [TestMethod]
        public void TestCaseSensitivityGetHash()
        {
            string lowercase = "hello";
            string uppercase = "HELLO";
            Assert.AreNotEqual(PasswordHasher.GetHash(lowercase), PasswordHasher.GetHash(uppercase));
            Assert.AreEqual(PasswordHasher.GetHash(lowercase), PasswordHasher.GetHash(lowercase));
        }

        [TestMethod]
        public void TestUniqueGetHash()
        {
            string password1 = "hello";
            string password2 = "abcd123";
            Assert.AreNotEqual(PasswordHasher.GetHash(password1), PasswordHasher.GetHash(password2));
        }

        [TestMethod]
        public void TestGetHashUkrainian()
        {
            string ukrainian = "раоапо";
            Assert.IsNotNull(PasswordHasher.GetHash(ukrainian));
            Assert.AreNotEqual(PasswordHasher.GetHash(ukrainian), "");
        }

        [TestMethod]
        public void TestGetHashChinese()
        {
            string chinese = "汉语";
            Assert.IsNotNull(PasswordHasher.GetHash(chinese));
            Assert.AreNotEqual(PasswordHasher.GetHash(chinese), "");
        }

        [TestMethod]
        public void TestInitIncorrectSaltValue()
        {
            string wrongSalt1 = "";
            string wrongSalt2 = null;
            string password = "汉语";
            string hash = PasswordHasher.GetHash(password);
            PasswordHasher.Init(wrongSalt1, 65521);
            Assert.AreEqual(hash, PasswordHasher.GetHash(password));
            PasswordHasher.Init(wrongSalt2, 65521);
            Assert.AreEqual(hash, PasswordHasher.GetHash(password));
        }

        [TestMethod]
        public void TestInitSalt()
        {
            string password = "汉语";
            string salt = "somesalt";
            string hash = PasswordHasher.GetHash(password);
            PasswordHasher.Init(salt, 65521);
            Assert.AreNotEqual(hash, PasswordHasher.GetHash(password));
        }

        [TestMethod]
        public void TestInitIncorrectAdler32Value()
        {
            string password = "汉语";
            string salt = "some salt";

            string hash = PasswordHasher.GetHash(password);
            PasswordHasher.Init(salt, 0);
            Assert.AreEqual(hash, PasswordHasher.GetHash(password));
        }

        [TestMethod]
        public void TestInitAdler32()
        {
            string salt = "some salt";
            string password = "汉语";
            uint adler32 = 1000;
            string hash = PasswordHasher.GetHash(password);
            PasswordHasher.Init(salt, adler32);
            Assert.AreNotEqual(hash, PasswordHasher.GetHash(password));
        }

        [TestMethod]
        public void TestGetHashIncorrectSaltValue()
        {
            string wrongSalt1 = "";
            string wrongSalt2 = null;
            string password = "汉语";
            uint adler32 = 65521;
            string hash = PasswordHasher.GetHash(password);
            Assert.AreEqual(hash, PasswordHasher.GetHash(password, wrongSalt1, adler32));
            Assert.AreEqual(hash, PasswordHasher.GetHash(password, wrongSalt2, adler32));
        }

        [TestMethod]
        public void TestGetHashSalt()
        {
            string password = "汉语";
            string salt = "somesalt";
            uint adler32 = 65521;
            string hash = PasswordHasher.GetHash(password);
            Assert.AreNotEqual(hash, PasswordHasher.GetHash(password, salt, adler32));
        }

        [TestMethod]
        public void TestGetHashIncorrectAdler32Value()
        {
            string salt = "some salt";
            string password = "汉语";
            uint incorrect_adler32 = 0;
            string hash = PasswordHasher.GetHash(password);
            Assert.AreEqual(hash, PasswordHasher.GetHash(password, salt, incorrect_adler32));
        }

        [TestMethod]
        public void TestGetHashAdler32()
        {
            string password = "汉语";
            uint adler32 = 1000;
            string salt = "some salt";
            string hash = PasswordHasher.GetHash(password);
            Assert.AreNotEqual(hash, PasswordHasher.GetHash(password, salt, adler32));
        }
    }
}
