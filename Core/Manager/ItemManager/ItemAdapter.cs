using Core.Manager.TransacManager;
using Repository;
using System;

namespace Core.Manager.ItemManager
{
	public class ItemAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<ItemQuery> Query { get; set; }
		public Lazy<ItemCreator> Creator { get; set; }
		public Lazy<ItemUpdater> Updater { get; set; }
		public Lazy<ItemDeleter> Deleter { get; set; }
		public Lazy<TransacAdapter> TransacManager { get; set; }

		public ItemAdapter() : base()
		{
			Assistance(context);
		}

		public ItemAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<ItemQuery>(() => { return new ItemQuery(this); }, true);
			Creator = new Lazy<ItemCreator>(() => { return new ItemCreator(this); }, true);
			Updater = new Lazy<ItemUpdater>(() => { return new ItemUpdater(this); }, true);
			Deleter = new Lazy<ItemDeleter>(() => { return new ItemDeleter(this); }, true);
			TransacManager = new Lazy<TransacAdapter>(() => { return new TransacAdapter(this.Database); }, true);
		}
	}
}
