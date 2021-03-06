﻿using Repository;
using System;
using System.Transactions;

namespace Core.Manager.ZoneManager
{
	public class ZoneUpdater : AsistanceBase<ZoneAdapter, Zone>
	{
		public ZoneUpdater(ZoneAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Zone Update(ZoneDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Database.Zones.Find(dto.ZoneId);

				if (exist == null)
					throw new Exception("data not found");

				exist.ZoneName = dto.ZoneName;
				exist.RegionId = dto.RegionId;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
