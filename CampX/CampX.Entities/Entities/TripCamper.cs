using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class TripCamper
{
    public int TripId { get; set; }

    public int CamperId { get; set; }

    public int? NoteId { get; set; }

    public bool? IsOrganizer { get; set; }

    public virtual Camper Camper { get; set; } = null!;

    public virtual Note? Note { get; set; }

    public virtual Trip Trip { get; set; } = null!;
}
