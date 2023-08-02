using AutoMapper;
using CampX.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CampX.DataAccess;
using CampX.Common.ViewModels;

namespace CampX.BusinessLogic.Base
{
    public class BaseService
    {
        protected readonly IMapper Mapper;
        protected readonly UnitOfWork UnitOfWork;
        protected readonly CurrentCamperDTO CurrentCamper;

        public BaseService(ServiceDependencies serviceDependencies)
        {
            Mapper = serviceDependencies.Mapper;
            UnitOfWork = serviceDependencies.UnitOfWork;
            CurrentCamper = serviceDependencies.CurrentCamper;
        }

        protected TResult ExecuteInTransaction<TResult>(Func<UnitOfWork, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            using (var transactionScope = new TransactionScope())
            {
                var result = func(UnitOfWork);

                transactionScope.Complete();

                return result;
            }
        }

        protected void ExecuteInTransaction(Action<UnitOfWork> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using (var transactionScope = new TransactionScope())
            {
                action(UnitOfWork);

                transactionScope.Complete();
            }
        }
    }
}
