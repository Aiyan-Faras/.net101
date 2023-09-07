
using DotnetApi.Models;
using Microsoft.EntityFrameworkCore;


namespace DotnetApi.Data {
    public class DataContextEF : DbContext {
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config) { 
            _config = config;
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<User>()
                .ToTable("Users", "TutorialAppSchema")
                .HasKey(u => u.UserId);
        }
    }
}