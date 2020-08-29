using Repository;
using System;

namespace Core.Manager.HeadZoneManager
{
	public class HeadZoneAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<HeadZoneQuery> Query { get; set; }
		public Lazy<HeadZoneCreator> Creator { get; set; }
		public Lazy<HeadZoneUpdater> Updater { get; set; }
		public Lazy<HeadZoneDeleter> Deleter { get; set; }

		public HeadZoneAdapter() : base()
		{
			Assistance(context);
		}

		public HeadZoneAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<HeadZoneQuery>(() => { return new HeadZoneQuery(this); }, true);
			Creator = new Lazy<HeadZoneCreator>(() => { return new HeadZoneCreator(this); }, true);
			Updater = new Lazy<HeadZoneUpdater>(() => { return new HeadZoneUpdater(this); }, true);
			Deleter = new Lazy<HeadZoneDeleter>(() => { return new HeadZoneDeleter(this); }, true);
		}
	}
}
