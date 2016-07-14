using Domain.Models;
using Domain.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Infrastructure.Tests
{
    public class LinksStoreFixture
    {
		private ServiceCollection _services;
		private IServiceProvider _serviceProvider;

		public LinksStoreFixture()
		{
			_services = new ServiceCollection();
			_services.AddEntityFramework()
				.AddEntityFrameworkInMemoryDatabase()
				.AddRelational()
				.AddDbContext<BitlyDbContext>(options => options.UseInMemoryDatabase());

			_services.AddTransient<IBitlyUow, BitlyUow>();

			_serviceProvider = _services.BuildServiceProvider();
		}

		[Fact]
		public async Task Add_And_Get_New_Link()
		{
			var userId = Guid.NewGuid();
			using (var bitlyUow = _serviceProvider.GetRequiredService<IBitlyUow>())
			{
				//stage
				var link = new Link("http://yandex.ru", "ya");
				var userLink = new UserLink(userId, link);

				bitlyUow.Links.AddLink(userLink);
				await bitlyUow.SaveAsync();

				//act & assert
				Assert.NotEqual(0, link.Id);
				Assert.NotEqual(0, userLink.Id);

				//получение ссылки по короткому коду
				link = await bitlyUow.Links.GetLinkByShortCodeAsync("ya");
				Assert.NotNull(link);

				link = await bitlyUow.Links.GetLinkAsync("http://yandex.ru");
				Assert.NotNull(link);

				//получение списка ссылок пользователя
				var links = await bitlyUow.Links.GetLinksAsync(userId);
				Assert.Equal(1, links.Count());
			}
		}

		[Fact]
		public async Task Add_Link_By_Second_User()
		{
			//user1 добавил ссылку
			//позже user2 добавляет ту же ссылку

			var userId1 = Guid.NewGuid();
			var userId2 = Guid.NewGuid();
			using (var bitlyUow = _serviceProvider.GetRequiredService<IBitlyUow>())
			{
				//stage
				{
					var link = new Link("http://yandex.ru", "ya");
					var userLink = new UserLink(userId1, link);
					bitlyUow.Links.AddLink(userLink);
					await bitlyUow.SaveAsync();
				}

				{
					var link = await bitlyUow.Links.GetLinkAsync("http://yandex.ru");
					var userLink = new UserLink(userId2, link);
					bitlyUow.Links.AddLink(userLink);
					await bitlyUow.SaveAsync();
				}

				var links1 = await bitlyUow.Links.GetLinksAsync(userId1);
				Assert.Equal(1, links1.Count());

				var links2 = await bitlyUow.Links.GetLinksAsync(userId2);
				Assert.Equal(1, links2.Count());

				//возвращается одна и та же ссылка
				Assert.Equal(links1.First(), links2.First());
			}
		}
	}
}
