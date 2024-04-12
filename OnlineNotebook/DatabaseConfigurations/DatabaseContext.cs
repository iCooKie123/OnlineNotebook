using Microsoft.EntityFrameworkCore;

namespace OnlineNotebook.DatabaseConfigurations
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>options): base(options)
        {
        }
    }
}
