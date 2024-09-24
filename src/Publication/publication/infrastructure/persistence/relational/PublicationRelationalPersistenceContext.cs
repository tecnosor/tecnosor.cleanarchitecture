using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using stolenCars.publication.domain;


namespace stolenCars.publication.infrastructure.persistence.relational;

public partial class UserRelationalPersistenceContext : IdentityDbContext<IdentityUser>
{   
    public DbSet<Publication> Publications { get; set; }

    public UserRelationalPersistenceContext(DbContextOptions<UserRelationalPersistenceContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
