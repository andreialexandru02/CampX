using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX;

public partial class Badge : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Milestone { get; set; }

    public int ImageId { get; set; }

    public virtual ICollection<CamperBadge> CamperBadges { get; set; } = new List<CamperBadge>();

    public virtual Image Image { get; set; } = null!;
}
