using Core.Manager.PresenceManager;
using DLHK_API.Models;
using System;
using System.Web;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class PresenceController : ApiController
    {
		[HttpGet]
		[Route("api/presence")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new PresenceAdapter())
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
		[Route("api/presence/{zoneParams}/{statusParams}")]
		public IHttpActionResult GetZoneAndStatus([FromUri] string zoneParams, string statusParams)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformWithPhotoAndParam(statusParams, zoneParams);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/dashboard")]
		public IHttpActionResult GetDashboard()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformDashboard();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/dashboard-contract")]
		public IHttpActionResult GetDashboardContract()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformDashboardContract();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/dashboard-item")]
		public IHttpActionResult GetDashboardItem()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformDashboardItem();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/headregion")]
		public IHttpActionResult GetHeadRegion()
		{
			try
			{
				using (var manager = new PresenceAdapter())
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
		[Route("api/presence/{zoneName}/{regionName}/sweeper/{shift}")]
		public IHttpActionResult GetSweeper([FromUri] string zoneName, string regionName, string shift)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformSweeper(zoneName, regionName, shift);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/{zoneName}/{regionName}/drainage/{shift}")]
		public IHttpActionResult GetDrainage([FromUri] string zoneName, string regionName, string shift)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformDrainage(zoneName, regionName, shift);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/{regionName}/headzone")]
		public IHttpActionResult GetHeadZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformHeadZone();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/{zoneName}/{regionName}/garbage/{shift}")]
		public IHttpActionResult GetGarbage([FromUri] string zoneName, string regionName, string shift)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformGarbage(zoneName, regionName, shift);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/photo")]
		public IHttpActionResult GetWithPhoto()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformWithPhoto();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/resume")]
		public IHttpActionResult GetResume()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformResume();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/filter/{start}/{end}")]
		public IHttpActionResult GetResume([FromUri] string start, string end)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{var data = manager.Query.Value.TransformResume(start, end);
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/resume/{zoneParams}/{regionParams}")]
		public IHttpActionResult GetResumeZoneRegion(string zoneParams, string regionParams)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformResumeZoneRegion(zoneParams, regionParams);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/live/zone")]
		public IHttpActionResult GetPrsenceLiveZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformLivePresenceZone();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/live/zone/{regionParams}")]
		public IHttpActionResult GetPrsenceLiveZoneParams(string regionParams)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformLivePresenceZone(regionParams);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/live/perform/zone")]
		public IHttpActionResult GetPerformLiveZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformPerformLive();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/zone")]
		public IHttpActionResult GetResumeZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformZoneTestResume();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/zone/{regionParams}")]
		public IHttpActionResult GetResumeZoneRegion([FromUri] string regionParams)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformZoneTestResumeRegion(regionParams);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/region")]
		public IHttpActionResult GetResumeRegion()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformRegionResume();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform/sweeper/{employeeId}")]
		public IHttpActionResult GetResumePerformSweeper([FromUri] long employeeId)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{var data = manager.Query.Value.TransformResumePerformSweeper(employeeId);
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform/drainage/{employeeId}")]
		public IHttpActionResult GetResumePerformDrainage([FromUri] long employeeId)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformResumePerformDrainage(employeeId);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform/garbage/{employeeId}")]
		public IHttpActionResult GetResumePerformGarbage([FromUri] long employeeId)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformResumePerformGarbage(employeeId);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform/headzone/{employeeId}")]
		public IHttpActionResult GetResumePerformHeadZone([FromUri] long employeeId)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformResumePerformHeadZone(employeeId);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform/zone")]
		public IHttpActionResult GetResumePerformZone()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformPerformZone();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform/zone/{regionName}")]
		public IHttpActionResult GetResumePerformZone([FromUri] string regionName)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformPerformZone(regionName);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform/region")]
		public IHttpActionResult GetResumePerformRegion()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformPerformRegion();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform")]
		public IHttpActionResult GetPerform()
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformPerform();

					return Json(Response.Success(data));
				} 
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform/filter/{start}/{end}")]
		public IHttpActionResult GetResumePerformFilter([FromUri] string start, string end)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformPerformFilter(start, end);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/perform/{zoneName}")]
		public IHttpActionResult GetPerform([FromUri] string zoneName)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformPerform(zoneName);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/presence/check/{id}")]
		public IHttpActionResult GetExistHeadZone([FromUri] long id)
		{
			try
			{
				using (var manager = new PresenceAdapter())
				{
					var data = manager.Query.Value.TransformExistPresence(id);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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
					var data = manager.Query.Value.TransformId(result.PresenceId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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
					var data = manager.Query.Value.TransformId(result.PresenceId);

					return Json(Response.Updated(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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

					return Json(Response.Deleted());
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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

					return Json(Response.Deleted());
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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