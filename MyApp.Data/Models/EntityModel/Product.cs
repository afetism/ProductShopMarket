using MyApp.Data.Models.BaseEntityModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Data.Models.EntityModel;

public class Product:BaseEntity
{
	private int currentCount;

	public string Name { get; set; } = null!;
    public int Quantity { get; set; }   
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
    public virtual PhotoProduct? Photo { get; set; }
    public virtual IEnumerable<OrderItem> OrderItems { get; set; } = [];
	[NotMapped]
	public int CurrentCount {
        get => currentCount;
        set
        {
            currentCount=value;
			OnPropertyChanged(nameof(CurrentCount));

		}
    }

}
