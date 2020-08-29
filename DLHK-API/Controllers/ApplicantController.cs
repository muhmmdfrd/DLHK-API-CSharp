using Core.Manager.ApplicantManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class ApplicantController : ApiController
    {
		private readonly ApiResponse<ApplicantDTO> resp = new ApiResponse<ApplicantDTO>();

		[HttpPost]
		[Route("api/applicant/add")]
		public IHttpActionResult Post([FromBody] ApplicantDTO dto)
		{
			try
			{
				using (var manager = new ApplicantAdapter())
				{
					manager.Creator.Value.Save(dto);

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

		[HttpPost]
		[Route("api/applicant/login")]
		public IHttpActionResult Login([FromBody] ApplicantDTO dto)
		{
			try
			{
				using (var manager = new ApplicantAdapter())
				{
					manager.Creator.Value.Login(dto);

					resp.Message = "data found";
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
	}
}
