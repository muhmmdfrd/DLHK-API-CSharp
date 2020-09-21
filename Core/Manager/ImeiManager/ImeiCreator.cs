using Repository;
using System.Transactions;

namespace Core.Manager.ImeiManager
{
	public class ImeiCreator : AsistanceBase<ImeiAdapter, IMEI>
	{
		public ImeiCreator(ImeiAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IMEI Save(ImeiDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new IMEI
				{
					Imei1 = dto.ImeiName,
					Device = dto.Device
				};

				Manager.Database.IMEIs.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
