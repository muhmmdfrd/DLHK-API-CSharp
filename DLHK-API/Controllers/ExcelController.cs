using Core.Manager.EmployeeManager;
using DLHK_API.Models;
using System;
using System.Web.Http;

namespace DLHK_API.Controllers
{
	public class ExcelController : ApiController
    {
		private readonly ApiResponse<EmployeeDTO> resp = new ApiResponse<EmployeeDTO>();
        
        [HttpPost]
        public IHttpActionResult ImportFromExcel()
        {
            try
			{
				new Utilities.Util().ExcelUpload();

				resp.Message = "data uploaded";
				resp.MessageCode = 201;
				resp.ErrorCode = 0;
				resp.Data = null;
			}
            catch (Exception ex)
			{
				resp.Message = ex.Message;
				resp.MessageCode = 500;
				resp.ErrorCode = 1;
				resp.Data = null;
			}

			return Json(resp);
		}
    }
}
