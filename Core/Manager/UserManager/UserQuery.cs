using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.UserManager
{
	public class UserQuery : AsistanceBase<UserAdapter, User>
	{
		public UserQuery(UserAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<UserDTO> GetQuery()
		{
			return Manager.Database.Users.Select(val => new UserDTO()
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
			});
		}

		public List<UserDTO> Tranform()
		{
			return GetQuery().ToList();
		}

		public UserDTO TransformId(long id)
		{
			return GetQuery().FirstOrDefault(x => x.UserId == id);
		}

		public UserDTO TransformUsername(string username)
		{
			return GetQuery().FirstOrDefault(x => x.Username.Equals(username));
		}
		
	}
}
