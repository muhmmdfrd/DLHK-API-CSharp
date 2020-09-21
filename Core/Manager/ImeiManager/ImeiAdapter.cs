using Repository;
using System;

namespace Core.Manager.ImeiManager
{
	public class ImeiAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<ImeiQuery> Query { get; set; }
		public Lazy<ImeiCreator> Creator { get; set; }
		public Lazy<ImeiUpdater> Updater { get; set; }
		public Lazy<ImeiDeleter> Deleter { get; set; }

		public ImeiAdapter() : base()
		{
			Assistance(context);
		}

		public ImeiAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<ImeiQuery>(() => { return new ImeiQuery(this); }, true);
			Creator = new Lazy<ImeiCreator>(() => { return new ImeiCreator(this); }, true);
			Updater = new Lazy<ImeiUpdater>(() => { return new ImeiUpdater(this); }, true);
			Deleter = new Lazy<ImeiDeleter>(() => { return new ImeiDeleter(this); }, true);
		}
	}
}
