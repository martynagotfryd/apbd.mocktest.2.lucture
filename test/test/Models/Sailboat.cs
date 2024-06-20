using System;
using System.Collections.Generic;

namespace test.Models;

public partial class Sailboat
{
    public int Id { get; set; }

    public int? IdBoatStandard { get; set; }

    public string? Name { get; set; }

    public virtual BoatStandard? IdBoatStandardNavigation { get; set; }

    public virtual ICollection<Reservation> IdReservations { get; set; } = new List<Reservation>();
}
