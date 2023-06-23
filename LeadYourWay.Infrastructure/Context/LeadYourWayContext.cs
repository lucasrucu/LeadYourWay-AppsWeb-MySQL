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
    public DbSet<Rent> Rents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=localhost,3306;Uid=root;Pwd=1234;Database=db_leadyourway_appsweb;",
                serverVersion);
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
        builder.Entity<User>().Property(c => c.Phone).HasMaxLength(15);
        builder.Entity<User>().Property(c => c.Image);
        builder.Entity<User>().Property(c => c.BirthDate);
        builder.Entity<User>().Property(c => c.IsActive).HasDefaultValue(true);
        builder.Entity<User>().Property(c => c.DateCreated).IsRequired();

        // Bicycle
        builder.Entity<Bicycle>().ToTable("Bicycles");
        builder.Entity<Bicycle>().HasKey(p => p.Id);
        builder.Entity<Bicycle>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Bicycle>().Property(p => p.Name).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Entity<Bicycle>().Property(p => p.Price).IsRequired();
        builder.Entity<Bicycle>().Property(p => p.Size).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Model).HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Image);
        builder.Entity<Bicycle>().Property(p => p.UserId).IsRequired();
        builder.Entity<Bicycle>().Property(p => p.IsActive).HasDefaultValue(true);
        builder.Entity<Bicycle>().Property(c => c.DateCreated).IsRequired();

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
        builder.Entity<Card>().Property(c => c.DateCreated).IsRequired();

        // Rent
        builder.Entity<Rent>().ToTable("Rents");
        builder.Entity<Rent>().HasKey(p => p.Id);
        builder.Entity<Rent>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Rent>().Property(p => p.StartDate).IsRequired();
        builder.Entity<Rent>().Property(p => p.EndDate).IsRequired();
        builder.Entity<Rent>().Property(p => p.TotalPrice).IsRequired();
        builder.Entity<Rent>().Property(p => p.IsActive).HasDefaultValue(true);
        builder.Entity<Rent>().Property(c => c.DateCreated).HasDefaultValue(DateTime.Now);
        builder.Entity<Rent>().Property(p => p.BicycleId).IsRequired();
        builder.Entity<Rent>().Property(p => p.CardId).IsRequired();

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
        builder.Entity<Rent>()
            .HasOne(b => b.Bicycle)
            .WithMany(e => e.Rents)
            .HasForeignKey(e => e.BicycleId)
            .IsRequired();
        builder.Entity<Rent>()
            .HasOne(c => c.Card)
            .WithMany(e => e.Rents)
            .HasForeignKey(e => e.CardId)
            .IsRequired();
        // builder.Entity<Bicycle>()
        //     .HasMany(c => c.Rents)
        //     .WithOne(e => e.Bicycle)
        //     .HasForeignKey(e => e.BicycleId)
        //     .IsRequired();
    }
}