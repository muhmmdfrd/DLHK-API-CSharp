using Core.Manager.ItemManager;
using Repository;
using System;

namespace Core.Manager.TransacManager
{
	public class TransacAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<TransacQuery> Query { get; set; }
		public Lazy<TransacCreator> Creator { get; set; }
		public Lazy<ItemAdapter> ItemManager { get; set; }

		public TransacAdapter() : base()
		{
			Assistance(context);
		}

		public TransacAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<TransacQuery>(() => { return new TransacQuery(this); }, true);
			Creator = new Lazy<TransacCreator>(() => { return new TransacCreator(this); }, true);
			ItemManager = new Lazy<ItemAdapter>(() => { return new ItemAdapter(this.Database); }, true);
		}
	}
}
