using System;
using System.ComponentModel;

namespace Domain.Base
{
	public abstract class IdentityPersistenceBase<TObject, TIdentity>
		where TObject : IdentityPersistenceBase<TObject, TIdentity>
	{
		private static TypeConverter DoubleTypeConverter = TypeDescriptor.GetConverter(typeof(double));

		protected TIdentity _id;

		public TIdentity Id
		{
			get { return _id; }
			private set
			{
				if (IsTransient)
					_id = value;
			}
		}

		public virtual bool IsTransient
		{
			get
			{
				if (_id.GetType() == typeof(Guid))
					return (Guid)(object)_id == Guid.Empty;

				double testDouble;
				if (double.TryParse(_id.ToString(), out testDouble))
					return (double)DoubleTypeConverter.ConvertFrom(_id.ToString()) == 0d;

				if (_id.GetType() == (typeof(string)))
					return _id == null || _id.ToString() == string.Empty;

				throw new ArgumentException("IdentityPersistenceBase<TObject, TIdentity> Class only provides native support for Guid, Numeric (Int, Int32, Int64, Double, etc.) or String as the TIdentity type.  For other types (including any custom types), you *must* override the IsTransient virtual property to provide your own implementation!", "TIdentity");
			}
		}

		public static bool operator ==(IdentityPersistenceBase<TObject, TIdentity> left, IdentityPersistenceBase<TObject, TIdentity> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(IdentityPersistenceBase<TObject, TIdentity> left, IdentityPersistenceBase<TObject, TIdentity> right)
		{
			return !(left == right);
		}

		public override bool Equals(object other)
		{
			if ((other != null) && (other is TObject) && (ReferenceEquals(this, other)))
				return true;

			if ((other == null) || (!(other is TObject)) || (IsTransient))
				return false;

			return (GetHashCode() == other.GetHashCode());
		}

		public override int GetHashCode()
		{
			return string.Format("{0}|{1}", GetType().ToString(), _id).GetHashCode();
		}
	}
}
