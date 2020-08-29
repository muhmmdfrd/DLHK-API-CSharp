using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.RegionManager
{
	public class RegionDeleter : AsistanceBase<RegionAdapter, Region>
	{
		public RegionDeleter(RegionAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.RegionId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Regions.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
