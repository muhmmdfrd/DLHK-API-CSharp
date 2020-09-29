using Repository;
using System;

namespace Core.Manager.UserManager
{
	public class UserAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<UserQuery> Query { get; set; }
		public Lazy<UserCreator> Creator { get; set; }
		public Lazy<UserUpdater> Updater { get; set; }
		public Lazy<UserDeleter> Deleter { get; set; }

		public UserAdapter() : base()
		{
			Assistance(context);
		}

		public UserAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<UserQuery>(() => { return new UserQuery(this); }, true);
			Creator = new Lazy<UserCreator>(() => { return new UserCreator(this); }, true);
			Updater = new Lazy<UserUpdater>(() => { return new UserUpdater(this); }, true);
			Deleter = new Lazy<UserDeleter>(() => { return new UserDeleter(this); }, true);
		}
	}
}
