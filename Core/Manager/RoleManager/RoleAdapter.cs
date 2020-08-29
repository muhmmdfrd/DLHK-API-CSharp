using Repository;
using System;

namespace Core.Manager.RoleManager
{
	public class RoleAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<RoleQuery> Query { get; set; }
		public Lazy<RoleCreator> Creator { get; set; }
		public Lazy<RoleUpdater> Updater { get; set; }
		public Lazy<RoleDeleter> Deleter { get; set; }

		public RoleAdapter() : base()
		{
			Assistance(context);
		}

		public RoleAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<RoleQuery>(() => { return new RoleQuery(this); }, true);
			Creator = new Lazy<RoleCreator>(() => { return new RoleCreator(this); }, true);
			Updater = new Lazy<RoleUpdater>(() => { return new RoleUpdater(this); }, true);
			Deleter = new Lazy<RoleDeleter>(() => { return new RoleDeleter(this); }, true);
		}
	}
}
