using BCrypt.Net;
using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.Common.Extensions;
using CampX.Common.ViewModels;
using CampX.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampX.BusinessLogic.Implementations.Account
{
    public class CamperAccountService : BaseService
    {
        private readonly RegisterCamperValidator RegisterCamperValidator;

        public CamperAccountService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.RegisterCamperValidator = new RegisterCamperValidator();
        }



        public CurrentCamperDTO Login(string email, string password)
        {
            HashType hashType = HashType.SHA384;
            //ar x = BCrypt.Net.BCrypt.EnhancedVerify(password, "$2a$13$xaKMThAzwgmFatV3x3bp1uVIFO3iTfoQPFcUU6c6XOw.6y9WUgc7a");//, hashType));
            var camper = UnitOfWork.Campers.Get()
                .Include(u => u.Roles)
                .SingleOrDefault(u => u.Email == email);


            if (camper == null || !BCrypt.Net.BCrypt.EnhancedVerify(password, camper.Password))
            
            {
                return new CurrentCamperDTO { IsAuthenticated = false };
            }

            return new CurrentCamperDTO
            {
                Id = camper.Id,
                Email = camper.Email,
                FirstName = camper.FirstName,
                LastName = camper.LastName,
                IsAuthenticated = true,
                Roles = camper.Roles.Select(ur => ur.Name).ToList()
            };
        }

        public void RegisterNewCamper(RegisterModel model)
        {
            RegisterCamperValidator.Validate(model).ThenThrow();

            var camper = Mapper.Map<RegisterModel, Camper>(model);

            camper.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password, 13);

            camper.Roles.Add(UnitOfWork.Roles.Get().Where(r => r.Id == 3).SingleOrDefault());

            UnitOfWork.Campers.Insert(camper);
            // trigger mail notifi
            // insert audit 

            UnitOfWork.SaveChanges();
        }

        public List<ListItemModel<string, int>> GetCampers()
        {
            return UnitOfWork.Campers.Get()
                .Select(u => new ListItemModel<string, int>
                {
                    Text = $"{u.FirstName} {u.LastName}",
                    Value = u.Id
                })
                .ToList();
        }

        public void DisableUser() 
        {
        }

        public void ChangePassword() { }
    }
}

