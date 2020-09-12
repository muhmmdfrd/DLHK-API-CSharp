using Repository;
using System.Transactions;

namespace Core.Manager.RoleManager
{
	public class RoleCreator : AsistanceBase<RoleAdapter, Role>
	{
		public RoleCreator(RoleAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Role Save(RoleDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Role
				{
					RoleName = dto.RoleName,
					Status = "true"
				};

				Manager.Database.Roles.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
