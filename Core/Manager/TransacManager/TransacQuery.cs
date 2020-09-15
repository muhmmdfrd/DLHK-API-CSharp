using Repository;
using System;
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
						UserRequest = val.UserRequest,
						Zone = val.ZoneName,
						Region = val.RegionName
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
						UserRequest = val.UserRequest,
						Zone = val.ZoneName,
						Region = val.RegionName
					}).FirstOrDefault();
		}

		public List<TransacDTO> TransformOutDate(string start, string end)
		{
			var startDate = Convert.ToDateTime(start);
			var endDate = Convert.ToDateTime(end);

			return (from val in Get()
					where val.TypeOfTransac.Equals("OUT") &&
					val.DateTransac >= startDate &&
					val.DateTransac <= endDate
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
						Zone = val.ZoneName,
						Region = val.RegionName
					}).ToList();
		}

		public List<TransacDTO> TransformInDate(string start, string end)
		{
			var startDate = Convert.ToDateTime(start);
			var endDate = Convert.ToDateTime(end);

			return (from val in Get()
					where val.TypeOfTransac.Equals("IN") &&
					val.DateTransac >= startDate &&
					val.DateTransac <= endDate
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
	}
}
