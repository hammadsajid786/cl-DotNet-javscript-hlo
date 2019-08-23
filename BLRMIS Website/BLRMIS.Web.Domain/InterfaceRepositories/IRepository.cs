using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceRepositories
{
   public interface IRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetById(int id);

        TEntity GetByCondition(Func<TEntity, bool> condition);
        List<TEntity> GetAll(Func<TEntity, bool> condition);
        void Insert(TEntity obj);
        
        void Update(TEntity obj);
        void Delete(object id);
        void Save();
        void SetState(TEntity entity, EntityState state);

    }
}
