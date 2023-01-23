using System;
using System.Collections.Generic;

namespace App02.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string Photo { get; set; } = null!;

    public int Stock { get; set; }

    public virtual ICollection<Inventory> Inventories { get; } = new List<Inventory>();

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
}
