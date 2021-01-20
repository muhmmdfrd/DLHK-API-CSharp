using Core.Manager.EmployeeManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class EmployeeController : ApiController
	{
		[HttpGet]
		[Route("api/employee/unregister")]
		public IHttpActionResult GetUser()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TranformUserLogin();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
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
		[Route("api/employee/{zoneName}/{regionName}")]
		public IHttpActionResult Get([FromUri]string zoneName, string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformZoneAndRegion(zoneName, regionName);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/{zoneName}/{regionName}/{shift}")]
		public IHttpActionResult Get([FromUri]string zoneName, string regionName, string shift)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformZoneAndRegionShift(zoneName, regionName, shift);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/headzone/{regionName}")]
		public IHttpActionResult GetHeadZone([FromUri] string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformHeadZoneShift(regionName);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/headregion")]
		public IHttpActionResult GetCoordRegion()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformRegionShift();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/presence/headregion")]
		public IHttpActionResult GetCoorPresenceRegion()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformHeadRegion();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/{zoneName}/{regionName}/sweeper")]
		public IHttpActionResult GetSweeper([FromUri]string zoneName, string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformZoneAndRegionSweeper(zoneName, regionName);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/{zoneName}/{regionName}/drainage")]
		public IHttpActionResult GetDrainage([FromUri]string zoneName, string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformZoneAndRegionDrainage(zoneName, regionName);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/{zoneName}/{regionName}/garbage")]
		public IHttpActionResult GetGarbage([FromUri]string zoneName, string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformZoneAndRegionGarbage(zoneName, regionName);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/zone/{region}")]
		public IHttpActionResult GetRegionName([FromUri] string region)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformHeadZone(region);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
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
		[Route("api/employee/name/{employeeName}")]
		public IHttpActionResult EmployeeName([FromUri] string employeeName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var data = manager.Query.Value.TransformName(employeeName);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/sweeper")]
		public IHttpActionResult GetSweeper()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{var data = manager.Query.Value.TransformSweeper();
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/drainage")]
		public IHttpActionResult GetDrainage()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{var data = manager.Query.Value.TransformDrainage();
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/employee/garbage")]
		public IHttpActionResult GetGarbage()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{var data = manager.Query.Value.TransformGarbage();
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPost]
		[Route("api/employee")]
		public IHttpActionResult Post([FromBody] EmployeeDTO dto)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var result = manager.Creator.Value.Save(dto);
					var data = manager.Query.Value.TransformId(result.EmployeeId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		[Route("api/employee")]
		public IHttpActionResult Put([FromBody] EmployeeDTO dto)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var result = manager.Updater.Value.Update(dto);
					var data = manager.Query.Value.TransformId(result.EmployeeId);

					return Json(Response.Updated(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		[Route("api/employee/mutation")]
		public IHttpActionResult PutLocation([FromBody] EmployeeDTO dto)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var result = manager.Updater.Value.UpdateLocation(dto);
					var data = manager.Query.Value.TransformId(result.EmployeeId);

					return Json(Response.Updated(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPut]
		[Route("api/employee/contract")]
		public IHttpActionResult PutContract([FromBody] EmployeeDTO dto)
		{
			using (var manager = new EmployeeAdapter())
			{
				try
				{
					var result = manager.Updater.Value.UpdateContract(dto);
					var data = manager.Query.Value.TransformId(result.EmployeeId);

					return Json(Response.Updated(data));
				}
				catch (Exception ex)
				{
					return Json(Response.Fail(ex.Message));
				}
			}
		}

		[HttpDelete]
		[Route("api/employee/{id}")]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
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
