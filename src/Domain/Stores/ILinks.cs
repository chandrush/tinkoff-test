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
		/// <param name="link">Добавляемая ссылка.</param>
		void AddLink(Link link);

		/// <summary>
		/// Добавление ссылки.
		/// </summary>
		/// <param name="userLink">Добавляемая ссылка пользователя.</param>
		void AddUserLink(UserLink userLink);

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
		Task<IEnumerable<Link>> GetUserLinksAsync(Guid userId);

		/// <summary>
		/// Возвращает ссылку пользователя
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <param name="linkId">Идентификатор ссылки.</param>
		/// <returns></returns>
		Task<UserLink> GetUserLinkAsync(Guid userId, int linkId);

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
