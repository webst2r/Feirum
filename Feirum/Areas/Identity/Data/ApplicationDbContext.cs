﻿using Feirum.Areas.Identity.Data;
using Feirum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Feirum.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<FavoriteFair>()
       .HasKey(p => new { p.UserId, p.FairId });

        builder.ApplyConfiguration(new UserEntityConfiguration());
    }

    public DbSet<Categories> Categories { get; set; }
    public DbSet<Fairs> Fairs { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Feirum.Models.Orders> Orders { get; set; }
    public DbSet<Feirum.Models.FavoriteFair> FavoriteFair { get; set; }
}

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    // Aqui podemos configurar os campos do User (FirstName, ...), dizer se é obrigatório, etc.
    void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
    {
        // configurar o nome
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}
