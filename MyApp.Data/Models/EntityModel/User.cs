using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;

public class User:BaseEntity
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public DateOnly DateofBirth { get; set; }
    public string Email { get; set; } = null!;

  
    public byte[] Password { get; set; } = null!;
    public virtual PhotoUser? PhotoUs { get; set; }
	public virtual Cart? Cart { get; set; }
	public virtual LikedItem? LikedItem { get; set; }
	public virtual IEnumerable<CreditCard>? CreditCards { get; } = [];
	public virtual IEnumerable<Order>? History { get; } = [];

}
