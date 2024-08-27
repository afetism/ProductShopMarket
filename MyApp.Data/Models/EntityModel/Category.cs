using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;
public class Category:BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual List<Product> Products { get; set; }

}
