using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using teemUpAPIv2.Models;

namespace teemUpAPIv2.Data

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        public DbSet<UserDetail> userDetails => Set<UserDetail>();
        

    }
}
