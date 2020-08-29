using Core.Manager.AssessmentZoneManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class AssessmentZoneController : ApiController
    {
		private readonly ApiResponse<List<AssessmentZoneDTO>> respList = new ApiResponse<List<AssessmentZoneDTO>>();
		private readonly ApiResponse<AssessmentZoneDTO> resp = new ApiResponse<AssessmentZoneDTO>();

		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new AssessmentZoneAdapter())
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
		public IHttpActionResult Post([FromBody] AssessmentZoneDTO dto)
		{
			try
			{
				using (var manager = new AssessmentZoneAdapter())
				{
					var result = manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.AssessmentZoneId);
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
