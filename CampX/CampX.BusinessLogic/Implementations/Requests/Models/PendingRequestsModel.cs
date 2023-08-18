using CampX.BusinessLogic.Implementations.Campers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Requests.Models
{
    public class PendingRequestsModel
    {
        public CamperModel Camper { get; set; }

        public PendingRequestsTripModel Trip { get; set; }

        public string? Description { get; set; }
    }
}
