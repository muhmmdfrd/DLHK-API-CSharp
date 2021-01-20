using Core.Manager.ImeiManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class ImeiController : ApiController
    {
		[HttpGet]
		[Route("api/Imei")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new ImeiAdapter())
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
		[Route("api/Imei/{deviceParams}")]
		public IHttpActionResult GetImei([FromUri] string deviceParams)
		{
			try
			{
				using (var manager = new ImeiAdapter())
				{
					var data = manager.Query.Value.TransformIMEICheck(deviceParams);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPost]
		[Route("api/Imei")]
		public IHttpActionResult Post([FromBody] ImeiDTO dto)
		{
			try
			{
				using (var manager = new ImeiAdapter())
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
		[Route("api/Imei")]
		public IHttpActionResult Put([FromBody] ImeiDTO dto)
		{
			try
			{
				using (var manager = new ImeiAdapter())
				{
					var result = manager.Updater.Value.Update(dto);

					return Json(Response.Updated(""));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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
