namespace DLHK_API.Models
{
	public static class Response
	{
		public static object Success(object value)
		{
			return new { message = "data found", messageCode = 200, errorCode = 0, data = value };
		}

		public static object Updated(object value)
		{
			return new { message = "data updated", messageCode = 200, errorCode = 0, data = value };
		}

		public static object Deleted()
		{
			return new { message = "data deleted", messageCode = 202, errorCode = 0, data = "" };
		}

		public static object Fail(string message)
		{
			return new { message = message, messageCode = 400, errorCode = 1, data = "" };
		}
	}
}