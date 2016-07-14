using System;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Services
{
	public class ShortenSha1 : IShortLinkGenerationAlgorithm
	{
		public string AlgorithmName
		{
			get
			{
				return "sha1";
			}
		}

		public string Shorten(string url)
		{
			var shortUrlCode = BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(url)))
				.Replace("-", "");
			return shortUrlCode;
		}
	}
}
