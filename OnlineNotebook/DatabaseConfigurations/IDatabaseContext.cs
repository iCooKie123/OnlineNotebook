using Microsoft.EntityFrameworkCore;

namespace OnlineNotebook.DatabaseConfigurations
{
    public interface IDatabaseContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}