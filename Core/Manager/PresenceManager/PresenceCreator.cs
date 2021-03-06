﻿using Repository;
using System;
using System.Transactions;

namespace Core.Manager.PresenceManager
{
	public class PresenceCreator : AsistanceBase<PresenceAdapter, Presence>
	{
		public PresenceCreator(PresenceAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Presence Save(PresenceDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Presence
				{
					Coordinate = dto.Coordinate,
					DateOfPresence = dto.DateOfPresence,
					EmployeeId = dto.EmployeeId,
					LivePhoto = dto.LivePhoto,
					PresenceStatus = "1",
					Location = dto.Location,
					Counter = 0
				};

				Manager.Database.Presences.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}

		public Presence SaveLeave(long? employeeIdParams, string locationParams)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Presence
				{
					Coordinate = "",
					DateOfPresence = DateTime.Now,
					EmployeeId = employeeIdParams,
					LivePhoto = null,
					PresenceStatus = "2",
					Location = locationParams
				};

				Manager.Database.Presences.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}

		public Presence SaveAbsence(long? employeeIdParams, string locationParams)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Presence
				{
					Coordinate = "",
					DateOfPresence = DateTime.Now,
					EmployeeId = employeeIdParams,
					LivePhoto = null,
					PresenceStatus = "0",
					Location = locationParams
				};

				Manager.Database.Presences.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
