using CampX.BusinessLogic.Implementations.Badges.Models;
using CampX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Campers.Models
{
    public class CamperBadgeModel
    {

        public int CamperId { get; set; }

        public int Score { get; set; }

        public BadgeModel Badge { get; set; } = null!;

    }
}
