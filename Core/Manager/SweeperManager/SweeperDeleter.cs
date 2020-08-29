using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.SweeperManager
{
	public class SweeperDeleter : AsistanceBase<SweeperAdapter, Sweeper>
	{
		public SweeperDeleter(SweeperAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.SweeperId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Sweepers.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
