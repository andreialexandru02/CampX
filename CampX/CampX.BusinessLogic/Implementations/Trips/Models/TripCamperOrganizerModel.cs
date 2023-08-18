using CampX.BusinessLogic.Implementations.Campers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Trips.Models
{
    public class TripCamperOrganizerModel
    {
        public CamperModel? Camper { get; set; }

        public bool IsOrganizer { get; set; }
    }
}
