using IdentityService.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Api.Infrastructure.Context
{
    public class IdentityApiDbContext:DbContext
    {
        public IdentityApiDbContext(DbContextOptions options):base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageRole> PageRoles { get; set; }
        public DbSet<Role> Roles { get; set; }



    }
}
