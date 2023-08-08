using AutoMapper;
using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Map.Validations;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.BusinessLogic.Implementations.Reviews.Validations;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.BusinessLogic.Implementations.Trips.Validations;
using CampX.Common.Extensions;
using CampX.Common.ViewModels;
using CampX.DataAccess;
using CampX.Entities;
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
        public void AddTrip(AddTripModel model)
        {
            TripValidator.Validate(model).ThenThrow();

            var trip = Mapper.Map<AddTripModel, Trip>(model);

            trip = UnitOfWork.Trips.Insert(trip);

            var campsites = UnitOfWork.Campsites.Get()
                .Where(c => model.Campsites.Contains(c.Id))
                .ToList();

            trip.Campsites = campsites;

            UnitOfWork.SaveChanges();

            
            var tripCamper = Mapper.Map<TripCamperModel, TripCamper>(
                new TripCamperModel
                {
                    TripId = trip.Id,
                    CamperId = model.TripCampers[0],
                    IsOrganizer = true
                });
            
            UnitOfWork.TripCampers.Insert(tripCamper); 

            UnitOfWork.SaveChanges();

        }
    }

}
