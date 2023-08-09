using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Trips.Models
{
    public class TripCampsitesModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int Difficulty { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
