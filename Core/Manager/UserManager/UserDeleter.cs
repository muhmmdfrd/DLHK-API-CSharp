using Repository;
using System;
using System.Transactions;

namespace Core.Manager.UserManager
{
	public class UserDeleter : AsistanceBase<UserAdapter, User>
	{
		public UserDeleter(UserAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Database.Users.Find(id);

				if (exist == null)
					throw new Exception("data doesn't exist");

				Manager.Database.Users.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
