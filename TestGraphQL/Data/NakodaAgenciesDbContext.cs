using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Models;

namespace TestGraphQL.Data
{
    public class NakodaAgenciesDbContext : DbContext
    {
        public NakodaAgenciesDbContext(DbContextOptions options) : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }
    }
}
