using Domain.Base;
using System;

namespace Domain
{
	/// <summary>
	/// Ссылка.
	/// </summary>
	public class Link: IdentityPersistenceBase<Link, int>
	{
		/// <summary>
		/// Дата и время создания ссылки в формате UTC.
		/// </summary>
		public DateTime CreationDateUTC { get; set; }

		/// <summary>
		/// Оригинальная ссылка.
		/// </summary>
		public string OriginalLink { get; set; }

		/// <summary>
		/// Код сокращенной ссылки.
		/// </summary>
		public string ShortenLink { get; set; }

		/// <summary>
		/// Количество использований ссылки.
		/// </summary>
		public int UsesNumber { get; set; }
	}
}
