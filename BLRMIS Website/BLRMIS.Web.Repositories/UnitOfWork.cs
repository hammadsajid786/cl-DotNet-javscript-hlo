using BLRMIS.Web.DataAccess;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLRMIS.Web.Repositories
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork
        where TContext : DbContext, IDisposable
    { 
        private readonly BLRMIS_WebsiteContext _context;
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(BLRMIS_WebsiteContext context)
        {
            _context = context;
        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(_context);
            return (IRepository<TEntity>)_repositories[type];
        }
        public void DiscardChanges()
        {
            foreach (var Entry in _context.ChangeTracker.Entries())
            {
                Entry.State = EntityState.Unchanged;
            }
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<bool> SaveChangesAsync(bool overwriteDbChangesInCaseOfConcurrentUpdates = true)
        {
            bool saveFailed = false;
            do
            {
                saveFailed = false;

                try
                {
                    int count = await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    if (overwriteDbChangesInCaseOfConcurrentUpdates)
                    {
                        foreach (var Entry in ex.Entries)
                        {
                            //foreach (var property in Entry.Entity.GetType().DeclaredProperties)
                            //{
                            //    //var originalValue = Entry.Property(property.Name).OriginalValue;
                            //    var currentValue = Entry.Property(property.Name).CurrentValue;
                            //    Entry.Property(property.Name).OriginalValue = currentValue;
                            //}
                        }
                    }
                    else
                    {
                        foreach (var Entry in ex.Entries)
                        {
                            //foreach (var property in Entry.Entity.GetType().GetTypeInfo().DeclaredProperties)
                            //{
                            //    var originalValue = Entry.Property(property.Name).OriginalValue;
                            //    //var currentValue = Entry.Property(property.Name).CurrentValue;
                            //    Entry.Property(property.Name).CurrentValue = originalValue;
                            //}
                        }
                    }
                }
            } while (saveFailed);
            return await Task.FromResult(saveFailed);
        }

     

        public TRepository GetInstance<TRepository>() where TRepository : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(TRepository);
            if (!_repositories.ContainsKey(type)) _repositories[type] = (TRepository)Activator.CreateInstance(type,this._context);
            return (TRepository)_repositories[type];
        }
    }
}
