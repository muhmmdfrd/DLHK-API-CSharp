using Repository;
using System.Transactions;

namespace Core.Manager.SweeperManager
{
	public class SweeperCreator : AsistanceBase<SweeperAdapter, Sweeper>
	{
		public SweeperCreator(SweeperAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Sweeper Save(SweeperDTO dto)
		{
			using (var transac = new TransactionScope())
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

				Manager.Database.Sweepers.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
