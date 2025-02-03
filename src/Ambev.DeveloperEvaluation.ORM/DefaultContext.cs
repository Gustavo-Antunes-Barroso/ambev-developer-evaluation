using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        MapUserEntity(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void MapUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
               .ToTable("userprofile")
               .Property(u => u.Id).HasColumnName("id");

        modelBuilder.Entity<User>()
               .Property(u => u.Username).HasColumnName("username");

        modelBuilder.Entity<User>()
               .Property(u => u.Email).HasColumnName("email");

        modelBuilder.Entity<User>()
               .Property(u => u.Phone).HasColumnName("phone");

        modelBuilder.Entity<User>()
               .Property(u => u.Password).HasColumnName("password");

        modelBuilder.Entity<User>()
               .Property(u => u.Role).HasConversion<int>().HasColumnName("role");

        modelBuilder.Entity<User>()
               .Property(u => u.Status).HasConversion<int>().HasColumnName("status");

        modelBuilder.Entity<User>()
               .Property(u => u.CreatedAt).HasColumnName("createdat");

        modelBuilder.Entity<User>()
               .Property(u => u.UpdatedAt).HasColumnName("updatedat");
    }
}

public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.WebApi")
        );

        return new DefaultContext(builder.Options);
    }
}