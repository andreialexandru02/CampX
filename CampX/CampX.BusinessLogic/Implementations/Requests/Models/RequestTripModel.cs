using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Requests.Models
{
    public class RequestTripModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? Date { get; set; }
    }
}
