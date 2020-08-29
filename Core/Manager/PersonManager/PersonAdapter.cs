using Repository;
using System;

namespace Core.Manager.PersonManager
{
	public class PersonAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<PersonQuery> Query { get; set; }
		public Lazy<PersonCreator> Creator { get; set; }
		public Lazy<PersonUpdater> Updater { get; set; }
		public Lazy<PersonDeleter> Deleter { get; set; }
		
		public PersonAdapter() : base()
		{
			Assistance(context);
		}

		public PersonAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<PersonQuery>(() => { return new PersonQuery(this); }, true);
			Creator = new Lazy<PersonCreator>(() => { return new PersonCreator(this); }, true);
			Updater = new Lazy<PersonUpdater>(() => { return new PersonUpdater(this); }, true);
			Deleter = new Lazy<PersonDeleter>(() => { return new PersonDeleter(this); }, true);
		}
	}
}
