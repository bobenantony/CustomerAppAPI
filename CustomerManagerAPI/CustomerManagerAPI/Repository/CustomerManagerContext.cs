using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomerManagerAPI.Model;

namespace CustomerManagerAPI.Repository
{
    public class CustomerManagerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<State> States { get; set; }

        public CustomerManagerContext(DbContextOptions<CustomerManagerContext> options)
           : base(options)
        {
            Database.Migrate();
        }
    }
}
