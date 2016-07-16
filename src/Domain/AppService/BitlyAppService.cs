using Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.AppService
{
	/// <summary>
	/// Сервис приложения.
	/// </summary>
    public class BitlyAppService
    {
		/// <summary>
		/// Процедура сжатия ссылки.
		/// </summary>
		/// <param name="url">Оригинальная ссылка.</param>
		public void ShortenLink(string url)
		{

		}

		public async Task<IEnumerable<Link>> GetLinksAsync(Guid userId) //TODO: возвращать нужно DTO
		{
			var testLinks = new[] {
				new Link("http://yandex.ru", "http://bitly.ru/ya", DateTime.UtcNow),
				new Link("http://google.ru", "http://bitly.ru/goo", DateTime.UtcNow - TimeSpan.FromDays(1)),
			};

			return testLinks;
		}
    }
}
