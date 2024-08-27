using MyApp.Data.Models.BaseEntityModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Data.Models.EntityModel;

public class PhotoProduct:BaseEntity
{
	public byte[] Bytes { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string FileExtension { get; set; } = null!;
	public decimal Size { get; set; }
	public int ProductId { get; set; }
	public virtual Product Product { get; set; } = null!;
}
