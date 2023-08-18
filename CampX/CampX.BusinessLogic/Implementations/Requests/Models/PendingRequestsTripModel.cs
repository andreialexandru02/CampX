using CampX.BusinessLogic.Implementations.Trips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Requests.Models
{
    public class PendingRequestsTripModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }

        public List<TripCamperOrganizerModel> TripCampers { get; set; }
    }
}
