using News.Models.Common.Hashers;
using System.Security.Cryptography;
using System.Text;

namespace News.Models.Entities.Hashers;

public class SHA256Hasher : IHasher
{
	public string Hash(string input)
	{
		return Convert.ToHexString(SHA256.HashData(Encoding.ASCII.GetBytes(input)));
	}
}
