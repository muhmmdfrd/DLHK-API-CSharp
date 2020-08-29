using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.GarbageManager
{
	public class GarbageQuery : AsistanceBase<GarbageAdapter, Garbage>
	{
		public GarbageQuery(GarbageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Garbage> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.Garbages;

			return withDetail ? dataContext.AsQueryable().Include(x => x.Presence) : dataContext;
		}

		public List<GarbageDTO> Transform()
		{
			return (from val in Get(true)
					select new GarbageDTO() {
						Calculation = val.Calculation,
						Dicipline = val.Dicipline,
						GerbageId = val.GerbageId,
						PresenceId = val.PresenceId,
						Separation = val.Separation,
						TPS = val.TPS,
						VolumeOfAnorganic = val.VolumeOfAnorganic,
						VolumeOfOrganic = val.VolumeOfOrganic
					}).ToList();
		}

		public GarbageDTO TransformId(long id)
		{
			return (from val in Get(true)
					where val.GerbageId == id
					select new GarbageDTO()
					{
						Calculation = val.Calculation,
						Dicipline = val.Dicipline,
						GerbageId = val.GerbageId,
						PresenceId = val.PresenceId,
						Separation = val.Separation,
						TPS = val.TPS,
						VolumeOfAnorganic = val.VolumeOfAnorganic,
						VolumeOfOrganic = val.VolumeOfOrganic
					}).FirstOrDefault();
		}
	}
}
