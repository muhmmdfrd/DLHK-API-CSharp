using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.RegionManager
{
	public class RegionQuery : AsistanceBase<RegionAdapter, Region>
	{
		public RegionQuery(RegionAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Region> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.Regions;

			return withDetail ? dataContext.AsQueryable().Include(x => x.Zones) : dataContext; 
		}

		public List<RegionDTO> Transform()
		{
			return (from val in Get()
					select new RegionDTO()
					{
						RegionId = val.RegionId,
						RegionName = val.RegionName
					}).ToList();

		}

		public RegionDTO TransformId(long id)
		{
			return (from val in Get()
					where val.RegionId == id
					select new RegionDTO()
					{
						RegionId = val.RegionId,
						RegionName = val.RegionName
					}).FirstOrDefault();
		}
	}
}
