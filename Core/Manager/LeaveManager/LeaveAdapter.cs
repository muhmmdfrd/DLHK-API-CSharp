using Core.Manager.PresenceManager;
using Repository;
using System;

namespace Core.Manager.LeaveManager
{
	public class LeaveAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<LeaveQuery> Query { get; set; }
		public Lazy<LeaveCreator> Creator { get; set; }
		public Lazy<LeaveUpdater> Updater { get; set; }
		public Lazy<LeaveDeleter> Deleter { get; set; }
		public Lazy<PresenceAdapter> PresenceManager { get; set; }

		public LeaveAdapter() : base()
		{
			Assistance(context);
		}

		public LeaveAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<LeaveQuery>(() => { return new LeaveQuery(this); }, true);
			Creator = new Lazy<LeaveCreator>(() => { return new LeaveCreator(this); }, true);
			Updater = new Lazy<LeaveUpdater>(() => { return new LeaveUpdater(this); }, true);
			Deleter = new Lazy<LeaveDeleter>(() => { return new LeaveDeleter(this); }, true);
			PresenceManager = new Lazy<PresenceAdapter>(() => { return new PresenceAdapter(this.Database); }, true);
		}
	}
}
