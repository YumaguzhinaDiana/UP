using System;
using System.Collections.Generic;

namespace ShoesShop.Models;

public partial class OrderComposition
{
    public int OrderCompositionId { get; set; }

    public int? OcOrderId { get; set; }

    public string? OcTovarId { get; set; }

    public int? OcTovarAmount { get; set; }

    public virtual Order? OcOrder { get; set; }

    public virtual Tovar? OcTovar { get; set; }
}
