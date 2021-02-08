using System;

namespace DLHK_API.Extensions
{
	public static class NumberExtension
	{
		public static long ToLong(this string value)
		{
			return Convert.ToInt64(value);
		}

		public static int ToInt(this string value)
		{
			return Convert.ToInt32(value);
		}

		public static int ToInt(this object value)
		{
			return Convert.ToInt32(value);
		}

		public static long ToLong(this object value)
		{
			return Convert.ToInt64(value);
		}
	}
}