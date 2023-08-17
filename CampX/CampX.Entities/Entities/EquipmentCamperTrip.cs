using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX;

public partial class EquipmentCamperTrip : IEntity
{
    public int CamperId { get; set; }

    public int EquipmentId { get; set; }

    public int TripId { get; set; }

    public virtual Camper Camper { get; set; } = null!;

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual Trip Trip { get; set; } = null!;
}
