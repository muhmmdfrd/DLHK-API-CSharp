using Core.Manager.TransacManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class TransacController : ApiController
    {
		private readonly ApiResponse<List<TransacDTO>> respList = new ApiResponse<List<TransacDTO>>();
		private readonly ApiResponse<TransacDTO> resp = new ApiResponse<TransacDTO>();

		[HttpGet]
		[Route("api/transac")]
		public IHttpActionResult GetAll()
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformAll();
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
		[Route("api/transac/in")]
		public IHttpActionResult GetIn()
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformIn();
				}
			}
			catch (Exception ex)
			{
				respList.Message = ex.Message;
				respList.MessageCode = 400;
				respList.ErrorCode = 0;
				respList.Data = null;
			}

			return Json(respList);
		}

		[HttpGet]
		[Route("api/transac/in/{id}")]
		public IHttpActionResult GetInId(long id)
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					resp.Message = "data found";
					resp.MessageCode = 200;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformInId(id);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 0;
				resp.Data = null;
			}

			return Json(resp);
		}

		[HttpGet]
		[Route("api/transac/out")]
		public IHttpActionResult GetOut()
		{
			try
			{
				using (var manager = new TransacAdapter())
				{
					respList.Message = "data found";
					respList.MessageCode = 200;
					respList.ErrorCode = 0;
					respList.Data = manager.Query.Value.TransformOut();
				}
			}
			catch (Exception ex)
			{
				respList.Message = ex.Message;
				respList.MessageCode = 400;
				respList.ErrorCode = 0;
				respList.Data = null;
			}

			return Json(respList);
		}

		[HttpGet]
		[Route("api/transac/out/{id}")]
		public IHttpActionResult GetOutId(long id)
		{
			try
			{
				using (var manager = new TransacAdapter())
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
				resp.ErrorCode = 0;
				resp.Data = null;
			}

			return Json(resp);
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

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformInId(result.TransacId);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 0;
				resp.Data = null;
			}

			return Json(resp);
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
		
					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.TransacId);
				}
			}
			catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 400;
				resp.ErrorCode = 0;
				resp.Data = null;
			}

			return Json(resp);
		}
	}
}
