using Microsoft.EntityFrameworkCore;
using CourseScheduleApi.Models;

namespace CourseScheduleApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Veritabanýndaki 'users' tablosunu kod tarafýnda 'Users' olarak temsil et
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Veritabaný tablosunun adý küçük harfle 'users' olsun (PostgreSQL standardý)
            modelBuilder.Entity<User>().ToTable("users");

            // Sütun isimlerini de PostgreSQL formatýna (snake_case) uyduralým
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(u => u.FullName).HasColumnName("full_name");
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<User>().Property(u => u.Role).HasColumnName("role");
            modelBuilder.Entity<User>().Property(u => u.DepartmentId).HasColumnName("department_id");
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasColumnName("created_at");
        }
    }
}