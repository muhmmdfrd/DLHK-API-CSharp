using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.PresenceManager
{
	public class PresenceQuery : AsistanceBase<PresenceAdapter, Presence>
	{
		public PresenceQuery(PresenceAdapter manager) : base(manager)
		{
			// do nothing
		}

		private readonly DateTime Today = DateTime.Now;

		public IQueryable<Presence> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.Presences;

			return withDetail ?
				dataContext.AsQueryable()
				.Include(x => x.Employee)
				.Include(x => x.Sweepers)
				.Include(x => x.Drainages)
				.Include(x => x.Sweepers) : dataContext;
		}

		public List<PresenceDTO> Transform()
		{
			return (from val in Get(true)
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = null,
						PresenceId = val.PresenceId,
						PresenceStatus = val.PresenceStatus,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Counter = val.Counter,
						Location = val.Location
					}).ToList();
		}

		public List<PresenceDTO> TransformWithPhoto()
		{
			return (from val in Get(true)
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = val.LivePhoto,
						PresenceId = val.PresenceId,
						PresenceStatus = val.PresenceStatus,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Counter = val.Counter,
						Location = val.Location
					}).ToList();
		}

		public List<PresenceDTO> TransformWithPhotoAndParam(string status, string zoneParams)
		{
			return (from val in Get(true).AsEnumerable()
					where val.PresenceStatus.Equals(status) &&
					val.Employee.Zone.ZoneName.Equals(zoneParams) &&
					val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = val.LivePhoto,
						PresenceId = val.PresenceId,
						PresenceStatus = val.PresenceStatus,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Counter = val.Counter,
						Location = val.Location,
						TimeOfPresence = val.DateOfPresence.Value.ToShortTimeString()
					}).ToList();
		}

		public List<PresenceDTO> TransformHeadRegion()
		{
			return (from val in Get(true).AsEnumerable()
					where val.Employee.RoleId == 3 &&
					val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = null,
						PresenceId = val.PresenceId,
						PresenceStatus = val.PresenceStatus,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Counter = val.Counter,
						Location = val.Location,
					}).GroupBy(x => x.EmployeeName).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<PresenceDTO> TransformSweeper(string zoneParams, string regionParams, string shiftParams)
		{
			return (from val in Get(true).AsEnumerable()
					where val.Employee.RoleId == 4 &&
					val.Employee.Zone.ZoneName.Equals(zoneParams) &&
					val.Employee.Region.RegionName.Equals(regionParams) &&
					val.Employee.Shift.Equals(shiftParams) &&
					val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString()) &&
					val.Counter < 3
					// &&
					//!(from sw in Manager.Database.Sweepers.AsEnumerable()
					//  where sw.Presence.DateOfPresence.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString())
					//  select sw.Presence.PresenceId).Contains(val.PresenceId)
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = null,
						PresenceId = val.PresenceId,
						PresenceStatus = val.PresenceStatus,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Counter = val.Counter,
						Location = val.Location,
					}).GroupBy(x => x.EmployeeName).Select(x => x.FirstOrDefault()).ToList();
		}


		public List<PresenceDTO> TransformDrainage(string zoneParams, string regionParams, string shiftParams)
		{
			return (from val in Get(true).AsEnumerable()
					where val.Employee.RoleId == 10 &&
					val.Employee.Zone.ZoneName.Equals(zoneParams) &&
					val.Employee.Region.RegionName.Equals(regionParams) &&
					val.Employee.Shift.Equals(shiftParams) &&
					val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())  &&
					val.Counter < 3
					// &&
					//!(from d in Manager.Database.Drainages.AsEnumerable()
					//  where d.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())
					//  select d.Presence.PresenceId).Contains(val.PresenceId)
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = null,
						PresenceId = val.PresenceId,
						PresenceStatus = val.PresenceStatus,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Counter = val.Counter,
						Location = val.Location
					}).GroupBy(x => x.EmployeeName).Select(x => x.FirstOrDefault()).ToList();
		}

		public DashboardDTO TransformDashboard()
		{
			var db = Manager.Database;
			var employees = from employee in db.Employees
							select employee;

			var allPresence = (from all in Get() 
							  select all).Count();

			var presences = (from presence in Get()
							where presence.PresenceStatus.Equals("1")
							select presence).Count() * 100;

			var sweeper = from sw in db.Sweepers
						  select sw;

			var drainage = from dr in db.Drainages
						   select dr;

			var garbage = from gr in db.Garbages
						  select gr;

			return new DashboardDTO()
			{
				Employees = employees.Count(),
				Presences = presences / allPresence,
				Performances = 
				((sweeper.Sum(x => x.Dicipline + x.Completeness + x.WaterRope + x.Sidewalk + x.Road + x.RoadMedian) / (sweeper.Count() * 6)) +
				(drainage.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed) / (drainage.Count() * 5)) +
				(garbage.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS) / (garbage.Count() * 4))) / 3,
				Score = (((sweeper.Sum(x => x.Dicipline + x.Completeness + x.WaterRope + x.Sidewalk + x.Road + x.RoadMedian) / (sweeper.Count() * 6)) +
				(drainage.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed) / (drainage.Count() * 5)) +
				(garbage.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS) / (garbage.Count() * 4))) / 3 + presences / allPresence) / 2
			};
		}

		public DashboardItemDTO TransformDashboardItem()
		{
			var db = Manager.Database;
			var item = (from i in db.Items
					   select i).Count();

			var transac = from t in db.Transacs
						  select t;

			return new DashboardItemDTO()
			{
				Items = item,
				In = transac.Where(x => x.TypeOfTransac.Equals("IN")).Count(),
				Out = transac.Where(x => x.TypeOfTransac.Equals("OUT")).Count()
			};
		}

		public DashboardContractDTO TransformDashboardContract()
		{
			var db = Manager.Database;
			var app = from p in db.People
					  where p.Jobdesk != "Employee" &&
					  p.Jobdesk != "Interview"
					  select p;

			var interview = from p in db.People
							 where p.Jobdesk.Equals("Interview")
							 select p;

			var role = (from r in db.Roles
						where r.RoleId != 6
						select r).Count();

			var today = DateTime.Now;
			var employee = from emp in db.Employees
						   where (emp.LastContract.Value.Month == today.Month + 1 &&
						   emp.LastContract.Value.Year <= today.Year) ||
						   emp.LastContract.Value.Month <= today.Month &&
						   emp.LastContract.Value.Year <= today.Year
						   select emp;

			return new DashboardContractDTO()
			{
				Applicants = app.Count(),
				Interviewers = interview.Count(),
				RoleActive = role,
				Expired = employee.Count()
			};
		}

		public List<PresenceDTO> TransformGarbage(string zoneParams, string regionParams, string shiftParams)
		{
			return (from val in Get(true).AsEnumerable()
					where val.Employee.RoleId == 5 &&
					val.Employee.Zone.ZoneName.Equals(zoneParams) &&
					val.Employee.Region.RegionName.Equals(regionParams) &&
					val.Employee.Shift.Equals(shiftParams) &&
					val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())  &&
					val.Counter < 3
					// &&
					//!(from g in Manager.Database.Garbages.AsEnumerable()
					//  where g.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())
					// select g.Presence.PresenceId).Contains(val.PresenceId)
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = null,
						PresenceId = val.PresenceId,
						PresenceStatus = val.PresenceStatus,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Counter = val.Counter,
						Location = val.Location
					}).GroupBy(x => x.EmployeeName).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<PresenceDTO> TransformHeadZone()
		{
			return (from val in Get(true).AsEnumerable()
					where val.Employee.RoleId == 1 &&
					val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString()) &&
					!(from hz in Manager.Database.HeadOfZones.AsEnumerable()
					  where hz.Presence.DateOfPresence.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString())
					  select hz.Presence.PresenceId).Contains(val.PresenceId)
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = null,
						PresenceId = val.PresenceId,
						PresenceStatus = val.PresenceStatus,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Counter = val.Counter,
						Location = val.Location
					}).ToList();
		}

		public List<PresenceResumeDTO> TransformResume()
		{
			var absence = from val in Get()
						  where val.PresenceStatus.Equals("0")
						  select val;

			var leave = from val in Get()
						where val.PresenceStatus.Equals("2")
						select val;

			var presence = from val in Get()
						   where val.PresenceStatus.Equals("1")
						   select val;

			var allPresence = from val in Get() 
							  select val;

			return (from val in Get(true)
					select new PresenceResumeDTO()
					{
						LocationContract = val.Employee.LocationContract,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						Absence = absence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
						Leave = leave.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
						PresenceTotal = presence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Percentage = presence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count() * 100 /
						allPresence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
					}).OrderBy(x => x.ZoneName).GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<PresenceResumeDTO> TransformResume(string start, string end)
		{
			var startDate = Convert.ToDateTime(start);
			var endDate = Convert.ToDateTime(end);

			var absence = from val in Get()
						  where val.PresenceStatus.Equals("0") &&
						  val.DateOfPresence >= startDate &&
						  val.DateOfPresence <= endDate
						  select val;

			var leave = from val in Get()
						where val.PresenceStatus.Equals("2") &&
						val.DateOfPresence >= startDate &&
						val.DateOfPresence <= endDate
						select val;

			var presence = from val in Get()
						   where val.PresenceStatus.Equals("1") &&
						   val.DateOfPresence >= startDate &&
						   val.DateOfPresence <= endDate
						   select val;

			var allPresence = from val in Get()
							  where val.DateOfPresence >= startDate &&
							  val.DateOfPresence <= endDate
							  select val;

			return (from val in Get(true)
					where val.DateOfPresence >= startDate &&
					val.DateOfPresence <= endDate
					select new PresenceResumeDTO()
					{
						LocationContract = val.Employee.LocationContract,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						Absence = absence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
						Leave = leave.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
						PresenceTotal = presence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Percentage = presence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count() * 100 /
						allPresence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
					}).OrderBy(x => x.ZoneName).GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<PresenceResumeDTO> TransformResumeZoneRegion(string zoneParams, string regionParams)
		{
			var absence = from val in Get()
						  where val.PresenceStatus.Equals("0")
						  select val;

			var leave = from val in Get()
						where val.PresenceStatus.Equals("2")
						select val;

			var presence =	from val in Get()
							where val.PresenceStatus.Equals("1")
							select val;

			var allPresence = from val in Get() select val; 

			return (from val in Get(true)
					where val.Employee.Region.RegionName.Equals(regionParams) &&
					val.Employee.Zone.ZoneName.Equals(zoneParams)
					select new PresenceResumeDTO()
					{
						LocationContract = val.Employee.LocationContract,
						EmployeeId = val.EmployeeId,
						Photo = val.Employee.Person.Photo,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						Absence = absence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
						Leave = leave.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
						PresenceTotal = presence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count(),
						Percentage = ((presence.Where(x => x.EmployeeId == val.EmployeeId).Select(x => x.PresenceStatus).Count() * 100) /
						allPresence.Where(x => x.EmployeeId == val.EmployeeId).Select(X => X.PresenceStatus).Count()),
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift
					}).OrderBy(x => x.ZoneName).GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<EmployeePerformDTO> TransformPerform()
		{
			var db = Manager.Database;
			var sweeper = db.Sweepers;
			var drainage = db.Drainages;
			var garbage = db.Garbages;
			var headZone = db.HeadOfZones;

			return (from val in Get(true)
					select new EmployeePerformDTO()
					{
						LocationContract = val.Employee.LocationContract,
						EmployeeId = val.EmployeeId,
						Photo = null,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Percentage = val.Employee.RoleId == 4 ? ((sweeper.Where(x => x.Presence.Employee.RoleId == 4 &&
							x.Presence.Employee.EmployeeId == val.EmployeeId)
								.Sum(x => x.Dicipline + x.Completeness + x.WaterRope + x.Sidewalk + x.Road + x.RoadMedian)) /
								(6 * sweeper.Where(x => x.Presence.Employee.RoleId == 4 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.Employee.RoleId == 10 ? ((drainage.Where(x => x.Presence.Employee.RoleId == 10 &&
								x.Presence.Employee.EmployeeId == val.EmployeeId)
								.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed)) /
								(5 * drainage.Where(x => x.Presence.Employee.RoleId == 10 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.Employee.RoleId == 5 ?
								((garbage.Where(x => x.Presence.Employee.RoleId == 5 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId)
									.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS)) /
									(4 * garbage.Where(x => x.Presence.Employee.RoleId == 5 &&
										x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.Employee.RoleId == 1 ?
								((headZone.Where(x => x.Presence.Employee.RoleId == 1 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId)
									.Sum(x => x.CleanlinessOfZone + x.CompletenessOfTeam + x.DataOfGarbage + x.DiciplinePresence +
										x.FirstSession + x.SecondSession + x.ThirdSession)) /
											(7 * headZone.Where(x => x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) : 0
					}).GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<EmployeePerformDTO> TransformPerform(string zoneParams)
		{
			var db = Manager.Database;
			var sweeper = db.Sweepers;
			var drainage = db.Drainages;
			var garbage = db.Garbages;
			var headZone = db.HeadOfZones;

			return (from val in Get(true)
					where val.Employee.Zone.ZoneName.Equals(zoneParams)
					select new EmployeePerformDTO()
					{
						LocationContract = val.Employee.LocationContract,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						Photo = val.Employee.Person.Photo,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Percentage = val.Employee.RoleId == 4 ? ((sweeper.Where(x => x.Presence.Employee.RoleId == 4 &&
							x.Presence.Employee.EmployeeId == val.EmployeeId)
								.Sum(x => x.Dicipline + x.Completeness + x.WaterRope + x.Sidewalk + x.Road + x.RoadMedian)) /
								(6 * sweeper.Where(x => x.Presence.Employee.RoleId == 4 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.Employee.RoleId == 10 ? ((drainage.Where(x => x.Presence.Employee.RoleId == 10 &&
								x.Presence.Employee.EmployeeId == val.EmployeeId)
								.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed)) / 
								(5 * drainage.Where(x => x.Presence.Employee.RoleId == 10 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.Employee.RoleId == 5 ?
								((garbage.Where(x => x.Presence.Employee.RoleId == 5 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId)
									.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS)) / 
									(4 * garbage.Where(x => x.Presence.Employee.RoleId == 5 &&
										x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.Employee.RoleId == 1 ?
								((headZone.Where(x => x.Presence.Employee.RoleId == 1 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId)
									.Sum(x => x.CleanlinessOfZone + x.CompletenessOfTeam + x.DataOfGarbage + x.DiciplinePresence +
										x.FirstSession + x.SecondSession + x.ThirdSession)) / 
											(7 * headZone.Where(x => x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) : 0
					}).GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<EmployeePerformDTO> TransformPerformFilter(string start, string to)
		{
			var db = Manager.Database;
			var sweeper = db.Sweepers;
			var drainage = db.Drainages;
			var garbage = db.Garbages;
			var headZone = db.HeadOfZones;

			var startDate = Convert.ToDateTime(start);
			var endDate = Convert.ToDateTime(to);

			return (from val in Get(true)
					where val.DateOfPresence >= startDate &&
					val.DateOfPresence <= endDate
					select new EmployeePerformDTO()
					{
						LocationContract = val.Employee.LocationContract,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						Photo = val.Employee.Person.Photo,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift,
						Percentage = val.Employee.RoleId == 4 ? ((sweeper.Where(x => x.Presence.Employee.RoleId == 4 &&
							x.Presence.Employee.EmployeeId == val.EmployeeId)
								.Sum(x => x.Dicipline + x.Completeness + x.WaterRope + x.Sidewalk + x.Road + x.RoadMedian)) /
								(6 * sweeper.Where(x => x.Presence.Employee.RoleId == 4 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.Employee.RoleId == 10 ? ((drainage.Where(x => x.Presence.Employee.RoleId == 10 &&
								x.Presence.Employee.EmployeeId == val.EmployeeId)
								.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed)) /
								(5 * drainage.Where(x => x.Presence.Employee.RoleId == 10 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.Employee.RoleId == 5 ?
								((garbage.Where(x => x.Presence.Employee.RoleId == 5 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId)
									.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS)) /
									(4 * garbage.Where(x => x.Presence.Employee.RoleId == 5 &&
										x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) :
							val.Employee.RoleId == 1 ?
								((headZone.Where(x => x.Presence.Employee.RoleId == 1 &&
									x.Presence.Employee.EmployeeId == val.EmployeeId)
									.Sum(x => x.CleanlinessOfZone + x.CompletenessOfTeam + x.DataOfGarbage + x.DiciplinePresence +
										x.FirstSession + x.SecondSession + x.ThirdSession)) /
											(7 * headZone.Where(x => x.Presence.Employee.EmployeeId == val.EmployeeId).Count())) : 0
					}).GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<ZonePropertyDTO> TransformLivePresenceZone()
		{
			var db = Manager.Database;
			var presence = from val in Get(true).AsEnumerable() select val;
			var totalEmployee = from employee in db.Employees select employee;

			return (from z in db.Zones.AsEnumerable()
					select new ZonePropertyDTO()
					{
						ZoneId = z.ZoneId,
						CodeZone = z.ZoneName,
						TotalEmployee = totalEmployee.Where(x => x.ZoneId == z.ZoneId).Count(),
						RegionName = z.Region.RegionName,
						Value = (from val in db.Presences.AsEnumerable()
								where val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString()) &&
								val.Employee.ZoneId == z.ZoneId
								select new ZoneValueDTO()
								{
									PresenceTotal = presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId && 
										x.PresenceStatus.Equals("1") && 
										x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count(),

									Absence = presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId && 
										x.PresenceStatus.Equals("0") &&
										x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count(),

									Leave = presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId && 
										x.PresenceStatus.Equals("2") &&
										x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count(),

									Percentage = (presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
										x.PresenceStatus.Equals("1") &&
										x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count() * 100) /
									(presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
										x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count()) == 0 ? 1 :
									(presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
										x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count())
								}).FirstOrDefault()
					}).ToList();
		}

		public List<ZonePropertyDTO> TransformLivePresenceZone(string regionParams)
		{
			var db = Manager.Database;
			var presence = from val in Get(true).AsEnumerable() select val;
			var totalEmployee = from employee in db.Employees select employee;

			return (from z in db.Zones.AsEnumerable()
					where z.Region.RegionName.Equals(regionParams)
					select new ZonePropertyDTO()
					{
						ZoneId = z.ZoneId,
						CodeZone = z.ZoneName,
						TotalEmployee = totalEmployee.Where(x => x.ZoneId == z.ZoneId).Count(),
						RegionName = z.Region.RegionName,
						Value = (from val in db.Presences.AsEnumerable()
								 where val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString()) &&
								 val.Employee.ZoneId == z.ZoneId
								 select new ZoneValueDTO()
								 {
									 PresenceTotal = presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
										x.PresenceStatus.Equals("1") &&
										x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count(),

									 Absence = presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
										 x.PresenceStatus.Equals("0") &&
										 x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count(),

									 Leave = presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
										 x.PresenceStatus.Equals("2") &&
										 x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count(),
									 Percentage =
										(presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
											x.PresenceStatus.Equals("1") &&
											x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count() * 100) /
										(presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
											x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count()) == 0 ? 1 :
										(presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
											x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count())
								 }).FirstOrDefault()
					}).ToList();
		}

		public List<ZonePerformLiveDTO> TransformPerformLive()
		{
			var db = Manager.Database;
			var sweeper = db.Sweepers.AsEnumerable();
			var garbage = db.Garbages.AsEnumerable();
			var drainage = db.Drainages.AsEnumerable();
			var headZone = db.HeadOfZones.AsEnumerable();

			return (from val in db.Zones.AsEnumerable()
					select new ZonePerformLiveDTO()
					{
						CodeZone = val.ZoneName,
						RegionName = val.Region.RegionName,
						ZoneId = val.ZoneId,
						Percentage =
						(((sweeper.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count()) == 0 ? 0 :
						(sweeper.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString()))
								.Sum(x => x.Completeness + x.Dicipline + x.Road + x.RoadMedian + x.Sidewalk + x.WaterRope)) /
						(sweeper.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count() * 6)) + 

						(drainage.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count() == 0 ? 0 :
						(drainage.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString()))
								.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed)) /
						(drainage.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count() * 5)) +

						(garbage.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count() == 0 ? 0 :
						(garbage.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString()))
								.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS)) /
						(garbage.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count() * 4)) +

						(headZone.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count() == 0 ? 0 :
						(headZone.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString()))
								.Sum(x => x.CleanlinessOfZone + x.CompletenessOfTeam + x.DataOfGarbage + x.DiciplinePresence +
									x.FirstSession + x.SecondSession + x.ThirdSession)) /
						(headZone.Where(x => x.Presence.Employee.ZoneId == val.ZoneId &&
							x.Presence.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())).Count() * 7))) / 4
					}).ToList();
		}

		public List<PresenceDTO> TransformLiveSweeper(string zoneParams)
		{
			return (from val in Get(true).AsEnumerable()
					where val.Employee.RoleId == 4 && 
					val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = null,
						PresenceId = val.PresenceId,
						Shift = val.Employee.Shift,
						PresenceStatus = val.PresenceStatus.Equals("0") ? "Alfa" : val.PresenceStatus.Equals("1") ? "Hadir" : "Izin",
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName
					}).ToList();
		}

		public List<PresenceDTO> TransformLiveDrainage(string zoneParams)
		{
			return (from val in Get(true).AsEnumerable()
					where val.Employee.RoleId == 10 &&
					val.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString())
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = null,
						PresenceId = val.PresenceId,
						Shift = val.Employee.Shift,
						PresenceStatus = val.PresenceStatus.Equals("0") ? "Alfa" : val.PresenceStatus.Equals("1") ? "Hadir" : "Izin",
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName
					}).ToList();
		}

		public List<ZoneResumeDTO> TransformZoneTestResume()
		{
			var db = Manager.Database;
			var presence = from val in Get(true) select val;
			var employee = db.Employees;

			return (from z in db.Zones
					join r in db.Regions
					on z.RegionId equals r.RegionId
					join pr in Get(true)
					on z.ZoneId equals pr.Employee.ZoneId
					into value
					from val in value.DefaultIfEmpty()
					select new ZoneResumeDTO()
					{
						ZoneId = z.ZoneId,
						CodeZone = z.ZoneName,
						TotalEmployee = employee
							.Where(x => x.ZoneId == val.Employee.Zone.ZoneId).GroupBy(x => x.EmployeeId).Count(),
						RegionName = r.RegionName,
						PresenceTotal = presence
							.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId && x.PresenceStatus.Equals("1")).Count(),
						Absence = presence
							.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId && x.PresenceStatus.Equals("0")).Count(),
						Leave = presence
							.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId && x.PresenceStatus.Equals("2")).Count(),
						Percentage = 
							presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId).Count() == 0 ? 0 :
							((presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
								x.PresenceStatus.Equals("1")).Count() * 100) /
							presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId).Count())
					}).GroupBy(x => x.ZoneId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<ZoneResumeDTO> TransformZoneTestResumeRegion(string regionParams)
		{
			var db = Manager.Database;
			var presence = from val in Get(true) select val;
			var employee = from val in Get(true) select val.Employee;

			return (from z in db.Zones
					join r in db.Regions
					on z.RegionId equals r.RegionId
					join pr in Get(true)
					on z.ZoneId equals pr.Employee.ZoneId
					into value
					from val in value.DefaultIfEmpty()
					where r.RegionName.Equals(regionParams)
					select new ZoneResumeDTO()
					{
						ZoneId = z.ZoneId,
						CodeZone = z.ZoneName,
						TotalEmployee = employee
							.Where(x => x.ZoneId == val.Employee.Zone.ZoneId).GroupBy(x => x.EmployeeId).Count(),
						RegionName = r.RegionName,
						PresenceTotal = presence
							.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId && x.PresenceStatus.Equals("1")).Count(),
						Absence = presence
							.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId && x.PresenceStatus.Equals("0")).Count(),
						Leave = presence
							.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId && x.PresenceStatus.Equals("2")).Count(),
						Percentage =
							presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId).Count() == 0 ? 0 :
							((presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId &&
								x.PresenceStatus.Equals("1")).Count() * 100) /
							presence.Where(x => x.Employee.ZoneId == val.Employee.Zone.ZoneId).Count())
					}).GroupBy(x => x.ZoneId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<ZonePerformDTO> TransformPerformZone()
		{
			var db = Manager.Database;
			var employee = from val in Get(true) select val.Employee;
			var sweep = from val in db.Sweepers select val;
			var drainage = from val in db.Drainages select val;
			var garbage = from val in db.Garbages select val;
			var headZone = from val in db.HeadOfZones select val;

			return (from z in db.Zones
					join r in db.Regions
					on z.RegionId equals r.RegionId
					join pr in Get(true)
					on z.ZoneId equals pr.Employee.ZoneId
					into value
					from val in value.DefaultIfEmpty()
					select new ZonePerformDTO()
					{
						ZoneId = z.ZoneId,
						CodeZone = z.ZoneName,
						TotalEmployee = employee.Where(x => x.ZoneId == z.ZoneId).GroupBy(x => x.EmployeeId).Count(),
						RegionName = z.Region.RegionName,
						Percentage =
							(((sweep.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() == 0 ? 0 :
								sweep.Where(x => x.Presence.Employee.Zone.ZoneId == z.ZoneId)
								.Sum(x => x.Dicipline + x.Completeness + x.WaterRope + x.Sidewalk + x.Road + x.RoadMedian) /
							(sweep.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() * 6))) +

							((headZone.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() == 0 ? 0 :
							headZone.Where(x => x.Presence.Employee.Zone.ZoneId == z.ZoneId)
								.Sum(x => x.CleanlinessOfZone + x.CompletenessOfTeam + x.DataOfGarbage + x.DiciplinePresence +
									x.FirstSession + x.SecondSession + x.ThirdSession) /
							(headZone.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() * 7))) +

							((drainage.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() == 0 ? 0 :
							drainage.Where(x => x.Presence.Employee.Zone.ZoneId == z.ZoneId)
								.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed) /
							(drainage.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() * 5))) +

							((garbage.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() == 0 ? 0 :
							garbage.Where(x => x.Presence.Employee.Zone.ZoneId == z.ZoneId)
								.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS) /
							(garbage.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() * 4)))) / 4
					}).GroupBy(x => x.ZoneId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<ZonePerformDTO> TransformPerformZone(string regionParams)
		{
			var db = Manager.Database;
			var employee = from val in Get(true) select val.Employee;
			var sweep = from val in db.Sweepers select val;
			var drainage = from val in db.Drainages select val;
			var garbage = from val in db.Garbages select val;
			var headZone = from val in db.HeadOfZones select val;

			return (from z in db.Zones
					join r in db.Regions
					on z.RegionId equals r.RegionId
					join pr in Get(true)
					on z.ZoneId equals pr.Employee.ZoneId
					into value
					from val in value.DefaultIfEmpty()
					where r.RegionName.Equals(regionParams)
					select new ZonePerformDTO()
					{
						ZoneId = z.ZoneId,
						CodeZone = z.ZoneName,
						TotalEmployee = employee.Where(x => x.ZoneId == z.ZoneId).GroupBy(x => x.EmployeeId).Count(),
						RegionName = z.Region.RegionName,
						Percentage =
							(((sweep.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() == 0 ? 0 : 
								sweep.Where(x => x.Presence.Employee.Zone.ZoneId == z.ZoneId)
								.Sum(x => x.Dicipline + x.Completeness + x.WaterRope + x.Sidewalk + x.Road + x.RoadMedian) /
							(sweep.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() * 6))) +

							((headZone.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() == 0 ? 0 : 
							headZone.Where(x => x.Presence.Employee.Zone.ZoneId == z.ZoneId)
								.Sum(x => x.CleanlinessOfZone + x.CompletenessOfTeam + x.DataOfGarbage + x.DiciplinePresence +
									x.FirstSession + x.SecondSession + x.ThirdSession) /
							(headZone.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() * 7))) +

							((drainage.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() == 0 ? 0 :
							drainage.Where(x => x.Presence.Employee.Zone.ZoneId == z.ZoneId)
								.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed) /
							(drainage.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() * 5))) +

							((garbage.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() == 0 ? 0 : 
							garbage.Where(x => x.Presence.Employee.Zone.ZoneId == z.ZoneId)
								.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS) /
							(garbage.Where(x => x.Presence.Employee.ZoneId == z.ZoneId).Count() * 4)))) / 4
					}).GroupBy(x => x.ZoneId).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<RegionPerformDTO> TransformPerformRegion()
		{
			var db = Manager.Database;
			var employee = from val in db.Employees select val;
			var sweep = from val in db.Sweepers select val;
			var drainage = from val in db.Drainages select val;
			var garbage = from val in db.Garbages select val;
			var headZone = from val in db.HeadOfZones select val;

			return (from r in db.Regions
					join z in db.Zones on r.RegionId equals z.RegionId
					join pr in Get(true)
					on r.RegionId equals pr.Employee.RegionId
					into values
					from val in values.DefaultIfEmpty()
					select new RegionPerformDTO()
					{
						RegionId = r.RegionId,
						EmployeeTotal = employee.Where(x => x.Region.RegionId == r.RegionId).Count(),
						ZoneTotal = r.Zones.Where(x => x.RegionId == z.RegionId).Count(),
						RegionName = z.Region.RegionName,
						Percentage =
						((sweep.Where(x => x.Presence.Employee.Zone.Region.RegionId == r.RegionId)
							.Sum(x => x.Dicipline + x.Completeness + x.WaterRope + x.Sidewalk + x.Road + x.RoadMedian) /
						(sweep.Where(x => x.Presence.Employee.Zone.Region.RegionId == r.RegionId).Select(x => x.SweeperId).Count() * 6)) +

						(headZone.Where(x => x.Presence.Employee.Zone.Region.RegionId == r.RegionId)
							.Sum(x => x.CleanlinessOfZone + x.CompletenessOfTeam + x.DataOfGarbage + x.DiciplinePresence + x.FirstSession + 
								x.SecondSession + x.ThirdSession) / 
						(headZone.Where(x => x.Presence.Employee.Zone.Region.RegionId == r.RegionId).Select(x => x.HeadOfZoneId).Count() * 7)) +

						(drainage.Where(x => x.Presence.Employee.Zone.Region.RegionId == z.Region.RegionId)
							.Sum(x => x.Cleanliness + x.Completeness + x.Dicipline + x.Sediment + x.Weed) /
						(drainage.Where(x => x.Presence.Employee.Zone.Region.RegionId == r.RegionId).Select(x => x.DrainageId).Count() * 5)) +

						(garbage.Where(x => x.Presence.Employee.Zone.Region.RegionId == z.Region.RegionId)
							.Sum(x => x.Calculation + x.Dicipline + x.Separation + x.TPS) /
						(garbage.Where(x => x.Presence.Employee.Zone.Region.RegionId == r.RegionId).Select(x => x.GerbageId).Count() * 4))) / 4
					}).GroupBy(x => x.RegionName).Select(x => x.FirstOrDefault()).ToList();
		}

		public List<RegionResumeDTO> TransformRegionResume()
		{
			var db = Manager.Database;
			var employee = db.Employees;
			var presence = from val in Get() select val;
		
			return (from r in db.Regions
					join z in db.Zones on r.RegionId equals z.RegionId
					join pr in Get(true)
					on r.RegionId equals pr.Employee.RegionId
					into values
					from val in values.DefaultIfEmpty()
					select new RegionResumeDTO()
					{
						RegionId = r.RegionId,
						RegionName = r.RegionName,
						EmployeeTotal = employee.Where(x => x.RegionId == r.RegionId).Count(),
						ZoneTotal = r.Zones.Where(x => x.RegionId == z.RegionId).Count(),
						Absence = presence
							.Where(x => x.Employee.RegionId == r.RegionId && x.PresenceStatus.Equals("0")).Count(),
						Leave = presence
							.Where(x => x.Employee.RegionId == r.RegionId && x.PresenceStatus.Equals("2")).Count(),
						PresenceTotal = presence
							.Where(x => x.Employee.RegionId == r.RegionId && x.PresenceStatus.Equals("1")).Count(),
						Percentage =
							presence.Where(x => x.Employee.RegionId == r.RegionId).Count() == 0 ? 0 :
							((presence.Where(x => x.Employee.RegionId == r.RegionId && 
								x.PresenceStatus.Equals("1")).Count() * 100) / 
							presence.Where(x => x.Employee.RegionId == r.RegionId).Count())
					}).GroupBy(x => x.RegionName).Select(x => x.FirstOrDefault()).ToList();
		}

		public PresenceDTO TransformId(long id)
		{
			return (from val in Get(true).AsEnumerable()
					where val.PresenceId == id
					select new PresenceDTO()
					{
						Coordinate = val.Coordinate,
						DateOfPresence = val.DateOfPresence,
						TimeOfPresence = val.DateOfPresence.Value.ToShortTimeString(),
						EmployeeId = val.EmployeeId,
						EmployeeName = val.Employee.Person.PersonName,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LivePhoto = null,
						PresenceId = val.PresenceId,
						PresenceStatus = val.PresenceStatus,
						RegionName = val.Employee.Region.RegionName,
						RoleName = val.Employee.Role.RoleName,
						ZoneName = val.Employee.Zone.ZoneName,
						Shift = val.Employee.Shift
					}).FirstOrDefault();
		}

		public PerformSweeperDTO TransformResumePerformSweeper(long idParams)
		{
			var db = Manager.Database;
			var employee = db.Employees;
			var totalSelf = from val in db.Sweepers
							where val.Presence.Employee.RoleId == 4 &&
							val.Presence.Employee.EmployeeId == idParams
							select val;

			return (from e in employee
					join pr in Get()
					on e.EmployeeId equals pr.EmployeeId
					join sw in db.Sweepers
					on pr.PresenceId equals sw.PresenceId
					into value
					from val in value
					where e.EmployeeId == idParams
					select new PerformSweeperDTO()
					{
						EmployeeId = e.EmployeeId,
						EmployeeName = e.Person.PersonName,
						EmployeeNumber = e.EmployeeNumber,
						RegionName = e.Region.RegionName,
						RoleName = e.Role.RoleName,
						ZoneName = e.Zone.ZoneName,
						Completeness = totalSelf.Sum(x => x.Completeness) / totalSelf.Count(),
						Dicipline = totalSelf.Sum(x => x.Dicipline) / totalSelf.Count(),
						Road = totalSelf.Sum(x => x.Road) / totalSelf.Where(x => x.Presence.EmployeeId == e.EmployeeId).Count(),
						RoadMedian = totalSelf.Sum(x => x.RoadMedian) / totalSelf.Count(),
						Sidewalk = totalSelf.Sum(x => x.Sidewalk) / totalSelf.Count(),
						WaterRope = totalSelf.Sum(x => x.WaterRope) / totalSelf.Count()
					}).FirstOrDefault();
		}

		public PerformDrainageDTO TransformResumePerformDrainage(long idParams)
		{
			var db = Manager.Database;
			var employee = db.Employees;
			var drainage = db.Drainages;

			var totalSelf = from val in drainage
							where val.Presence.Employee.RoleId == 10 &&
							val.Presence.Employee.EmployeeId == idParams
							select val;

			return (from e in db.Employees
					join pr in Get()
					on e.EmployeeId equals pr.EmployeeId
					join d in db.Drainages
					on pr.PresenceId equals d.PresenceId
					into value
					from val in value
					where e.EmployeeId == idParams
					select new PerformDrainageDTO()
					{
						EmployeeId = e.EmployeeId,
						EmployeeName = e.Person.PersonName,
						EmployeeNumber = e.EmployeeNumber,
						RegionName = e.Region.RegionName,
						RoleName = e.Role.RoleName,
						ZoneName = e.Zone.ZoneName,
						Cleanliness = totalSelf.Sum(x => x.Cleanliness) / totalSelf.Count(),
						Completeness = totalSelf.Sum(x => x.Completeness) / totalSelf.Count(),
						Dicipline = totalSelf.Sum(x => x.Dicipline) / totalSelf.Count(),
						Sediment = totalSelf.Sum(x => x.Sediment) / totalSelf.Count(),
						Weed = totalSelf.Sum(x => x.Weed) / totalSelf.Count()
					}).FirstOrDefault();
		}

		public PerformGarbageDTO TransformResumePerformGarbage(long idParams)
		{
			var db = Manager.Database;
			var employee = db.Employees;
			var totalSelf = from val in db.Garbages
							where val.Presence.Employee.RoleId == 5 &&
							val.Presence.Employee.EmployeeId == idParams
							select val;

			return (from e in employee
					join pr in Get()
					on e.EmployeeId equals pr.EmployeeId
					join g in db.Garbages
					on pr.PresenceId equals g.PresenceId
					into value
					from val in value
					where e.EmployeeId == idParams
					select new PerformGarbageDTO()
					{
						EmployeeId = e.EmployeeId,
						EmployeeName = e.Person.PersonName,
						EmployeeNumber = e.EmployeeNumber,
						RegionName = e.Region.RegionName,
						RoleName = e.Role.RoleName,
						ZoneName = e.Zone.ZoneName,
						Calculation = totalSelf.Sum(x => x.Calculation) / totalSelf.Count(),
						Dicipline = totalSelf.Sum(x => x.Dicipline) / totalSelf.Count(),
						Separation = totalSelf.Sum(x => x.Separation) / totalSelf.Count(),
						TPS = totalSelf.Sum(x => x.TPS) / totalSelf.Count(),
						VolumeOfAnorganic = totalSelf.Sum(x => x.VolumeOfAnorganic) / totalSelf.Count(),
						VolumeOfOrganic = totalSelf.Sum(x => x.VolumeOfOrganic) / totalSelf.Count()
					}).GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault()).FirstOrDefault();
		}

		public PerformHeadZoneDTO TransformResumePerformHeadZone(long idParams)
		{
			var db = Manager.Database;
			var employee = db.Employees;
			var totalSelf = from val in db.HeadOfZones
							where val.Presence.Employee.RoleId == 1 &&
							val.Presence.Employee.EmployeeId == idParams
							select val;

			return (from e in employee
					join pr in Get()
					on e.EmployeeId equals pr.EmployeeId
					join g in db.HeadOfZones
					on pr.PresenceId equals g.PresenceId
					into value
					from val in value
					where e.EmployeeId == idParams
					select new PerformHeadZoneDTO()
					{
						EmployeeId = e.EmployeeId,
						EmployeeName = e.Person.PersonName,
						EmployeeNumber = e.EmployeeNumber,
						RegionName = e.Region.RegionName,
						RoleName = e.Role.RoleName,
						ZoneName = e.Zone.ZoneName,
						CleanlinessOfZone = totalSelf.Sum(x => x.CleanlinessOfZone) / totalSelf.Count(),
						CompletenessOfTeam = totalSelf.Sum(x => x.CompletenessOfTeam) / totalSelf.Count(),
						DataOfGarbage = totalSelf.Sum(x => x.DataOfGarbage) / totalSelf.Count(),
						DiciplinePresence = totalSelf.Sum(x => x.DiciplinePresence) / totalSelf.Count(),
						FirstSession = totalSelf.Sum(x => x.FirstSession) / totalSelf.Count(),
						SecondSession = totalSelf.Sum(x => x.SecondSession) / totalSelf.Count(),
						ThirdSession = totalSelf.Sum(x => x.ThirdSession) / totalSelf.Count()
					}).GroupBy(x => x.EmployeeId).Select(x => x.FirstOrDefault()).FirstOrDefault();
		}

		public bool TransformExistPresence(long idParams)
		{
			var exist = Manager.Query.Value.Get()
							.AsEnumerable()
							.FirstOrDefault(x => x.EmployeeId == idParams && 
								x.DateOfPresence.Value.ToShortDateString().Equals(Today.ToShortDateString()));

			return exist != null;
		}
	}
}
