using Repository;
using System.Transactions;

namespace Core.Manager.AssessmentZoneManager
{
	public class AssessmentZoneCreator : AsistanceBase<AssessmentZoneAdapter, AssessmentZone>
	{
		public AssessmentZoneCreator(AssessmentZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public AssessmentZone Save(AssessmentZoneDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new AssessmentZone
				{
					CleanlinessOfZone = dto.CleanlinessOfZone,
					CompletenessOfTeam = dto.CompletenessOfTeam,
					DataOfGarbage = dto.DataOfGarbage,
					DiciplinePresence = dto.DiciplinePresence,
					FirstSession = dto.FirstSession,
					EmployeeId = dto.EmployeeId,
					SecondSession = dto.SecondSession,
					ThirdSession = dto.ThirdSession,
					TypeZone = dto.TypeZone
				};

				Manager.Database.AssessmentZones.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
