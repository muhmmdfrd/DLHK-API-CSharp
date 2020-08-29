using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.RoleManager
{
	public class RoleDeleter : AsistanceBase<RoleAdapter, Role>
	{
		public RoleDeleter(RoleAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.RoleId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Roles.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
