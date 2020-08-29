using Repository;
using System;

namespace Core.Manager.DrainageManager
{
	public class DrainageAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<DrainageQuery> Query { get; set; }
		public Lazy<DrainageCreator> Creator { get; set; }
		public Lazy<DrainageUpdater> Updater { get; set; }
		public Lazy<DrainageDeleter> Deleter { get; set; }

		public DrainageAdapter() : base()
		{
			Assistance(context);
		}

		public DrainageAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<DrainageQuery>(() => { return new DrainageQuery(this); }, true);
			Creator = new Lazy<DrainageCreator>(() => { return new DrainageCreator(this); }, true);
			Updater = new Lazy<DrainageUpdater>(() => { return new DrainageUpdater(this); }, true);
			Deleter = new Lazy<DrainageDeleter>(() => { return new DrainageDeleter(this); }, true);
		}
	}
}
