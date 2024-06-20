using System;
using System.Collections.Generic;

namespace test.Models;

public partial class BoatStandard
{
    public int Id { get; set; }

    public int? Level { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Sailboat> Sailboats { get; set; } = new List<Sailboat>();
}
