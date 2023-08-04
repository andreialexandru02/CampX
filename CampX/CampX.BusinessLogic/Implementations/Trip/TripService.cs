using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Map.Validations;
using CampX.BusinessLogic.Implementations.Trip.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Trip
{
    public class TripService : BaseService
    {

        private readonly TripValidator TripValidator;

        public TripService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.TripValidator = new TripValidator(UnitOfWork);
        }
    }
}
