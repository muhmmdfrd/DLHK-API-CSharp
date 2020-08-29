using Repository;
using System;

namespace Core.Manager.UserManager
{
	public class UserAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<UserQuery> Query { get; set; }

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
		}
	}
}
