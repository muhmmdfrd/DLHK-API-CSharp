using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.GarbageManager
{
	public class GarbageUpdater : AsistanceBase<GarbageAdapter, Garbage>
	{
		public GarbageUpdater(GarbageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Garbage Update(GarbageDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.GerbageId == dto.GerbageId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Calculation = dto.Calculation;
				exist.Dicipline = dto.Dicipline;
				exist.PresenceId = dto.PresenceId;
				exist.Separation = dto.Separation;
				exist.TPS = dto.TPS;
				exist.VolumeOfAnorganic = dto.VolumeOfAnorganic;
				exist.VolumeOfOrganic = dto.VolumeOfOrganic;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
