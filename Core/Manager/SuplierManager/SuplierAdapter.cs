using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Manager.SuplierManager
{
	public class SuplierAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<SuplierQuery> Query { get; set; }
		public Lazy<SuplierCreator> Creator { get; set; }
		public Lazy<SuplierUpdater> Updater { get; set; }
		public Lazy<SuplierDeleter> Deleter { get; set; }

		public SuplierAdapter() : base()
		{
			Assistance(context);
		}

		public SuplierAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<SuplierQuery>(() => { return new SuplierQuery(this); }, true);
			Creator = new Lazy<SuplierCreator>(() => { return new SuplierCreator(this); }, true);
			Updater = new Lazy<SuplierUpdater>(() => { return new SuplierUpdater(this); }, true);
			Deleter = new Lazy<SuplierDeleter>(() => { return new SuplierDeleter(this); }, true);
		}
	}
}
