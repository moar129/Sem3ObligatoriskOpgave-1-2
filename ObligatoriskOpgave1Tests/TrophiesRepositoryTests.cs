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
    public class TrophiesRepositoryTests
    {
        private ITrophiesRepository _repo;
        [TestInitialize()]
        public void TestInitialize()
        {
            // Kør før hver test
            _repo = new TrophiesRepository();
            _repo.Add(new Trophy("Test Competition 1", 1970));
            _repo.Add(new Trophy("Test Competition 2", 2022));
            _repo.Add(new Trophy("Test Competition 3", 2025));
        }
        [TestMethod()]
        public void GetAllTest()
        {
            var trophies = _repo.Get();
            Assert.AreEqual(8, trophies.Count());
        }

        [TestMethod()]
        public void GetFilterByYearTest()
        {
            IEnumerable<Trophy> trophies = _repo.Get(year: 2021);
            Assert.AreEqual(2, trophies.Count());
            Assert.AreEqual(trophies.First().Competition, "FA Cup");
        }

        [TestMethod()]
        public void GetSortByCompetitionAscTest()
        {
            IEnumerable<Trophy> trophies = _repo.Get(sortby: "competition_asc");
            Assert.AreEqual(trophies.First().Competition, "Bundesliga");
            Assert.AreEqual(trophies.Last().Competition, "Test Competition 3");
        }
        
        [TestMethod()]
        public void GetSortByCompetitionDescTest()
        {
            IEnumerable<Trophy> trophies = _repo.Get(sortby: "competition_desc");
            Assert.AreEqual(trophies.First().Competition, "Test Competition 3");
            Assert.AreEqual(trophies.Last().Competition, "Bundesliga");
        }

        [TestMethod()]
        public void GetSortByYearAscTest()
        {
            IEnumerable<Trophy> trophies = _repo.Get(sortby: "year_asc");
            Assert.AreEqual(trophies.First().Year, 1970);
            Assert.AreEqual(trophies.Last().Year, 2025);
        }

        [TestMethod()]
        public void GetSortByYearDescTest()
        {
            IEnumerable<Trophy> trophies = _repo.Get(sortby: "year_desc");
            Assert.AreEqual(trophies.First().Year, 2025);
            Assert.AreEqual(trophies.Last().Year, 1970);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Trophy trophy = _repo.GetById(1);
            Assert.IsNotNull(trophy);
            Assert.AreEqual(trophy.Competition, "Premier League");
            Assert.AreEqual(trophy.Year, 2020);
            trophy = _repo.GetById(100);
            Assert.IsNull(trophy);
            trophy = _repo.GetById(0);
            Assert.IsNull(trophy);
        }

        [TestMethod()]
        public void AddTest()
        {
            Trophy trophy = _repo.Add(new Trophy("New Test Competition", 2023));
            Assert.AreEqual(9, _repo.Get().Count());
            Assert.AreEqual("New Test Competition", trophy.Competition);
        }

        [TestMethod()]
        public void AddExceptionTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _repo.Add(null));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Trophy result = _repo.Remove(6);
            Assert.AreEqual(7, _repo.Get().Count());
            Assert.AreEqual("Test Competition 1", result.Competition);
            Assert.IsNull(_repo.Remove(100));
            Assert.AreEqual(7, _repo.Get().Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.AreEqual(8, _repo.Get().Count());
            Trophy trophy = new Trophy("Updated Competition", 2024);
            Assert.IsNull(_repo.Update(100, trophy));
            Assert.AreEqual(6, _repo.Update(6, trophy)?.Id);
            Assert.AreEqual(8, _repo.Get().Count());
            Assert.ThrowsException<ArgumentException>(() => _repo.Update(6, new Trophy("",1969)));
        }
    }
}