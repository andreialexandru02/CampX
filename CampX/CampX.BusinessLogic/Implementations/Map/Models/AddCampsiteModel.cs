using CampX.BusinessLogic.Implementations.Images.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Map.Models
{
    public class AddCampsiteModel
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int Difficulty { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public List<IFormFile>? Images { get; set; }
    }
}
