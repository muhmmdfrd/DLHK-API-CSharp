using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.TransacManager
{
	public class TransacCreator : AsistanceBase<TransacAdapter, Transac>
	{
		public TransacCreator(TransacAdapter manager) : base(manager)
		{
			// do nothing
		}

		private readonly DateTime today = DateTime.Now;

		public Transac DataIn(TransacInDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Transac
				{
					DateTransac = today,
					Qty = dto.Qty,
					TypeOfTransac = "IN",
					UserRecorder = dto.UserRecorder,
					ItemCode = dto.ItemCode,
					ItemName = dto.ItemName,
					Note = dto.Note,
					UserRequest = dto.UserRequest,
					SuplierName = dto.SuplierName
				};

				Manager.Database.Transacs.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}

		public Transac DataOut(TransacOutDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Transac
				{
					DateTransac = today,
					Qty = dto.Qty,
					TypeOfTransac = "OUT",
					UserRecorder = dto.UserRecorder,
					ItemCode = dto.ItemCode,
					ItemName = dto.ItemName,
					Note = dto.Note,
					UserRequest = dto.UserRequest
				};

				var dataItem = Manager.ItemManager.Value.Query.Value.Get()
					.FirstOrDefault(x => x.ItemName.Equals(newEntity.ItemName));

				if (dataItem != null)
				{
					if (dataItem.ItemQty > 0 && dataItem.ItemQty > newEntity.Qty && newEntity.Qty > 0)
					{
						dataItem.ItemQty -= newEntity.Qty;
					}
					else
					{
						throw new Exception("qty not enough");
					}
				}
				else
				{
					throw new Exception("data not found");
				}

				Manager.Database.Transacs.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
