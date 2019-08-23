using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Repositories
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
