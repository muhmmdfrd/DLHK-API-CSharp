using Core.Manager.SuplierManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class SuplierController : ApiController
    {
		private readonly ApiResponse<List<SuplierDTO>> respList = new ApiResponse<List<SuplierDTO>>();
		private readonly ApiResponse<SuplierDTO> resp = new ApiResponse<SuplierDTO>();

		[HttpGet]
		[Route("api/suplier")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new SuplierAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.Transform();
				}
			}
			catch (Exception ex)
			{
				respList.Message = ex.Message;
				respList.MessageCode = 400;
				respList.ErrorCode = 1;
				respList.Data = null;
			}

			return Ok(respList);
		}

		[HttpGet]
		[Route("api/suplier/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new SuplierAdapter())
				{
					resp.Message = "data found";
					resp.MessageCode = 200;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(id);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Ok(resp);
		}

		[HttpPost]
		[Route("api/suplier")]
		public IHttpActionResult Post([FromBody] SuplierDTO dto)
		{
			try
			{
				using (var manager = new SuplierAdapter())
				{
					var result = manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.SuplierId);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Ok(resp);
		}

		[HttpPut]
		[Route("api/suplier")]
		public IHttpActionResult Put([FromBody] SuplierDTO dto)
		{
			try
			{
				using (var manager = new SuplierAdapter())
				{
					var result = manager.Updater.Value.Update(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.SuplierId);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Ok(resp);
		}

		[HttpDelete]
		[Route("api/suplier/{id}")]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new SuplierAdapter())
				{
					manager.Deleter.Value.Delete(id);

					resp.Message = "data deleted";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = null;
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Ok(resp);
		}
	}
}
