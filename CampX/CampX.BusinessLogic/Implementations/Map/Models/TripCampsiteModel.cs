using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Map.Models
{
    public class TripCampsiteModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
