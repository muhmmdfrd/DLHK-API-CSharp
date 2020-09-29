using Core.Manager.ImeiManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class ImeiController : ApiController
    {
		private readonly ApiResponse<List<ImeiDTO>> respList = new ApiResponse<List<ImeiDTO>>();
		private readonly ApiResponse<ImeiDTO> resp = new ApiResponse<ImeiDTO>();

		[HttpGet]
		[Route("api/Imei")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new ImeiAdapter())
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

		[HttpGet]
		[Route("api/Imei/{deviceParams}")]
		public IHttpActionResult GetImei([FromUri] string deviceParams)
		{
			try
			{
				using (var manager = new ImeiAdapter())
				{
					

					resp.Message = "data found";
					resp.MessageCode = 200;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformIMEICheck(deviceParams);
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

		[HttpPost]
		[Route("api/Imei")]
		public IHttpActionResult Post([FromBody] ImeiDTO dto)
		{
			try
			{
				using (var manager = new ImeiAdapter())
				{
					var result = manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
					resp.MessageCode = 201;
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

		[HttpPut]
		[Route("api/Imei")]
		public IHttpActionResult Put([FromBody] ImeiDTO dto)
		{
			try
			{
				using (var manager = new ImeiAdapter())
				{
					var result = manager.Updater.Value.Update(dto);

					resp.Message = "data updated";
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

		[HttpDelete]
		[Route("api/Imei/{id}")]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new ImeiAdapter())
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
