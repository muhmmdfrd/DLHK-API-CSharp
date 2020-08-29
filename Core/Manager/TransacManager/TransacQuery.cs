using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.TransacManager
{
	public class TransacQuery : AsistanceBase<TransacAdapter, Transac>
	{
		public TransacQuery(TransacAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Transac> Get()
		{
			return Manager.Database.Transacs;
		}
		
		public List<TransacDTO> TransformAll()
		{
			return (from val in Get()
					select new TransacDTO()
					{
						DateTransac = val.DateTransac,
						Qty = val.Qty,
						TransacId = val.TransacId,
						TypeOfTransac = val.TypeOfTransac,
						ItemCode = val.ItemCode,
						ItemName = val.ItemName,
						Note = val.Note,
						UserRecorder = val.UserRecorder,
						UserRequest = val.UserRequest,
						SuplierName = val.SuplierName
					}).ToList();
		}

		public List<TransacDTO> TransformIn()
		{
			return (from val in Get()
					where val.TypeOfTransac.Equals("IN")
					select new TransacDTO()
					{
						DateTransac = val.DateTransac,
						Qty = val.Qty,
						TransacId = val.TransacId,
						TypeOfTransac = val.TypeOfTransac,
						ItemCode = val.ItemCode,
						ItemName = val.ItemName,
						Note = val.Note,
						UserRecorder = val.UserRecorder,
						UserRequest = val.UserRequest,
					}).ToList();
		}

		public TransacDTO TransformInId(long id)
		{
			return (from val in Get()
					where val.TransacId == id &&
					val.TypeOfTransac.Equals("IN")
					select new TransacDTO()
					{
						DateTransac = val.DateTransac,
						Qty = val.Qty,
						TransacId = val.TransacId,
						TypeOfTransac = val.TypeOfTransac,
						ItemCode = val.ItemCode,
						ItemName = val.ItemName,
						Note = val.Note,
						UserRecorder = val.UserRecorder,
						UserRequest = val.UserRequest
					}).FirstOrDefault();
		}

		public List<TransacDTO> TransformOut()
		{
			return (from val in Get()
					where val.TypeOfTransac.Equals("OUT")
					select new TransacDTO()
					{
						DateTransac = val.DateTransac,
						Qty = val.Qty,
						TransacId = val.TransacId,
						TypeOfTransac = val.TypeOfTransac,
						ItemCode = val.ItemCode,
						ItemName = val.ItemName,
						Note = val.Note,
						UserRecorder = val.UserRecorder,
						UserRequest = val.UserRequest
					}).ToList();
		}

		public TransacDTO TransformId(long id)
		{
			return (from val in Get()
					where val.TransacId == id &&
					val.TypeOfTransac.Equals("OUT")
					select new TransacDTO()
					{
						DateTransac = val.DateTransac,
						Qty = val.Qty,
						TransacId = val.TransacId,
						TypeOfTransac = val.TypeOfTransac,
						ItemCode = val.ItemCode,
						ItemName = val.ItemName,
						Note = val.Note,
						UserRecorder = val.UserRecorder,
						UserRequest = val.UserRequest
					}).FirstOrDefault();
		}
	}
}
