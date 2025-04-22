using Microsoft.EntityFrameworkCore;
using tns_test.Models;



namespace tns_test.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Users)
                .WithOne(u => u.Department)
                .HasForeignKey(u => u.departmentid)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

