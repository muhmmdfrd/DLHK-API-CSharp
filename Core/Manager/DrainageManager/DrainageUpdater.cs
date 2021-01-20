using Repository;
using System;
using System.Transactions;

namespace Core.Manager.DrainageManager
{
	public class DrainageUpdater : AsistanceBase<DrainageAdapter, Drainage>
	{
		public DrainageUpdater(DrainageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Drainage Update(DrainageDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Database.Drainages.Find(dto.DrainageId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Cleanliness = dto.Cleanliness;
				exist.Completeness = dto.Completeness;
				exist.Dicipline = dto.Dicipline;
				exist.PresenceId = dto.PresenceId;
				exist.Sediment = dto.Sediment;
				exist.Weed = dto.Weed;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
