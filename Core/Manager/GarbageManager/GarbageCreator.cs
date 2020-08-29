using Repository;
using System.Transactions;

namespace Core.Manager.GarbageManager
{
	public class GarbageCreator : AsistanceBase<GarbageAdapter, Garbage>
	{
		public GarbageCreator(GarbageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Garbage Save(GarbageDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Garbage
				{
					Calculation = dto.Calculation,
					Dicipline = dto.Dicipline,
					PresenceId = dto.PresenceId,
					Separation = dto.Separation,
					TPS = dto.TPS,
					VolumeOfAnorganic = dto.VolumeOfAnorganic,
					VolumeOfOrganic = dto.VolumeOfOrganic
				};

				Manager.Database.Garbages.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
