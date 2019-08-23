using BLRMIS.Web.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}
