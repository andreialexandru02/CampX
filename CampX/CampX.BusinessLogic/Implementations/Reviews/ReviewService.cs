using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Campers.Models;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Map.Validations;
using CampX.BusinessLogic.Implementations.Requests.Models;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.BusinessLogic.Implementations.Reviews.Validations;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Common.Extensions;
using CampX.DataAccess;
using CampX.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Reviews
{
    public class ReviewService : BaseService
    {
        private readonly ReviewValidator ReviewValidator;
        private readonly ReviewEditValidator ReviewEditValidator;

        public ReviewService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.ReviewValidator = new ReviewValidator(UnitOfWork);
            this.ReviewEditValidator = new ReviewEditValidator(UnitOfWork);
        }
        public List<ReviewModel> ShowReviews(int id)
        {
            var reviews = UnitOfWork.Reviews.Get()
            .Where(r => r.CampsiteId == id && r.Pending == false)
            .Select(r => new ReviewModel
            {
                Id = r.Id
                ,
                Rating = r.Rating
                ,
                Content = r.Content
                ,
                CamperId = r.CamperId

            })
            .ToList();
            // UnitOfWork.SaveChanges();
            return reviews;
        }
        public void DeleteReview(int id)
        {
            UnitOfWork.Reviews.Delete(
                UnitOfWork.Reviews.Get()
                 .Where(r => r.Id == id)
                 .SingleOrDefault()
            );
            UnitOfWork.SaveChanges();
        }
        public void AcceptPendingReview(int id)
        {
            var review = UnitOfWork.Reviews.Get().Where(r => r.Id == id).SingleOrDefault();

            review.Pending = false;
            UnitOfWork.Reviews.Update(review);

            UnitOfWork.SaveChanges();

        }


        public void AddReview(AddReviewModel model)
        {
            ReviewValidator.Validate(model).ThenThrow();

            var review = Mapper.Map<AddReviewModel, Review>(model);

            review.Pending = true;

            UnitOfWork.Reviews.Insert(review);

            UnitOfWork.SaveChanges();
            var campsite = UnitOfWork.Campsites.Get()
                .SingleOrDefault(c => c.Id == model.CampsiteId);

            var reviews = UnitOfWork.Reviews.Get()
                .Where(r => r.CampsiteId == model.CampsiteId)
                .ToList();


            campsite.Rating *= reviews.Count() - 1;
            campsite.Rating += model.Rating;
            campsite.Rating /= reviews.Count();
            campsite.Rating = Math.Round(campsite.Rating, 1);

            UnitOfWork.Campsites.Update(campsite);
            UnitOfWork.SaveChanges();
        }


        public void EditReview(EditReviewModel model)
        {
            ReviewEditValidator.Validate(model).ThenThrow();


            var review = UnitOfWork.Reviews.Get()
                .Where(c => c.Id == model.Id)
                .AsNoTracking()
                .SingleOrDefault();

            review = Mapper.Map<EditReviewModel, Review>(model);

            UnitOfWork.Reviews.Update(review);


            UnitOfWork.SaveChanges();
        }
        public bool CheckReviewOwner(int id)
        {
            var reviewOwnerId = UnitOfWork.Reviews.Get()
                .Where(r => r.Id == id)
                .Select(r => r.CamperId)
                .SingleOrDefault();

            return reviewOwnerId == CurrentCamper.Id || CurrentCamper.Roles.Contains("Admin");
        }
        public List<PendingReviewModel> ShowPendingReviews()
        {
            var reviews = UnitOfWork.Reviews.Get()
                .Include(r => r.Campsite)
                .Where(r => r.Pending == true)
                .Select(r => new PendingReviewModel
                {
                    Campsite = new ShowCampsitesModel
                    {
                       Name = r.Campsite.Name,   
                       Id = r.Campsite.Id
                    },
                    Id = r.Id,
                    Content = r.Content,
                    Rating = r.Rating
                    

                })
                .ToList();
            return reviews;
        }
    }



}
