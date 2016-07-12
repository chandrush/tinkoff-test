using System;
using System.Collections.Generic;
using System.Reflection;

namespace Domain.Base
{
	/// <summary>
	/// The base for value objects.
	/// </summary>
	public abstract class ValueObjectBase<T> : IEquatable<T> where T : ValueObjectBase<T>
	{
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			return Equals(obj as T);
		}

		public override int GetHashCode()
		{
			IEnumerable<FieldInfo> fields = GetFields();

			var startValue = 17;
			var multiplier = 59;

			var hashCode = startValue;

			foreach (var field in fields)
			{
				var value = field.GetValue(this);

				if (value != null)
					hashCode = hashCode * multiplier + value.GetHashCode();
			}

			return hashCode;
		}

		public bool Equals(T other)
		{
			if (other == null)
				return false;

			var t = GetType();
			var otherType = other.GetType();

			if (t != otherType)
				return false;

			var fields = t.GetTypeInfo().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			foreach (var field in fields)
			{
				var value1 = field.GetValue(other);
				var value2 = field.GetValue(this);

				if (value1 == null)
				{
					if (value2 != null)
						return false;
				}
				////http://www.infoq.com/presentations/ddd-net-2 0:35:50
				////else if (typeof(DateTime).IsAssignableFrom(field.FieldType) || typeof(DateTime?).IsAssignableFrom(field.FieldType))
				////{
				////	if (!((DateTime)value1).ToLongDateString().Equals(((DateTime)value2).ToLongDateString()))
				////		return false;
				////}
				else if (!value1.Equals(value2))
					return false;
			}
			return true;
		}

		public static bool operator ==(ValueObjectBase<T> x, ValueObjectBase<T> y)
		{
			if (ReferenceEquals(x, null))
				return ReferenceEquals(y, null);
			return x.Equals(y);
		}

		public static bool operator !=(ValueObjectBase<T> x, ValueObjectBase<T> y)
		{
			return !(x == y);
		}

		private IEnumerable<FieldInfo> GetFields()
		{
			Type t = GetType();

			List<FieldInfo> fields = new List<FieldInfo>();

			while (t != typeof(object))
			{
				fields.AddRange(t.GetTypeInfo().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
				t = t.GetTypeInfo().BaseType;
			}

			return fields;
		}
	}
}
