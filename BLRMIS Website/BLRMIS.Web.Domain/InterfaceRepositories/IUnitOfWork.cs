using BLRMIS.Web.Domain.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace BLRMIS.Web.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        TRepository GetInstance<TRepository>() where TRepository : class;
        // int Commit();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }

}
