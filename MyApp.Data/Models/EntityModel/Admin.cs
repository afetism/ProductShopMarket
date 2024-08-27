using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;

public class Admin : BaseEntity
{
	public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
	public byte[] Password { get; set; } = null!;
	public string Email { get; set; } = null!;

}
