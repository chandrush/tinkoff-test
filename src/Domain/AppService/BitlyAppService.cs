using Domain.Models;
using Domain.Services;
using Domain.Stores;
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

		public async Task<IEnumerable<Link>> GetLinksAsync(Guid userId)
		{
			//TODO: вообще говоря, из AppService возвращать нужно DTO
			return await _bitlyUow.Links.GetUserLinksAsync(userId);
		}
    }
}
