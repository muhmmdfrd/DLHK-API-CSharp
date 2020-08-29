using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.CoordinatorManager
{
	public class CoordinatorQuery : AsistanceBase<CoordinatorAdapter, Coordinator>
	{
		public CoordinatorQuery(CoordinatorAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Coordinator> Get(bool withDetail = false)
		{
			return Manager.Database.Coordinators;
		}

		public List<CoordinatorDTO> Transform()
		{
			return (from val in Get(true)
					select new CoordinatorDTO()
					{
						Cleanliness = val.Cleanliness,
						CoordinatorId = val.CoordinatorId,
						DataOfGarbage = val.DataOfGarbage,
						PercentOfCompletion = val.PercentOfCompletion,
						PercentOfPresence = val.PercentOfPresence,
						PercentOfReport = val.PercentOfReport,
						PercentOfSatisfaction = val.PercentOfSatisfaction,
						EmployeeId = val.EmployeeId
					}).ToList();
		}

		public CoordinatorDTO TransformId(long id)
		{
			return (from val in Get(true)
					where val.CoordinatorId == id
					select new CoordinatorDTO()
					{
						Cleanliness = val.Cleanliness,
						CoordinatorId = val.CoordinatorId,
						DataOfGarbage = val.DataOfGarbage,
						PercentOfCompletion = val.PercentOfCompletion,
						PercentOfPresence = val.PercentOfPresence,
						PercentOfReport = val.PercentOfReport,
						PercentOfSatisfaction = val.PercentOfSatisfaction,
						EmployeeId = val.EmployeeId
					}).FirstOrDefault();
		}
	}
}
