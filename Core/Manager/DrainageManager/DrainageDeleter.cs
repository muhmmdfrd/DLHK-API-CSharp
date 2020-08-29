using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.DrainageManager
{
	public class DrainageDeleter : AsistanceBase<DrainageAdapter, Drainage>
	{
		public DrainageDeleter(DrainageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.DrainageId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Drainages.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
