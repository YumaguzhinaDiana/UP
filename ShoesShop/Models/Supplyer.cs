using System;
using System.Collections.Generic;

namespace ShoesShop.Models;

public partial class Supplyer
{
    public int SupplyerId { get; set; }

    public string? SupplyerName { get; set; }

    public virtual ICollection<Tovar> Tovars { get; set; } = new List<Tovar>();
}
