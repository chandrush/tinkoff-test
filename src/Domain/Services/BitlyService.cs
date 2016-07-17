using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
	/// <summary>
	/// Сервис, реализующий логику сокращения ссылки.
	/// </summary>
	public class BitlyService
    {
		private readonly IShortLinkGenerationAlgorithm _shortenAlgorithm;

		public BitlyService(IShortLinkGenerationAlgorithm shortenAlgorithm)
		{
			if (shortenAlgorithm == null)
				throw new ArgumentNullException(nameof(shortenAlgorithm));

			_shortenAlgorithm = shortenAlgorithm;
		}

		/// <summary>
		/// Реализация задачи сокращения ссылки.
		/// </summary>
		/// <param name="url">Url для сокращения</param>
		/// <returns>Экземпляр <see cref="Models.Link"/>, описывающий сокращенную ссылку. </returns>
		public Task<Link> ShortenLinkAsync(string url)
		{
			Uri urlUri;
			if (!Uri.TryCreate(url, UriKind.Absolute, out urlUri))
				throw new ArgumentException("Формат переданной строки не соответствует формату URL.");

			//пусть будет 6 первых символов
			//для более реалистичного сокращателя ссылок, нужно разработать
			//алгоритм хэширования, логику детекции и разрешения коллизий
			var shortUrlCode = _shortenAlgorithm.Shorten(url)
				.Substring(0, 6)
				.ToLowerInvariant(); 

			var link = new Link(url, shortUrlCode, DateTime.UtcNow);

			return Task.FromResult(link);
		}
    }
}
