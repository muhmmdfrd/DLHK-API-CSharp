using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.SuplierManager
{
	public class SuplierUpdater : AsistanceBase<SuplierAdapter, Suplier>
	{
		public SuplierUpdater(SuplierAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Suplier Update(SuplierDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.SuplierId == dto.SuplierId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Note = dto.Note;
				exist.Phone = dto.Phone;
				exist.SuplierName = dto.SuplierName;
				exist.Address = dto.Address;
				exist.Email = dto.Email;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
