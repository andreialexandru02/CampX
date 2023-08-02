using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Camper
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public int? ImageId { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<CamperBadge> CamperBadges { get; set; } = new List<CamperBadge>();

    public virtual ICollection<EquipmentCamperTrip> EquipmentCamperTrips { get; set; } = new List<EquipmentCamperTrip>();

    public virtual Image? Image { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<TripCamper> TripCampers { get; set; } = new List<TripCamper>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
