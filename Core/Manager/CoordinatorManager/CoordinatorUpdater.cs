using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.CoordinatorManager
{
	public class CoordinatorUpdater : AsistanceBase<CoordinatorAdapter, Coordinator>
	{
		public CoordinatorUpdater(CoordinatorAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Coordinator Update(CoordinatorDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.CoordinatorId == dto.CoordinatorId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Cleanliness = dto.Cleanliness;
				exist.CoordinatorId = dto.CoordinatorId;
				exist.DataOfGarbage = dto.DataOfGarbage;
				exist.PercentOfCompletion = dto.PercentOfCompletion;
				exist.PercentOfPresence = dto.PercentOfPresence;
				exist.PercentOfReport = dto.PercentOfReport;
				exist.PercentOfSatisfaction = dto.PercentOfSatisfaction;
				exist.EmployeeId = dto.EmployeeId;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
