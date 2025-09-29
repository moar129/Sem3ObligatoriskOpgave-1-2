using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave1
{
    public class TrophiesRepoDb: ITrophiesRepoDb
    {
        private readonly TrophiesDbContext _context;
        public TrophiesRepoDb(TrophiesDbContext context)
        {
            _context = context;
        }

        public async Task<Trophy> AddAsync(Trophy trophy)
        {
            if (trophy == null)
            {
                throw new ArgumentNullException("Trophy er Null");
            }
            await _context.Trophies.AddAsync(trophy);
            await _context.SaveChangesAsync();
            return trophy;
        }

        public IQueryable<Trophy> GetTropiesDb(int? year = null, string? sort = null)
        {
            // kopier af listen
            IQueryable<Trophy> trophies = _context.Trophies;
            // FIltering
            if (year != null)
            {
                trophies = trophies.Where(t => t.Year == year);
            }
            // Sorting
            if (!string.IsNullOrEmpty(sort))
            {
                sort = sort.ToLower();
                switch (sort)
                {
                    case "competition": // går igennem til næste case
                    case "competition_asc":
                        trophies = trophies.OrderBy(t => t.Competition);
                        break;
                    case "competition_desc":
                        trophies = trophies.OrderByDescending(t => t.Competition);
                        break;
                    case "year":
                    case "year_asc":
                        trophies = trophies.OrderBy(t => t.Year);
                        break;
                    case "year_desc":
                        trophies = trophies.OrderByDescending(t => t.Year);
                        break;
                    default:
                        throw new ArgumentException("Ukendt sortering: " + sort);
                }
            }
            return trophies;
        }

        public async Task<Trophy?> GetByIdAsync(int id)
        {
            return await _context.Trophies.FindAsync(id);
        }

        public async Task<Trophy?> RemoveAsync(int id)
        {
            var trophy = await _context.Trophies.FindAsync(id);
            if (trophy == null)
            {
                return null;
            }
            _context.Trophies.Remove(trophy);
            await _context.SaveChangesAsync();
            return trophy;
        }

        public async Task<Trophy?> UpdateAsync(int id, Trophy trophy)
        {
            var existingTrophy = await _context.Trophies.FindAsync(id);
            if (existingTrophy == null)
            {
                return null;
            }
            existingTrophy.Competition = trophy.Competition;
            existingTrophy.Year = trophy.Year;
            await _context.SaveChangesAsync();
            return existingTrophy;
        }
    }
}
