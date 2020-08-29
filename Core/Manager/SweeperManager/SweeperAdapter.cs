using Repository;
using System;

namespace Core.Manager.SweeperManager
{
	public class SweeperAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<SweeperQuery> Query { get; set; }
		public Lazy<SweeperCreator> Creator { get; set; }
		public Lazy<SweeperUpdater> Updater { get; set; }
		public Lazy<SweeperDeleter> Deleter { get; set; }

		public SweeperAdapter() : base()
		{
			Assistance(context);
		}

		public SweeperAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<SweeperQuery>(() => { return new SweeperQuery(this); }, true);
			Creator = new Lazy<SweeperCreator>(() => { return new SweeperCreator(this); }, true);
			Updater = new Lazy<SweeperUpdater>(() => { return new SweeperUpdater(this); }, true);
			Deleter = new Lazy<SweeperDeleter>(() => { return new SweeperDeleter(this); }, true);
		}
	}
}
