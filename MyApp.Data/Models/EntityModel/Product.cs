using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;

public class Product:BaseEntity
{
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }   
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
    public virtual PhotoProduct? Photo { get; set; }
    public virtual IEnumerable<OrderItem> OrderItems { get; set; } = [];

}
