using AutoMapper;
using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Requests.Models;
using CampX.BusinessLogic.Implementations.Requests.Validations;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.BusinessLogic.Implementations.Reviews.Validations;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Common.Extensions;
using CampX.DataAccess;
using CampX.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        }
    }

    
   
}
