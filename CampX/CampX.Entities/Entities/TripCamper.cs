using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX;

public partial class TripCamper : IEntity
{
    public int TripId { get; set; }

    public int CamperId { get; set; }

    public bool IsOrganizer { get; set; }

    public virtual Camper Camper { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual Trip Trip { get; set; } = null!;
}
