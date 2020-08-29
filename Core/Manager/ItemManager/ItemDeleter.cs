using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.ItemManager
{
	public class ItemDeleter : AsistanceBase<ItemAdapter, Item>
	{
		public ItemDeleter(ItemAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.ItemId == id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Items.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
