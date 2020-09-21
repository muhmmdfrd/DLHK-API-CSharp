using Core.Manager.PresenceManager;
using DLHK_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class PresenceController : ApiController
    {
		private readonly ApiResponse<List<PresenceDTO>> respList = new ApiResponse<List<PresenceDTO>>();
		private readonly ApiResponse<PresenceDTO> resp = new ApiResponse<PresenceDTO>();
		private readonly ApiResponse<List<PresenceResumeDTO>> respResume = new ApiResponse<List<PresenceResumeDTO>>();
		private readonly ApiResponse<List<ZoneResumeDTO>> respZone = new ApiResponse<List<ZoneResumeDTO>>();
		private readonly ApiResponse<List<RegionResumeDTO>> respRegion = new ApiResponse<List<RegionResumeDTO>>();
		private readonly ApiResponse<PerformSweeperDTO> respSweeper = new ApiResponse<PerformSweeperDTO>();
		private readonly ApiResponse<PerformDrainageDTO> respDrainage = new ApiResponse<PerformDrainageDTO>();
		private readonly ApiResponse<PerformGarbageDTO> respGarbage = new ApiResponse<PerformGarbageDTO>();
		private readonly ApiResponse<List<ZonePerformDTO>> respZonePerform = new ApiResponse<List<ZonePerformDTO>>();
		private readonly ApiResponse<List<RegionPerformDTO>> respRegionPerform = new ApiResponse<List<RegionPerformDTO>>();
		private readonly ApiResponse<PerformHeadZoneDTO> respHeadZone = new ApiResponse<PerformHeadZoneDTO>();
		private readonly ApiResponse<List<ZonePropertyDTO>> respZoneLive = new ApiResponse<List<ZonePropertyDTO>>();
		private readonly ApiResponse<List<ZonePerformLiveDTO>> respZonePerformLive = new ApiResponse<List<ZonePerformLiveDTO>>();
		private readonly ApiResponse<List<EmployeePerformDTO>> respEmployee = new ApiResponse<List<EmployeePerformDTO>>();
		private readonly ApiResponse<DashboardDTO> respDashboard = new ApiResponse<DashboardDTO>();

		[HttpGet]
		[Route("api/presence")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new PresenceAdapter())
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
		[Route("api/presence/dashboard")]
		public IHttpActionResult GetDashboard()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respDashboard.Message = "data found";
					respDashboard.MessageCode = 200;
					respDashboard.ErrorCode = 0;
					respDashboard.Data = manager.Query.Value.TransformDashboard();
				}
			}
			catch (Exception ex)
			{
				respDashboard.Message = ex.Message;
				respDashboard.MessageCode = 400;
				respDashboard.ErrorCode = 1;
				respDashboard.Data = null;
			}

			return Json(respDashboard);
		}

		[HttpGet]
		[Route("api/presence/headregion")]
		public IHttpActionResult GetHeadRegion()
		{
			try
			{
				using (var manager = new PresenceAdapter())
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
		[Route("api/presence/{zoneName}/{regionName}/sweeper/{shift}")]
		public IHttpActionResult GetSweeper([FromUri] string zoneName, string regionName, string shift)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformSweeper(zoneName, regionName, shift);
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
		[Route("api/presence/{zoneName}/{regionName}/drainage/{shift}")]
		public IHttpActionResult GetDrainage([FromUri] string zoneName, string regionName, string shift)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformDrainage(zoneName, regionName, shift);
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
		[Route("api/presence/{regionName}/headzone")]
		public IHttpActionResult GetHeadZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformHeadZone();
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
		[Route("api/presence/{zoneName}/{regionName}/garbage/{shift}")]
		public IHttpActionResult GetGarbage([FromUri] string zoneName, string regionName, string shift)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformGarbage(zoneName, regionName, shift);
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
		[Route("api/presence/photo")]
		public IHttpActionResult GetWithPhoto()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformWithPhoto();
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
		[Route("api/presence/resume")]
		public IHttpActionResult GetResume()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respResume.Message = "data found";
					respResume.MessageCode = 200;
					respResume.ErrorCode = 0;
					respResume.Data = manager.Query.Value.TransformResume();
				}
			}
			catch (Exception ex)
			{
				respResume.Message = ex.Message;
				respResume.MessageCode = 400;
				respResume.ErrorCode = 1;
				respResume.Data = null;
			}

			return Json(respResume);
		}

		[HttpGet]
		[Route("api/presence/resume/{zoneParams}/{regionParams}")]
		public IHttpActionResult GetResumeZoneRegion(string zoneParams, string regionParams)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respResume.Message = "data found";
					respResume.MessageCode = 200;
					respResume.ErrorCode = 0;
					respResume.Data = manager.Query.Value.TransformResumeZoneRegion(zoneParams, regionParams);
				}
			}
			catch (Exception ex)
			{
				respResume.Message = ex.Message;
				respResume.MessageCode = 400;
				respResume.ErrorCode = 1;
				respResume.Data = null;
			}

			return Json(respResume);
		}

		[HttpGet]
		[Route("api/presence/live/sweeper/{zoneParams}")]
		public IHttpActionResult GetLiveSweeper([FromBody]string zoneParams)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformLiveSweeper(zoneParams);
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
		[Route("api/presence/live/drainage/{zoneParams}")]
		public IHttpActionResult GetLiveDrainage([FromBody]string zoneParams)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformLiveDrainage(zoneParams);
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
		[Route("api/presence/live/zone")]
		public IHttpActionResult GetPrsenceLiveZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respZoneLive.Message = "data found";
					respZoneLive.MessageCode = 200;
					respZoneLive.ErrorCode = 0;
					respZoneLive.Data = manager.Query.Value.TransformLivePresenceZone();
				}
			}
			catch (Exception ex)
			{
				respZoneLive.Message = ex.Message;
				respZoneLive.MessageCode = 400;
				respZoneLive.ErrorCode = 1;
				respZoneLive.Data = null;
			}

			return Json(respZoneLive);
		}

		[HttpGet]
		[Route("api/presence/live/zone/{regionParams}")]
		public IHttpActionResult GetPrsenceLiveZoneParams(string regionParams)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respZoneLive.Message = "data found";
					respZoneLive.MessageCode = 200;
					respZoneLive.ErrorCode = 0;
					respZoneLive.Data = manager.Query.Value.TransformLivePresenceZone(regionParams);
				}
			}
			catch (Exception ex)
			{
				respZoneLive.Message = ex.Message;
				respZoneLive.MessageCode = 400;
				respZoneLive.ErrorCode = 1;
				respZoneLive.Data = null;
			}

			return Json(respZoneLive);
		}

		[HttpGet]
		[Route("api/presence/live/perform/zone")]
		public IHttpActionResult GetPerformLiveZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respZonePerformLive.Message = "data found";
					respZonePerformLive.MessageCode = 200;
					respZonePerformLive.ErrorCode = 0;
					respZonePerformLive.Data = manager.Query.Value.TransformPerformLive();
				}
			}
			catch (Exception ex)
			{
				respZonePerformLive.Message = ex.Message;
				respZonePerformLive.MessageCode = 400;
				respZonePerformLive.ErrorCode = 1;
				respZonePerformLive.Data = null;
			}

			return Json(respZonePerformLive);
		}

		[HttpGet]
		[Route("api/presence/zone")]
		public IHttpActionResult GetResumeZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respZone.Message = "data found";
					respZone.MessageCode = 200;
					respZone.ErrorCode = 0;
					respZone.Data = manager.Query.Value.TransformZoneTestResume();
				}
			}
			catch (Exception ex)
			{
				respZone.Message = ex.Message;
				respZone.MessageCode = 400;
				respZone.ErrorCode = 1;
				respZone.Data = null;
			}

			return Json(respZone);
		}

		[HttpGet]
		[Route("api/presence/zone/{regionParams}")]
		public IHttpActionResult GetResumeZoneRegion([FromUri] string regionParams)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respZone.Message = "data found";
					respZone.MessageCode = 200;
					respZone.ErrorCode = 0;
					respZone.Data = manager.Query.Value.TransformZoneTestResumeRegion(regionParams);
				}
			}
			catch (Exception ex)
			{
				respZone.Message = ex.Message;
				respZone.MessageCode = 400;
				respZone.ErrorCode = 1;
				respZone.Data = null;
			}

			return Json(respZone);
		}

		[HttpGet]
		[Route("api/presence/region")]
		public IHttpActionResult GetResumeRegion()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respRegion.Message = "data found";
					respRegion.MessageCode = 200;
					respRegion.ErrorCode = 0;
					respRegion.Data = manager.Query.Value.TransformRegionResume();
				}
			}
			catch (Exception ex)
			{
				respRegion.Message = ex.Message;
				respRegion.MessageCode = 400;
				respRegion.ErrorCode = 1;
				respRegion.Data = null;
			}

			return Json(respRegion);
		}

		[HttpGet]
		[Route("api/presence/perform/sweeper/{employeeId}")]
		public IHttpActionResult GetResumePerformSweeper([FromUri] long employeeId)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respSweeper.Message = "data found";
					respSweeper.MessageCode = 200;
					respSweeper.ErrorCode = 0;
					respSweeper.Data = manager.Query.Value.TransformResumePerformSweeper(employeeId);
				}
			}
			catch (Exception ex)
			{
				respSweeper.Message = ex.Message;
				respSweeper.MessageCode = 400;
				respSweeper.ErrorCode = 1;
				respSweeper.Data = null;
			}

			return Json(respSweeper);
		}

		[HttpGet]
		[Route("api/presence/perform/drainage/{employeeId}")]
		public IHttpActionResult GetResumePerformDrainage([FromUri] long employeeId)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respDrainage.Message = "data found";
					respDrainage.MessageCode = 200;
					respDrainage.ErrorCode = 0;
					respDrainage.Data = manager.Query.Value.TransformResumePerformDrainage(employeeId);
				}
			}
			catch (Exception ex)
			{
				respDrainage.Message = ex.Message;
				respDrainage.MessageCode = 400;
				respDrainage.ErrorCode = 1;
				respDrainage.Data = null;
			}

			return Json(respDrainage);
		}

		[HttpGet]
		[Route("api/presence/perform/garbage/{employeeId}")]
		public IHttpActionResult GetResumePerformGarbage([FromUri] long employeeId)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respGarbage.Message = "data found";
					respGarbage.MessageCode = 200;
					respGarbage.ErrorCode = 0;
					respGarbage.Data = manager.Query.Value.TransformResumePerformGarbage(employeeId);
				}
			}
			catch (Exception ex)
			{
				respGarbage.Message = ex.Message;
				respGarbage.MessageCode = 400;
				respGarbage.ErrorCode = 1;
				respGarbage.Data = null;
			}

			return Json(respGarbage);
		}

		[HttpGet]
		[Route("api/presence/perform/headzone/{employeeId}")]
		public IHttpActionResult GetResumePerformHeadZone([FromUri] long employeeId)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respHeadZone.Message = "data found";
					respHeadZone.MessageCode = 200;
					respHeadZone.ErrorCode = 0;
					respHeadZone.Data = manager.Query.Value.TransformResumePerformHeadZone(employeeId);
				}
			}
			catch (Exception ex)
			{
				respHeadZone.Message = ex.Message;
				respHeadZone.MessageCode = 400;
				respHeadZone.ErrorCode = 1;
				respHeadZone.Data = null;
			}

			return Json(respHeadZone);
		}

		[HttpGet]
		[Route("api/presence/perform/zone")]
		public IHttpActionResult GetResumePerformZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respZonePerform.Message = "data found";
					respZonePerform.MessageCode = 200;
					respZonePerform.ErrorCode = 0;
					respZonePerform.Data = manager.Query.Value.TransformPerformZone();
				}
			}
			catch (Exception ex)
			{
				respZonePerform.Message = ex.Message;
				respZonePerform.MessageCode = 400;
				respZonePerform.ErrorCode = 1;
				respZonePerform.Data = null;
			}

			return Json(respZonePerform);
		}

		[HttpGet]
		[Route("api/presence/perform/zone/{regionName}")]
		public IHttpActionResult GetResumePerformZone([FromUri] string regionName)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respZonePerform.Message = "data found";
					respZonePerform.MessageCode = 200;
					respZonePerform.ErrorCode = 0;
					respZonePerform.Data = manager.Query.Value.TransformPerformZone(regionName);
				}
			}
			catch (Exception ex)
			{
				respZonePerform.Message = ex.Message;
				respZonePerform.MessageCode = 400;
				respZonePerform.ErrorCode = 1;
				respZonePerform.Data = null;
			}

			return Json(respZonePerform);
		}

		[HttpGet]
		[Route("api/presence/perform/region")]
		public IHttpActionResult GetResumePerformRegion()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respRegionPerform.Message = "data found";
					respRegionPerform.MessageCode = 200;
					respRegionPerform.ErrorCode = 0;
					respRegionPerform.Data = manager.Query.Value.TransformPerformRegion();
				}
			}
			catch (Exception ex)
			{
				respRegionPerform.Message = ex.Message;
				respRegionPerform.MessageCode = 400;
				respRegionPerform.ErrorCode = 1;
				respRegionPerform.Data = null;
			}

			return Json(respRegionPerform);
		}

		[HttpGet]
		[Route("api/presence/perform")]
		public IHttpActionResult GetPerform()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respEmployee.Message = "data found";
					respEmployee.MessageCode = 200;
					respEmployee.ErrorCode = 0;
					respEmployee.Data = manager.Query.Value.TransformPerform();
				} 
			}
			catch (Exception ex)
			{
				respEmployee.Message = ex.Message;
				respEmployee.MessageCode = 400;
				respEmployee.ErrorCode = 1;
				respEmployee.Data = null;
			}

			return Json(respEmployee);
		}

		[HttpGet]
		[Route("api/presence/perform/{zoneName}")]
		public IHttpActionResult GetPerform([FromUri] string zoneName)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respEmployee.Message = "data found";
					respEmployee.MessageCode = 200;
					respEmployee.ErrorCode = 0;
					respEmployee.Data = manager.Query.Value.TransformPerform(zoneName);
				}
			}
			catch (Exception ex)
			{
				respEmployee.Message = ex.Message;
				respEmployee.MessageCode = 400;
				respEmployee.ErrorCode = 1;
				respEmployee.Data = null;
			}

			return Json(respEmployee);
		}

		private struct RespBoolean
		{
			[JsonProperty("message")]
			public string Message { get; set; }

			[JsonProperty("messageCode")]
			public int MessageCode { get; set; }

			[JsonProperty("errorCode")]
			public int ErrorCode { get; set; }

			[JsonProperty("data")]
			public bool? Data { get; set; }
		}

		[HttpGet]
		[Route("api/presence/check/{id}")]
		public IHttpActionResult GetExistHeadZone([FromUri] long id)
		{
			var respBool = new RespBoolean();
			try
			{
				using (var manager = new PresenceAdapter())
				{
					respBool.Message = "data found";
					respBool.MessageCode = 200;
					respBool.ErrorCode = 0;
					respBool.Data = manager.Query.Value.TransformExistPresence(id);
				}
			}
			catch (Exception ex)
			{
				respBool.Message = ex.Message;
				respBool.MessageCode = 400;
				respBool.ErrorCode = 1;
				respBool.Data = null;
			}

			return Json(respBool);
		}

		[HttpPost]
		[Route("api/presence")]
		public IHttpActionResult Post()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var formData = HttpContext.Current.Request.Params;

					var dto = new PresenceDTO
					{
						Coordinate = formData["coordinate"],
						EmployeeId = Convert.ToInt64(formData["employeeId"]),
						LivePhoto = Utilities.Util.Compress(),
						Shift = formData["shift"],
						DateOfPresence = DateTime.Now
					};

					var result = manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.PresenceId);
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
		[Route("api/presence")]
		public IHttpActionResult Put([FromBody] PresenceDTO dto)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var result = manager.Updater.Value.Update(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.PresenceId);
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

		[HttpDelete]
		[Route("api/presence/{id}")]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new PresenceAdapter())
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

		[HttpDelete]
		[Route("api/presence/all")]
		public IHttpActionResult DeleteAll()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					manager.Deleter.Value.DeleteAll();

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

		[HttpDelete]
		[Route("api/presence/score")]
		public IHttpActionResult DeleteData()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					manager.Deleter.Value.DeleteAllScore();

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
