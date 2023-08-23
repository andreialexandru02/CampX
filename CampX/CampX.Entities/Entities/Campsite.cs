using CampX.Entities;
using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Campsite
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Difficulty { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public int CamperId { get; set; }

    public virtual Camper? Camper { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
