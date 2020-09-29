using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.ImeiManager
{
	public class ImeiQuery : AsistanceBase<ImeiAdapter, IMEI>
	{
		public ImeiQuery(ImeiAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<IMEI> Get()
		{
			return Manager.Database.IMEIs;
		}

		public List<ImeiDTO> Transform()
		{
			return (from val in Get()
					select new ImeiDTO()
					{
						ImeiId = val.ImeiId,
						ImeiName = val.Imei1,
						Device = val.Device
					}).ToList();
		}

		public ImeiDTO TransformIMEICheck(string deviceParams)
		{
			var exist = Get().FirstOrDefault(x => x.Device.Equals(deviceParams));

			if (exist == null)
				throw new Exception("DeviceId not found");

			return (from val in Get()
					where val.Device.Equals(deviceParams)
					select new ImeiDTO()
					{
						ImeiId = val.ImeiId,
						ImeiName = val.Imei1,
						Device = val.Device
					}).FirstOrDefault();
		}
	}
}
