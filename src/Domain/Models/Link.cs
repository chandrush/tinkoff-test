using Domain.Base;
using System;

namespace Domain
{
	/// <summary>
	/// Ссылка.
	/// </summary>
	public class Link: IdentityPersistenceBase<Link, int>
	{
		private DateTime _creationDateUtc;

		private string _originalLink;

		private string _shortenLinkCode;

		private int _usesNumber;

		Link()
		{
			//ef
		}

		public Link(DateTime creationDateUtc, string originalLink, string shortenLinkCode)
		{
			_creationDateUtc = creationDateUtc;
			_originalLink = originalLink;
			_shortenLinkCode = shortenLinkCode;
		}

		public Link(string originalLink, string shortenLinkCode)
		{
			_creationDateUtc = DateTime.UtcNow;
			_originalLink = originalLink;
			_shortenLinkCode = shortenLinkCode;
		}

		/// <summary>
		/// Дата и время создания ссылки в формате UTC.
		/// </summary>
		public DateTime CreationDateUTC => _creationDateUtc;

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
		/// <remarks>
		/// Не потокобезопасно.
		/// </remarks>
		public void IncrementLinkUses()
		{
			_usesNumber++;
		}
	}
}
