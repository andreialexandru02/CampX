using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX;

public partial class Equipment : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EquipmentCamperTrip> EquipmentCamperTrips { get; set; } = new List<EquipmentCamperTrip>();
}
