using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.SuplierManager
{
	public class SuplierDeleter : AsistanceBase<SuplierAdapter, Suplier>
	{
		public SuplierDeleter(SuplierAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.SuplierId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Supliers.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
