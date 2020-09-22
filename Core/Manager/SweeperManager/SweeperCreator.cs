using Core.Manager.PresenceManager;
using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.SweeperManager
{
	public class SweeperCreator : AsistanceBase<SweeperAdapter, Sweeper>
	{
		public SweeperCreator(SweeperAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Save(SweeperDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var existPresence = Manager.Database.Presences.FirstOrDefault(x => x.PresenceId == dto.PresenceId);
				var existSweeper = Manager.Query.Value.Get().FirstOrDefault(x => x.PresenceId == dto.PresenceId);
				var counter = existPresence.Counter + 1;

				if (counter == 1 && existSweeper == null)
				{
					var newEntity = new Sweeper
					{
						Completeness = dto.Completeness,
						Dicipline = dto.Dicipline,
						PresenceId = dto.PresenceId,
						Road = dto.Road,
						RoadMedian = dto.RoadMedian,
						Sidewalk = dto.Sidewalk,
						WaterRope = dto.WaterRope
					};

					new PresenceAdapter().Updater.Value.UpdateLocationPresence(dto.Location, dto.PresenceId);
					
					Manager.Database.Sweepers.Add(newEntity);
				}
				else if (counter <= 2 && existSweeper != null)
				{
					existSweeper.Completeness += dto.Completeness;
					existSweeper.Completeness /= 2;

					existSweeper.Dicipline += dto.Dicipline;
					existSweeper.Dicipline /= 2;

					existSweeper.Road += dto.Road;
					existSweeper.Road /= 2;

					existSweeper.RoadMedian += dto.RoadMedian;
					existSweeper.RoadMedian /= 2;

					existSweeper.Sidewalk += dto.Sidewalk;
					existSweeper.Sidewalk /= 2;

					existSweeper.WaterRope += dto.WaterRope;
					existSweeper.WaterRope /= 2;
				}
				else if (counter == 3 && existSweeper != null)
				{
					existSweeper.Completeness += dto.Completeness;
					existSweeper.Completeness /= 3;

					existSweeper.Dicipline += dto.Dicipline;
					existSweeper.Dicipline /= 3;

					existSweeper.Road += dto.Road;
					existSweeper.Road /= 3;

					existSweeper.RoadMedian += dto.RoadMedian;
					existSweeper.RoadMedian /= 3;

					existSweeper.Sidewalk += dto.Sidewalk;
					existSweeper.Sidewalk /= 3;

					existSweeper.WaterRope += dto.WaterRope;
					existSweeper.WaterRope /= 3;
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
