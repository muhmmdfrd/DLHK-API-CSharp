using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.AssessmentZoneManager
{
	public class AssessmentZoneQuery : AsistanceBase<AssessmentZoneAdapter, AssessmentZone>
	{
		public AssessmentZoneQuery(AssessmentZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<AssessmentZoneDTO> GetQuery()
		{
			return Manager.Database.AssessmentZones.Select(val => new AssessmentZoneDTO()
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
			});
		}

		public List<AssessmentZoneDTO> Transform()
		{
			return GetQuery().ToList();
		}

		public AssessmentZoneDTO TransformId(long id)
		{
			return GetQuery().FirstOrDefault(x => x.AssessmentZoneId == id);
		}
	}
}
