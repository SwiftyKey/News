using News.Models.Common;
using News.Models.Hashers;
using System.ComponentModel.DataAnnotations.Schema;

namespace News.Models.Entities;

public class User : BaseEntity
{
	public required string Login { get; set; }
	string hashPassword = null!;
	public required string HashPassword 
	{
		get { return hashPassword; }
		set
		{
			hashPassword = SHA256Hasher.Hash(value);
		}
	}
	public List<Source> Sources { get; set; } = [];

	public List<Feed> FeedsFavourites { get; set; } = [];
	public List<Favourites> Favourites { get; set; } = [];

	public List<Feed> FeedsReadLater { get; set; } = [];
	public List<ReadLater> ReadLater { get; set; } = [];
}
