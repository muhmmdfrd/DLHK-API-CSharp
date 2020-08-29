using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.DrainageManager
{
	public class DrainageQuery : AsistanceBase<DrainageAdapter, Drainage>
	{
		public DrainageQuery(DrainageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Drainage> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.Drainages;
			var fullData = withDetail ?
				dataContext.AsQueryable().Include(x => x.Presence) : dataContext;

			return fullData;
		}

		public List<DrainageDTO> Transform()
		{
			return (from val in Get(true)
					select new DrainageDTO() {
						Cleanliness = val.Cleanliness,
						Completeness = val.Completeness,
						Dicipline = val.Dicipline,
						DrainageId = val.DrainageId,
						PresenceId = val.PresenceId,
						Sediment = val.Sediment,
						Weed = val.Weed
					}).ToList();
		}

		public DrainageDTO TransformId(long id)
		{
			return (from val in Get(true)
					where val.DrainageId == id
					select new DrainageDTO()
					{
						Cleanliness = val.Cleanliness,
						Completeness = val.Completeness,
						Dicipline = val.Dicipline,
						DrainageId = val.DrainageId,
						PresenceId = val.PresenceId,
						Sediment = val.Sediment,
						Weed = val.Weed
					}).FirstOrDefault();
		}
	}
}
