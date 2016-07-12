using System;
using System.Threading.Tasks;

namespace Domain.Base
{
	/// <summary>
	/// Базовый интерфейс для unit-of-work.
	/// </summary>
	public interface IUow : IDisposable
	{
		Task SaveAsync();

		Task Rollback();
	}
}
