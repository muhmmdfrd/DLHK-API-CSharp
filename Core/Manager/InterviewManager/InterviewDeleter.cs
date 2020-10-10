using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.InterviewManager
{
	public class InterviewDeleter : AsistanceBase<InterviewAdapter, Interview>
	{
		public InterviewDeleter(InterviewAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var data = Manager.Query.Value.Get().FirstOrDefault(x => x.InterviewId == id);

				if (data == null)
					throw new Exception("data not found");

				Manager.Database.Interviews.Remove(data);

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
