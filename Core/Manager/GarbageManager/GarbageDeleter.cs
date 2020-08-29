using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.GarbageManager
{
	public class GarbageDeleter : AsistanceBase<GarbageAdapter, Garbage>
	{
		public GarbageDeleter(GarbageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.GerbageId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Garbages.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
