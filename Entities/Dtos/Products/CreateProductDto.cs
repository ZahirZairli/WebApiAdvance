namespace WebApiAdvance.Entities.Dtos.Products;
public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int BrandId { get; set; }
}
