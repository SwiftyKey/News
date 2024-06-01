using System.ComponentModel.DataAnnotations;

namespace News.Models.Common;

public abstract class BaseEntity: BaseChanged
{
	[Key]
	public int Id { get; set; }
}
