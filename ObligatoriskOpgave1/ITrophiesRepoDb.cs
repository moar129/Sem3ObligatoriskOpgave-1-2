using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave1
{
    public interface ITrophiesRepoDb
    {
        Task<Trophy> AddAsync(Trophy trophy);
        IQueryable<Trophy> GetTropiesDb(int? year = null, string? sort = null);
        Task<Trophy?> GetByIdAsync(int id);
        Task<Trophy?> RemoveAsync(int id);
        Task<Trophy?> UpdateAsync(int id, Trophy trophy);
    }
}
