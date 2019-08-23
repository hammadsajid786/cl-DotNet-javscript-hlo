using BLRMIS.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLRMIS.Web.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork unitOfWork;
        public BaseService(IUnitOfWork UnitOfWork)
        {
            unitOfWork = UnitOfWork;
        }

        public void Filter(string Param)
        {
            if(!string.IsNullOrEmpty(Param))
            {
                string[] Params = Param.Split(';');

            }
        }
    }
}
