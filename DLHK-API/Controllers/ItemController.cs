using Core.Manager.ItemManager;
using DLHK_API.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class ItemController : ApiController
	{
		private readonly ApiResponse<List<ItemDTO>> respList = new ApiResponse<List<ItemDTO>>();
		private readonly ApiResponse<ItemDTO> resp = new ApiResponse<ItemDTO>();

		[HttpGet]
		[Route("api/item")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new ItemAdapter())
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
		[Route("api/item/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new ItemAdapter())
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

		[HttpPost]
		[Route("api/item")]
		public IHttpActionResult Post()
		{
			try
			{
				using (var manager = new ItemAdapter())
				{
					var formData = HttpContext.Current.Request.Params;
					var upload = Utilities.Util.Compress();

					var dto = new ItemDTO
					{
						ItemName = formData["itemName"],
						ItemQty = Convert.ToInt32(formData["itemQty"]),
						Note = formData["note"],
						ItemPhoto = upload,
						CategoryId = Convert.ToInt64(formData["categoryId"]),
						SuplierName = formData["suplierName"]
					};

					manager.Creator.Value.Save(dto);

					resp.Message = "data inserted";
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

			return Json(resp);
		}

		[HttpPut]
		[Route("api/item")]
		public IHttpActionResult Put()
		{
			try
			{
				using (var manager = new ItemAdapter())
				{
					var formData = HttpContext.Current.Request.Params;
					var util = new Utilities.Util();

					var dto = new ItemDTO()
					{
						ItemId = Convert.ToInt64(formData["itemId"]),
						ItemName = formData["itemName"],
						ItemQty = Convert.ToInt32(formData["itemQty"]),
						Note = formData["note"],
						ItemPhoto = util.UploadPhotoToBinary(),
					};

					var result = manager.Updater.Value.Update(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.ItemId);
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
		[Route("api/item/qty")]
		public IHttpActionResult PutQty()
		{
			try
			{
				using (var manager = new ItemAdapter())
				{
					var formData = HttpContext.Current.Request.Params;
					var util = new Utilities.Util();

					var dto = new ItemDTO()
					{
						ItemId = Convert.ToInt64(formData["itemId"]),
						ItemQty = Convert.ToInt32(formData["itemQty"])
					};

					var result = manager.Updater.Value.UpdateQty(dto);

					resp.Message = "data updated";
					resp.MessageCode = 202;
					resp.ErrorCode = 0;
					resp.Data = manager.Query.Value.TransformId(result.ItemId);
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
		[Route("api/item/{id}")]
		public IHttpActionResult Delete([FromUri] long id)
		{
			try
			{
				using (var manager = new ItemAdapter())
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
