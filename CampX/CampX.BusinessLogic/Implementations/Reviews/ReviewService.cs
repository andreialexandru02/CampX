using CampX.BusinessLogic.Base;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.BusinessLogic.Implementations.Map.Validations;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.BusinessLogic.Implementations.Reviews.Validations;
using CampX.DataAccess;
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

        public ReviewService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.ReviewValidator = new ReviewValidator(UnitOfWork);
        }
        public List<ReviewModel> ShowReviews(int id)
        {
            var reviews = UnitOfWork.Reviews.Get()
            .Where(r => r.CampsiteId == id)
            .Select(r => new ReviewModel
            {
                Id = r.Id
                ,Rating = r.Rating
                ,Content = r.Content

            })
            .ToList();
            UnitOfWork.SaveChanges();
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
    }

    
}
