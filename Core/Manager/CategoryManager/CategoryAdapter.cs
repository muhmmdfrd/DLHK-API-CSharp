using Repository;
using System;

namespace Core.Manager.CategoryManager
{
	public class CategoryAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<CategoryQuery> Query { get; set; }
		public Lazy<CategoryCreator> Creator { get; set; }
		public Lazy<CategoryUpdater> Updater { get; set; }
		public Lazy<CategoryDeleter> Deleter { get; set; }

		public CategoryAdapter() : base()
		{
			Assistance(context);
		}

		public CategoryAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<CategoryQuery>(() => { return new CategoryQuery(this); }, true);
			Creator = new Lazy<CategoryCreator>(() => { return new CategoryCreator(this); }, true);
			Updater = new Lazy<CategoryUpdater>(() => { return new CategoryUpdater(this); }, true);
			Deleter = new Lazy<CategoryDeleter>(() => { return new CategoryDeleter(this); }, true);
		}
	}
}
