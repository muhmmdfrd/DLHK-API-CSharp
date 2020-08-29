using Core.Manager.PersonManager;
using Repository;
using System;

namespace Core.Manager.InterviewManager
{
	public class InterviewAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<InterviewQuery> Query { get; set; }
		public Lazy<InterviewCreator> Creator { get; set; }
		public Lazy<PersonAdapter> PersonManager { get; set; }

		public InterviewAdapter() : base()
		{
			Assistance(context);
		}

		public InterviewAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<InterviewQuery>(() => { return new InterviewQuery(this); }, true);
			Creator = new Lazy<InterviewCreator>(() => { return new InterviewCreator(this); }, true);
			PersonManager = new Lazy<PersonAdapter>(() => { return new PersonAdapter(this.Database); }, true);
		}
	}
}
