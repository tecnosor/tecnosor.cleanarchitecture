using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iam.infrastructure.persistence.relational;

internal sealed class UserRelationalPersistenceContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<ApplicationUser> Users { get; set; }

    public UserRelationalPersistenceContext(DbContextOptions<UserRelationalPersistenceContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);
    protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
}
