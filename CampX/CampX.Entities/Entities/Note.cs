using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Note : IEntity
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public virtual ICollection<TripCamper> TripCampers { get; set; } = new List<TripCamper>();
}
