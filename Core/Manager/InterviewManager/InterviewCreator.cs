using Repository;
using System.Transactions;

namespace Core.Manager.InterviewManager
{
	public class InterviewCreator : AsistanceBase<InterviewAdapter, Interview>
	{
		public InterviewCreator(InterviewAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Interview Save(InterviewDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Interview
				{
					DateOfInterview = dto.DateOfInterview,
					Email = dto.Email,
					Interviewer = dto.Interviewer,
					Phone = dto.Phone,
					Place = dto.Place
				};

				Manager.Database.Interviews.Add(newEntity);
				Manager.Database.SaveChanges();

				Manager.PersonManager.Value.Updater.Value.InterviewPerson(dto.Interviewer);

				transac.Complete();

				return newEntity;
			}
		}
	}
}
