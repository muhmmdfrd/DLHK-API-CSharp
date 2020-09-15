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
				var existSweeper = Manager.Query.Value.Get().FirstOrDefault(x => x.PresenceId == dto.PresenceId);
				var counter = existPresence.Counter + 1;

				if (counter == 1 && existSweeper == null)
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
				else if (counter <= 2 && existSweeper != null)
				{
					existSweeper.Completeness += dto.Completeness;
					existSweeper.Dicipline += dto.Dicipline;
					existSweeper.Sediment += dto.Sediment;
					existSweeper.Weed += dto.Weed;
					existSweeper.Cleanliness += dto.Cleanliness;
				}
				else if (counter == 3 && existSweeper != null)
				{
					existSweeper.Completeness += dto.Completeness;
					existSweeper.Completeness /= 3;

					existSweeper.Dicipline += dto.Dicipline;
					existSweeper.Dicipline /= 3;

					existSweeper.Sediment += dto.Sediment;
					existSweeper.Sediment /= 3;

					existSweeper.Weed += dto.Weed;
					existSweeper.Weed /= 3;

					existSweeper.Cleanliness += dto.Cleanliness;
					existSweeper.Cleanliness /= 3;
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
