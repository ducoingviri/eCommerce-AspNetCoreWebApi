using System;
using System.Collections.Generic;

namespace App02.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public int ProductId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
