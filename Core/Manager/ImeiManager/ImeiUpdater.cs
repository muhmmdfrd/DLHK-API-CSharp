using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.ImeiManager
{
	public class ImeiUpdater : AsistanceBase<ImeiAdapter, IMEI>
	{
		public ImeiUpdater(ImeiAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IMEI Update(ImeiDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.ImeiId == dto.ImeiId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Imei1 = dto.ImeiName;
				exist.Device = dto.Device;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
