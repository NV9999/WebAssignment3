using System;
using System.Collections.Generic;

namespace Assignment3.Data;

public partial class Product
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public decimal? Pricing { get; set; }

    public decimal? ShippingCost { get; set; }

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
}
