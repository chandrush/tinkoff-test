using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
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
		/// Получение списка ссылок от конкретного пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <returns>Список ссылок пользователя.</returns>
		Task<IList<Link>> GetLinksAsync(Guid userId);

		/// <summary>
		/// Возвращает ссылку по короткому коду.
		/// </summary>
		/// <param name="shortLinkCode">Код короткой ссылки</param>
		/// <returns>Найденная ссылка или null.</returns>
		Task<Link> GetLink(string shortLinkCode);
    }
}
