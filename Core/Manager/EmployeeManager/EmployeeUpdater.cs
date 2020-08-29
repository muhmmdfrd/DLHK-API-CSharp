using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.EmployeeManager
{
	public class EmployeeUpdater : AsistanceBase<EmployeeAdapter, Employee>
	{
		public EmployeeUpdater(EmployeeAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Employee Update(EmployeeDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.EmployeeId == dto.EmployeeId);

				if (exist == null)
					throw new Exception("data not found");

				exist.EmployeeNumber = dto.EmployeeNumber;
				exist.FirstContract = dto.FirstContract;
				exist.LastContract = dto.LastContract;
				exist.LocationContract = dto.LocationContract;
				exist.PersonId = dto.PersonId;
				exist.RegionId = dto.RegionId;
				exist.RoleId = dto.RoleId;
				exist.ZoneId = dto.ZoneId;
				exist.Bank = dto.Bank;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}

		public Employee UpdateLocation(EmployeeDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.EmployeeId == dto.EmployeeId);
				var db = Manager.Database;

				if (exist == null)
					throw new Exception("data not found");

				switch (exist.RoleId)
				{
					case 4:
						var dataSweeper = db.Sweepers.AsQueryable().Where(x => x.Presence.EmployeeId == exist.EmployeeId);
						foreach (var row in dataSweeper)
						{
							db.Sweepers.Remove(row);
						}
					break;
					case 10:
						var dataDrainage = db.Drainages.AsQueryable().Where(x => x.Presence.EmployeeId == exist.EmployeeId);
						foreach (var row in dataDrainage)
						{
							db.Drainages.Remove(row);
						}
					break;
					case 5:
						var dataGarbage = db.Garbages.AsQueryable().Where(x => x.Presence.EmployeeId == exist.EmployeeId);
						foreach (var row in dataGarbage)
						{
							db.Garbages.Remove(row);
						}
					break;
					default:
					break;
				}

				exist.ZoneId = dto.ZoneId;
				exist.RegionId = dto.RegionId;
				exist.RoleId = dto.RoleId;
				exist.Shift = dto.Shift;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}

		public Employee UpdateContract(EmployeeDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == dto.PersonId);

				if (exist == null)
					throw new Exception("data not found");

				exist.FirstContract = dto.FirstContract;
				exist.LastContract = dto.FirstContract.Value.AddMonths(3);

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
