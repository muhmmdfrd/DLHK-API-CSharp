using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.PresenceManager
{
	public class PresenceUpdater : AsistanceBase<PresenceAdapter, Presence>
	{
		public PresenceUpdater(PresenceAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Presence Update(PresenceDTO dto)
		{
			using (var transac = new TransactionScope())
			{

				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PresenceId == dto.PresenceId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Coordinate = dto.Coordinate;
				exist.DateOfPresence = dto.DateOfPresence;
				exist.EmployeeId = dto.EmployeeId;
				exist.LivePhoto = dto.LivePhoto;
				exist.PresenceStatus = dto.PresenceStatus;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
