using AutoMapper;
using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Campers.Models;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Map.Validations;
using CampX.BusinessLogic.Implementations.Nights.Models;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.BusinessLogic.Implementations.Reviews.Validations;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.BusinessLogic.Implementations.Trips.Validations;
using CampX.Common.Extensions;
using CampX.Common.ViewModels;
using CampX.DataAccess;
using CampX.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                ,
                Name = c.Name
                ,
                Latitude = c.Latitude
                ,
                Longitude = c.Longitude,

            })
            .ToList();

            UnitOfWork.SaveChanges();
            return campsites;
        }
        public void AddTrip(AddTripModel model)
        {
            ExecuteInTransaction(uow =>
            {
            
                TripValidator.Validate(model).ThenThrow();

                var trip = Mapper.Map<AddTripModel, Trip>(model);

                var campsites = uow.Campsites.Get()
                    .Where(c => model.Campsites.Contains(c.Id))
                    .ToList();

                //INSERT IN NIGHTS


                trip.Campsites = campsites;

                trip = uow.Trips.Insert(trip);

                uow.SaveChanges();

                var tripCamper = Mapper.Map<TripCamperIdModel, TripCamper>(
                    new TripCamperIdModel
                    {
                        TripId = trip.Id,
                        CamperId = model.TripCampers[0],
                        IsOrganizer = true
                    });
                uow.TripCampers.Insert(tripCamper);

                uow.SaveChanges();
                foreach (var campsite in campsites)
                {
                    var nights = Mapper.Map<AddNightsModel, Night>(new AddNightsModel
                    {
                        CampsiteId = campsite.Id
                        , TripId = trip.Id
                        ,
                        NumberOfNights = model.NightsAtCampsite[campsite.Id]
                    });
                    uow.Nights.Insert(nights);

                    uow.SaveChanges();
                }
            });

    }
    public List<ShowTripsModel> ShowTrips() {


            var trips = UnitOfWork.Trips.Get()
                .Include(c => c.Campsites)
                .Include(tc => tc.TripCampers).ThenInclude(tcc => tcc.Camper)
                .Where(t => t.IsPublic == true)
                .ToList();
            
            var tripModels = new List<ShowTripsModel>();



            foreach (var trip in trips)
            {
                tripModels.Add(new ShowTripsModel
                {
                
                    Id = trip.Id,
                    Name =  trip.Name,
                    Description = trip.Description,
                    IsPublic = trip.IsPublic,
                    Date = trip.Date,
                    Code = trip.Code,
                    TripCampers = trip.TripCampers.Select(tc => new TripCamperModel
                    {
                        TripId = tc.TripId,
                        Camper = new CamperModel
                        {
                            Id = tc.Camper.Id,
                            FirstName = tc.Camper.FirstName,
                            LastName = tc.Camper.LastName,
                            Email = tc.Camper.Email
                        },
                        IsOrganizer = tc.IsOrganizer,
                    }).ToList(),
                    Campsites = trip.Campsites.Select(tc => new TripCampsitesModel
                    {
                        Id = tc.Id,
                        Name = tc.Name,
                        Description = tc.Description,
                        Difficulty = tc.Difficulty,
                        Latitude = tc.Latitude,
                        Longitude = tc.Longitude
                    }).ToList()
                });
            }
            return tripModels;
        }
        public ShowTripsModel TripDetails(int id)
        {
            var nightsAtCampsite = new Dictionary<int, int>();

            var nights = UnitOfWork.Nights.Get()
                .Where(n => n.TripId == id)
                .ToList();


            foreach(var night in nights)
            {
                nightsAtCampsite.Add(night.CampsiteId ,night.NumberOfNights);
            }
            var trip = UnitOfWork.Trips.Get()
                .Include(c => c.Campsites)
                .Include(tc => tc.TripCampers).ThenInclude(tcc => tcc.Camper)
                .Where(c => c.Id == id)
                .Select(c => new ShowTripsModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsPublic = c.IsPublic,
                    Date = c.Date,
                    Code = c.Code,
                    TripCampers = c.TripCampers.Select(tc => new TripCamperModel
                    {
                        TripId = tc.TripId,
                        Camper = new CamperModel
                        {
                            Id = tc.Camper.Id,
                            FirstName = tc.Camper.FirstName,
                            LastName = tc.Camper.LastName,
                            Email = tc.Camper.Email
                        },
                        IsOrganizer = tc.IsOrganizer,
                    }).ToList(),
                    Campsites = c.Campsites.Select(tc => new TripCampsitesModel
                    {
                        Id = tc.Id,
                        Name = tc.Name,
                        Description = tc.Description,
                        Difficulty = tc.Difficulty,
                        Latitude = tc.Latitude,
                        Longitude = tc.Longitude
                    }).ToList(),
                    NightsAtCampsite = nightsAtCampsite


                })
                .SingleOrDefault();

            return trip;
        }
        public void DeleteTrip(int id)
        {
            UnitOfWork.Trips.Delete(
                UnitOfWork.Trips.Get()
                    .Where(t => t.Id == id)
                    .SingleOrDefault()
            );
            UnitOfWork.SaveChanges();
        }
        public int SearchCode(string code)
        {
            var trip = UnitOfWork.Trips.Get()
                .SingleOrDefault(t => t.Code == code);
            if(trip == null ) {
                return -1;
            }
            return trip.Id;
        }

        public List<ShowTripsModel> ShowCurrentCamperTrips()
        {
            var trips = UnitOfWork.Trips.Get()
                    .Include(tc => tc.TripCampers).ThenInclude(c => c.Camper)
                    .Include(c => c.Campsites)
                    .Where(t => t.TripCampers.Any(tc => tc.IsOrganizer && tc.CamperId == CurrentCamper.Id))
                    .ToList();

           

            var currentCamperTrips = new List<ShowTripsModel>();
            foreach (var trip in trips)
            {
                var campsites = new List<TripCampsitesModel>();
                var tripCampers = new List<TripCamperModel>();
                foreach (var campsite in trip.Campsites)
                {
                    campsites.Add(new TripCampsitesModel
                    {
                        Id = campsite.Id,
                        Name = campsite.Name,
                        Description = campsite.Description,
                        Difficulty = campsite.Difficulty,
                        Latitude = campsite.Latitude,
                        Longitude = campsite.Longitude,
                    });                 
                }
                foreach(var tripCamper in trip.TripCampers)
                {
                    tripCampers.Add(new TripCamperModel
                    {
                        TripId = trip.Id,
                        Camper = new CamperModel
                        {
                            Id = tripCamper.Camper.Id,
                            FirstName = tripCamper.Camper.FirstName,
                            LastName = tripCamper.Camper.LastName,
                            Email = tripCamper.Camper.Email
                        },
                        IsOrganizer = tripCamper.IsOrganizer
                    });
                }
                    currentCamperTrips.Add(new ShowTripsModel
                {
                    Id = trip.Id,
                    Name = trip.Name,
                    Description = trip.Description,
                    Date = trip.Date,
                    IsPublic = trip.IsPublic,
                    Code = trip.Code,
                    Campsites = campsites,
                    TripCampers = tripCampers
                    });
            }
            return currentCamperTrips; 
        }

        public bool CheckOrganizer(int id)
        {
            var organizerId = UnitOfWork.Trips.Get()
               .Include(t => t.TripCampers).ThenInclude(tc => tc.Camper)
               .Where(t => t.Id == id)
               .Select(t => t.TripCampers
                           .SingleOrDefault(tc => tc.IsOrganizer).Camper.Id).SingleOrDefault();

            return organizerId == CurrentCamper.Id;
           
        }
        public ShowTripsModel TripToEdit(int id)
        {
            
           
            var trip = UnitOfWork.Trips.Get()
                .Include(c => c.Campsites)
                .Include(tc => tc.TripCampers).ThenInclude(c => c.Camper)
                .Select(t => new ShowTripsModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    IsPublic = t.IsPublic,
                    Code = t.Code,
                    Campsites = UnitOfWork.Campsites.Get()
                                .Where(c => t.Campsites.Contains(c))
                                .Select(c => new TripCampsitesModel
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    Description = c.Description,
                                    Difficulty = c.Difficulty,
                                    Latitude = c.Latitude,
                                    Longitude = c.Longitude
                                })
                                .ToList(),
                    TripCampers = UnitOfWork.TripCampers.Get()
                                .Where(tc => tc.TripId == t.Id)
                                .Select(tc => new TripCamperModel
                                {
                                    TripId = tc.TripId,
                                    Camper = new CamperModel
                                    {
                                        Id = tc.CamperId,
                                        FirstName = tc.Camper.FirstName,
                                        LastName = tc.Camper.LastName,
                                        Email = tc.Camper.Email
                                    },
                                    IsOrganizer = tc.IsOrganizer
                                })
                                .ToList()

                })
                .SingleOrDefault(t => t.Id == id);
            return trip;
        }
        public void FinishTrip(int id)
        {

            var numberOfNights = UnitOfWork.Trips.Get()
                .Where(t => t.Id == id)
                .Select(t => t.Nights)
                .SingleOrDefault();

            var tripCampers = UnitOfWork.Trips.Get()
                .Include(tc => tc.TripCampers).ThenInclude(c => c.Camper).ThenInclude(cb => cb.CamperBadges)
                .Where(t => t.Id == id)
                .Select(t => t.TripCampers)
                .SingleOrDefault();

            foreach (var tripCamper in tripCampers)
            {              

                foreach (var camperBadge in tripCamper.Camper.CamperBadges)
                {

                   


                    var badgeCamper = UnitOfWork.CamperBadges.Get()
                           .Where(cb => cb.BadgeId == camperBadge.BadgeId && cb.CamperId == tripCamper.CamperId)
                           .SingleOrDefault();

                    //badgeCamper.Score += numberOfNights;
                    UnitOfWork.CamperBadges.Update(badgeCamper);
                    UnitOfWork.SaveChanges();
                }
            }
        }


        public void EditTrip(ShowTripsModel model)
        {

            var trip = UnitOfWork.Trips.Get()
                .Include(n => n.Nights)
                .Include(c => c.Campsites)
                .Include(c => c.TripCampers)
                .SingleOrDefault(t => t.Id == model.Id);


            Mapper.Map<ShowTripsModel, Trip>(model,trip);

            var nightAtCampsite = new Collection<Night>();
            trip.TripCampers.Clear();

            foreach(var night in model.NightsAtCampsite)
            {
                Console.WriteLine(night);
                nightAtCampsite.Add(new Night
                {
                    CampsiteId = night.Key,
                    NumberOfNights = night.Value,
                    TripId = trip.Id
                });
            }
            var campsites = UnitOfWork.Campsites.Get()
                .Where(c => model.NightsAtCampsite.Keys.Contains(c.Id))
                .ToList();
            trip.Nights = nightAtCampsite;
            trip.Campsites = campsites;
            foreach (var tripCamper in model.TripCampers) 
            {
                
                trip.TripCampers.Add(new TripCamper
                {
                    CamperId = tripCamper.Camper.Id,
                    TripId = trip.Id,
                    IsOrganizer = tripCamper.IsOrganizer

                });
            }
            //trip.TripCampers = model.TripCampers;

            UnitOfWork.Trips.Update(trip);
            UnitOfWork.SaveChanges();

        }
        public bool IdExists(int id)
        {
            var trip = UnitOfWork.Trips.Get()
                .SingleOrDefault(r => r.Id == id);

            return trip != null;

        }

    }

}
