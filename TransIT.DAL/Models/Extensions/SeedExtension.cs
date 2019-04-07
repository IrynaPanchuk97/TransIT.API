using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Extensions
{
    public static class SeedExtension
    {
        public static ModelBuilder SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                    new Role { Name = "ADMIN" },
                    new Role { Name = "WORKER" },
                    new Role { Name = "ENGINEER" },
                    new Role { Name = "CUSTOMER" },
                    new Role { Name = "ANALYST" });
            return modelBuilder;
        }
        
        public static ModelBuilder SeedStates(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasData(
                new State { Name = "NEW" },
                new State { Name = "VERIFIED" },
                new State { Name = "REJECTED" },
                new State { Name = "TODO" },
                new State { Name = "EXECUTING" },
                new State { Name = "DONE" },
                new State { Name = "CONFIRMED" },
                new State { Name = "UNCONFIRMED" });
            return modelBuilder;
        }
    }
}