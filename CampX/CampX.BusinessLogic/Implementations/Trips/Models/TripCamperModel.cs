using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampX.BusinessLogic.Implementations.Campers.Models;

namespace CampX.BusinessLogic.Implementations.Trips.Models
{
    public class TripCamperModel
    {
        public int TripId { get; set; }

        public CamperModel? Camper { get; set; }

        public bool IsOrganizer { get; set; }
    }
}
