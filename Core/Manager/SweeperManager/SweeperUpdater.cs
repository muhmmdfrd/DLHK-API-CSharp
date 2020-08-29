using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.SweeperManager
{
	public class SweeperUpdater : AsistanceBase<SweeperAdapter, Sweeper>
	{
		public SweeperUpdater(SweeperAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Sweeper Update(SweeperDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.SweeperId == dto.SweeperId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Completeness = dto.Completeness;
				exist.Dicipline = dto.Dicipline;
				exist.PresenceId = dto.PresenceId;
				exist.Road = dto.Road;
				exist.RoadMedian = dto.RoadMedian;
				exist.Sidewalk = dto.Sidewalk;
				exist.WaterRope = dto.WaterRope;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
