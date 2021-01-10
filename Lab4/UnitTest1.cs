using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IIG.CoSFE.DatabaseUtils;
using IIG.PasswordHashingUtils;

namespace Lab4
{
    [TestClass]
    public class UnitTest1
    {
        private const string Server = @"TIN\MSSQLSERVER1";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"KPI2020";
        private const int ConnectionTime = 75;


        AuthDatabaseUtils authDatabaseUtils = new AuthDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);
        [TestMethod]
        public void TestAddingCredentials()
        {
            string login = "login";
            string password = "password";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestAddingUkrainianCredentials()
        {
            string login = "логин";
            string password = "пароль";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestAddingChineseCredentials()
        {
            string login = "汉语";
            string password = "汉语";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestAddingEmptyCredentials()
        {
            string login1 = "";
            string login2 = null;
            string password = "汉语";

            Assert.IsFalse(authDatabaseUtils.AddCredentials(login1, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.AddCredentials(login2, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestAddingNullPassword()
        {
            string login = "汉语";
            string password = null;

            Assert.ThrowsException<ArgumentNullException>(() => authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestAddingSameCredentials()
        {
            string login = "somelogin";
            string password = "123456";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestUpdateLogin()
        {
            string login = "somelogin1";
            string password = "123456";
            string new_login = "new_login";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password), new_login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestUpdatePassword()
        {
            string login = "new_login123";
            string password = "123456";
            string new_password = "new_password";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password), login, PasswordHasher.GetHash(new_password)));
        }

        [TestMethod]
        public void TestUpdateNullCredentials()
        {
            string login = "new_login321";
            string password = "new_password";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password), null, null));
        }

        [TestMethod]
        public void TestUpdateWrongCredentials()
        {
            string login = "somelogg";
            string password = "somepass";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash("password")));
            Assert.IsFalse(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password), login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestUpdateSameCredentials()
        {
            string login = "hello";
            string password = "ssap";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password), login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestUpdateRandomCredentials()
        {
            string login = "gol2";
            string password = "ssap2";
            string wrong_password_hash = "asdashdk";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password), login, wrong_password_hash));
        }

        [TestMethod]
        public void TestUpdateCredentialsToExistingCredentials()
        {
            string login1 = "gol";
            string password = "ssap";
            string login2 = "login2";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login1, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.AddCredentials(login2, PasswordHasher.GetHash(password)));

            Assert.IsFalse(authDatabaseUtils.UpdateCredentials(login1, PasswordHasher.GetHash(password), login2, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestCheckCredentials()
        {
            string login = "log";
            string password = "pass";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.CheckCredentials(login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestCheckWrongCredentials()
        {
            string login = "wrong_login";
            string password = "wrong_password";

            Assert.IsFalse(authDatabaseUtils.CheckCredentials(login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestDeleteCredentials()
        {
            string login = "somelog";
            string password = "somepass";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.DeleteCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.CheckCredentials(login, PasswordHasher.GetHash(password)));
        }

        [TestMethod]
        public void TestDeleteWrongCredentials()
        {
            string login = "wrong_log";
            string password = "wrong_pass";

            Assert.IsFalse(authDatabaseUtils.DeleteCredentials(login, PasswordHasher.GetHash(password)));
        }
    }
}
