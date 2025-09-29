using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObligatoriskOpgave1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave1.Tests
{
    [TestClass()]
    public class TrophyTests
    {
        [TestMethod()]
        public void TrophyDefaultConstructorTest()
        {
            var defaultTophyTest = new Trophy();
            Assert.IsNotNull(defaultTophyTest);
            Assert.AreEqual("Unkown", defaultTophyTest.Competition);
            Assert.AreEqual(2025, defaultTophyTest.Year);
        }

        [TestMethod()]
        public void TrophyConstructorTest()
        {
            var tophyTest = new Trophy("Champions League", 2020);
            Assert.IsNotNull(tophyTest);
            Assert.AreEqual("Champions League", tophyTest.Competition);
            Assert.AreEqual(2020, tophyTest.Year);
        }

        [TestMethod()]
        public void CompetitionNotNullTest()
        {
            var tophyTest = new Trophy();
            Assert.ThrowsException<ArgumentException>(() => tophyTest.Competition = null);
            Assert.ThrowsException<ArgumentException>(() => tophyTest.Competition = "");
            Assert.ThrowsException<ArgumentException>(() => tophyTest.Competition = "  ");
        }


        [TestMethod()]
        public void CompetitionUnder3Test()
        {
            var tophyTest = new Trophy();
            Assert.ThrowsException<ArgumentException>(() => tophyTest.Competition = "ab");
            Assert.ThrowsException<ArgumentException>(() => tophyTest.Competition = "a");
        }
        [TestMethod()]
        public void CompetitionOkTest()
        {
            var tophyTest = new Trophy();
            tophyTest.Competition = "abc";
            Assert.AreEqual("abc", tophyTest.Competition);
            tophyTest.Competition = "abcd";
            Assert.AreEqual("abcd", tophyTest.Competition);
        }
        [TestMethod()]
        public void YearUnder1970Test()
        {
            var tophyTest = new Trophy();
            Assert.ThrowsException<ArgumentException>(() => tophyTest.Year = 1969);
            Assert.ThrowsException<ArgumentException>(() => tophyTest.Year = 1900);
        }
        [TestMethod()]
        public void YearOver2025Test()
        {
            var tophyTest = new Trophy();
            Assert.ThrowsException<ArgumentException>(() => tophyTest.Year = 2026);
            Assert.ThrowsException<ArgumentException>(() => tophyTest.Year = 3000);
        }
        [TestMethod()]
        public void YearOkTest()
        {
            var tophyTest = new Trophy();
            // grænseværdi 1970 og 1971
            tophyTest.Year = 1970;
            Assert.AreEqual(1970, tophyTest.Year);
            tophyTest.Year = 1971;
            Assert.AreEqual(1971, tophyTest.Year);
            // grænseværdi 2024 og 2025
            tophyTest.Year = 2024;
            Assert.AreEqual(2024, tophyTest.Year);
            tophyTest.Year = 2024;
            Assert.AreEqual(2024, tophyTest.Year);
            tophyTest.Year = 2025;
            Assert.AreEqual(2025, tophyTest.Year);
            // midt imellem
            tophyTest.Year = 2000;
            Assert.AreEqual(2000, tophyTest.Year);
        }
        [TestMethod()]
        public void TrophyToStringTest()
        {  
            var tophyTest = new Trophy("Champions League", 2020);
            Assert.AreEqual("Competition: Champions League, Year: 2020", tophyTest.ToString());
        }
    }
}