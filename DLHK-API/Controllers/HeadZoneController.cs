using Core.Manager.HeadZoneManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class HeadZoneController : ApiController
    {
		private readonly ApiResponse<List<HeadZoneDTO>> respList = new ApiResponse<List<HeadZoneDTO>>();
		private readonly ApiResponse<HeadZoneDTO> resp = new ApiResponse<HeadZoneDTO>();

		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new HeadZoneAdapter())
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
		public IHttpActionResult Post([FromBody] HeadZoneDTO dto)
		{
			try
			{
				using (var manager = new HeadZoneAdapter())
				{
					var result = manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.HeadOfZoneId);
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
		public IHttpActionResult Put([FromBody] HeadZoneDTO dto)
		{
			try
			{
				using (var manager = new HeadZoneAdapter())
				{
					var result = manager.Updater.Value.Update(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.HeadOfZoneId);
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
				using (var manager = new HeadZoneAdapter())
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
