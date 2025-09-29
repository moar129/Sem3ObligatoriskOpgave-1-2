using Microsoft.EntityFrameworkCore;
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
    public class TrophiesRepoDbTests
    {
        private ITrophiesRepoDb _repo;
        [TestInitialize()]
        public void TestInitialize()
        {
            // Arrange - laver en database context til at teste med
            // laver en options builder til at konfigurere dbcontexten og angive connection string til DbContext
            var optionsBuilder = new DbContextOptionsBuilder<TrophiesDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TrophiesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False");
            // Opretter en DbContext instans med de angivne options
            TrophiesDbContext _context = new TrophiesDbContext(optionsBuilder.Options);
            // Slet og genopretter databasen for at sikre en ren tilstand før hver test
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Trophies");
            // laver en Repository instans og udfylder den med DbContexten information
            _repo = new TrophiesRepoDb(_context);

            // Tilføjer nogle test data
            _repo.AddAsync(new Trophy("Test Competition 1", 1970)).Wait();
            _repo.AddAsync(new Trophy("Test Competition 2", 2022)).Wait();
            _repo.AddAsync(new Trophy("Test Competition 3", 2025)).Wait();

        }
        [TestMethod()]
        public void TrophiesRepoDbTest()
        {
            Assert.IsNotNull(_repo);
            Assert.IsInstanceOfType(_repo, typeof(ITrophiesRepoDb));
        }

        [TestMethod()]
        public void AddAsyncTest()
        {
            var trophy = new Trophy("Test Competition", 2024);
            var result = _repo.AddAsync(trophy).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(trophy.Competition, result.Competition);
            Assert.AreEqual(trophy.Year, result.Year);
        }

        [TestMethod()]
        public void GetTropiesDbTest()
        {
            // test uden filter og sort
            var trophies = _repo.GetTropiesDb().ToList();
            Assert.AreEqual(3, trophies.Count);
            // test med filter
            trophies = _repo.GetTropiesDb(year: 2022).ToList();
            Assert.AreEqual(1, trophies.Count);
            // test med sort
            trophies = _repo.GetTropiesDb(sort: "competition_desc").ToList();
            Assert.AreEqual(3, trophies.Count);
            Assert.AreEqual("Test Competition 3", trophies.First().Competition);
            // test med filter og sort
            trophies = _repo.GetTropiesDb(year: 2025, sort: "competition_asc").ToList();
            Assert.AreEqual(1, trophies.Count);
            Assert.AreEqual("Test Competition 3", trophies.First().Competition);
        }

        [TestMethod()]
        public void GetByIdAsyncTest()
        {
            var trophy = _repo.GetByIdAsync(1).Result;
            Assert.IsNotNull(trophy);
            Assert.AreEqual(1, trophy.Id);
            Assert.AreEqual("Test Competition 1", trophy.Competition);
            Assert.AreEqual(1970, trophy.Year);
        }

        [TestMethod()]
        public void RemoveAsyncTest()
        {
            var trophy = _repo.RemoveAsync(1).Result;
            Assert.IsNotNull(trophy);
            Assert.AreEqual(1, trophy.Id);
            Assert.AreEqual("Test Competition 1", trophy.Competition);
            Assert.AreEqual(1970, trophy.Year);
            var trophies = _repo.GetTropiesDb().ToList();
            Assert.AreEqual(2, trophies.Count);
        }

        [TestMethod()]
        public void UpdateAsyncTest()
        {
            var trophy = new Trophy("Updated Competition", 2023);
            var result = _repo.UpdateAsync(1, trophy).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Updated Competition", result.Competition);
            Assert.AreEqual(2023, result.Year);
            var updatedTrophy = _repo.GetByIdAsync(1).Result;
            Assert.IsNotNull(updatedTrophy);
            Assert.AreEqual(1, updatedTrophy.Id);
            Assert.AreEqual("Updated Competition", updatedTrophy.Competition);
            Assert.AreEqual(2023, updatedTrophy.Year);
        }
    }
}