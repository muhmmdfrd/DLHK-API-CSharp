using Core.Manager.AssessmentZoneManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class AssessmentZoneController : ApiController
    {
		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new AssessmentZoneAdapter())
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
		public IHttpActionResult Post([FromBody] AssessmentZoneDTO dto)
		{
			try
			{
				using (var manager = new AssessmentZoneAdapter())
				{
					var result = manager.Creator.Value.Save(dto);
					var data = manager.Query.Value.TransformId(result.AssessmentZoneId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}
	}
}
