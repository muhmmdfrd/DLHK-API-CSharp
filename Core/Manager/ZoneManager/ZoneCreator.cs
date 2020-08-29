using Repository;
using System.Transactions;

namespace Core.Manager.ZoneManager
{
	public class ZoneCreator : AsistanceBase<ZoneAdapter, Zone>
	{
		public ZoneCreator(ZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Zone Save(ZoneDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Zone
				{
					ZoneName = dto.ZoneName,
					RegionId = dto.RegionId
				};

				Manager.Database.Zones.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
