using Core.Manager.PresenceManager;
using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.GarbageManager
{
	public class GarbageCreator : AsistanceBase<GarbageAdapter, Garbage>
	{
		public GarbageCreator(GarbageAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Save(GarbageDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var existPresence = Manager.Database.Presences.FirstOrDefault(x => x.PresenceId == dto.PresenceId);
				var existSweeper = Manager.Query.Value.Get().FirstOrDefault(x => x.PresenceId == dto.PresenceId);
				var counter = existPresence.Counter + 1;

				if (counter == 1 && existSweeper == null)
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

					new PresenceAdapter().Updater.Value.UpdateLocationPresence(dto.Location, dto.PresenceId);

					Manager.Database.Garbages.Add(newEntity);
				}
				else if (counter <= 2 && existSweeper != null)
				{
					existSweeper.Calculation += dto.Calculation;
					existSweeper.Dicipline += dto.Dicipline;
					existSweeper.Separation += dto.Separation;
					existSweeper.TPS += dto.TPS;
					existSweeper.VolumeOfAnorganic += dto.VolumeOfAnorganic;
					existSweeper.VolumeOfOrganic += dto.VolumeOfOrganic;
				}
				else if (counter == 3 && existSweeper != null)
				{
					existSweeper.Calculation += dto.Calculation;
					existSweeper.Calculation /= 3;

					existSweeper.Dicipline += dto.Dicipline;
					existSweeper.Dicipline /= 3;

					existSweeper.Separation += dto.Separation;
					existSweeper.Separation /= 3;

					existSweeper.TPS += dto.TPS;
					existSweeper.TPS /= 3;

					existSweeper.VolumeOfAnorganic += dto.VolumeOfAnorganic;
					existSweeper.VolumeOfAnorganic /= 3;

					existSweeper.VolumeOfOrganic += dto.VolumeOfOrganic;
					existSweeper.VolumeOfOrganic /= 3;
				}
				else
				{
					throw new Exception("You have entered the value more than 3 times");
				}

				existPresence.Counter++;
			
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
