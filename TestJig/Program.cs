using System;
using TSD.Reference.Core.Entities;
using TSD.Reference.Core.Services;
using TSD.Reference.Data.PostgreSQL.Repositories;

namespace TestJig
{
	class Program
	{
		private static void Main(string[] args)
		{
			CustomerRepo();

			Console.ReadKey();
		}

		static void AutomobileService()
		{
			var aAutoRepo = new AutomobileRepository();
			var _service = new AutomobileService(aAutoRepo);
		}

		static void CustomerRepo()
		{
			var _repository = new CustomerRepository();

			var aCustomer = new Customer { Name = "ABC Rentals" };

			var aRetrievedCustomer = _repository.GetCustomerByNameAsync(aCustomer.Name).Result;
			if (aRetrievedCustomer == null)
			{
				var aKey = _repository.AddCustomer(aCustomer);

				var aSavedCustomer = _repository.GetCustomer(aKey);

				Console.WriteLine(aSavedCustomer.Id == aKey
					? "Customer was saved and retrieved successfully!"
					: $"Saved customer key was {aSavedCustomer.Id} and new customer key was {aKey}");
			}
			else
			{
				var aSavedCustomer = _repository.GetCustomer(aRetrievedCustomer.Id);

				Console.WriteLine(aSavedCustomer.Id == aRetrievedCustomer.Id
					? "Customer was already in the database and retrieved successfully!"
					: $"Saved customer key was {aSavedCustomer.Id} and new customer key was {aRetrievedCustomer.Id}");

			}
		}
	}
}
