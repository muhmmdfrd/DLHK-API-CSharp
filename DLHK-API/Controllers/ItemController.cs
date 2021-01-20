using Core.Manager.ItemManager;
using DLHK_API.Models;
using System;
using System.Web;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class ItemController : ApiController
	{
		[HttpGet]
		[Route("api/item")]
		public IHttpActionResult Get()
		{
			try
			{
				using (var manager = new ItemAdapter())
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
		[Route("api/item/{id}")]
		public IHttpActionResult GetId([FromUri] long id)
		{
			try
			{
				using (var manager = new ItemAdapter())
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
						SuplierName = formData["suplierName"]
					};

					dto.CategoryId = dto.CategoryId == null ? 0 : Convert.ToInt32(formData["categoryId"]);

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
					var data = manager.Query.Value.TransformId(result.ItemId);

					return Json(Response.Success(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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
					var data = manager.Query.Value.TransformId(result.ItemId);

					return Json(Response.Updated(data));
				}
			}
			catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
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
