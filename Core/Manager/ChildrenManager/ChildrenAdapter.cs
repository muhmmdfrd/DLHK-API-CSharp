using Repository;
using System;

namespace Core.Manager.ChildrenManager
{
	public class ChildrenAdapter : ManagerBase<Child>
	{
		public Lazy<ChildrenQuery> Query { get; set; }
		public Lazy<ChildrenCreator> Creator { get; set; }
		public Lazy<ChildrenUpdater> Updater { get; set; }
		public Lazy<ChildrenDeleter> Deleter { get; set; }

		public ChildrenAdapter() : base()
		{
			Assistance(context);
		}

		public ChildrenAdapter(Child context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(Child dbContext)
		{
			Query = new Lazy<ChildrenQuery>(() => { return new ChildrenQuery(this); }, true);
			Creator = new Lazy<ChildrenCreator>(() => { return new ChildrenCreator(this); }, true);
			Updater = new Lazy<ChildrenUpdater>(() => { return new ChildrenUpdater(this); }, true);
			Deleter = new Lazy<ChildrenDeleter>(() => { return new ChildrenDeleter(this); }, true);
		}
	}
}
