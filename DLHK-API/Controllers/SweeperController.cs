using Core.Manager.SweeperManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class SweeperController : ApiController
    {
		private readonly ApiResponse<List<SweeperDTO>> respList = new ApiResponse<List<SweeperDTO>>();
		private readonly ApiResponse<SweeperDTO> resp = new ApiResponse<SweeperDTO>();

		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new SweeperAdapter())
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

		[HttpPost]
		public IHttpActionResult Post([FromBody] SweeperDTO dto)
		{
			try
			{
				using (var manager = new SweeperAdapter())
				{
					var result = manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
					resp.MessageCode = 201;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.SweeperId);
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
		public IHttpActionResult Put([FromBody] SweeperDTO dto)
		{
			try
			{
				using (var manager = new SweeperAdapter())
				{
					var result = manager.Updater.Value.Update(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.SweeperId);
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
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new SweeperAdapter())
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
	}
}
