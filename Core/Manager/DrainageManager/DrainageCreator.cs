using Repository;
using System.Transactions;

namespace Core.Manager.DrainageManager
{
	public class DrainageCreator : AsistanceBase<DrainageAdapter, Drainage>
	{
		public DrainageCreator(DrainageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Drainage Save(DrainageDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Drainage
				{
					Cleanliness = dto.Cleanliness,
					Completeness = dto.Completeness,
					Dicipline = dto.Dicipline,
					PresenceId = dto.PresenceId,
					Sediment = dto.Sediment,
					Weed = dto.Weed
				};

				Manager.Database.Drainages.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
