using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class ExcelController : ApiController
    {        
        [HttpPost]
        public IHttpActionResult ImportFromExcel()
        {
            try
			{
				new Utilities.Util().ExcelUpload();

				return Json(Response.Success(""));
			}
            catch (Exception ex)
			{
				return Json(Response.Fail(ex.Message));
			}
		}
    }
}
