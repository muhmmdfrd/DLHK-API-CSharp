using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.RoleManager
{
	public class RoleUpdater : AsistanceBase<RoleAdapter, Role>
	{
		public RoleUpdater(RoleAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Role Update(RoleDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.RoleId == dto.RoleId);

				if (exist == null)
					throw new Exception("data not found");

				exist.RoleName = dto.RoleName;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
