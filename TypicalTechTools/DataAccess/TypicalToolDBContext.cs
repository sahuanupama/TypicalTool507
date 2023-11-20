using Microsoft.CodeAnalysis.Scripting;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TypicalTechTools.Models;

namespace TypicalTechTools.DataAccess
    {
    public class TypicalToolDBContext : DbContext
        {

        public TypicalToolDBContext(DbContextOptions options) : base(options)
            {

            }
        // public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; } = null;
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<AdminUser>().HasData(
            new AdminUser
                {
                Id = 1,
                UserName = "Admin",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("Password_1")
                });
            }


        }
    }
