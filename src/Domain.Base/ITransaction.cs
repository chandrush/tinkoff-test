using System;

namespace Domain.Base
{
    public interface ITransaction: IDisposable
	{
		void Commit();

		void Rollback();
	}
}
