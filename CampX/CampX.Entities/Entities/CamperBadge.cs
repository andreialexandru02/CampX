using System;
using System.Collections.Generic;

namespace CampX;

public partial class CamperBadge
{
    public int BadgeId { get; set; }

    public int CamperId { get; set; }

    public int Score { get; set; }

    public virtual Badge Badge { get; set; } = null!;

    public virtual Camper Camper { get; set; } = null!;
}
