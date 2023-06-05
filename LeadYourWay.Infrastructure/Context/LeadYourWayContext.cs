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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=localhost,3306;Uid=root;Pwd=1234;Database=db_leadyourway_appsweb;", serverVersion);
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
        builder.Entity<User>().Property(c => c.Password).IsRequired().HasMaxLength(60);
        builder.Entity<User>().Property(c => c.Phone).IsRequired().HasMaxLength(60);
        builder.Entity<User>().Property(c => c.BirthDate).IsRequired();
        
        // Bicycle
        builder.Entity<Bicycle>().ToTable("Bicycles");
        builder.Entity<Bicycle>().HasKey(p => p.Id);
        builder.Entity<Bicycle>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Bicycle>().Property(p => p.Name).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Description).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Price).IsRequired();
        builder.Entity<Bicycle>().Property(p => p.Size).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Model).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.Image).IsRequired().HasMaxLength(60);
        builder.Entity<Bicycle>().Property(p => p.UserId).IsRequired();
        
        // Connections
        builder.Entity<Bicycle>()
            .HasOne(c => c.User)
            .WithMany(e => e.Bicycles)
            .HasForeignKey(e => e.UserId);
    }
}