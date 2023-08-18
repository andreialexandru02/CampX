using AutoMapper;
using Azure.Core;
using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Campers.Models;
using CampX.BusinessLogic.Implementations.Requests.Models;
using CampX.BusinessLogic.Implementations.Requests.Validations;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.BusinessLogic.Implementations.Reviews.Validations;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Common.Extensions;
using CampX.Common.ViewModels;
using CampX.DataAccess;
using CampX.Entities;

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CampX.BusinessLogic.Implementations.Requests
{
    public class RequestService : BaseService
    {
        private readonly RequestValidator RequestValidator;
      

        public RequestService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.RequestValidator = new RequestValidator();         
        }
        public void AddRequest(AddRequestModel model)
        {
            RequestValidator.Validate(model).ThenThrow();

            var request = Mapper.Map<AddRequestModel, Request>(model);

            request.Date = DateTime.Now;

            var camper = UnitOfWork.Campers.Get()
                .Where(c => c.Id == model.CamperId)
                .SingleOrDefault();
            var trip = UnitOfWork.Trips.Get()
                .Where(t => t.Id == model.TripId)
                .SingleOrDefault();

            request.Camper = camper;
            request.Trip = trip;

            UnitOfWork.Requests.Insert(request);

 
            UnitOfWork.SaveChanges();
        }
        public bool CheckRequestDuplicate(AddRequestModel model)
        {
            return UnitOfWork.Requests.Get()
                .Where(c => c.TripId == model.TripId && c.CamperId == model.CamperId)
                .Any();
        }
        public List<ShowRequestsModel> ShowRequests()
        { 
                       
            var requests = UnitOfWork.Requests.Get()
                    .Include(t => t.Trip).ThenInclude(tc => tc.TripCampers)
                    .Include(c=> c.Camper)
                    .Where(r => r.Trip.TripCampers
                        .Any(tc => tc.IsOrganizer && tc.CamperId == CurrentCamper.Id))
                    .ToList();

            Console.WriteLine(requests);

            var requestsList = new List<ShowRequestsModel>();
            foreach(var request in requests)
            {
                Console.WriteLine("aklmndla");
                requestsList.Add(

                new ShowRequestsModel
                { 
                    Camper = new CamperModel
                    {
                        FirstName = request.Camper.FirstName,
                        LastName = request.Camper.LastName,
                        Email = request.Camper.Email,
                        Id = request.Camper.Id
                    },
                    Trip = new RequestTripModel
                    {
                        Name = request.Trip.Name,
                        Id = request.Trip.Id,
                        Date = request.Trip.Date
                    },
                    Description = "asdasd"
                });
                    
            }
            return requestsList;
        }
        public void DeleteRequest(CamperIdTripIdModel model)
        {          
            UnitOfWork.Requests.Delete(
                UnitOfWork.Requests.Get()
                    .Where(c => c.CamperId == model.CamperId && c.TripId == model.TripId)
                    .SingleOrDefault()
            );
            UnitOfWork.SaveChanges();    
        }
        public void AcceptRequest(CamperIdTripIdModel model)
        {
            var tripCamper = Mapper.Map<TripCamperIdModel, TripCamper>(
                 new TripCamperIdModel
                 {
                     TripId = model.TripId,
                     CamperId = model.CamperId,
                     IsOrganizer = false
                 });

            UnitOfWork.TripCampers.Insert(tripCamper);

            UnitOfWork.SaveChanges();
        }

        public List<Request> ShowPendingRequests()
        {
            var requests = UnitOfWork.Requests.Get()
                .Include(r => r.Camper)
                .Include(r => r.Trip)
                .Where(r => r.CamperId == CurrentCamper.Id)
                /*.Select(r => new PendingRequestsModel
                {
                    Camper = new CamperModel {
                        FirstName = r.Camper.FirstName,
                        LastName = r.Camper.LastName,
                        Email = r.Camper.Email,
                        Id = r.Camper.Id
                    },
                    Trip = new PendingRequestsTripModel
                    {
                        Id = r.Trip.Id,
                        Name = r.Trip.Name,
                        Date = r.Trip.Date,
                        TripCampers = UnitOfWork.TripCampers.Get()
                                        .Include(tc => tc.Camper)
                                        .Where(tc => tc.TripId == r.Trip.Id && tc.IsOrganizer)
                                        .Select(tc => new TripCamperOrganizerModel
                                        {
                                            IsOrganizer = tc.IsOrganizer,
                                            Camper = new CamperModel
                                            {
                                                FirstName = tc.Camper.FirstName,
                                                LastName = tc.Camper.LastName,
                                                Email = tc.Camper.Email,
                                                Id = tc.Camper.Id
                                            },
                                        })
                                        .ToList()
                    }

                })*/
                .ToList();
            return requests;
        }
    }
}

    

