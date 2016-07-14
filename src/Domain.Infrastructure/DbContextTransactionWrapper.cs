using Domain.Base;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Infrastructure
{
	public class DbContextTransactionWrapper : ITransaction
	{
		private IDbContextTransaction _dbContextTransaction;

		public DbContextTransactionWrapper(IDbContextTransaction dbContextTransaction)
		{
			_dbContextTransaction = dbContextTransaction;
		}

		public void Commit()
		{
			_dbContextTransaction.Commit();
		}

		public void Dispose()
		{
			_dbContextTransaction.Dispose();
		}

		public void Rollback()
		{
			_dbContextTransaction.Rollback();
		}
	}
}
