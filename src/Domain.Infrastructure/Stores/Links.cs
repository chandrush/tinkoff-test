using Domain.Models;
using Domain.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Stores
{
    public class Links: ILinks
	{
		private readonly IBitlyDbContext _bitlyDbContext;

		internal Links(IBitlyDbContext bitlyDbContext)
		{
			_bitlyDbContext = bitlyDbContext;
		}

		public void AddLink(UserLink userLink)
		{
			_bitlyDbContext.UserLinks.Add(userLink);
		}

		public Task<Link> GetLinkAsync(string linkUrl)
		{
			return _bitlyDbContext.Links
				.FirstOrDefaultAsync(x => x.OriginalLink == linkUrl);
		}

		public Task<Link> GetLinkByShortCodeAsync(string shortLinkCode)
		{
			return _bitlyDbContext.Links
				.FirstOrDefaultAsync(x => x.ShortenLinkCode == shortLinkCode);
		}

		public async Task<IEnumerable<Link>> GetLinksAsync(Guid userId)
		{
			return (await _bitlyDbContext.UserLinks
				.Include(x => x.Link)
				.Where(x => x.UserId == userId)
				.ToListAsync().ConfigureAwait(false))
				.Select(x => x.Link);
		}

		public void UpdateLink(Link link)
		{
			if (link == null)
				throw new ArgumentNullException("link");
			if (link.IsTransient)
				throw new ArgumentException(@"Обновляемый объект должен иметь заполненое поле Id. 
Возможно, вместо обновления, требуется процедура добавления.");

			_bitlyDbContext.Links.Update(link);
		}
	}
}
