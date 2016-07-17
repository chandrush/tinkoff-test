using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
	/// <summary>
	/// Сегрегация интерфейса DbContext: управляющие процедуры.
	/// </summary>
	public interface IBitlyDbContext: IDisposable, IBitlyDbContextSets
    {
		DatabaseFacade Database { get; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
		ChangeTracker ChangeTracker { get; }
	}
}
