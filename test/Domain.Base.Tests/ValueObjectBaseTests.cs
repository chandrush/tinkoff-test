using System;
using Xunit;

namespace Domain.Base.Tests
{
	// http://grabbagoft.blogspot.ru/2007/06/generic-value-object-equality.html
	public class ValueObjectBaseTests
	{
		private class Address : ValueObjectBase<Address>
		{
			private readonly string _address1;
			private readonly string _city;
			private readonly string _state;

			public Address(string address1, string city, string state)
			{
				_address1 = address1;
				_city = city;
				_state = state;
			}

			public string Address1
			{
				get { return _address1; }
			}

			public string City
			{
				get { return _city; }
			}

			public string State
			{
				get { return _state; }
			}
		}

		private class ExpandedAddress : Address
		{
			private readonly string _address2;

			public ExpandedAddress(string address1, string address2, string city, string state)
				: base(address1, city, state)
			{
				_address2 = address2;
			}

			public string Address2
			{
				get { return _address2; }
			}

		}

		private class DateFieldObject : ValueObjectBase<DateFieldObject>
		{
			private readonly string _name;

			private readonly DateTime? _appearanceDate;

			public string Name => _name;

			public DateTime? AppearanceDate => _appearanceDate;

			public DateFieldObject(string name, DateTime? appearanceDate)
			{
				_name = name;
				_appearanceDate = appearanceDate;
			}
		}

		[Fact]
		public void DateFieldObjectWithIdenticalAddresses()
		{
			var someDate = DateTime.UtcNow;
			var obj1 = new DateFieldObject("a", someDate);
			var obj2 = new DateFieldObject("a", someDate);

			Assert.True(obj1.Equals(obj2));
		}

		[Fact]
		public void DateFieldObjectWithNotIdenticalAddresses()
		{
			var someDate1 = DateTime.UtcNow;
			var someDate2 = someDate1.AddSeconds(1);
			var obj1 = new DateFieldObject("a", someDate1);
			var obj2 = new DateFieldObject("a", someDate2);

			Assert.False(obj1 == obj2);
		}

		[Fact]
		public void DateFieldObjectWithNotIdenticalAddressesAndNullDates()
		{
			var someDate = DateTime.UtcNow;
			var obj1 = new DateFieldObject("a", null);
			var obj2 = new DateFieldObject("a", someDate);

			Assert.True(obj1 != obj2);
		}

		[Fact]
		public void AddressEqualsWorksWithIdenticalAddresses()
		{
			Address address = new Address("Address1", "Austin", "TX");
			Address address2 = new Address("Address1", "Austin", "TX");

			Assert.True(address.Equals(address2));
		}

		[Fact]
		public void AddressEqualsWorksWithNonIdenticalAddresses()
		{
			Address address = new Address("Address1", "Austin", "TX");
			Address address2 = new Address("Address2", "Austin", "TX");

			Assert.False(address.Equals(address2));
		}

		[Fact]
		public void AddressEqualsWorksWithNulls()
		{
			Address address = new Address(null, "Austin", "TX");
			Address address2 = new Address("Address2", "Austin", "TX");

			Assert.False(address.Equals(address2));
		}

		[Fact]
		public void AddressEqualsWorksWithNullsOnOtherObject()
		{
			Address address = new Address("Address2", "Austin", "TX");
			Address address2 = new Address("Address2", null, "TX");

			Assert.False(address.Equals(address2));
		}

		[Fact]
		public void AddressEqualsIsReflexive()
		{
			Address address = new Address("Address1", "Austin", "TX");

			Assert.True(address.Equals(address));
		}

		[Fact]
		public void AddressEqualsIsSymmetric()
		{
			Address address = new Address("Address1", "Austin", "TX");
			Address address2 = new Address("Address2", "Austin", "TX");

			Assert.False(address.Equals(address2));
			Assert.False(address2.Equals(address));
		}

		[Fact]
		public void AddressEqualsIsTransitive()
		{
			Address address = new Address("Address1", "Austin", "TX");
			Address address2 = new Address("Address1", "Austin", "TX");
			Address address3 = new Address("Address1", "Austin", "TX");

			Assert.True(address.Equals(address2));
			Assert.True(address2.Equals(address3));
			Assert.True(address.Equals(address3));
		}

		[Fact]
		public void AddressOperatorsWork()
		{
			Address address = new Address("Address1", "Austin", "TX");
			Address address2 = new Address("Address1", "Austin", "TX");
			Address address3 = new Address("Address2", "Austin", "TX");

			Assert.True(address == address2);
			Assert.True(address2 != address3);
		}

		[Fact]
		public void DerivedTypesBehaveCorrectly()
		{
			Address address = new Address("Address1", "Austin", "TX");
			ExpandedAddress address2 = new ExpandedAddress("Address1", "Apt 123", "Austin", "TX");

			Assert.False(address.Equals(address2));
			Assert.False(address == address2);
		}

		[Fact]
		public void EqualValueObjectsHaveSameHashCode()
		{
			Address address = new Address("Address1", "Austin", "TX");
			Address address2 = new Address("Address1", "Austin", "TX");

			Assert.Equal(address.GetHashCode(), address2.GetHashCode());
		}

		[Fact]
		public void TransposedValuesGiveDifferentHashCodes()
		{
			Address address = new Address(null, "Austin", "TX");
			Address address2 = new Address("TX", "Austin", null);

			Assert.NotEqual(address.GetHashCode(), address2.GetHashCode());
		}

		[Fact]
		public void UnequalValueObjectsHaveDifferentHashCodes()
		{
			Address address = new Address("Address1", "Austin", "TX");
			Address address2 = new Address("Address2", "Austin", "TX");

			Assert.NotEqual(address.GetHashCode(), address2.GetHashCode());
		}

		[Fact]
		public void TransposedValuesOfFieldNamesGivesDifferentHashCodes()
		{
			Address address = new Address("_city", null, null);
			Address address2 = new Address(null, "_address1", null);

			Assert.NotEqual(address.GetHashCode(), address2.GetHashCode());
		}

		[Fact]
		public void DerivedTypesHashCodesBehaveCorrectly()
		{
			ExpandedAddress address = new ExpandedAddress("Address99999", "Apt 123", "New Orleans", "LA");
			ExpandedAddress address2 = new ExpandedAddress("Address1", "Apt 123", "Austin", "TX");

			Assert.NotEqual(address.GetHashCode(), address2.GetHashCode());
		}
	}
}
