using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.HeadZoneManager
{
	public class HeadZoneDeleter : AsistanceBase<HeadZoneAdapter, HeadOfZone>
	{
		public HeadZoneDeleter(HeadZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.HeadOfZoneId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.HeadOfZones.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}

		}
	}
}
