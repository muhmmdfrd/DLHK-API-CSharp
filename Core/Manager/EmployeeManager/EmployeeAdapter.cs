using Core.Manager.PersonManager;
using Repository;
using System;

namespace Core.Manager.EmployeeManager
{
	public class EmployeeAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<EmployeeQuery> Query { get; set; }
		public Lazy<EmployeeCreator> Creator { get; set; }
		public Lazy<EmployeeUpdater> Updater { get; set; }
		public Lazy<EmployeeDeleter> Deleter { get; set; }
		public Lazy<PersonAdapter> PersonManager { get; set; }

		public EmployeeAdapter() : base()
		{
			Assistance(context);
		}

		public EmployeeAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<EmployeeQuery>(() => { return new EmployeeQuery(this); }, true);
			Creator = new Lazy<EmployeeCreator>(() => { return new EmployeeCreator(this); }, true);
			Updater = new Lazy<EmployeeUpdater>(() => { return new EmployeeUpdater(this); }, true);
			Deleter = new Lazy<EmployeeDeleter>(() => { return new EmployeeDeleter(this); }, true);
			PersonManager = new Lazy<PersonAdapter>(() => { return new PersonAdapter(this.Database); }, true);
		}
	}
}
