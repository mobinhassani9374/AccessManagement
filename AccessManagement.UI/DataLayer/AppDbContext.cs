using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.UI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessManagement.UI.DataLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserAccess> UserAccesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(c => c.Accesses)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
