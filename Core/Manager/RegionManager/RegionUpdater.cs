using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.RegionManager
{
	public class RegionUpdater : AsistanceBase<RegionAdapter, Region>
	{
		public RegionUpdater(RegionAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Region Update(RegionDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.RegionId == dto.RegionId);

				if (exist == null)
					throw new Exception("data not found");

				exist.RegionName = dto.RegionName;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
