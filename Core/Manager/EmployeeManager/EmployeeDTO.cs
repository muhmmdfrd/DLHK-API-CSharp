using Newtonsoft.Json;
using System;

namespace Core.Manager.EmployeeManager
{
	public class EmployeeDTO
	{
		[JsonProperty("employeeId")]
		public long EmployeeId { get; set; }

		[JsonProperty("name")]
		public string NamePerson { get; set; }

		[JsonProperty("employeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("age")]
		public int Age { get; set; }

		[JsonProperty("roleId")]
		public long? RoleId { get; set; }

		[JsonProperty("role")]
		public string Role { get; set; }

		[JsonProperty("bank")]
		public string Bank { get; set; }

		[JsonProperty("firstContract")]
		public DateTime? FirstContract { get; set; }

		[JsonProperty("lastContract")]
		public DateTime? LastContract { get; set; }

		[JsonProperty("locationContract")]
		public string LocationContract { get; set; }

		[JsonProperty("regionId")]
		public long? RegionId { get; set; }

		[JsonProperty("region")]
		public string Region { get; set; }

		[JsonProperty("zoneId")]
		public long? ZoneId { get; set; }

		[JsonProperty("zone")]
		public string ZoneName { get; set; }

		[JsonProperty("personId")]
		public long? PersonId { get; set; }

		[JsonProperty("shift")]
		public string Shift { get; set; }

		[JsonProperty("smartPresence")]
		public int? SmartPresence { get; set; }

		[JsonProperty("perform")]
		public int? Perform { get; set; }
	}
}
