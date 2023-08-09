using CampX.BusinessLogic.Implementations.Campers.Models;
using CampX.BusinessLogic.Implementations.Trips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Requests.Models
{
    public class AddRequestModel
    {

        public int CamperId { get; set; } 
 
        public ShowTripsModel Trip { get; set; }
 
        public string? Description { get; set; }
    }
}
