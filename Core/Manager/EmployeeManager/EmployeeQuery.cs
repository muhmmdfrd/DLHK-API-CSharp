using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.EmployeeManager
{
	public class EmployeeQuery : AsistanceBase<EmployeeAdapter, Employee>
	{
		private readonly DateTime todayDateTime = DateTime.Now;

		public EmployeeQuery(EmployeeAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Employee> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.Employees;

			return withDetail ? 
				dataContext.AsQueryable()
				.Include(x => x.Person)
				.Include(x => x.Role)
				.Include(x => x.Region)
				.Include(x => x.Zone) : dataContext;
		}

		public List<EmployeeDTO> TranformUserLogin()
		{
			return (from val in Get()
					where
					!(from user in Manager.Database.Users
					  select user.EmployeeId).Contains(val.EmployeeId) &&
					val.RoleId != 5 &&
					val.RoleId != 4 &&
					val.RoleId != 10
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year,
						Shift = val.Shift
					}).ToList();
		}

		public List<EmployeeDTO> TransformHeadRegion()
		{
			return (from val in Get(true)
					where val.RoleId == 3 &&
					!(from hr in Manager.Database.Coordinators
					  select hr.EmployeeId).Contains(val.EmployeeId)
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year,
						Shift = val.Shift
					}).OrderBy(x => x.RoleId).ToList();
		}

		public List<EmployeeDTO> Transform()
		{
			var db = Manager.Database;
			var presence = db.Presences;
			var sweeper = db.Sweepers;
			var drainage = db.Drainages;
			var garbage = db.Garbages;
			var headZone = db.HeadOfZones;

			var allPresence = from all in presence
							  select all;
			
			var attend = from pr in presence
						 where pr.PresenceStatus.Equals("1")
						 select pr;

			return (from val in Get(true)
					where val.RoleId != 6
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year,
						Shift = val.Shift,
						SmartPresence =
							 attend.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count() * 100 /
						(allPresence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count() == 0 ? 1 
						:
						allPresence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count()),
						Perform = val.RoleId == 4 ? ((sweeper.Where(x => x.Presence.Employee.RoleId == 4 &&
							 x.Presence.Employee.EmployeeId == val.EmployeeId)
								.Sum(x => x.Dicipline + x.Completeness + x.WaterRope + x.Sidewalk + x.Road + x.RoadMedian)) /
								(6 * sweeper.Where(x => x.Presence.Employee.RoleId == 4 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.RoleId == 10 ? ((drainage.Where(x => x.Presence.Employee.RoleId == 10 &&
								x.Presence.Employee.EmployeeId == val.EmployeeId)
								.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed)) /
								(5 * drainage.Where(x => x.Presence.Employee.RoleId == 10 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.RoleId == 5 ?
								((garbage.Where(x => x.Presence.Employee.RoleId == 5 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId)
									.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS)) /
									(4 * garbage.Where(x => x.Presence.Employee.RoleId == 5 &&
										x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.RoleId == 1 ?
								((headZone.Where(x => x.Presence.Employee.RoleId == 1 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId)
									.Sum(x => x.CleanlinessOfZone + x.CompletenessOfTeam + x.DataOfGarbage + x.DiciplinePresence +
										x.FirstSession + x.SecondSession + x.ThirdSession)) /
											(7 * headZone.Where(x => x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) : 0
					}).OrderBy(x => x.RoleId).ToList();
		}

		public List<EmployeeDTO> TransformZoneAndRegion(string zoneParams, string regionParams)
		{
			var db = Manager.Database;
			var today = todayDateTime.Day;
			var thisMonth = todayDateTime.Month;
			var thisYear = todayDateTime.Year;

			return (from val in Get(true)
					where val.Zone.ZoneName.Equals(zoneParams) &&
					val.Region.RegionName.Equals(regionParams) &&
					val.RoleId != 1 && val.RoleId != 3 && val.RoleId != 6 &&
					!(from pr in db.Presences
					  where pr.DateOfPresence.Value.Day == today &&
					  pr.DateOfPresence.Value.Month == thisMonth &&
					  pr.DateOfPresence.Value.Year == thisYear
					  select pr.EmployeeId).Contains(val.EmployeeId) &&
					!(from l in db.Leaves
					  where l.DateOfLeave.Value.Day == today &&
					  l.DateOfLeave.Value.Month == thisMonth &&
					  l.DateOfLeave.Value.Year == thisYear
					  select l.EmployeeId).Contains(val.EmployeeId)
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year
					}).ToList();
		}

		public List<EmployeeDTO> TransformZoneAndRegionShift(string zoneParams, string regionParams, string shiftParams)
		{
			var db = Manager.Database;
			var today = todayDateTime.Day;
			var thisMonth = todayDateTime.Month;
			var thisYear = todayDateTime.Year;

			return (from val in Get(true)
					where val.Zone.ZoneName.Equals(zoneParams) &&
					val.Region.RegionName.Equals(regionParams) &&
					val.Shift.Equals(shiftParams) &&
					val.RoleId != 1 && val.RoleId != 3 && val.RoleId != 6 &&
					!(from pr in db.Presences
					  where pr.DateOfPresence.Value.Day == today &&
					  pr.DateOfPresence.Value.Month == thisMonth &&
					  pr.DateOfPresence.Value.Year == thisYear
					  select pr.EmployeeId).Contains(val.EmployeeId) &&
					!(from l in db.Leaves
					  where l.DateOfLeave.Value.Day == today &&
					  l.DateOfLeave.Value.Month == thisMonth &&
					  l.DateOfLeave.Value.Year == thisYear
					  select l.EmployeeId).Contains(val.EmployeeId)
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year
					}).ToList();
		}

		public List<EmployeeDTO> TransformHeadZoneShift(string regionParams)
		{
			var db = Manager.Database;
			var today = todayDateTime.Day;
			var thisMonth = todayDateTime.Month;
			var thisYear = todayDateTime.Year;

			return (from val in Get(true)
					where val.Region.RegionName.Equals(regionParams) &&
					val.RoleId == 1 &&
					!(from pr in db.Presences
					  where pr.DateOfPresence.Value.Day == today &&
					  pr.DateOfPresence.Value.Month == thisMonth &&
					  pr.DateOfPresence.Value.Year == thisYear
					  select pr.EmployeeId).Contains(val.EmployeeId) &&
					!(from l in db.Leaves
					  where l.DateOfLeave.Value.Day == today &&
					  l.DateOfLeave.Value.Month == thisMonth &&
					  l.DateOfLeave.Value.Year == thisYear
					  select l.EmployeeId).Contains(val.EmployeeId)
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year
					}).ToList();
		}

		public List<EmployeeDTO> TransformRegionShift()
		{
			var db = Manager.Database;
			var today = todayDateTime.Day;
			var thisMonth = todayDateTime.Month;
			var thisYear = todayDateTime.Year;

			return (from val in Get(true)
					where val.RoleId == 3 &&
					!(from pr in db.Presences
					  where pr.DateOfPresence.Value.Day == today &&
					  pr.DateOfPresence.Value.Month == thisMonth &&
					  pr.DateOfPresence.Value.Year == thisYear
					  select pr.EmployeeId).Contains(val.EmployeeId) &&
					!(from l in db.Leaves
					  where l.DateOfLeave.Value.Day == today &&
					  l.DateOfLeave.Value.Month == thisMonth &&
					  l.DateOfLeave.Value.Year == thisYear
					  select l.EmployeeId).Contains(val.EmployeeId)
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year
					}).ToList();
		}

		public List<EmployeeDTO> TransformZoneAndRegionSweeper(string zoneParams, string regionParams)
		{
			return (from val in Get(true)
					where val.Zone.ZoneName.Equals(zoneParams) &&
					val.Region.RegionName.Equals(regionParams) &&
					val.RoleId == 4 &&
					val.RoleId != 1 && 
					val.RoleId != 3 &&
					val.RoleId != 6
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year
					}).ToList();
		}

		public List<EmployeeDTO> TransformZoneAndRegionDrainage(string zoneParams, string regionParams)
		{
			return (from val in Get(true)
					where val.Zone.ZoneName.Equals(zoneParams) &&
					val.Region.RegionName.Equals(regionParams) &&
					val.RoleId == 10 &&
					val.RoleId != 1 && 
					val.RoleId != 3 && 
					val.RoleId != 6
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year,
					}).ToList();
		}

		public List<EmployeeDTO> TransformZoneAndRegionGarbage(string zoneParams, string regionParams)
		{
			return (from val in Get(true)
					where val.Zone.ZoneName.Equals(zoneParams) &&
					val.Region.RegionName.Equals(regionParams) &&
					val.RoleId == 5 &&
					val.RoleId != 1 && 
					val.RoleId != 3 && 
					val.RoleId != 6
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year
					}).ToList();
		}

		public List<EmployeeDTO> TransformSweeper()
		{
			var db = Manager.Database;

			return (from val in Get(true).AsEnumerable()
					where val.RoleId == 4 && 
					!(from pr in db.Presences.AsEnumerable()
					  where pr.DateOfPresence.Value.ToShortDateString()
					  .Equals(todayDateTime.ToShortDateString())
					  select pr.EmployeeId).Contains(val.EmployeeId) &&
					  !(from sw in db.Sweepers.AsEnumerable()
					   where sw.Presence.DateOfPresence.Value.ToShortDateString()
					   .Equals(todayDateTime.ToShortDateString())
					   select sw.Presence.EmployeeId).Contains(val.EmployeeId)
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year
					}).ToList();
		}

		public List<EmployeeDTO> TransformDrainage()
		{
			var db = Manager.Database;

			return (from emp in Get(true)
					where emp.RoleId == 10 &&
					!(from pr in db.Presences
					  where pr.DateOfPresence.Value.Day == todayDateTime.Day &&
					  pr.DateOfPresence.Value.Month == todayDateTime.Month &&
					  pr.DateOfPresence.Value.Year == todayDateTime.Year
					  select pr.EmployeeId).Contains(emp.EmployeeId)
					select new EmployeeDTO()
					{
						EmployeeId = emp.EmployeeId,
						EmployeeNumber = emp.EmployeeNumber,
						FirstContract = emp.FirstContract,
						LastContract = emp.LastContract,
						LocationContract = emp.LocationContract,
						PersonId = emp.PersonId,
						Bank = emp.Bank,
						RegionId = emp.RegionId,
						RoleId = emp.RoleId,
						ZoneId = emp.ZoneId,
						NamePerson = emp.Person.PersonName,
						Role = emp.Role.RoleName,
						Region = emp.Zone.Region.RegionName,
						ZoneName = emp.Zone.ZoneName,
						Age = todayDateTime.Year - emp.Person.DateOfBirth.Value.Year
					}).ToList();
		}

		public List<EmployeeDTO> TransformGarbage()
		{
			var db = Manager.Database;

			return (from val in Get(true)
					where val.RoleId == 5 &&
					!(from pr in db.Presences
					  where pr.DateOfPresence.Value.Day == todayDateTime.Day &&
					  pr.DateOfPresence.Value.Month == todayDateTime.Month &&
					  pr.DateOfPresence.Value.Year == todayDateTime.Year
					  select pr.EmployeeId).Contains(val.EmployeeId)
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						NamePerson = val.Person.PersonName,
						Role = val.Role.RoleName,
						Region = val.Zone.Region.RegionName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year
					}).ToList();
		}

		public List<EmployeeDTO> TransformHeadZone(string regionParams)
		{
			return (from val in Get(true)
					where val.Region.RegionName.Equals(regionParams)
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						NamePerson = val.Person.PersonName,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						Region = val.Region.RegionName,
						Role = val.Role.RoleName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year
					}).Where(x => x.RoleId == 1).ToList();
		}

		public EmployeeDTO TransformId(long id)
		{
			return (from val in Get(true)
					where val.EmployeeId == id
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						NamePerson = val.Person.PersonName,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						Region = val.Region.RegionName,
						Role = val.Role.RoleName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year,
						Shift = val.Shift
					}).FirstOrDefault();
		}

		public EmployeeDTO TransformName(string nameParams)
		{
			return (from val in Get(true)
					join person in Manager.Database.People
					on val.PersonId equals person.PersonId
					where person.PersonName.ToLower().Trim().Equals(nameParams.ToLower().Trim())
					select new EmployeeDTO()
					{
						EmployeeId = val.EmployeeId,
						NamePerson = val.Person.PersonName,
						EmployeeNumber = val.EmployeeNumber,
						FirstContract = val.FirstContract,
						LastContract = val.LastContract,
						LocationContract = val.LocationContract,
						PersonId = val.PersonId,
						Bank = val.Bank,
						RegionId = val.RegionId,
						RoleId = val.RoleId,
						ZoneId = val.ZoneId,
						Region = val.Region.RegionName,
						Role = val.Role.RoleName,
						ZoneName = val.Zone.ZoneName,
						Age = todayDateTime.Year - val.Person.DateOfBirth.Value.Year,
						Shift = val.Shift
					}).FirstOrDefault();
		}
	}
}
