using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace tecnosor.cleanarchitecture.common.infrastructure.persistence.relational;

public partial class RelationalPersistenceContext : IdentityDbContext<IdentityUser>
{   

    public RelationalPersistenceContext(DbContextOptions<RelationalPersistenceContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
