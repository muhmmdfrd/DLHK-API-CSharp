using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.LeaveManager
{
	public class LeaveUpdater : AsistanceBase<LeaveAdapter, Leave>
	{
		public LeaveUpdater(LeaveAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Leave Update(LeaveDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.LeaveId == dto.LeaveId);

				if (exist == null)
					throw new Exception("data not found");

				exist.DateOfLeave = dto.DateOfLeave;
				exist.Description = dto.Description;
				exist.EmployeeId = dto.EmployeeId;
				exist.LeaveStatus = dto.LeaveStatus;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}

		public Leave Confirm(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.LeaveId == id);

				if (exist == null)
					throw new Exception("data not found");

				exist.LeaveStatus = "terkonfirmasi";

				Manager.PresenceManager.Value.Creator.Value.SaveLeave(exist.EmployeeId);

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
