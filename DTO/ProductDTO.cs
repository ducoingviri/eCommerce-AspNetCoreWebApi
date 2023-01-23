namespace App02.DTO;

public class ProductDTO
{
    
    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string Photo { get; set; } = null!;

    public int Stock { get; set; }
}