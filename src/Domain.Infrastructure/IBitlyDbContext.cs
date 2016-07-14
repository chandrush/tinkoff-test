using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure
{
	/// <summary>
	/// 
	/// </summary>
    interface IBitlyDbContext
    {
		DbSet<Link> Links { get; set; }

		DbSet<UserLink> UserLinks { get; set; }
	}
}
