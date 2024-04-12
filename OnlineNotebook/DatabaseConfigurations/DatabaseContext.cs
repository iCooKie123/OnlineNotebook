using Microsoft.EntityFrameworkCore;
using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.DatabaseConfigurations
{
    public partial class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<User>().ToTable("users");
    }
}