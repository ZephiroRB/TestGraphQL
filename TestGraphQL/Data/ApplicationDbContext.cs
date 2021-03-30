using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGraphQL.Resolver;

namespace TestGraphQL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<Region> Region { get; set; }
    }
}
