using LeadYourWay.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadYourWay.Infrastructure.Context;

public class LeadYourWayContext : DbContext
{
    public LeadYourWayContext()
    {
        
    }
    
    public LeadYourWayContext(DbContextOptions<LeadYourWayContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Bicycle> Bicycles { get; set; }
    public DbSet<Card> Cards { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=sql10.freemysqlhosting.net,3306;Uid=sql10624180;Pwd=ZePeyx6ZNS;Database=sql10624180;", serverVersion);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        // User
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(c => c.Name).IsRequired().HasMaxLength(60);
        builder.Entity<User>().Property(c => c.Email).IsRequired().HasMaxLength(60);
        builder.Entity<User>().Property(c => c.Password).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(c => c.Phone).IsRequired().HasMaxLength(15);
        builder.Entity<User>().Property(c => c.BirthDate).IsRequired();
        builder.Entity<User>().Property(c => c.IsActive).HasDefaultValue(true);

        // Bicycle
        builder.Entity<Bicycle>().ToTable("Bicycles");
        builder.Entity<Bicycle>().HasKey(p => p.Id);
        builder.Entity<Bicycle>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Bicycle>().Property(p => p.Name).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Entity<Bicycle>().Property(p => p.Price).IsRequired();
        builder.Entity<Bicycle>().Property(p => p.Size).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Model).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Image).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.UserId).IsRequired();
        
        // Card
        builder.Entity<Card>().ToTable("Cards");
        builder.Entity<Card>().HasKey(p => p.Id);
        builder.Entity<Card>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Card>().Property(p => p.Name).IsRequired().HasMaxLength(60);
        builder.Entity<Card>().Property(p => p.Number).IsRequired().HasMaxLength(60);
        builder.Entity<Card>().Property(p => p.ExpirationDate).IsRequired();
        builder.Entity<Card>().Property(p => p.Cvv).IsRequired().HasMaxLength(60);
        builder.Entity<Card>().Property(p => p.Type).IsRequired().HasMaxLength(60);
        builder.Entity<Card>().Property(p => p.IsActive).HasDefaultValue(true);
        builder.Entity<Card>().Property(p => p.UserId).IsRequired();
        
        // Connections
        builder.Entity<Bicycle>()
            .HasOne(c => c.User)
            .WithMany(e => e.Bicycles)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        builder.Entity<Card>()
            .HasOne(c => c.User)
            .WithMany(e => e.Cards)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}