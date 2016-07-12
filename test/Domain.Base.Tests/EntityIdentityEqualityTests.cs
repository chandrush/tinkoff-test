using Xunit;

namespace Domain.Base.Tests
{
	public class EntityIdentityEqualityTests
	{
		private class Customer : IdentityPersistenceBase<Customer, int>
		{
			private string _name;

			public int Id
			{
				get { return _id; }
			}

			public string Name
			{
				get { return _name; }
			}

			public Customer(int id, string name)
			{
				_id = id;
				_name = name;
			}
		}

		[Fact]
		public void Entities_Are_Equal()
		{
			Customer customer = new Customer(1, "a");
			Customer otherCustomer = new Customer(1, "a");

			Assert.Equal(customer, otherCustomer);
		}

		[Fact]
		public void Entities_Are_Not_Equal()
		{
			Customer customer = new Customer(1, "a");
			Customer otherCustomer = new Customer(2, "a");

			Assert.NotEqual(customer, otherCustomer);
		}

		[Fact]
		public void Identity_The_Same_But_Properties_Differ()
		{
			Customer customer = new Customer(1, "a");
			Customer otherCustomer = new Customer(1, "b");

			Assert.Equal(customer, otherCustomer);
		}
	}
}
