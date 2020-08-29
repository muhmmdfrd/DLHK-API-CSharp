using Repository;
using System;
using System.Transactions;

namespace Core.Manager.CoordinatorManager
{
	public class CoordinatorCreator : AsistanceBase<CoordinatorAdapter, Coordinator>
	{
		public CoordinatorCreator(CoordinatorAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Coordinator Save(CoordinatorDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Coordinator
				{
					Cleanliness = dto.Cleanliness,
					DataOfGarbage = dto.DataOfGarbage,
					PercentOfCompletion = dto.PercentOfCompletion,
					PercentOfPresence = dto.PercentOfPresence,
					PercentOfReport = dto.PercentOfReport,
					PercentOfSatisfaction = dto.PercentOfSatisfaction,
					EmployeeId = dto.EmployeeId,
					DateOfAssessment = DateTime.Now
				};

				Manager.Database.Coordinators.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
