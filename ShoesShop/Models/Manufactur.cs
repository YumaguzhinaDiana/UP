using System;
using System.Collections.Generic;

namespace ShoesShop.Models;

public partial class Manufactur
{
    public int ManufacturId { get; set; }

    public string? ManufacturName { get; set; }

    public virtual ICollection<Tovar> Tovars { get; set; } = new List<Tovar>();
}
