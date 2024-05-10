using System.Security.Cryptography;
using System.Text;

namespace News.Models.Entities.Hashers;

public static class SHA256Hasher
{
	public static string Hash(string input)
	{
		return Convert.ToHexString(SHA256.HashData(Encoding.ASCII.GetBytes(input)));
	}
}
