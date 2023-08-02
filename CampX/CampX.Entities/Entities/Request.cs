using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Request : IEntity
{
    public int IdTrip { get; set; }

    public int IdCamper { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public virtual Camper IdCamperNavigation { get; set; } = null!;

    public virtual Trip IdTripNavigation { get; set; } = null!;
}
