

using MyApp.Data.Models.BaseEntityModel;

namespace MyApp.Data.Models.EntityModel;

public class PhotoUser:BaseEntity
{
	public byte[] Bytes { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string FileExtension { get; set; } = null!;
	public decimal Size { get; set; }
	public int UserId { get; set; }
	public virtual User User { get; set; } = null!;
}
