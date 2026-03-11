using System;
using System.Collections.Generic;

namespace ShoesShop.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? OrderDeliveryDate { get; set; }

    public int? OrderPickupPoint { get; set; }

    public int? OrderClientId { get; set; }

    public string? OrderCode { get; set; }

    public string? OrderStatus { get; set; }

    public virtual User? OrderClient { get; set; }

    public virtual ICollection<OrderComposition> OrderCompositions { get; set; } = new List<OrderComposition>();

    public virtual PickUpPoint? OrderPickupPointNavigation { get; set; }

    public string OrderPickUpAddress => $"{OrderPickupPointNavigation.PickUpPointIndex}, {OrderPickupPointNavigation.PickUpPointCity}," +
        $" ул. {OrderPickupPointNavigation.PickUpPointStreet}, {OrderPickupPointNavigation.PickUpPointHouse}";
}
