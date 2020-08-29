using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.PresenceManager
{
	public class PresenceCreator : AsistanceBase<PresenceAdapter, Presence>
	{
		public PresenceCreator(PresenceAdapter manager) : base(manager)
		{
			// do nothing
		}

		private TimeSpan onTimePresence;
		private DateTime? separateEmployee;

		public Presence Save(PresenceDTO dto)
		{
			var presenceData = Manager.Query.Value.Get().AsEnumerable();

			// izin, hadir, alfa, atau telat
			string presenceStatus = "";
			var dbEmployee = Manager.Database.Employees;
			var dataEmployee = dbEmployee.FirstOrDefault(x => x.EmployeeId == dto.EmployeeId);
			long? headZoneRole = dataEmployee.RoleId;
			long? headZoneId = dataEmployee.ZoneId;

			// absennya kepala zona
			if (presenceData == null)
			{
				throw new Exception("data not found");
			}
			else if (headZoneRole == 1)
			{
				var presenceHeadZone = presenceData.AsEnumerable()
					.FirstOrDefault(x => x.Employee.RoleId == 1 &&
						x.Employee.ZoneId == headZoneId &&
						x.DateOfPresence.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()));

				if (presenceHeadZone == null)
				{
					switch (dto.Shift)
					{
						case "1":
							onTimePresence = new TimeSpan(4, 30, 0);
							break;
						case "2":
							onTimePresence = new TimeSpan(12, 15, 0);
							break;
						case "3":
							onTimePresence = new TimeSpan(20, 15, 0);
							break;
						default:
							break;
					}

					if (dto.DateOfPresence.Value.Hour >= onTimePresence.Hours || 
					dto.DateOfPresence.Value.Minute > onTimePresence.Minutes)
						presenceStatus = "3";
					else
						presenceStatus = "1";
				}
				else
				{
					throw new Exception("head zone has presence yet");
				}
			}
			// absen penyapu dll
			else
			{
				var today = DateTime.Now;

				switch (dto.Shift)
				{
					case "1":
						separateEmployee = presenceData.FirstOrDefault(x => x.Employee.RoleId == 1 &&
							x.Employee.Shift.Equals("1") &&
							x.DateOfPresence.Value.ToShortDateString()
								.Equals(today.ToShortDateString())).DateOfPresence.Value.AddMinutes(30);
						break;
					case "2":
						separateEmployee = presenceData.FirstOrDefault(x => x.Employee.RoleId == 1 &&
							x.Employee.Shift.Equals("2") &&
							x.DateOfPresence.Value.ToShortDateString()
								.Equals(today.ToShortDateString())).DateOfPresence.Value.AddMinutes(15);
						break;
					case "3":
						separateEmployee = presenceData.FirstOrDefault(x => x.Employee.RoleId == 1 &&
							x.Employee.Shift.Equals("3") &&
							x.DateOfPresence.Value.ToShortDateString()
								.Equals(today.ToShortDateString())).DateOfPresence.Value.AddMinutes(15);
						break;
					default:
						break;
				}

				if (dto.DateOfPresence.Value.Hour >= separateEmployee.Value.Hour &&
					dto.DateOfPresence.Value.Minute > separateEmployee.Value.Minute)
					presenceStatus = "3";
				else
					presenceStatus = "1";
			}

			using (var transac = new TransactionScope())
			{
				var newEntity = new Presence
				{
					Coordinate = dto.Coordinate,
					DateOfPresence = dto.DateOfPresence,
					EmployeeId = dto.EmployeeId,
					LivePhoto = dto.LivePhoto,
					PresenceStatus = presenceStatus
				};

				Manager.Database.Presences.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}

		public Presence SaveLeave(long? employeeIdParams)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Presence
				{
					Coordinate = "",
					DateOfPresence = DateTime.Now,
					EmployeeId = employeeIdParams,
					LivePhoto = null,
					PresenceStatus = "2"
				};

				Manager.Database.Presences.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}

		public Presence SaveAbsence(long? employeeIdParams)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Presence
				{
					Coordinate = "",
					DateOfPresence = DateTime.Now,
					EmployeeId = employeeIdParams,
					LivePhoto = null,
					PresenceStatus = "0"
				};

				Manager.Database.Presences.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
