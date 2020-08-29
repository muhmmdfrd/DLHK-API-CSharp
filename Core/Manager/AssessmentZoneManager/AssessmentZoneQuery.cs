using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.AssessmentZoneManager
{
	public class AssessmentZoneQuery : AsistanceBase<AssessmentZoneAdapter, AssessmentZone>
	{
		public AssessmentZoneQuery(AssessmentZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<AssessmentZone> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.AssessmentZones;
			var fullData = withDetail ?
				dataContext.AsQueryable().Include(x => x.Employee) : dataContext;

			return fullData;
		}

		public List<AssessmentZoneDTO> Transform()
		{
			return (from val in Get(true)
					select new AssessmentZoneDTO()
					{
						CleanlinessOfZone = val.CleanlinessOfZone,
						CompletenessOfTeam = val.CompletenessOfTeam,
						DataOfGarbage = val.DataOfGarbage,
						DiciplinePresence = val.DiciplinePresence,
						FirstSession = val.FirstSession,
						AssessmentZoneId = val.AssessmentZoneId,
						EmployeeId = val.EmployeeId,
						SecondSession = val.SecondSession,
						ThirdSession = val.ThirdSession,
						TypeZone = val.TypeZone
					}).ToList();
		}

		public AssessmentZoneDTO TransformId(long id)
		{
			return (from val in Get(true)
					where val.AssessmentZoneId == id
					select new AssessmentZoneDTO()
					{
						CleanlinessOfZone = val.CleanlinessOfZone,
						CompletenessOfTeam = val.CompletenessOfTeam,
						DataOfGarbage = val.DataOfGarbage,
						DiciplinePresence = val.DiciplinePresence,
						FirstSession = val.FirstSession,
						AssessmentZoneId = val.AssessmentZoneId,
						EmployeeId = val.EmployeeId,
						SecondSession = val.SecondSession,
						ThirdSession = val.ThirdSession,
						TypeZone = val.TypeZone
					}).FirstOrDefault();

		}
	}
}
