using Repository;
using System.Transactions;

namespace Core.Manager.LeaveManager
{
	public class LeaveCreator : AsistanceBase<LeaveAdapter, Leave>
	{
		public LeaveCreator(LeaveAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Leave Save(LeaveDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Leave
				{
					DateOfLeave = dto.DateOfLeave,
					Description = dto.Description,
					EmployeeId = dto.EmployeeId,
					LeaveStatus = dto.LeaveStatus,
					Location = dto.Location
				};

				if (dto.LeaveStatus.Equals("Alfa"))
				{
					Manager.PresenceManager.Value.Creator.Value.SaveAbsence(dto.EmployeeId, dto.Location);
				}
				else
				{
					Manager.PresenceManager.Value.Creator.Value.SaveLeave(dto.EmployeeId, dto.Location);
				}

				Manager.Database.Leaves.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
