using Core.Manager.TransacManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class TransacController : ApiController
    {
		[HttpGet]
		[Route("api/transac")]
		public IHttpActionResult GetAll()
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					var data = manager.Query.Value.TransformAll();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/transac/in")]
		public IHttpActionResult GetIn()
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					var data = manager.Query.Value.TransformIn();
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/transac/in/{id}")]
		public IHttpActionResult GetInId(long id)
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					var data = manager.Query.Value.TransformInId(id);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/transac/in/{start}/{end}")]
		public IHttpActionResult GetTransacInDate([FromUri] string start, string end)
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					var data = manager.Query.Value.TransformInDate(start, end);
					
					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/transac/out")]
		public IHttpActionResult GetOut()
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					var data = manager.Query.Value.TransformOut();

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpGet]
		[Route("api/transac/out/{id}")]
		public IHttpActionResult GetOutId(long id)
		{
			try
			{
				using (var manager = new TransacAdapter())
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
		[Route("api/transac/out/{start}/{end}")]
		public IHttpActionResult GetTransacOutDate([FromUri] string start, string end)
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					var data = manager.Query.Value.TransformOutDate(start, end);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPost]
		[Route("api/transac/in")]
		public IHttpActionResult PostIn([FromBody] TransacInDTO dto)
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					var result = manager.Creator.Value.DataIn(dto);
					var data = manager.Query.Value.TransformInId(result.TransacId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}

		[HttpPost]
		[Route("api/transac/out")]
		public IHttpActionResult PostOut([FromBody] TransacOutDTO dto)
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					var result = manager.Creator.Value.DataOut(dto);
					var data = manager.Query.Value.TransformId(result.TransacId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}
	}
}
