using Microsoft.EntityFrameworkCore;
using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.DatabaseConfigurations
{
    public interface IDatabaseContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<StudyClass> Classes { get; set; }
        public DbSet<StudentClass> StudentClases { get; set; }
    }
}
