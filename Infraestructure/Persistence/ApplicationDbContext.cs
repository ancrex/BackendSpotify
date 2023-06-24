using BackendSpotify.Core.Domain.Models.State;
using BackendSpotify.Core.Domain.Models.Token;
using Microsoft.EntityFrameworkCore;

namespace BackendSpotify.Infraestructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<TokenResponse> Tokens { get; set; }
    public DbSet<StateRequest> States { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TokenResponse>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<TokenResponse>()
            .Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<TokenResponse>()
            .ToTable("Tokens");

        modelBuilder.Entity<StateRequest>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<StateRequest>()
            .Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<StateRequest>().ToTable("States");
    }
}





