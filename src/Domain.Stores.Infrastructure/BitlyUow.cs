using Domain.Base;
using Domain.Stores;
using Infrastructure.Stores;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BitlyUow: IBitlyUow
    {
		private readonly IBitlyDbContext _bitlyDbContext;

		private Lazy<ILinks> _links;

        public BitlyUow(IBitlyDbContext bitlyDbContext)
        {
			_bitlyDbContext = bitlyDbContext;
			_links = new Lazy<ILinks>(() => new Links(_bitlyDbContext), LazyThreadSafetyMode.ExecutionAndPublication);
		}

		public ILinks Links
		{
			get
			{
				return _links.Value;
			}
		}

		public void Dispose()
		{
			_bitlyDbContext.Dispose();
		}

		public async Task<ITransaction> BeginTransactionAsync()
		{
			return new DbContextTransactionWrapper(await _bitlyDbContext.Database.BeginTransactionAsync().ConfigureAwait(false));
		}

		public async Task SaveAsync()
		{
			EnsureChangesDetected();
			await _bitlyDbContext.SaveChangesAsync().ConfigureAwait(false);
		}

		private void EnsureChangesDetected()
		{
			if (!_bitlyDbContext.ChangeTracker.AutoDetectChangesEnabled)
				_bitlyDbContext.ChangeTracker.DetectChanges();
		}
	}
}
