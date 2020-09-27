using Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.UserManager
{
	public class UserQuery : AsistanceBase<UserAdapter, User>
	{
		public UserQuery(UserAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<User> Get(bool withDetail = false)
		{
			var dataContext = Manager.Database.Users;

			return withDetail ? dataContext.AsQueryable().Include(x => x.Employee) : dataContext;
		}

		public List<UserDTO> Tranform()
		{
			return (from val in Get(true)
					select new UserDTO()
					{
						UserId = val.UserId,
						EmployeeId = val.EmployeeId,
						Username = val.Username,
						Password = val.Password,
						PersonName = val.Employee.Person.PersonName,
						Photo = val.Employee.Person.Photo,
						RegionName = val.Employee.Region.RegionName,
						ZoneName = val.Employee.Zone.ZoneName,
						RoleName = val.Employee.Role.RoleName,
						Shift = val.Employee.Shift
					}).ToList();
		}

		public UserDTO TransformUsername(string username)
		{
			return (from val in Get(true)
					where val.Username.Equals(username)
					select new UserDTO()
					{
						UserId = val.UserId,
						EmployeeId = val.EmployeeId,
						Username = val.Username,
						Password = val.Password,
						PersonName = val.Employee.Person.PersonName,
						Photo = val.Employee.Person.Photo,
						RegionName = val.Employee.Region.RegionName,
						ZoneName = val.Employee.Zone.ZoneName,
						RoleName = val.Employee.Role.RoleName,
						Shift = val.Employee.Shift
					}).FirstOrDefault();
		}
		
	}
}
