using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Campsite : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Difficulty { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Image> IdImages { get; set; } = new List<Image>();

    public virtual ICollection<Trip> IdTrips { get; set; } = new List<Trip>();
}
