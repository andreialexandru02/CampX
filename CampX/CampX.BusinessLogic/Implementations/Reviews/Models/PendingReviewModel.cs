using CampX.BusinessLogic.Implementations.Map.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Reviews.Models
{
    public class PendingReviewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Content { get; set; }
        public ShowCampsitesModel? Campsite { get; set; }
    }
}
