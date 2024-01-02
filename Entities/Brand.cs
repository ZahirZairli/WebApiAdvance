namespace WebApiAdvance.Entities;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<Product>? Products { get; set; }
}
