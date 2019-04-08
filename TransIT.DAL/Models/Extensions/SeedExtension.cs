using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Extensions
{
    public static class SeedExtension
    {
        public static ModelBuilder SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "ADMIN" },
                new Role { Id = 2, Name = "WORKER" },
                new Role { Id = 3, Name = "ENGINEER" },
                new Role { Id = 4, Name = "CUSTOMER" },
                new Role { Id = 5, Name = "ANALYST" });
            return modelBuilder;
        }
        
        public static ModelBuilder SeedStates(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Name = "NEW" },
                new State { Id = 2, Name = "VERIFIED" },
                new State { Id = 3, Name = "REJECTED" },
                new State { Id = 4, Name = "TODO" },
                new State { Id = 5, Name = "EXECUTING" },
                new State { Id = 6, Name = "DONE" },
                new State { Id = 7, Name = "CONFIRMED" },
                new State { Id = 8, Name = "UNCONFIRMED" });
            return modelBuilder;
        }
    }
}
