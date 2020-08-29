using Repository;
using System.Transactions;

namespace Core.Manager.RegionManager
{
	public class RegionCreator : AsistanceBase<RegionAdapter, Region>
	{
		public RegionCreator(RegionAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Region Save(RegionDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Region
				{
					RegionName = dto.RegionName
				};

				Manager.Database.Regions.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
