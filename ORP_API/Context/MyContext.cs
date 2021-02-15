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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasOne(x => x.Role).WithMany(z => z.Employees);
            modelBuilder.Entity<Employee>().HasOne(x => x.Customer).WithMany(z => z.Employees);
            modelBuilder.Entity<Account>().HasOne(a => a.Employee).WithOne(b => b.Account).HasForeignKey<Account>(a => a.NIK);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
