using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.ZoneManager
{
	public class ZoneQuery : AsistanceBase<ZoneAdapter, Zone>
	{
		public ZoneQuery(ZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Zone> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.Zones;
			var fullData = withDetail ? dataContext.AsQueryable().Include(x => x.Region) : dataContext;

			return fullData;
		}

		public List<ZoneDTO> Transform()
		{
			return (from val in Get()
					select new ZoneDTO()
					{
						ZoneId = val.ZoneId,
						ZoneName = val.ZoneName,
						RegionId = val.Region.RegionId,
						RegionName = val.Region.RegionName
					}).ToList();
		}

		public ZoneDTO TransformId(long id)
		{
			return (from val in Get()
					where val.ZoneId == id
					select new ZoneDTO()
					{
						ZoneId = val.ZoneId,
						ZoneName = val.ZoneName,
						RegionId = val.Region.RegionId,
						RegionName = val.Region.RegionName
					}).FirstOrDefault();
		}
	}
}
