using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.SuplierManager
{
	public class SuplierQuery : AsistanceBase<SuplierAdapter, Suplier>
	{
		public SuplierQuery(SuplierAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Suplier> Get()
		{
			return Manager.Database.Supliers;
		}

		public List<SuplierDTO> Transform()
		{
			return (from val in Get()
					select new SuplierDTO()
					{
						Address = val.Address,
						Email = val.Email,
						Note = val.Note,
						Phone = val.Phone,
						SuplierName = val.SuplierName,
						SuplierId = val.SuplierId
					}).ToList();
		}

		public SuplierDTO TransformId(long id)
		{
			return (from val in Get()
					where val.SuplierId == id
					select new SuplierDTO()
					{
						Address = val.Address,
						Email = val.Email,
						Note = val.Note,
						Phone = val.Phone,
						SuplierName = val.SuplierName,
						SuplierId = val.SuplierId
					}).FirstOrDefault();
		}
	}
}
