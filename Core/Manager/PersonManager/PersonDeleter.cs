using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.PersonManager
{
	public class PersonDeleter : AsistanceBase<PersonAdapter, Person>
	{
		public PersonDeleter(PersonAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.People.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}

		public void DeletePersonEmployee(long personId)
		{
			using (var transac = new TransactionScope())
			{
				var dataPeson = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == personId);
				var dbEmployee = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == personId).Employees;
				var dataEmployee = dbEmployee.FirstOrDefault(x => x.PersonId == personId);

				if (dbEmployee == null || dataPeson == null || dataEmployee == null)
					throw new Exception("data not found");

				Manager.Database.Employees.Remove(dataEmployee);
				Manager.Database.People.Remove(dataPeson);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
