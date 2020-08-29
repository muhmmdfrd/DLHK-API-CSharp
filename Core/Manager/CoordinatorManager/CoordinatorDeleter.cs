using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.CoordinatorManager
{
	public class CoordinatorDeleter : AsistanceBase<CoordinatorAdapter, Coordinator>
	{
		public CoordinatorDeleter(CoordinatorAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.CoordinatorId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Coordinators.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
