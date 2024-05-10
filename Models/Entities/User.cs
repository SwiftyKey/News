using News.Models.Common;
using News.Models.Entities.Hashers;

namespace News.Models.Entities;

public class User : BaseEntity
{
	public required string Login {  get; set; }
	string hashPassword = null!;
	public required string HashPassword 
	{
		get { return hashPassword; }
		set
		{
			hashPassword = SHA256Hasher.Hash(value);
		}
	}
	public IEnumerable<Source>? Sources { get; set; }
	public IEnumerable<Feed>? Favourites { get; set; }
	public IEnumerable<Feed>? FeedsReadLater { get; set; }
}
