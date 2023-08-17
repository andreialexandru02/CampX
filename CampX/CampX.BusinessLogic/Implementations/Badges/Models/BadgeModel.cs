using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.BusinessLogic.Implementations.Badges.Models
{
    public class BadgeModel
    {
        public string Name { get; set; } = null!;

        public int Milestone { get; set; }

        public int ImageId { get; set; }
    }
}
