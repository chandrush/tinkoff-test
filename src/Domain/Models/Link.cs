using Domain.Base;
using System;
using System.Threading;

namespace Domain.Models
{
	/// <summary>
	/// Ссылка.
	/// </summary>
	public class Link: IdentityPersistenceBase<Link, int>
	{
		private DateTime _creationDateUTC;

		private string _originalLink;

		private string _shortenLinkCode;

		private int _usesNumber;

		Link()
		{
			//ef
		}

		public Link(string originalLink, string shortenLinkCode, DateTime creationDateUtc)
		{
			if (string.IsNullOrWhiteSpace(originalLink))
				throw new ArgumentNullException(@"originalLink");
			if (string.IsNullOrWhiteSpace(shortenLinkCode))
				throw new ArgumentNullException(@"shortenLinkCode");

			if (originalLink.Length > 2000)
				throw new ArgumentOutOfRangeException(@"originalLink", "Наружено ограничение на длину ссылки в 2000 символов.");
			if (shortenLinkCode.Length > 2000)
				throw new ArgumentOutOfRangeException(@"shortenLinkCode", "Наружено ограничение на длину ссылки в 2000 символов.");

			_creationDateUTC = creationDateUtc;
			_originalLink = originalLink;
			_shortenLinkCode = shortenLinkCode;
		}

		public Link(string originalLink, string shortenLinkCode)
			: this(originalLink, shortenLinkCode, DateTime.UtcNow)
		{
			
		}

		/// <summary>
		/// Дата и время создания ссылки в формате UTC.
		/// </summary>
		public DateTime CreationDateUTC => _creationDateUTC;

		/// <summary>
		/// Оригинальная ссылка.
		/// </summary>
		public string OriginalLink => _originalLink;

		/// <summary>
		/// Код сокращенной ссылки.
		/// </summary>
		public string ShortenLinkCode => _shortenLinkCode;

		/// <summary>
		/// Количество использований ссылки.
		/// </summary>
		public int UsesNumber => _usesNumber;

		/// <summary>
		/// Увеличивает количество использований ссылки на 1.
		/// </summary>
		public void IncrementLinkUses()
		{
			Interlocked.Increment(ref _usesNumber);
		}
	}
}
