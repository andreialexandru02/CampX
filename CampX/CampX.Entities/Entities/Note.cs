﻿using System;
using System.Collections.Generic;

namespace CampX;

public partial class Note
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int TripId { get; set; }

    public int CamperId { get; set; }

    public virtual TripCamper TripCamper { get; set; } = null!;
}
