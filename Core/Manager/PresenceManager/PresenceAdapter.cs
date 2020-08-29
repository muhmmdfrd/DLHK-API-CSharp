using Core.Manager.RegionManager;
using Core.Manager.ZoneManager;
using Repository;
using System;

namespace Core.Manager.PresenceManager
{
	public class PresenceAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<PresenceQuery> Query { get; set; }
		public Lazy<PresenceCreator> Creator { get; set; }
		public Lazy<PresenceUpdater> Updater { get; set; }
		public Lazy<PresenceDeleter> Deleter { get; set; }
		public Lazy<RegionAdapter> RegionAdapter { get; set; }
		public Lazy<ZoneAdapter> ZoneAdapter { get; set; }

		public PresenceAdapter() : base()
		{
			Assistance(context);
		}

		public PresenceAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<PresenceQuery>(() => { return new PresenceQuery(this); }, true);
			Creator = new Lazy<PresenceCreator>(() => { return new PresenceCreator(this); }, true);
			Updater = new Lazy<PresenceUpdater>(() => { return new PresenceUpdater(this); }, true);
			Deleter = new Lazy<PresenceDeleter>(() => { return new PresenceDeleter(this); }, true);
			RegionAdapter = new Lazy<RegionAdapter>(() => { return new RegionAdapter(this.Database); }, true);
			ZoneAdapter = new Lazy<ZoneAdapter>(() => { return new ZoneAdapter(this.Database); }, true);
		}
	}
}
