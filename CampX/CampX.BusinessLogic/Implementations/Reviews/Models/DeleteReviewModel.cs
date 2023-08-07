using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Reviews.Models
{
    public class DeleteReviewModel
    {
        public int Id { get; set; }

        public int? CampsiteId { get; set; }
    }
}
