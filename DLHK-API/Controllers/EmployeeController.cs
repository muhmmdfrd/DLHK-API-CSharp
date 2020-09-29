using Core.Manager.EmployeeManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class EmployeeController : ApiController
	{
		private readonly ApiResponse<List<EmployeeDTO>> respList = new ApiResponse<List<EmployeeDTO>>();
		private readonly ApiResponse<EmployeeDTO> resp = new ApiResponse<EmployeeDTO>();


		[HttpGet]
		[Route("api/employee/unregister")]
		public IHttpActionResult GetUser()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TranformUserLogin();
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

		[HttpGet]
		[Route("api/employee")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
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

		[HttpGet]
		[Route("api/employee/{zoneName}/{regionName}")]
		public IHttpActionResult Get([FromUri]string zoneName, string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformZoneAndRegion(zoneName, regionName);
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

		[HttpGet]
		[Route("api/employee/{zoneName}/{regionName}/{shift}")]
		public IHttpActionResult Get([FromUri]string zoneName, string regionName, string shift)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformZoneAndRegionShift(zoneName, regionName, shift);
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

		[HttpGet]
		[Route("api/employee/headzone/{regionName}")]
		public IHttpActionResult GetHeadZone([FromUri] string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformHeadZoneShift(regionName);
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

		[HttpGet]
		[Route("api/employee/headregion")]
		public IHttpActionResult GetCoordRegion()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformRegionShift();
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

		[HttpGet]
		[Route("api/employee/presence/headregion")]
		public IHttpActionResult GetCoorPresenceRegion()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformHeadRegion();
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

		[HttpGet]
		[Route("api/employee/{zoneName}/{regionName}/sweeper")]
		public IHttpActionResult GetSweeper([FromUri]string zoneName, string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformZoneAndRegionSweeper(zoneName, regionName);
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

		[HttpGet]
		[Route("api/employee/{zoneName}/{regionName}/drainage")]
		public IHttpActionResult GetDrainage([FromUri]string zoneName, string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformZoneAndRegionDrainage(zoneName, regionName);
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

		[HttpGet]
		[Route("api/employee/{zoneName}/{regionName}/garbage")]
		public IHttpActionResult GetGarbage([FromUri]string zoneName, string regionName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformZoneAndRegionGarbage(zoneName, regionName);
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

		[HttpGet]
		[Route("api/employee/zone/{region}")]
		public IHttpActionResult GetRegionName([FromUri] string region)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformHeadZone(region);
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

		[HttpGet]
		[Route("api/employee/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
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

			return Json(resp);
		}

		[HttpGet]
		[Route("api/employee/name/{employeeName}")]
		public IHttpActionResult EmployeeName([FromUri] string employeeName)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					resp.Message = "data found";
					resp.MessageCode = 200;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformName(employeeName);
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

		[HttpGet]
		[Route("api/employee/sweeper")]
		public IHttpActionResult GetSweeper()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformSweeper();
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

		[HttpGet]
		[Route("api/employee/drainage")]
		public IHttpActionResult GetDrainage()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformDrainage();
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

		[HttpGet]
		[Route("api/employee/garbage")]
		public IHttpActionResult GetGarbage()
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformGarbage();
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
		[Route("api/employee")]
		public IHttpActionResult Post([FromBody] EmployeeDTO dto)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var result = manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.EmployeeId);
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

		[HttpPut]
		[Route("api/employee")]
		public IHttpActionResult Put([FromBody] EmployeeDTO dto)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var result = manager.Updater.Value.Update(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.EmployeeId);
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

		[HttpPut]
		[Route("api/employee/mutation")]
		public IHttpActionResult PutLocation([FromBody] EmployeeDTO dto)
		{
			try
			{
				using (var manager = new EmployeeAdapter())
				{
					var result = manager.Updater.Value.UpdateLocation(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.EmployeeId);
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

		[HttpPut]
		[Route("api/employee/contract")]
		public IHttpActionResult PutContract([FromBody] EmployeeDTO dto)
		{
			using (var manager = new EmployeeAdapter())
			{
				try
				{
					var result = manager.Updater.Value.UpdateContract(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.EmployeeId);
				}
				catch (Exception ex)
				{
					resp.Message = ex.Message;
					resp.MessageCode = 400;
					resp.ErrorCode = 1;
					resp.Data = null;
				}
			}
	
			return Json(resp);
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

			return Json(resp);
		}

	}
}
