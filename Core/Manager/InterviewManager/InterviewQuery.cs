using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.InterviewManager
{
	public class InterviewQuery : AsistanceBase<InterviewAdapter, Interview>
	{
		public InterviewQuery(InterviewAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Interview> Get()
		{
			return Manager.Database.Interviews;
		}

		public List<InterviewDTO> Transform()
		{
			return (from val in Get()
					select new InterviewDTO()
					{
						DateOfInterview = val.DateOfInterview,
						Email = val.Email,
						Interviewer = val.Interviewer,
						InterviewId = val.InterviewId,
						Phone = val.Phone,
						Place = val.Place,
					}).ToList();
		}

		public InterviewDTO TransformId(long id)
		{
			return (from val in Get()
					where val.InterviewId == id
					select new InterviewDTO()
					{
						DateOfInterview = val.DateOfInterview,
						Email = val.Email,
						Interviewer = val.Interviewer,
						InterviewId = val.InterviewId,
						Phone = val.Phone,
						Place = val.Place,
					}).FirstOrDefault();
		}

	}
}
