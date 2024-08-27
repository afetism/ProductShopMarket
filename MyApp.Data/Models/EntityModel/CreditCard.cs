using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;

public class CreditCard : BaseEntity
{
	
	public string Number { get; set; } = null!;
	public DateTime ExpirationDate { get; set; }
	public string CVV { get; set; } = null!;
	public int UserId { get; set; }
	public virtual User User { get; set; } = null!;
}
