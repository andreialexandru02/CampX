using CampX.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Map.Models
{
    public class CampsiteDetailsModel
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int Difficulty { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public List <int> ImageIds { get; set; }
    }
}
