using Core.Manager.InterviewManager;
using Core.Manager.PersonManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class PersonController : ApiController
    {
		private readonly ApiResponse<List<PersonDTO>> respList = new ApiResponse<List<PersonDTO>>();
		private readonly ApiResponse<PersonDTO> resp = new ApiResponse<PersonDTO>();

		[HttpGet]
		[Route("api/person")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new PersonAdapter())
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

			return Ok(respList);
		}

		[HttpGet]
		[Route("api/person/applicant")]
		public IHttpActionResult GetApplicant()
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformApplicant();
				}
			}
			catch (Exception ex)
			{
				respList.Message = ex.Message;
				respList.MessageCode = 400;
				respList.ErrorCode = 1;
				respList.Data = null;
			}

			return Ok(respList);
		}

		[HttpGet]
		[Route("api/person/interview")]
		public IHttpActionResult GetInterview()
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformInterviewed();
				}
			}
			catch (Exception ex)
			{
				respList.Message = ex.Message;
				respList.MessageCode = 400;
				respList.ErrorCode = 1;
				respList.Data = null;
			}

			return Ok(respList);
		}

		[HttpGet]
		[Route("api/person/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new PersonAdapter())
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

			return Ok(resp);
		}

		[HttpGet]
		[Route("api/person/search/{personName}")]
		public IHttpActionResult GetPersonName([FromUri] string personName)
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					resp.Message = "data found";
					resp.MessageCode = 200;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformName(personName);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Ok(resp);
		}

		[HttpGet]
		[Route("api/person/applicant/{id}")]
		public IHttpActionResult GetApplicantId([FromUri] long id)
		{
			try
			{
				using (var manager = new PersonAdapter())
				{
					resp.Message = "data found";
					resp.MessageCode = 200;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformApplicantId(id);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Ok(resp);
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

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.PersonId);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Ok(resp);
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

					resp.Message = "data sent";
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

			return Ok(resp);
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

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.PersonId);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Ok(resp);
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

					resp.Message = "data uploaded";
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

			return Ok(resp);
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

					resp.Message = "data uploaded";
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

			return Ok(resp);
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

					resp.Message = "data uploaded";
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

			return Ok(resp);
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

					resp.Message = "data uploaded";
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

			return Ok(resp);
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

			return Ok(resp);
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

			return Ok(resp);
		}

	}
}
