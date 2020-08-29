using Repository;
using System.Transactions;

namespace Core.Manager.HeadZoneManager
{
	public class HeadZoneCreator : AsistanceBase<HeadZoneAdapter, HeadOfZone>
	{
		public HeadZoneCreator(HeadZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public HeadOfZone Save(HeadZoneDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new HeadOfZone
				{
					CleanlinessOfZone = dto.CleanlinessOfZone,
					CompletenessOfTeam = dto.CompletenessOfTeam,
					DataOfGarbage = dto.DataOfGarbage,
					DiciplinePresence = dto.DiciplinePresence,
					FirstSession = dto.FirstSession,
					PresenceId = dto.PresenceId,
					SecondSession = dto.SecondSession,
					ThirdSession = dto.ThirdSession,
					TypeZone = dto.TypeZone
				};

				Manager.Database.HeadOfZones.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
