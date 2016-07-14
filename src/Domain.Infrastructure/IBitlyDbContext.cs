using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure
{
    interface IBitlyDbContext
    {
		DbSet<Link> Links { get; set; }

		DbSet<UserLink> UserLinks { get; set; }
	}
}
