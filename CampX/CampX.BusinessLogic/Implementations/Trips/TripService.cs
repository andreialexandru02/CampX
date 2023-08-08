using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Map.Validations;
using CampX.BusinessLogic.Implementations.Trips.Validations;
using CampX.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Trips
{
    public class TripService : BaseService
    {

        private readonly TripValidator TripValidator;

        public TripService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.TripValidator = new TripValidator(UnitOfWork);
        }
        public List<TripCampsiteModel> DisplayCampsites()
        {
            List<TripCampsiteModel> campsites = UnitOfWork.Campsites.Get()
            .Select(c => new TripCampsiteModel
            {
                Id = c.Id
                ,Name = c.Name
                ,Latitude = c.Latitude
                ,Longitude = c.Longitude,
                
            })
            .ToList();

            UnitOfWork.SaveChanges();
            return campsites;
        }
    }

}
