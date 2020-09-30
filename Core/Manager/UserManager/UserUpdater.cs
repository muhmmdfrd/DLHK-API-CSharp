using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.UserManager
{
	public class UserUpdater : AsistanceBase<UserAdapter, User>
	{
		public UserUpdater(UserAdapter manager) : base(manager)
		{
			// do nothing
		}

		public User Update(UserDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.UserId == dto.UserId);

				if (exist == null)
					throw new Exception("data doesn't exist");

				exist.Username = dto.Username;
				exist.Password = string.IsNullOrEmpty(dto.Password) ? exist.Password : dto.Password;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
