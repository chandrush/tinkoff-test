using Domain.AppService.Dto;
using Domain.Models;
using Domain.Services;
using Domain.Stores;
using System;
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
		private IBitlyUow _bitlyUow;

		public BitlyAppService(IBitlyUow bitlyUow)
		{
			_bitlyUow = bitlyUow;
		}

		/// <summary>
		/// Процедура сжатия ссылки.
		/// </summary>
		/// <param name="url">Оригинальная ссылка.</param>
		/// <returns>Сокращенная ссылка.</returns>
		public async Task<string> ShortenLinkAsync(string url, Guid userId)
		{
			using (var transaction = await _bitlyUow.BeginTransactionAsync())
			{
				UserLink userLink;
				var shortenLink = await _bitlyUow.Links.GetLinkAsync(url);
				if (shortenLink == null)
				{
					var bitlyService = new BitlyService(new ShortenSha1());
					shortenLink = await bitlyService.ShortenLinkAsync(url);
					_bitlyUow.Links.AddLink(shortenLink);
					userLink = new UserLink(userId, shortenLink.Id); //TODO: ?похоже баг в efcore1.0, нельзя просто засунуть shortenLink, то же с аггрегатом
				}
				else
				{
					userLink = await _bitlyUow.Links.GetUserLinkAsync(userId, shortenLink.Id) ?? 
						new UserLink(userId, shortenLink.Id);
				}

				if (userLink.IsTransient)
					_bitlyUow.Links.AddUserLink(userLink);
				await _bitlyUow.SaveAsync();
				transaction.Commit();
				return shortenLink.ShortenLinkCode; //TODO: получать хост текущего приложения и конкатенировать
			}
		}

		public async Task<IEnumerable<LinkDto>> GetLinksAsync(Guid userId, string hostBase)
		{
			return (await _bitlyUow.Links.GetUserLinksAsync(userId))
				.Select(x => new LinkDto(x.OriginalLink, hostBase + "i/" + x.ShortenLinkCode, x.UsesNumber, x.CreationDateUTC));
		}

		/// <summary>
		/// Метод получения полного url по короткому url для редиректа.
		/// </summary>
		/// <param name="shortUrlCode">Короткий код url</param>
		/// <returns></returns>
		public async Task<string> GetRedirectionAsync(string shortUrlCode)
		{
			using (var transaction = await _bitlyUow.BeginTransactionAsync())
			{
				var link = await _bitlyUow.Links.GetLinkByShortCodeAsync(shortUrlCode);
				if (link != null)
				{
					link.IncrementLinkUses();
					_bitlyUow.Links.UpdateLink(link);
					await _bitlyUow.SaveAsync();
					transaction.Commit();
					//TODO: повторное извлечение и инкремент в случае ошибки на concurrencyToken
					//для оригинального bitly было бы выгоднее использовать какой-нибудь лёгкий вариант очереди для инкрементов
					//для проверки существования ссылки выгодно использовать Bloom filter

					return link.OriginalLink;
				}
				else
				{
					return "/"; //TODO: подготовить страничку с сообщением, что такая ссылка не зарегистрирована
				}
			}
		}
    }
}
