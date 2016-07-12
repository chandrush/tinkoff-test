using Domain.Base;
using System;

namespace Domain.Models
{
	/// <summary>
	/// Связь пользователя и ссылки.
	/// </summary>
	/// <remarks>
	/// Получается, мы здесь имеем в качестве PK сурогатный Id + ограничение на уникальность пары (UserId, LinkId)?
	/// в отличии от более естественного варианта с PK (UserId, LinkId).
	/// </remarks>
	public class UserLink: IdentityPersistenceBase<UserLink, int>
    {
		private Guid _userId;

		private int _linkId;

		UserLink()
		{
			//ef
		}

		public UserLink(Guid userId, int linkId)
		{
			_userId = userId;
			_linkId = linkId;
		}

		/// <summary>
		/// Id пользователя.
		/// </summary>
		public Guid UserId => _userId;

		/// <summary>
		/// Id ссылки.
		/// </summary>
		public int LinkId => _linkId;
    }
}
