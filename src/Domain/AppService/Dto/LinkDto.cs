using System;

namespace Domain.AppService.Dto
{
    public class LinkDto
    {
		public LinkDto(string originalLink, string shortenLink, int usesNumber, DateTime creationDateUTC)
		{
			OriginalLink = originalLink;
			ShortenLink = shortenLink;
			CreationDateUTC = creationDateUTC;
			UsesNumber = usesNumber;
		}

		public DateTime CreationDateUTC { get; private set; }

		/// <summary>
		/// Оригинальная ссылка.
		/// </summary>
		public string OriginalLink { get; private set; }

		/// <summary>
		/// Код сокращенной ссылки.
		/// </summary>
		public string ShortenLink { get; private set; }

		/// <summary>
		/// Количество использований ссылки.
		/// </summary>
		public int UsesNumber { get; private set; }
	}
}
