namespace TestProject.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using TestProject.Data.Models;

    public class TestProjectContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public TestProjectContext()
        {
        }

        public TestProjectContext(DbContextOptions<TestProjectContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Account>().HasOne(a => a.User).WithOne(u => u.Account);
            modelBuilder.Entity<Account>().Property(p => p.MaximumMonthlyCreditLimit)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            modelBuilder.Entity<User>().Property(p => p.Email)
                .IsRequired();

            modelBuilder.Entity<User>().Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<User>().Property(p => p.MonthlySalary)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            modelBuilder.Entity<User>().Property(p => p.MonthlyExpenses)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
