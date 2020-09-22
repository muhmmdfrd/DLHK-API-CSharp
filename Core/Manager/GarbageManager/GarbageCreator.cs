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
				var existGarbage = Manager.Query.Value.Get().FirstOrDefault(x => x.PresenceId == dto.PresenceId);
				var counter = existPresence.Counter + 1;

				if (counter == 1 && existGarbage == null)
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
				else if (counter <= 2 && existGarbage != null)
				{
					existGarbage.Calculation += dto.Calculation;
					existGarbage.Calculation /= 2;

					existGarbage.Dicipline += dto.Dicipline;
					existGarbage.Dicipline /= 2;
					
					existGarbage.Separation += dto.Separation;
					existGarbage.Separation /= 2;
					
					existGarbage.TPS += dto.TPS;
					existGarbage.TPS /= 2;
					
					existGarbage.VolumeOfAnorganic += dto.VolumeOfAnorganic;
					existGarbage.VolumeOfAnorganic /= 2;
					
					existGarbage.VolumeOfOrganic += dto.VolumeOfOrganic;
					existGarbage.VolumeOfOrganic /= 2;
				}
				else if (counter == 3 && existGarbage != null)
				{
					existGarbage.Calculation += dto.Calculation;
					existGarbage.Calculation /= 3;

					existGarbage.Dicipline += dto.Dicipline;
					existGarbage.Dicipline /= 3;

					existGarbage.Separation += dto.Separation;
					existGarbage.Separation /= 3;

					existGarbage.TPS += dto.TPS;
					existGarbage.TPS /= 3;

					existGarbage.VolumeOfAnorganic += dto.VolumeOfAnorganic;
					existGarbage.VolumeOfAnorganic /= 3;

					existGarbage.VolumeOfOrganic += dto.VolumeOfOrganic;
					existGarbage.VolumeOfOrganic /= 3;
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
