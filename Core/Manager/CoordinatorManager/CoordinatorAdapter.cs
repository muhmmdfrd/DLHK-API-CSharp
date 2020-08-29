using Repository;
using System;

namespace Core.Manager.CoordinatorManager
{
	public class CoordinatorAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<CoordinatorQuery> Query { get; set; }
		public Lazy<CoordinatorCreator> Creator { get; set; }
		public Lazy<CoordinatorUpdater> Updater { get; set; }
		public Lazy<CoordinatorDeleter> Deleter { get; set; }

		public CoordinatorAdapter() : base()
		{
			Assistance(context);
		}

		public CoordinatorAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<CoordinatorQuery>(() => { return new CoordinatorQuery(this); }, true);
			Creator = new Lazy<CoordinatorCreator>(() => { return new CoordinatorCreator(this); }, true);
			Updater = new Lazy<CoordinatorUpdater>(() => { return new CoordinatorUpdater(this); }, true);
			Deleter = new Lazy<CoordinatorDeleter>(() => { return new CoordinatorDeleter(this); }, true);
		}
	}
}
