using Core.Manager.TransacManager;
using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.ItemManager
{
	public class ItemCreator : AsistanceBase<ItemAdapter, Item>
	{
		public ItemCreator(ItemAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Save(ItemDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var dataItem = Manager.Query.Value.Get()
					.FirstOrDefault(x => x.ItemName.Equals(dto.ItemName));

				var code = null as string;
				var itemCode = null as string;

				if (dataItem != null)
				{
					dataItem.ItemQty += dto.ItemQty;
				}
				else
				{
					var res = Manager.Database.Categories
					   .AsNoTracking()
					   .FirstOrDefault(x => x.CategoryId == dto.CategoryId);

					code = res.CategoryCode;

					itemCode = $"{code}-{(Guid.NewGuid().ToString().ToUpper()).Substring(0, 4)}";

					var newEntity = new Item
					{
						ItemName = dto.ItemName,
						ItemPhoto = dto.ItemPhoto,
						ItemQty = dto.ItemQty,
						Note = dto.Note,
						ItemCode = itemCode,
						CategoryId = dto.CategoryId,
					};

					Manager.Database.Items.Add(newEntity);
				}

				Manager.Database.SaveChanges();

				var transacIn = new TransacInDTO()
				{
					ItemCode = itemCode,
					ItemName = dto.ItemName,
					DateTransac = DateTime.Today,
					Note = dto.Note,
					Qty = dto.ItemQty,
					TypeOfTransac = "IN",
					UserRecorder = "Admin",
					UserRequest = "User Request",
					SuplierName = dto.SuplierName
				};

				Manager.TransacManager.Value.Creator.Value.DataIn(transacIn);

				transac.Complete();
			}
		}
	}
}
