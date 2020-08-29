using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.PresenceManager
{
	public class PresenceDeleter : AsistanceBase<PresenceAdapter, Presence>
	{
		public PresenceDeleter(PresenceAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PresenceId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Presences.Remove(exist);
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
					Manager.Database.Presences.Remove(row);
				}

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}

		public void DeleteAllScore()
		{
			using (var transac = new TransactionScope())
			{
				foreach (var row in Manager.Database.Sweepers)
				{
					Manager.Database.Sweepers.Remove(row);
				}

				foreach (var row in Manager.Database.Drainages)
				{
					Manager.Database.Drainages.Remove(row);
				}

				foreach (var row in Manager.Database.Garbages)
				{
					Manager.Database.Garbages.Remove(row);
				}

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
