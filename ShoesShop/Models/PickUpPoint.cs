using System;
using System.Collections.Generic;

namespace ShoesShop.Models;

public partial class PickUpPoint
{
    public int PickUpPointId { get; set; }

    public string? PickUpPointIndex { get; set; }

    public string? PickUpPointCity { get; set; }

    public string? PickUpPointStreet { get; set; }

    public string? PickUpPointHouse { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public string PickUpAddress => $"{PickUpPointIndex}, {PickUpPointCity}," +
       $" ул. {PickUpPointStreet}, {PickUpPointHouse}";
}
