using Microsoft.EntityFrameworkCore;

namespace FLCManagement.Models
{
    public class FLCMDBContext : DbContext
    {
        public FLCMDBContext(DbContextOptions opt) : base(opt) { }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassDetail> ClassDetails { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerExamShift> CustomerExamShifts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamExamType> ExamExamTypes { get; set; }
        public DbSet<ExamSession> ExamSessions { get; set; }
        public DbSet<ExamSessionResult> ExamSessionResults { get; set; }
        public DbSet<ExamShift> ExamShifts { get; set; }
        public DbSet<ExamType> ExamTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Exam)
                .WithMany()
                .HasForeignKey(o => o.examID)
                .OnDelete(DeleteBehavior.Restrict); // Chọn hành động không cho phép xóa

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.customerID)
                .OnDelete(DeleteBehavior.Restrict); // Chọn hành động không cho phép xóa
        }
    }
}
