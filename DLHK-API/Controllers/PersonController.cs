using Core.Manager.InterviewManager;
using Core.Manager.PersonManager;
using DLHK_API.Models;
using System;
using System.Web;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class PersonController : ApiController
	{
		[HttpGet]
		[Route("api/person")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new PersonAdapter())
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
		[Route("api/person/applicant")]
		public IHttpActionResult GetApplicant()
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var data = manager.Query.Value.TransformApplicant();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/person/interview")]
		public IHttpActionResult GetInterview()
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var data = manager.Query.Value.TransformInterviewed();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/person/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new PersonAdapter())
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

		[HttpGet]
		[Route("api/person/search/{personName}")]
		public IHttpActionResult GetPersonName([FromUri] string personName)
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var data = manager.Query.Value.TransformName(personName);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/person/applicant/{id}")]
		public IHttpActionResult GetApplicantId([FromUri] long id)
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var data = manager.Query.Value.TransformApplicantId(id);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPost]
		[Route("api/person/applicant/export")]
		public IHttpActionResult ExportApplicant()
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var data = manager.Query.Value.ExportExcel();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPost]
		[Route("api/person")]
		public IHttpActionResult Post([FromBody] PersonDTO dto)
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var result = manager.Creator.Value.Save(dto);
					var data = manager.Query.Value.TransformId(result.PersonId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPost]
		[Route("api/person/email")]
		public IHttpActionResult PostEmail([FromBody] InterviewDTO dto)
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					new Utilities.Util().SendEmail(dto);

					return Json(Response.Success(""));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		[Route("api/person")]
		public IHttpActionResult Put([FromBody] PersonDTO dto)
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var result = manager.Updater.Value.Update(dto);
					var data = manager.Query.Value.TransformId(result.PersonId);

					return Json(Response.Updated(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		[Route("api/person/ktp")]
		public IHttpActionResult UploadKTP()
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var formData = HttpContext.Current.Request.Params;

					var dto = new PersonDTO
					{
						PersonId = Convert.ToInt64(formData["personId"]),
						KTP = Utilities.Util.Compress()
					};

					manager.Updater.Value.KTP(dto);

					return Json(Response.Success(""));
				}	
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		[Route("api/person/photo")]
		public IHttpActionResult UploadPhoto()
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var formData = HttpContext.Current.Request.Params;

					var dto = new PersonDTO
					{
						PersonId = Convert.ToInt64(formData["personId"]),
						Photo = Utilities.Util.Compress()
					};

					manager.Updater.Value.Photo(dto);

					return Json(Response.Success(""));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		[Route("api/person/letter")]
		public IHttpActionResult UploadLetter()
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var formData = HttpContext.Current.Request.Params;

					var dto = new PersonDTO
					{
						PersonId = Convert.ToInt64(formData["personId"]),
						AppLetter = Utilities.Util.Compress()
					};

					manager.Updater.Value.Letter(dto);

					return Json(Response.Success(""));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		[Route("api/person/sertificate")]
		public IHttpActionResult UploadSertificate()
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					var formData = HttpContext.Current.Request.Params;

					var dto = new PersonDTO
					{
						PersonId = Convert.ToInt64(formData["personId"]),
						Sertificate = Utilities.Util.Compress()
					};

					manager.Updater.Value.Sertificate(dto);

					return Json(Response.Success(""));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpDelete]
		[Route("api/person/{id}")]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new PersonAdapter())
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
		[Route("api/person/employee/{id}")]
		public IHttpActionResult DeletePersonEmployee([FromUri] long id)
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					manager.Deleter.Value.DeletePersonEmployee(id);

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
