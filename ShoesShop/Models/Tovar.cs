using System;
using System.Collections.Generic;

namespace ShoesShop.Models;

public partial class Tovar
{
    public string TovarId { get; set; } = null!;

    public int? TovarType { get; set; }

    public string? TovarUnit { get; set; }

    public decimal? TovarPrice { get; set; }

    public int? TovarSupplyer { get; set; }

    public int? TovarManufactur { get; set; }

    public int? TovarCategory { get; set; }

    public float? TovarCurrentDiscount { get; set; }

    public int? TovarStorageAmount { get; set; }

    public string? TovarDescription { get; set; }

    public string? TovarImage { get; set; }

    public string? TovarStatus { get; set; }

    public virtual ICollection<OrderComposition> OrderCompositions { get; set; } = new List<OrderComposition>();

    public virtual TovarCategory? TovarCategoryNavigation { get; set; }

    public virtual Manufactur? TovarManufacturNavigation { get; set; }

    public virtual Supplyer? TovarSupplyerNavigation { get; set; }

    public virtual TovarType? TovarTypeNavigation { get; set; }

    public string TovarPublicName => $"{TovarCategoryNavigation.TovarCategoryName} | {TovarTypeNavigation.TovarTypeName}";

    public string TovarImageString => string.IsNullOrEmpty(TovarImage) ? $"/resources/picture.png" : $"/resources/{TovarImage}";
    public string CurrentDiscountColor => TovarCurrentDiscount <= 15 ? "#FFFFFF" : "#2E8B57";
    public string StorageAmountColor => TovarStorageAmount == 0 ? "blue" : "black";


}
