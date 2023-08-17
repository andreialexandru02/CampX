using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX;

public partial class Review : IEntity
{
    public int Id { get; set; }

    public int Rating { get; set; }

    public string? Content { get; set; }

    public int CampsiteId { get; set; }

    public virtual Campsite Campsite { get; set; } = null!;
}
