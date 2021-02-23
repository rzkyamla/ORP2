using Microsoft.EntityFrameworkCore;
using ORP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Context
{
    public class MyContext : DbContext
    {
        public MyContext() { }
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Role> Role { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<OvertimeForm> OvertimeForm { get; set; }
        public DbSet<DetailOvertimeRequest> DetailOvertimeRequests { get; set; }
        public DbSet<OvertimeFormEmployee> OvertimeFormEmployee { get; set; }
        public DbSet<DetailOvertimeRequest> DetailOvertimeRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasOne(x => x.Role).WithMany(z => z.Employees);
            modelBuilder.Entity<Employee>().HasOne(x => x.Customer).WithMany(z => z.Employees);
            modelBuilder.Entity<Account>().HasOne(a => a.Employee).WithOne(b => b.Account).HasForeignKey<Account>(a => a.NIK);
            modelBuilder.Entity<OvertimeFormEmployee>().HasOne(x => x.OvertimeForm).WithMany(z => z.OvertimeFormEmployees);
            modelBuilder.Entity<OvertimeFormEmployee>().HasOne(x => x.Employee).WithMany(z => z.OvertimeFormEmployees);
            modelBuilder.Entity<DetailOvertimeRequest>().HasOne(x => x.OvertimeForm).WithMany(z => z.DetailOvertimeReq);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
