using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.ItemManager
{
	public class ItemUpdater : AsistanceBase<ItemAdapter, Item>
	{
		public ItemUpdater(ItemAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Item Update(ItemDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.ItemId == dto.ItemId);

				if (exist == null)
					throw new Exception("data not found");

				exist.ItemName = dto.ItemName;
				exist.ItemPhoto = dto.ItemPhoto;
				exist.ItemQty = dto.ItemQty;
				exist.Note = dto.Note;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}

		public Item UpdateQty(ItemDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.ItemId == dto.ItemId);

				if (exist == null)
					throw new Exception("data not found");

				exist.ItemQty += dto.ItemQty;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
