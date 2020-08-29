using Repository;
using System;

namespace Core.Manager.ZoneManager
{
	public class ZoneAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<ZoneQuery> Query { get; set; }
		public Lazy<ZoneCreator> Creator { get; set; }
		public Lazy<ZoneUpdater> Updater { get; set; }
		public Lazy<ZoneDeleter> Deleter { get; set; }

		public ZoneAdapter() : base()
		{
			Assistance(context);
		}

		public ZoneAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<ZoneQuery>(() => { return new ZoneQuery(this); }, true);
			Creator = new Lazy<ZoneCreator>(() => { return new ZoneCreator(this); }, true);
			Updater = new Lazy<ZoneUpdater>(() => { return new ZoneUpdater(this); }, true);
			Deleter = new Lazy<ZoneDeleter>(() => { return new ZoneDeleter(this); }, true);
		}
	}
}
