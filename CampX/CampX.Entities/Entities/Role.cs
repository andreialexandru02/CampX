using System;
using System.Collections.Generic;

namespace CampX;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Camper> Campers { get; set; } = new List<Camper>();
}
