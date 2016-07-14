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

		UserLink()
		{
			//ef
		}

		private UserLink(Guid userId)
		{
			if (userId == Guid.Empty)
				throw new ArgumentException(@"userId", "Не корректное значение идентификатора пользователя, ожидается не пустое значение.");
			_userId = userId;
		}

		public UserLink(Guid userId, int linkId)
			:this(userId)
		{
			LinkId = linkId;
		}

		public UserLink(Guid userId, Link link)
			: this(userId)
		{
			LinkId = link.Id;
			Link = link;
		}

		/// <summary>
		/// Id пользователя.
		/// </summary>
		public Guid UserId => _userId;

		/// <summary>
		/// Id ссылки.
		/// </summary>
		public int LinkId { get; private set; }

		/// <summary>
		/// Навигация на ссылку.
		/// </summary>
		public Link Link { get; private set; }
	}
}
