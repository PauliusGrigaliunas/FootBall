using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            String namet1 = null;
            String namet2;

            namet2 = Football.MainMenu.name1;

            Assert.AreNotEqual(namet1, namet2);


        }
    }
}
