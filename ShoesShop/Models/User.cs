using System;
using System.Collections.Generic;

namespace ShoesShop.Models;

public partial class User
{
    public int UserId { get; set; }

    public int? UserRole { get; set; }

    public string? UserSurname { get; set; }

    public string? UserName { get; set; }

    public string? UserPatronymic { get; set; }

    public string? UserLogin { get; set; }

    public string? UserPassword { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual UserRole? UserRoleNavigation { get; set; }
}
