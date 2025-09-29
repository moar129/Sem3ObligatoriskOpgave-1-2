using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave1
{
    public class TrophiesDbContext: DbContext
    {
        public TrophiesDbContext(DbContextOptions<TrophiesDbContext> options) : base(options)
        {
        }
        public DbSet<Trophy> Trophies { get; set; }
    }
}
