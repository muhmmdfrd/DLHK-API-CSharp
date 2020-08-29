using Core.Manager.CoordinatorManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class CoordinatorController : ApiController
    {
		private readonly ApiResponse<List<CoordinatorDTO>> respList = new ApiResponse<List<CoordinatorDTO>>();
		private readonly ApiResponse<CoordinatorDTO> resp = new ApiResponse<CoordinatorDTO>();

		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new CoordinatorAdapter())
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

			return Json(respList);
		}

		[HttpPost]
		public IHttpActionResult Post([FromBody] CoordinatorDTO dto)
		{
			try
			{
				using (var manager = new CoordinatorAdapter())
				{
					var result = manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.CoordinatorId);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Json(resp);
		}

		[HttpPut]
		public IHttpActionResult Put([FromBody] CoordinatorDTO dto)
		{
			try
			{
				using (var manager = new CoordinatorAdapter())
				{
					var result = manager.Updater.Value.Update(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.CoordinatorId);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Json(resp);
		}

		[HttpDelete]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new CoordinatorAdapter())
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

			return Json(resp);
		}
	}
}
