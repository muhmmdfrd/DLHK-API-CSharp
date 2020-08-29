using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.EmployeeManager
{
	public class EmployeeDeleter : AsistanceBase<EmployeeAdapter, Employee>
	{
		public EmployeeDeleter(EmployeeAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.EmployeeId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Employees.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
