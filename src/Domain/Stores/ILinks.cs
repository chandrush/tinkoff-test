using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Stores
{
	/// <summary>
	/// Ссылки.
	/// </summary>
    public interface ILinks
    {
		/// <summary>
		/// Добавление ссылки.
		/// </summary>
		/// <param name="userLink">Добавляемая ссылка пользователя.</param>
		void AddLink(UserLink userLink);

		/// <summary>
		/// Обновление ссылки.
		/// </summary>
		/// <param name="link">Ссылка.</param>
		void UpdateLink(Link link);

		/// <summary>
		/// Получение списка ссылок от конкретного пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <returns>Список ссылок пользователя.</returns>
		Task<IEnumerable<Link>> GetLinksAsync(Guid userId);

		/// <summary>
		/// Возвращает ссылку по короткому коду.
		/// </summary>
		/// <param name="shortLinkCode">Код короткой ссылки</param>
		/// <returns>Найденная ссылка или null.</returns>
		Task<Link> GetLinkByShortCodeAsync(string shortLinkCode);

		/// <summary>
		/// Возвращает ссылку по оригинальному url.
		/// </summary>
		/// <param name="linkUrl">Url ссылки.</param>
		/// <returns>Найденная ссылка или null.</returns>
		Task<Link> GetLinkAsync(string linkUrl);
	}
}
