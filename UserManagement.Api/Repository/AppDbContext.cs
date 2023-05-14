using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Domain;

namespace UserManagement.Api.Repository
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Class>()
            .HasMany(c => c.Users)
            .WithOne(o => o.Class)
            .HasForeignKey(o => o.ClassId);
            base.OnModelCreating(builder);
        }

        public DbSet<Users> Users {get; set;}
        public DbSet<Class> Class {get; set;}
    }
}