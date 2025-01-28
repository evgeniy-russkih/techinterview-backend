using Microsoft.EntityFrameworkCore;
using TechInterviewTask.Entities;
using TechInterviewTask.Entities.Configurations;

namespace TechInterviewTask;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
    }
}