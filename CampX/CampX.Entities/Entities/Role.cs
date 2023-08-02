using CampX.Common;
using System;
using System.Collections.Generic;

namespace CampX.Entities;

public partial class Role : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Camper> IdCampers { get; set; } = new List<Camper>();
}
