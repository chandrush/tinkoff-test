using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Utils
{
	public static class DbExtensions
	{
		public static void EnsureMigrationsApplied(this IServiceProvider serviceProvider)
		{
			try
			{
				using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
				{
					serviceScope.ServiceProvider.GetService<BitlyDbContext>()
						 .Database
						 .Migrate();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw;
			}
		}
	}
}
