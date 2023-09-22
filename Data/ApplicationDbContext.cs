
using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        // add configuration
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


            
        }

        // this is how to create table, use add-migration
        public DbSet<Category> Categories { get; set; }

        // seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1},
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2},
                new Category { Id = 3, Name = "History", DisplayOrder = 5}
                );
        }
    }
}
