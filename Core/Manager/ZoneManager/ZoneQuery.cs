using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.ZoneManager
{
	public class ZoneQuery : AsistanceBase<ZoneAdapter, Zone>
	{
		public ZoneQuery(ZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<ZoneDTO> GetQuery()
		{
			return Manager.Database.Zones
				.Join(Manager.Database.Regions, z => z.RegionId, r => r.RegionId, (z, r) => new { z, r })
				.Select(x => new ZoneDTO()
				{
					ZoneId = x.z.ZoneId,
					ZoneName = x.z.ZoneName,
					RegionId = x.r.RegionId,
					RegionName = x.r.RegionName
				});
		}

		public List<ZoneDTO> Transform()
		{
			return GetQuery().ToList();
		}

		public ZoneDTO TransformId(long id)
		{
			return GetQuery().FirstOrDefault(x => x.ZoneId == id);
		}
	}
}
