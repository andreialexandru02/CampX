using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX;

public partial class CamperBadge : IEntity
{
    public int BadgeId { get; set; }

    public int CamperId { get; set; }

    public int Score { get; set; }

    public virtual Badge Badge { get; set; } = null!;

    public virtual Camper Camper { get; set; } = null!;
}
