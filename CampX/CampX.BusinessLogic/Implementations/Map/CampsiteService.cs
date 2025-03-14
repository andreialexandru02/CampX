﻿using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Account;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.BusinessLogic.Implementations.Images;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Map.Validations;
using CampX.Common.Extensions;
using CampX.Common.ViewModels;
using CampX.DataAccess;
using CampX.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Map
{
    public class CampsiteService : BaseService
    {
        private readonly CampsiteValidator CampsiteValidator;
        private readonly EditCampsiteValidator EditCampsiteValidator;


        public CampsiteService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.CampsiteValidator = new CampsiteValidator(UnitOfWork);
            this.EditCampsiteValidator = new EditCampsiteValidator();
        }


        public List<ShowCampsitesModel> GetCampsites()
        {

            var campsites = UnitOfWork.Campsites.Get()
                .Select(c => new ShowCampsitesModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Difficulty = c.Difficulty,
                    Rating = c.Rating
                })
                .ToList();

            return campsites;
        }

        public void AddCampsite(AddCampsiteModel model, List<int> imgList)
        {

            CampsiteValidator.Validate(model).ThenThrow();

            var campsite = Mapper.Map<AddCampsiteModel, Campsite>(model);

            //camper.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password, 13);

            // camper.Roles.Add(UnitOfWork.Roles.Get().Where(r => r.Id == 3).SingleOrDefault());

            var images = UnitOfWork.Images.Get()
                .Where(i => imgList.Contains(i.Id))
                .ToList();

            campsite.Images = images;

            campsite.Rating = 0;

            var insertedCampsite = UnitOfWork.Campsites.Insert(campsite);

            Console.WriteLine(insertedCampsite);

            // trigger mail notifi
            // insert audit 

            UnitOfWork.SaveChanges();
        }

        public List<DisplayCampsitesModel> DisplayCampsites()
        {
            List<DisplayCampsitesModel> campsites = UnitOfWork.Campsites.Get()
            .Select(c => new DisplayCampsitesModel
            {
                Id = c.Id
                ,
                Latitude = c.Latitude
                ,
                Longitude = c.Longitude,
            })
            .ToList();

            UnitOfWork.SaveChanges();
            return campsites;
        }

        public CampsiteDetailsModel CampsiteDetails(int id)
        {
            var campsite = UnitOfWork.Campsites.Get()
            //.Include(c => c.Images)
            .Where(c => c.Id == id)
            .Select(c => new CampsiteDetailsModel
            {
                Name = c.Name
                ,
                Description = c.Description
                ,
                Difficulty = c.Difficulty
                ,
                Latitude = c.Latitude
                ,
                Longitude = c.Longitude
                ,
                ImageIds = c.Images.Select(i => i.Id).ToList()
                ,
                CamperId = c.CamperId

            })
            .SingleOrDefault();

            //UnitOfWork.SaveChanges();
            return campsite;
        }

        public EditCampsiteModel CampsiteToEdit(int id)
        {
            var campsite = Mapper.Map<CampsiteDetailsModel, EditCampsiteModel>(CampsiteDetails(id));

            return campsite;
        }

        public void DeleteCampsite(int id)
        {
            UnitOfWork.Campsites.Delete(
                UnitOfWork.Campsites.Get()
                 .Where(c => c.Id == id)
                 .SingleOrDefault()
            );
            UnitOfWork.SaveChanges();
        }
        public void EditCampsite(EditCampsiteModel model, List<int> imgList, int id)
        {
            EditCampsiteValidator.Validate(model).ThenThrow();

            var campsite = UnitOfWork.Campsites.Get()
                .Include(c => c.Images)
                .Where(c => c.Id == id)
                .AsNoTracking()
                .SingleOrDefault();

            campsite = Mapper.Map<EditCampsiteModel, Campsite>(model);

            if (imgList.Count != 0)
            {

                campsite.Images = UnitOfWork.Images.Get()
                    .Where(i => imgList.Contains(i.Id))
                    .ToList(); ;

            }

            campsite.Id = id;

            UnitOfWork.Campsites.Update(campsite);

            UnitOfWork.SaveChanges();
        }
        public bool CheckCampsiteOwner(int id)
        {
            return UnitOfWork.Campsites.Get()
                .Where(r => r.Id == id)
                .Select(r => r.CamperId)
                .SingleOrDefault() == CurrentCamper.Id ||
                CurrentCamper.Roles.Contains("Admin");
        }
        public bool IdExists(int id)
        {
            var campsite = UnitOfWork.Campsites.Get()
                .SingleOrDefault(r => r.Id == id);

            return campsite != null;

        }
    }
}
