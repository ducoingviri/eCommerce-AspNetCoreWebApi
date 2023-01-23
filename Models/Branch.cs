using System;
using System.Collections.Generic;

namespace App02.Models;

public partial class Branch
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Inventory> Inventories { get; } = new List<Inventory>();
}
