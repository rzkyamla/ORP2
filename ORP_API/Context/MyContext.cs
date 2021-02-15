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
    }
}
