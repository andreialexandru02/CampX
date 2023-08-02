using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Review : IEntity
{
    public int Id { get; set; }

    public int Rating { get; set; }

    public string? Content { get; set; }

    public int? IdCampsite { get; set; }

    public virtual Campsite? IdCampsiteNavigation { get; set; }
}
