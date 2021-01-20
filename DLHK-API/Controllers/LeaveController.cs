using Core.Manager.LeaveManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class LeaveController : ApiController
    {
		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					var data = manager.Query.Value.Transform();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/leave/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					var data = manager.Query.Value.TransformId(id);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPost]
		public IHttpActionResult Post([FromBody] LeaveDTO dto)
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					var data = manager.Creator.Value.Save(dto);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		public IHttpActionResult Put([FromBody] LeaveDTO dto)
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					var data = manager.Updater.Value.Update(dto);
					
					return Json(Response.Updated(data));
				}
			}
			catch (Exception ex) { return Json(Response.Fail(ex.Message)); }
		}

		[HttpPut]
		[Route("api/leave/confirm/{id}")]
		public IHttpActionResult Confirm([FromUri] long id)
		{
			try
			{
				using (var manager = new LeaveAdapter())
				{
					var data = manager.Updater.Value.Confirm(id);

					return Json(Response.Updated(data));
				}
			}
			catch (Exception ex) { return Json(Response.Fail(ex.Message)); }
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

					return Json(Response.Deleted());
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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

					return Json(Response.Deleted());
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}
	}
}
