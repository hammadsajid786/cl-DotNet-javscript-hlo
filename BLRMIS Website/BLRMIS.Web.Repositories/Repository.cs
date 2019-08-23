using BLRMIS.Web.DataAccess;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLRMIS.Web.Repositories
{
   public class Repository<TEntity>: BaseRepository<TEntity>, IRepository<TEntity> where TEntity:class
    {
        protected readonly BLRMIS_WebsiteContext _context;
        //private dynamic _context = null;
        //private DbSet<TEntity> table = null;
        public Repository(BLRMIS_WebsiteContext context) : base(context)
        {
          _context = context;
        }
        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public TEntity GetByCondition(Func<TEntity, bool> condition)
        {
            return _context.Set<TEntity>().Where(condition).FirstOrDefault();
        }

        public List<TEntity> GetAll(Func<TEntity, bool> condition)
        {
            return _context.Set<TEntity>().Where(condition).ToList();
        }
        public virtual void Insert(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
        }
        public void Update(TEntity obj)
        {
            _context.Attach<TEntity>(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {               
            TEntity existing = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }
       
        public void SetState(TEntity entity, EntityState state)
        {
            _context.Entry(entity).State = state;
        }
    }

}
