using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Nights.Models
{
    public class AddNightsModel
    {
        public int TripId { get; set; }

        public int CampsiteId { get; set; }

        public int NumberOfNights { get; set; }
    }
}
