namespace Shop.Api.DTOs;

public class ProductCreateDto
{
    public string Name { get; set; } = "";

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}