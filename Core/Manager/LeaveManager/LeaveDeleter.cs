using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.LeaveManager
{
	public class LeaveDeleter : AsistanceBase<LeaveAdapter, Leave>
	{
		public LeaveDeleter(LeaveAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.LeaveId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Leaves.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}

		}

		public void DeleteAll()
		{
			using (var transac = new TransactionScope())
			{
				foreach (var row in Manager.Query.Value.Get())
				{
					Manager.Database.Leaves.Remove(row);
				}

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
