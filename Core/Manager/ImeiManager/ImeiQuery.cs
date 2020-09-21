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

		public ImeiDTO TransformIMEICheck(string imeiParams)
		{
			var exist = Get().FirstOrDefault(x => x.Imei1.Equals(imeiParams));

			if (exist == null)
				throw new Exception("IMEI not found");

			return (from val in Get()
					where val.Imei1.Equals(imeiParams)
					select new ImeiDTO()
					{
						ImeiId = val.ImeiId,
						ImeiName = val.Imei1,
						Device = val.Device
					}).FirstOrDefault();
		}
	}
}
