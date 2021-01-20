using Core.Manager.RoleManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class RoleController : ApiController
    {
		[HttpGet]
		[Route("api/role")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new RoleAdapter())
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
		[Route("api/role/applicant")]
		public IHttpActionResult GetRoleApplicant()
		{
			try
			{
				using (var manager = new RoleAdapter())
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
		[Route("api/role/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new RoleAdapter())
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
		[Route("api/role")]
		public IHttpActionResult Post([FromBody] RoleDTO dto)
		{
			try
			{
				using (var manager = new RoleAdapter())
				{
					var result = manager.Creator.Value.Save(dto);
					var data = manager.Query.Value.TransformId(result.RoleId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		[Route("api/role")]
		public IHttpActionResult Put([FromBody] RoleDTO dto)
		{
			try
			{
				using (var manager = new RoleAdapter())
				{
					var result = manager.Updater.Value.Update(dto);
					var data = manager.Query.Value.TransformId(result.RoleId);

					return Json(Response.Updated(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpDelete]
		[Route("api/role/{id}")]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new RoleAdapter())
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
