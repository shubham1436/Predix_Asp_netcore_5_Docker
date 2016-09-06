using AspnetCorePostgres.Postgres.POCO;
using Microsoft.EntityFrameworkCore;

namespace AspnetCorePostgres.Postgres
{
    public class AuthorsContext : DbContext
    {
        public AuthorsContext(DbContextOptions<AuthorsContext> options)
            : base(options)
        { }

        public DbSet<Author> Authors { get; set; }
    }
}
