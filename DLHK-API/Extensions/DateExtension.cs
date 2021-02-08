using System;

namespace DLHK_API.Extensions
{
	public static class DateExtension
	{
		public static DateTime ToDate(this object value)
		{
			return Convert.ToDateTime(value);
		}
	}
}