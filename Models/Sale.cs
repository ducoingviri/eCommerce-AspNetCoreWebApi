using System;
using System.Collections.Generic;

namespace App02.Models;

public partial class Sale
{
    public int Id { get; set; }

    public string AspNetUserId { get; set; } = null!;

    public int ProductId { get; set; }

    public DateTime PurchasedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
