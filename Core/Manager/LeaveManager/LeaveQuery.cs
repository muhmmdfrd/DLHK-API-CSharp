using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.LeaveManager
{
	public class LeaveQuery : AsistanceBase<LeaveAdapter, Leave>
	{
		public LeaveQuery(LeaveAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Leave> Get(bool withDetail = false)
		{
			return withDetail ?
				Manager.Database.Leaves.AsQueryable().Include(x => x.Employee) : Manager.Database.Leaves;
		}

		public List<LeaveDTO> Transform()
		{
			return (from val in Get(true)
					select new LeaveDTO()
					{
						DateOfLeave = val.DateOfLeave,
						Description = val.Description,
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LeaveId = val.LeaveId,
						LeaveStatus = val.LeaveStatus,
						PersonName = val.Employee.Person.PersonName,
						Phone = val.Employee.Person.Phone,
						Location = val.Location
					}).ToList();
		}

		public List<LeaveDTO> TransformStatus(string regionParams)
		{
			return (from val in Get(true)
					where val.LeaveStatus == ("Tertunda") && 
					val.DateOfLeave == DateTime.Today &&
					val.Employee.Region.RegionName.Equals(regionParams)
					select new LeaveDTO()
					{
						DateOfLeave = val.DateOfLeave,
						Description = val.Description,
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LeaveId = val.LeaveId,
						LeaveStatus = val.LeaveStatus,
						Location = val.Location,
						PersonName = val.Employee.Person.PersonName,
						Phone = val.Employee.Person.Phone
					}).ToList();
		}

		public LeaveDTO TransformId(long id)
		{
			return (from val in Get(true)
					where val.LeaveId == id
					select new LeaveDTO()
					{
						DateOfLeave = val.DateOfLeave,
						Description = val.Description,
						EmployeeId = val.EmployeeId,
						EmployeeNumber = val.Employee.EmployeeNumber,
						LeaveId = val.LeaveId,
						LeaveStatus = val.LeaveStatus,
						Location = val.Location,
						PersonName = val.Employee.Person.PersonName,
						Phone = val.Employee.Person.Phone
					}).FirstOrDefault();
		}
	}
}
