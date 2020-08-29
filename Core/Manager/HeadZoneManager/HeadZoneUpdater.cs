using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.HeadZoneManager
{
	public class HeadZoneUpdater : AsistanceBase<HeadZoneAdapter, HeadOfZone>
	{
		public HeadZoneUpdater(HeadZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public HeadOfZone Update(HeadZoneDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.HeadOfZoneId == dto.HeadOfZoneId);

				if (exist == null)
					throw new Exception("data not found");

				exist.CleanlinessOfZone = dto.CleanlinessOfZone;
				exist.CompletenessOfTeam = dto.CompletenessOfTeam;
				exist.DataOfGarbage = dto.DataOfGarbage;
				exist.DiciplinePresence = dto.DiciplinePresence;
				exist.FirstSession = dto.FirstSession;
				exist.HeadOfZoneId = dto.HeadOfZoneId;
				exist.PresenceId = dto.PresenceId;
				exist.SecondSession = dto.SecondSession;
				exist.ThirdSession = dto.ThirdSession;
				exist.TypeZone = dto.TypeZone;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
