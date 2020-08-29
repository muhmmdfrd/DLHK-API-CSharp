using Repository;
using System;

namespace Core.Manager.RegionManager
{
	public class RegionAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<RegionQuery> Query { get; set; }
		public Lazy<RegionCreator> Creator { get; set; }
		public Lazy<RegionUpdater> Updater { get; set; }
		public Lazy<RegionDeleter> Deleter { get; set; }

		public RegionAdapter() : base()
		{
			Assistance(context);
		}

		public RegionAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<RegionQuery>(() => { return new RegionQuery(this); }, true);
			Creator = new Lazy<RegionCreator>(() => { return new RegionCreator(this); }, true);
			Updater = new Lazy<RegionUpdater>(() => { return new RegionUpdater(this); }, true);
			Deleter = new Lazy<RegionDeleter>(() => { return new RegionDeleter(this); }, true);
		}
	}
}
