using AspnetCorePostgres.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCorePostgres.Postgres.POCO
{
    public class PartFamilyContext : DbContext
    {
        public PartFamilyContext(DbContextOptions<PartFamilyContext> options)
            : base(options)
        { }

        public DbSet<PartFamily> PartFamily { get; set; }
    }
}
