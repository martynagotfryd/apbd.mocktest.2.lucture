using System;
using System.Collections.Generic;

namespace test.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int? IdClient { get; set; }

    public DateOnly? DateFrom { get; set; }

    public DateOnly? DateTo { get; set; }

    public int? IdBoatStandard { get; set; }

    public int? NumOfBoats { get; set; }

    public decimal? Price { get; set; }

    public bool? Fulfilled { get; set; }

    public string? CancelReason { get; set; }

    public virtual BoatStandard? IdBoatStandardNavigation { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual ICollection<Sailboat> IdSailboats { get; set; } = new List<Sailboat>();
}
