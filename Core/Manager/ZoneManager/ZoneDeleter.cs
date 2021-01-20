using Repository;
using System;
using System.Transactions;

namespace Core.Manager.ZoneManager
{
	public class ZoneDeleter : AsistanceBase<ZoneAdapter, Zone>
	{
		public ZoneDeleter(ZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Database.Zones.Find(id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Zones.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
