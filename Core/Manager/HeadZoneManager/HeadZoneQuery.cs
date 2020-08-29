using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.HeadZoneManager
{
	public class HeadZoneQuery : AsistanceBase<HeadZoneAdapter, HeadOfZone>
	{
		public HeadZoneQuery(HeadZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<HeadOfZone> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.HeadOfZones;
			var fullData = withDetail ?
				dataContext.AsQueryable().Include(x => x.Presence) : dataContext;

			return fullData;
		}

		public List<HeadZoneDTO> Transform()
		{
			return (from val in Get(true)
					select new HeadZoneDTO()
					{
						CleanlinessOfZone = val.CleanlinessOfZone,
						CompletenessOfTeam = val.CompletenessOfTeam,
						DataOfGarbage = val.DataOfGarbage,
						DiciplinePresence = val.DiciplinePresence,
						FirstSession = val.FirstSession,
						HeadOfZoneId = val.HeadOfZoneId,
						PresenceId = val.PresenceId,
						SecondSession = val.SecondSession,
						ThirdSession = val.ThirdSession,
						TypeZone = val.TypeZone
					}).ToList();
		}

		public HeadZoneDTO TransformId(long id)
		{
			return (from val in Get(true)
					where val.HeadOfZoneId == id
					select new HeadZoneDTO()
					{
						CleanlinessOfZone = val.CleanlinessOfZone,
						CompletenessOfTeam = val.CompletenessOfTeam,
						DataOfGarbage = val.DataOfGarbage,
						DiciplinePresence = val.DiciplinePresence,
						FirstSession = val.FirstSession,
						HeadOfZoneId = val.HeadOfZoneId,
						PresenceId = val.PresenceId,
						SecondSession = val.SecondSession,
						ThirdSession = val.ThirdSession,
						TypeZone = val.TypeZone
					}).FirstOrDefault();

		}
	}
}
