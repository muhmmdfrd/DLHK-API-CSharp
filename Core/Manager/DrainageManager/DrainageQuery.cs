using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.DrainageManager
{
	public class DrainageQuery : AsistanceBase<DrainageAdapter, Drainage>
	{
		public DrainageQuery(DrainageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<DrainageDTO> Get()
		{
			return Manager.Database.Drainages.Select(val => new DrainageDTO()
			{
				Cleanliness = val.Cleanliness,
				Completeness = val.Completeness,
				Dicipline = val.Dicipline,
				DrainageId = val.DrainageId,
				PresenceId = val.PresenceId,
				Sediment = val.Sediment,
				Weed = val.Weed
			});
		}

		public List<DrainageDTO> Transform()
		{
			return Get().ToList();
		}

		public DrainageDTO TransformId(long id)
		{
			return Get().FirstOrDefault(x => x.DrainageId == id);
		}
	}
}
