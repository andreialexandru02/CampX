using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class CamperBadge : IEntity
{
    public int IdBadge { get; set; }

    public int IdCamper { get; set; }

    public int Score { get; set; }

    public virtual Badge IdBadgeNavigation { get; set; } = null!;

    public virtual Camper IdCamperNavigation { get; set; } = null!;
}
