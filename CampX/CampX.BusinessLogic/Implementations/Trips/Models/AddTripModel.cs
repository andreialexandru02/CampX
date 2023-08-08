using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Trips.Models
{
    public class AddTripModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? IsPublic { get; set; }

        public virtual ICollection<TripCamper> TripCampers { get; set; } = new List<TripCamper>();

        public virtual ICollection<Campsite> Campsites { get; set; } = new List<Campsite>();
    }
}
