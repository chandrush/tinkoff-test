using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
	//этот интерфейс используется при реализации хранилищ в которых нельзя, например, вызывать метод SaveChanges.

	/// <summary>
	/// Сегрегация интерфейса DbContext: сеты сущностей.
	/// </summary>
    public interface IBitlyDbContextSets
    {
		DbSet<Link> Links { get; set; }

		DbSet<UserLink> UserLinks { get; set; }
	}
}
