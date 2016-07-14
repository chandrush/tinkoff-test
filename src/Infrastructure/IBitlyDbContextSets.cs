using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
	//этот интерфейс используется при реализации хранилищ в которых нельзя, например, вызывать метод SaveChanges.

	//для реализации Приципа единственной ответственности можно было бы выделять сборки под Infrastructure.Api, 
	//Domain.Stores.Infrastructure, Infrastructure с соответствющим разделением логики и большей защитой от случайного схода с тропы.

	/// <summary>
	/// Сегрегация интерфейса DbContext: сеты сущностей.
	/// </summary>
    interface IBitlyDbContextSets
    {
		DbSet<Link> Links { get; set; }

		DbSet<UserLink> UserLinks { get; set; }
	}
}
