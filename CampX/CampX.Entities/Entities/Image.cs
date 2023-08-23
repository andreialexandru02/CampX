using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Image
{
    public int Id { get; set; }

    public byte[] ImageData { get; set; } = null!;

    public virtual ICollection<Badge> Badges { get; set; } = new List<Badge>();

    public virtual ICollection<Camper> Campers { get; set; } = new List<Camper>();

    public virtual ICollection<Campsite> Campsites { get; set; } = new List<Campsite>();
}
