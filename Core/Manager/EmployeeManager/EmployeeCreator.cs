using Repository;
using System.Transactions;

namespace Core.Manager.EmployeeManager
{
	public class EmployeeCreator : AsistanceBase<EmployeeAdapter, Employee>
	{
		public EmployeeCreator(EmployeeAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Employee Save(EmployeeDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Employee
				{
					EmployeeNumber = dto.EmployeeNumber,
					FirstContract = dto.FirstContract,
					LastContract = dto.FirstContract.Value.AddMonths(3),
					LocationContract = dto.LocationContract,
					PersonId = dto.PersonId,
					Bank = dto.Bank,
					RoleId = dto.RoleId,
					RegionId = dto.RegionId,
					ZoneId = dto.ZoneId
				};

				Manager.PersonManager.Value.Updater.Value.Accepted(dto.PersonId);

				Manager.Database.Employees.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
