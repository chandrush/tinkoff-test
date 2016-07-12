using Domain.Base;

namespace Domain.Stores
{
	/// <summary>
	/// UOW.
	/// </summary>
    public interface IBitlyUow: IUow
    {
		/// <summary>
		/// Ссылки.
		/// </summary>
		ILinks Links { get; }
    }
}
