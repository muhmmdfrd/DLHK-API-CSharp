using Core.Manager.UserManager;
using DLHK_API.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class UserController : ApiController
    {
		[HttpGet]
		[Route("api/user")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new UserAdapter())
				{
					var data = manager.Query.Value.Tranform();

					return Json(Response.Success(data));
				}
				 
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/user/{id}")]
		public IHttpActionResult Post([FromUri] long id)
		{
			try
			{
				using (var manager = new UserAdapter())
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
		[Route("api/user/claim")]
		public IHttpActionResult GetClaim()
		{
			try
			{
				var identity = (ClaimsIdentity)User.Identity;

				var data = new NameForClaim
				{
					RoleName = ClaimIdentityType(identity, ClaimTypes.Role),
					Name = ClaimIdentityType(identity, ClaimTypes.Name),
					Photo = ClaimIdentityType(identity, "Photo"),
					RegionName = ClaimIdentityType(identity, "Region"),
					ZoneName = ClaimIdentityType(identity, "Zone"),
					UserId = ClaimIdentityType(identity, "UserId"),
					Shift = ClaimIdentityType(identity, "Shift")
				};

				return Json(Response.Success(data));
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPost]
		[Route("api/user")]
		public IHttpActionResult Post([FromBody] UserDTO dto)
		{
			try
			{
				using (var manager = new UserAdapter())
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

		[HttpPut]
		[Route("api/user")]
		public IHttpActionResult Put([FromBody] UserDTO dto)
		{
			try
			{
				using (var manager = new UserAdapter())
				{
					manager.Updater.Value.Update(dto);

					return Json(Response.Updated(""));
				}

			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpDelete]
		[Route("api/user/{id}")]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new UserAdapter())
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

		private class NameForClaim
		{
			public string Name { get; set; }
			public string RoleName { get; set; }
			public string ZoneName { get; set; }
			public string RegionName { get; set; }
			public string Photo { get; set; }
			public string UserId { get; set; }
			public string Shift { get; set; }
		}

		private string ClaimIdentityType(ClaimsIdentity identity, string type)
		{
			return string.Join(",", identity.Claims.Where(s => s.Type.Equals(type)).Select(s => s.Value).ToList());
		}
	}
}
