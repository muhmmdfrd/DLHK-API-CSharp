using Core.Manager.LeaveManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class LeaveController : ApiController
    {
		private readonly ApiResponse<List<LeaveDTO>> respList = new ApiResponse<List<LeaveDTO>>();
		private readonly ApiResponse<LeaveDTO> resp = new ApiResponse<LeaveDTO>();

		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new LeaveAdapter())
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
		[Route("api/leave/postpone/{regionName}")]
		public IHttpActionResult GetPostPone([FromUri] string regionName)
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformStatus(regionName);
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
		[Route("api/leave/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new LeaveAdapter())
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

			return Json(resp);
		}

		[HttpPost]
		public IHttpActionResult Post([FromBody] LeaveDTO dto)
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					var result = manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.LeaveId);
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
		public IHttpActionResult Put([FromBody] LeaveDTO dto)
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					var result = manager.Updater.Value.Update(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.LeaveId);
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
		[Route("api/leave/confirm/{id}")]
		public IHttpActionResult Confirm([FromUri] long id)
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					var result = manager.Updater.Value.Confirm(id);

					resp.Message = "data confirm";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.LeaveId);
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
		[Route("api/leave/{id}")]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new LeaveAdapter())
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

		[HttpDelete]
		[Route("api/leave/all")]
		public IHttpActionResult DeleteAll()
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					manager.Deleter.Value.DeleteAll();

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
