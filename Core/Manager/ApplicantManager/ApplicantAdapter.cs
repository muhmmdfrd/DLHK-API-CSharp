using Core.Manager.PersonManager;
using Repository;
using System;

namespace Core.Manager.ApplicantManager
{
	public class ApplicantAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<ApplicantQuery> Query { get; set; }
		public Lazy<ApplicantCreator> Creator { get; set; }
		public Lazy<PersonAdapter> PersonManager { get; set; }

		public ApplicantAdapter() : base()
		{
			Assistance(context);
		}

		public ApplicantAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<ApplicantQuery>(() => { return new ApplicantQuery(this); }, true);
			Creator = new Lazy<ApplicantCreator>(() => { return new ApplicantCreator(this); }, true);
			PersonManager = new Lazy<PersonAdapter>(() => { return new PersonAdapter(Database); }, true);
		}
	}
}
