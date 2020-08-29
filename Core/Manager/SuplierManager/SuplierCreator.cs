using Repository;
using System.Transactions;

namespace Core.Manager.SuplierManager
{
	public class SuplierCreator : AsistanceBase<SuplierAdapter, Suplier>
	{
		public SuplierCreator(SuplierAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Suplier Save(SuplierDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Suplier
				{
					Address = dto.Address,
					Email = dto.Email,
					Note = dto.Note,
					Phone = dto.Phone,
					SuplierName = dto.SuplierName,
				};

				Manager.Database.Supliers.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
