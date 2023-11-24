using FLCManagement.Models;
using FLCManagementProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Data
{
    public class FLCMDBContext : DbContext
    {
        public FLCMDBContext(DbContextOptions<FLCMDBContext> options) : base(options)
        {

        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<BillCusClass> BillCusClasses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExamBatch> ExamBatches { get; set; }
        public DbSet<ExamSession> ExamSessions { get; set; }
        public DbSet<ExamShift> ExamShifts { get; set; }
        public DbSet<ExamType> ExamTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDto> UserDtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Exam)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.examID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserDto>().HasNoKey();
        }
    }
}
