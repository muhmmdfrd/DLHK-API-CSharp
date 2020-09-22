using Core.Manager.PresenceManager;
using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.DrainageManager
{
	public class DrainageCreator : AsistanceBase<DrainageAdapter, Drainage>
	{
		public DrainageCreator(DrainageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Save(DrainageDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var existPresence = Manager.Database.Presences.FirstOrDefault(x => x.PresenceId == dto.PresenceId);
				var existDrainage = Manager.Query.Value.Get().FirstOrDefault(x => x.PresenceId == dto.PresenceId);
				var counter = existPresence.Counter + 1;

				if (counter == 1 && existDrainage == null)
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

					new PresenceAdapter().Updater.Value.UpdateLocationPresence(dto.Location, dto.PresenceId);

					Manager.Database.Drainages.Add(newEntity);
				}
				else if (counter <= 2 && existDrainage != null)
				{
					existDrainage.Completeness += dto.Completeness;
					existDrainage.Completeness /= 2;	
					
					existDrainage.Dicipline += dto.Dicipline;
					existDrainage.Dicipline /= 2;
					
					existDrainage.Sediment += dto.Sediment;
					existDrainage.Sediment /= 2;
					
					existDrainage.Weed += dto.Weed;
					existDrainage.Weed /= 2;
					
					existDrainage.Cleanliness += dto.Cleanliness;
					existDrainage.Cleanliness /= 2;
				}
				else if (counter == 3 && existDrainage != null)
				{
					existDrainage.Completeness += dto.Completeness;
					existDrainage.Completeness /= 3;

					existDrainage.Dicipline += dto.Dicipline;
					existDrainage.Dicipline /= 3;

					existDrainage.Sediment += dto.Sediment;
					existDrainage.Sediment /= 3;

					existDrainage.Weed += dto.Weed;
					existDrainage.Weed /= 3;

					existDrainage.Cleanliness += dto.Cleanliness;
					existDrainage.Cleanliness /= 3;
				}
				else
				{
					throw new Exception("You have entered the value more than 3 times");
				}

				existPresence.Counter++;

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
