using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.SweeperManager
{
	public class SweeperQuery : AsistanceBase<SweeperAdapter, Sweeper>
	{
		public SweeperQuery(SweeperAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Sweeper> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.Sweepers;

			return  withDetail ? dataContext.AsQueryable().Include(x => x.Presence) : dataContext;
		}

		public List<SweeperDTO> Transform()
		{
			return
				(from val in Get(true).AsEnumerable()
				 select new SweeperDTO()
				 {
					 Completeness = val.Completeness,
					 Dicipline = val.Dicipline,
					 PresenceId = val.PresenceId,
					 Road = val.Road,
					 RoadMedian = val.RoadMedian,
					 Sidewalk = val.Sidewalk,
					 SweeperId = val.SweeperId,
					 WaterRope = val.WaterRope
				}).ToList();
		}

		public SweeperDTO TransformId(long id)
		{
			return (from val in Get(true)
					where val.SweeperId == id
					select new SweeperDTO()
					{
						Completeness = val.Completeness,
						Dicipline = val.Dicipline,
						PresenceId = val.PresenceId,
						Road = val.Road,
						RoadMedian = val.RoadMedian,
						Sidewalk = val.Sidewalk,
						SweeperId = val.SweeperId,
						WaterRope = val.WaterRope
					}).FirstOrDefault();
		}

		private int CalculateScore(Sweeper val)
		{
			var result = val.Completeness + val.Dicipline + val.Road + val.RoadMedian + val.Sidewalk + val.WaterRope;

			return (int)(result / 6);
		}
	}
}
