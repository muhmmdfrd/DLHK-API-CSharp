using Core.Manager.CoordinatorManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class CoordinatorController : ApiController
    {
		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new CoordinatorAdapter())
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

		[HttpPost]
		public IHttpActionResult Post([FromBody] CoordinatorDTO dto)
		{
			try
			{
				using (var manager = new CoordinatorAdapter())
				{
					var result = manager.Creator.Value.Save(dto);
					var data = manager.Query.Value.TransformId(result.CoordinatorId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		public IHttpActionResult Put([FromBody] CoordinatorDTO dto)
		{
			try
			{
				using (var manager = new CoordinatorAdapter())
				{
					var result = manager.Updater.Value.Update(dto);
					var data = manager.Query.Value.TransformId(result.CoordinatorId);

					return Json(Response.Updated(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpDelete]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new CoordinatorAdapter())
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
	}
}
