using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class EquipmentCamperTrip : IEntity
{
    public int IdCamper { get; set; }

    public int IdEquipment { get; set; }

    public int IdTrip { get; set; }

    public virtual Camper IdCamperNavigation { get; set; } = null!;

    public virtual Equipment IdEquipmentNavigation { get; set; } = null!;

    public virtual Trip IdTripNavigation { get; set; } = null!;
}
