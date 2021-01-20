using Core.Manager.HeadZoneManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class HeadZoneController : ApiController
    {

		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new HeadZoneAdapter())
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
		public IHttpActionResult Post([FromBody] HeadZoneDTO dto)
		{
			try
			{
				using (var manager = new HeadZoneAdapter())
				{
					var result = manager.Creator.Value.Save(dto);
					var data = manager.Query.Value.TransformId(result.HeadOfZoneId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}	
		}

		[HttpPut]
		public IHttpActionResult Put([FromBody] HeadZoneDTO dto)
		{
			try
			{
				using (var manager = new HeadZoneAdapter())
				{
					var result = manager.Updater.Value.Update(dto);
					var data = manager.Query.Value.TransformId(result.HeadOfZoneId);

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
				using (var manager = new HeadZoneAdapter())
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
