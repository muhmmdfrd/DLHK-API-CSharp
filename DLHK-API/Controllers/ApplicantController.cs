using Core.Manager.ApplicantManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class ApplicantController : ApiController
    {
		[HttpPost]
		[Route("api/applicant/add")]
		public IHttpActionResult Post([FromBody] ApplicantDTO dto)
		{
			try
			{
				using (var manager = new ApplicantAdapter())
				{
					manager.Creator.Value.Save(dto);

					return Json(Response.Success(""));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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

					return Json(Response.Success(""));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}
	}
}
