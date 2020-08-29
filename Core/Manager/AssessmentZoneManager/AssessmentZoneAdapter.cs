using Repository;
using System;

namespace Core.Manager.AssessmentZoneManager
{
	public class AssessmentZoneAdapter : ManagerBase<DLHKEntities>
	{
		public Lazy<AssessmentZoneQuery> Query { get; set; }
		public Lazy<AssessmentZoneCreator> Creator { get; set; }

		public AssessmentZoneAdapter() : base()
		{
			Assistance(context);
		}

		public AssessmentZoneAdapter(DLHKEntities context) : base(context)
		{
			Assistance(context);
		}

		protected override void Assistance(DLHKEntities dbContext)
		{
			Query = new Lazy<AssessmentZoneQuery>(() => { return new AssessmentZoneQuery(this); }, true);
			Creator = new Lazy<AssessmentZoneCreator>(() => { return new AssessmentZoneCreator(this); }, true);
		}
	}
}
