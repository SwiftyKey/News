namespace News.Models.Common.Hashers;

public interface IHasher
{
	public string Hash(string input);
}
