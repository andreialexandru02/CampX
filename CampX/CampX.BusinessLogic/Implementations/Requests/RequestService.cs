using AutoMapper;
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
;
            
            
            var tripCampers = UnitOfWork.Requests.Get()

                .Include(tc => tc.Trip)
                .ThenInclude(tcc => tcc.TripCampers)
                .Include(c => c.Camper)
                .Select(r => r.Trip.TripCampers).ToList();
            var organizer = new List<TripCamper>();
            foreach(var tripCamper in tripCampers)
            {

                if (tripCamper.SingleOrDefault().IsOrganizer &&
                    tripCamper.SingleOrDefault().CamperId == CurrentCamper.Id)
                {
                    
                    organizer.Add(tripCamper.SingleOrDefault());   
                }
            }

            Console.WriteLine(organizer);

            var requests = UnitOfWork.Requests.Get()
                .Include(t => t.Trip)
                .Include(c => c.Camper)
                .Select(t => t.Trip.TripCampers)
                .ToList();

            Console.WriteLine(requests);

            var requestsList = new List<ShowRequestsModel>();
            foreach(var request in requests)
            {
                requestsList.Add(

                    new ShowRequestsModel
                    {
                        /*Camper = new CamperModel
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
                        Description = request.Description*/
                    }
                );
            }
            return requestsList;
        }
    }
}

    

