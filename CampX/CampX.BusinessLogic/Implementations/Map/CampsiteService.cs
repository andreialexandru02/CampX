using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Account;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Map.Validations;
using CampX.Common.Extensions;
using CampX.DataAccess;
using CampX.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Map
{
    public class CampsiteService : BaseService
    {
        private readonly CampsiteValidator CampsiteValidator;

        public CampsiteService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.CampsiteValidator = new CampsiteValidator(UnitOfWork);
        }


        public void AddCampsite(AddCampsiteModel model)
        {
            CampsiteValidator.Validate(model).ThenThrow();

            var campsite = Mapper.Map<AddCampsiteModel, Campsite>(model);

            //camper.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password, 13);

           // camper.Roles.Add(UnitOfWork.Roles.Get().Where(r => r.Id == 3).SingleOrDefault());

            UnitOfWork.Campsites.Insert(campsite);
            // trigger mail notifi
            // insert audit 

            UnitOfWork.SaveChanges();
        }

        public List<List<decimal>> GetCoordinates()
        {
            List<List<decimal>> coordinatesList = UnitOfWork.Campsites.Get()
           .Select(c => new List<decimal> { c.Latitude, c.Longitude })
           .ToList();

            return coordinatesList;
        }

    }
}
