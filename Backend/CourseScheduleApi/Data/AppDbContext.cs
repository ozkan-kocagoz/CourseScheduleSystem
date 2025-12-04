using Microsoft.EntityFrameworkCore;
using CourseScheduleApi.Models;

namespace CourseScheduleApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(u => u.FullName).HasColumnName("full_name");
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<User>().Property(u => u.Role).HasColumnName("role");
            modelBuilder.Entity<User>().Property(u => u.DepartmentId).HasColumnName("department_id");
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasColumnName("created_at");


            modelBuilder.Entity<Department>().ToTable("departments");
            modelBuilder.Entity<Department>().Property(d => d.Id).HasColumnName("id");
            modelBuilder.Entity<Department>().Property(d => d.Name).HasColumnName("name");
            modelBuilder.Entity<Department>().Property(d => d.Code).HasColumnName("code");
        }
    }
}