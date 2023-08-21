using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Reviews.Models
{
    public class AddReviewModel
    {
        public int Rating { get; set; }

        public string? Content { get; set; }

        public int? CampsiteId { get; set; }
        public int? CamperId { get; set; }
    }
}
