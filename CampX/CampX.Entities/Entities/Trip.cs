using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Trip
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsPublic { get; set; }

    public DateTime? Date { get; set; }

    public string Code { get; set; } = null!;

    public int Nights { get; set; }

    public virtual ICollection<EquipmentCamperTrip> EquipmentCamperTrips { get; set; } = new List<EquipmentCamperTrip>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<TripCamper> TripCampers { get; set; } = new List<TripCamper>();

    public virtual ICollection<Campsite> Campsites { get; set; } = new List<Campsite>();
}
