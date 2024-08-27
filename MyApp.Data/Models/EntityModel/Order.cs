using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;

public class Order:BaseEntity
{
    public DateOnly Date { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual IEnumerable<OrderItem> OrderItems { get; } = [];

}
