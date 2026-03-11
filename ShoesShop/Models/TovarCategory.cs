using System;
using System.Collections.Generic;

namespace ShoesShop.Models;

public partial class TovarCategory
{
    public int TovarCategoryId { get; set; }

    public string? TovarCategoryName { get; set; }

    public virtual ICollection<Tovar> Tovars { get; set; } = new List<Tovar>();
}
