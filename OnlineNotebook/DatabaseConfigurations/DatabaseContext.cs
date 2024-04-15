using Microsoft.EntityFrameworkCore;
using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.DatabaseConfigurations
{
    public partial class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<StudyClass> Classes { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}