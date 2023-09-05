using BCrypt.Net;
using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.BusinessLogic.Implementations.Account.Validations;
using CampX.BusinessLogic.Implementations.Badges;
using CampX.BusinessLogic.Implementations.Badges.Models;
using CampX.BusinessLogic.Implementations.Campers.Models;
using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Common.Extensions;
using CampX.Common.ViewModels;
using CampX.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampX.BusinessLogic.Implementations.Account
{
    public class CamperAccountService : BaseService
    {
        private readonly RegisterCamperValidator RegisterCamperValidator;
        private readonly ChangePasswordValidator ChangePasswordValidator;
        private readonly EditCamperValidator EditCamperValidator;

        public CamperAccountService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.RegisterCamperValidator = new RegisterCamperValidator(UnitOfWork);
            this.ChangePasswordValidator =  new ChangePasswordValidator(UnitOfWork);
            this.EditCamperValidator = new EditCamperValidator(UnitOfWork,CurrentCamper);
        }


        public CurrentCamperDTO Login(string email, string password)
        {
            HashType hashType = HashType.SHA384;
            //ar x = BCrypt.Net.BCrypt.EnhancedVerify(password, "$2a$13$xaKMThAzwgmFatV3x3bp1uVIFO3iTfoQPFcUU6c6XOw.6y9WUgc7a");//, hashType));
            var camper = UnitOfWork.Campers.Get()
                .Include(u => u.Roles)
                .SingleOrDefault(u => u.Email == email);


            if (camper.IsBanned)
            {
                return new CurrentCamperDTO { IsBanned = true };
            }
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
                Roles = camper.Roles.Select(ur => ur.Name).ToList(),
                IsBanned =  false
            };
        }

        public CurrentCamperDTO RegisterNewCamper(RegisterModel model)
        {
            RegisterCamperValidator.Validate(model).ThenThrow();

            var camper = Mapper.Map<RegisterModel, Camper>(model);

            camper.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password, 13);

            camper.Roles.Add(UnitOfWork.Roles.Get().Where(r => r.Id == 3).SingleOrDefault());

            var CamperId = UnitOfWork.Campers.Insert(camper);
            // trigger mail notifi
            // insert audit 
            UnitOfWork.SaveChanges();
            
            AddCamperBadgesForCreatedCamper(CamperId.Id);

            return Login(model.Email, model.Password);

        }
        public void AddCamperBadgesForCreatedCamper(int id)
        {
            var badgeIds = UnitOfWork.Badges.Get()
                .Select(b => b.Id)
                .ToList();

            foreach (var badgeId in badgeIds)
            {
                var camperBadge = Mapper.Map<CamperBadgeModel, CamperBadge>(new CamperBadgeModel
                {
                    CamperId = id,
                    BadgeId = badgeId,
                    Score = 0
                });
                UnitOfWork.CamperBadges.Insert(camperBadge);
                UnitOfWork.SaveChanges();
            }
        }
        public void ChangePassword(ChangePasswordModel model)
        {
            ChangePasswordValidator.Validate(model).ThenThrow();
            var camper = UnitOfWork.Campers.Get()
                .SingleOrDefault(c => c.Id == model.Id);
            if (camper != null && BCrypt.Net.BCrypt.EnhancedVerify(model.OldPassword, camper.Password))

            {
                camper.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password, 13);

                UnitOfWork.Campers.Update(camper);
                UnitOfWork.SaveChanges();
            }

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
        public void DeleteCamper() 
        {
            
            
        }

        public EditCamperModel EditCamper(int id)
        {
            var camper =  UnitOfWork.Campers.Get()

                .Where(c => c.Id == id)
                .Select(c => new EditCamperModel
                {
                    Id = c.Id,
                    Email =  c.Email,
                    FirstName =  c.FirstName,
                    LastName = c.LastName, 
                    BirthDay = c.BirthDate
                })
                .SingleOrDefault();
            return camper;
        }

        public void EditCamper(EditCamperModel model)
        {

            var validationResult = EditCamperValidator.Validate(model);
            if (!validationResult.IsValid)
            {
                var X = 3;
                validationResult.ThenThrow();
            }



            var camper = UnitOfWork.Campers.Get()
                .AsNoTracking()
                .SingleOrDefault(c => c.Id == model.Id);

            var editedCamper = Mapper.Map<EditCamperModel, Camper>(model);
            editedCamper.Password = camper.Password;

            UnitOfWork.Campers.Update(editedCamper);
            UnitOfWork.SaveChanges();


        }

        public void ChangePassword() { }

        public CamperProfileModel GetCamperProfile(int id)
        {
            
            var camper =  UnitOfWork.Campers.Get()
                .Where(c => c.Id == id)
                .SingleOrDefault();

            var camperBadges = UnitOfWork.CamperBadges.Get()
                .Where(cb => cb.CamperId == id)
                .ToList();

            var camperBadgesList = new List<CamperBadgeModel>();

            foreach (var camperBadge in camperBadges)
            {
                var badge = UnitOfWork.Badges.Get()
                    .Where(b => b.Id == camperBadge.BadgeId)
                    .SingleOrDefault();

                camperBadgesList.Add(new CamperBadgeModel
                {
                    CamperId = camperBadge.CamperId,
                    Score = camperBadge.Score,
                    Badge = new BadgeModel
                    {
                        Name = badge.Name,
                        Milestone = badge.Milestone,
                        ImageId = badge.ImageId,
                    }
                });
            }
            var trips = new List<Trip>();
            if (camper.Id == CurrentCamper.Id)
            {
                trips = UnitOfWork.Trips.Get()
                    .Include(tc => tc.TripCampers)
                    .Where(t => t.TripCampers.Any(tc => tc.IsOrganizer && tc.CamperId == camper.Id))
                    .ToList();
            }
            else
            {
                trips = UnitOfWork.Trips.Get()
                        .Include(tc => tc.TripCampers)
                        .Where(t => t.IsPublic &&
                            t.TripCampers.Any(tc => tc.IsOrganizer && tc.CamperId == camper.Id))
                        .ToList();
            }

            var profileTrips = new List<TripForCamperProfileModel>();
            foreach(var trip in trips)
            {
                profileTrips.Add(new TripForCamperProfileModel
                {
                    Id = trip.Id,
                    Name = trip.Name,
                    Description = trip.Description,
                    Date = trip.Date,
                    isPublic = trip.IsPublic,
                    Code = trip.Code
                });
            }
            var camperProfileDebugger = new CamperProfileModel
            {
                Id = camper.Id,
                FirstName = camper.FirstName,
                LastName = camper.LastName,
                Email = camper.Email,
                CamperBadges = camperBadgesList,
                Trips = profileTrips,
                isBanned = camper.IsBanned
            };
            return camperProfileDebugger;
        }

        public bool CheckCamperOwner(int id)
        {
            return id == CurrentCamper.Id;
        }
        public void BanCamper(int id)
        {
            var camper = UnitOfWork.Campers.Get()
                .SingleOrDefault(c => c.Id == id);
            camper.IsBanned = true;

            UnitOfWork.Campers.Update(camper);
            UnitOfWork.SaveChanges();
        }
        public void UnBanCamper(int id)
        {
            var camper = UnitOfWork.Campers.Get()
                .SingleOrDefault(c => c.Id == id);
            camper.IsBanned = false;

            UnitOfWork.Campers.Update(camper);
            UnitOfWork.SaveChanges();
        }
        public bool IdExists(int id)
        {
            var camper = UnitOfWork.Campers.Get()
                .SingleOrDefault(r => r.Id == id);

            return camper != null;

        }
    }
}

