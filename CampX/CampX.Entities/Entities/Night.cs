using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Night
{
    public int TripId { get; set; }

    public int CampsiteId { get; set; }

    public int NumberOfNights { get; set; }

    public virtual Campsite Campsite { get; set; } = null!;
    public virtual Trip Trip { get; set; } = null!;
}
