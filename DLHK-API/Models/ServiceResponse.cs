namespace DLHK_API.Models
{
	public static class ServiceResponse
	{
		public static object Success(object value)
		{
			return new { Success = true, Message = "", Values = value };
		}

		public static object Success(string message, object value)
		{
			return new { Success = true, Message = message, Values = value };
		}

		public static object Error(string message)
		{
			return new { Success = false, Message = message, Values = "" };
		}
	}
}