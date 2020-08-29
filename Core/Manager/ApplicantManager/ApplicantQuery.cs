using Repository;
using System.Linq;

namespace Core.Manager.ApplicantManager
{
	public class ApplicantQuery : AsistanceBase<ApplicantAdapter, Applicant>
	{
		public ApplicantQuery(ApplicantAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Applicant> Get()
		{
			return Manager.Database.Applicants;
		}
	}
}
