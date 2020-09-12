using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.RoleManager
{
	public class RoleQuery : AsistanceBase<RoleAdapter, Role>
	{
		public RoleQuery(RoleAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Role> Get()
		{
			return Manager.Database.Roles;
		}

		public List<RoleDTO> Transform()
		{
			return (from val in Get()
					select new RoleDTO()
					{
						RoleId = val.RoleId,
						RoleName = val.RoleName,
						Status = val.Status
					}).ToList();

		}

		public List<RoleDTO> TransformApplicant()
		{
			return (from val in Get()
					where val.Status.Equals("true") &&
					val.RoleId != 6
					select new RoleDTO()
					{
						RoleId = val.RoleId,
						RoleName = val.RoleName,
						Status = val.Status
					}).ToList();

		}

		public RoleDTO TransformId(long id)
		{
			return (from val in Get()
					where val.RoleId == id
					select new RoleDTO()
					{
						RoleId = val.RoleId,
						RoleName = val.RoleName,
						Status = val.Status
					}).FirstOrDefault();
		}
	}
}
