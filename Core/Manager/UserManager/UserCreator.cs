using Repository;
using System.Transactions;

namespace Core.Manager.UserManager
{
	public class UserCreator : AsistanceBase<UserAdapter, User>
	{
		public UserCreator(UserAdapter manager) : base(manager)
		{
			// do nothing
		}

		public User Save(UserDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new User
				{
					EmployeeId = dto.EmployeeId,
					Password = dto.Password,
					Username = dto.Username
				};

				Manager.Database.Users.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
