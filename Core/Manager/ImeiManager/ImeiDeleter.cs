using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.ImeiManager
{
	public class ImeiDeleter : AsistanceBase<ImeiAdapter, IMEI>
	{
		public ImeiDeleter(ImeiAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.ImeiId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.IMEIs.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
