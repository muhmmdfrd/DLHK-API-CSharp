using Repository;
using System;

namespace Core.Manager.GarbageManager
{
	public class GarbageAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<GarbageQuery> Query { get; set; }
		public Lazy<GarbageCreator> Creator { get; set; }
		public Lazy<GarbageUpdater> Updater { get; set; }
		public Lazy<GarbageDeleter> Deleter { get; set; }

		public GarbageAdapter() : base()
		{
			Assistance(context);
		}

		public GarbageAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<GarbageQuery>(() => { return new GarbageQuery(this); }, true);
			Creator = new Lazy<GarbageCreator>(() => { return new GarbageCreator(this); }, true);
			Updater = new Lazy<GarbageUpdater>(() => { return new GarbageUpdater(this); }, true);
			Deleter = new Lazy<GarbageDeleter>(() => { return new GarbageDeleter(this); }, true);
		}
	}
}
