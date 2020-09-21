using Newtonsoft.Json;
using System;

namespace Core.Manager.PresenceManager
{
	public class PresenceDTO
	{
		[JsonProperty("presenceId")]
		public long PresenceId { get; set; }

		[JsonProperty("livePhoto")]
		public byte[] LivePhoto { get; set; }

		[JsonProperty("coordinate")]
		public string Coordinate { get; set; }

		[JsonProperty("location")]
		public string Location { get; set; }

		[JsonProperty("dateOfPresence")]
		public DateTime? DateOfPresence { get; set; }

		[JsonProperty("timeOfPresence")]
		public string TimeOfPresence { get; set; }

		[JsonProperty("presenceStatus")]
		public string PresenceStatus { get; set; }

		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }

		[JsonProperty("employeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("employeeName")]
		public string EmployeeName { get; set; }

		[JsonProperty("zoneName")]
		public string ZoneName { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }

		[JsonProperty("shift")]
		public string Shift { get; set; }

		[JsonProperty("counter")]
		public int? Counter { get; set; }
	}

	public class PresenceResumeDTO
	{
		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }

		[JsonProperty("photo")]
		public byte[] Photo { get; set; }

		[JsonProperty("employeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("employeeName")]
		public string EmployeeName { get; set; }

		[JsonProperty("locationContract")]
		public string LocationContract { get; set; }

		[JsonProperty("presence")]
		public int PresenceTotal { get; set; }

		[JsonProperty("leave")]
		public int Leave { get; set; }

		[JsonProperty("absence")]
		public int Absence { get; set; }

		[JsonProperty("percentage")]
		public int Percentage { get; set; }

		[JsonProperty("zoneName")]
		public string ZoneName { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("shift")]
		public string Shift { get; set; }
	}

	public class RegionResumeDTO
	{
		[JsonProperty("regionId")]
		public long RegionId { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("zoneTotal")]
		public int ZoneTotal { get; set; }

		[JsonProperty("employeTotal")]
		public int EmployeeTotal { get; set; }

		[JsonProperty("presence")]
		public int PresenceTotal { get; set; }

		[JsonProperty("leave")]
		public int Leave { get; set; }

		[JsonProperty("absence")]
		public int Absence { get; set; }

		[JsonProperty("percentage")]
		public int Percentage { get; set; }
	}

	public class ZoneResumeDTO
	{
		[JsonProperty("zoneId")]
		public long? ZoneId { get; set; }

		[JsonProperty("codeZone")]
		public string CodeZone { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("totalEmployee")]
		public int TotalEmployee { get; set; }

		[JsonProperty("presence")]
		public int PresenceTotal { get; set; }

		[JsonProperty("leave")]
		public int Leave { get; set; }

		[JsonProperty("absence")]
		public int Absence { get; set; }

		[JsonProperty("percentage")]
		public int Percentage { get; set; }
	}

	public class ZonePropertyDTO
	{
		[JsonProperty("zoneId")]
		public long? ZoneId { get; set; }

		[JsonProperty("codeZone")]
		public string CodeZone { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("totalEmployee")]
		public int TotalEmployee { get; set; }

		[JsonProperty("value")]
		public ZoneValueDTO Value { get; set; }
	}

	public class ZonePerformLiveDTO
	{
		[JsonProperty("zoneId")]
		public long? ZoneId { get; set; }

		[JsonProperty("codeZone")]
		public string CodeZone { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("percentage")]
		public int? Percentage { get; set; }
	}

	public class EmployeePerformDTO
	{
		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }

		[JsonProperty("employeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("employeeName")]
		public string EmployeeName { get; set; }

		[JsonProperty("photo")]
		public byte[] Photo { get; set; }

		[JsonProperty("locationContract")]
		public string LocationContract { get; set; }

		[JsonProperty("zoneName")]
		public string ZoneName { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("shift")]
		public string Shift { get; set; }

		[JsonProperty("percentage")]
		public int? Percentage { get; set; }
	}

	public class ZoneValueDTO
	{
		[JsonProperty("presence")]
		public int PresenceTotal { get; set; }

		[JsonProperty("leave")]
		public int Leave { get; set; }

		[JsonProperty("absence")]
		public int Absence { get; set; }

		[JsonProperty("percentage")]
		public int Percentage { get; set; }
	}

	public class PerformSweeperDTO
	{
		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }

		[JsonProperty("employeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("employeeName")]
		public string EmployeeName { get; set; }

		[JsonProperty("zoneName")]
		public string ZoneName { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("dicipline")]
		public int? Dicipline { get; set; }

		[JsonProperty("completeness")]
		public int? Completeness { get; set; }

		[JsonProperty("road")]
		public int? Road { get; set; }

		[JsonProperty("sidewalk")]
		public int? Sidewalk { get; set; }

		[JsonProperty("waterRope")]
		public int? WaterRope { get; set; }

		[JsonProperty("roadMedian")]
		public int? RoadMedian { get; set; }
	}

	public class PerformGarbageDTO
	{
		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }

		[JsonProperty("employeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("employeeName")]
		public string EmployeeName { get; set; }

		[JsonProperty("zoneName")]
		public string ZoneName { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("dicipline")]
		public int? Dicipline { get; set; }

		[JsonProperty("tps")]
		public int? TPS { get; set; }

		[JsonProperty("separation")]
		public int? Separation { get; set; }

		[JsonProperty("calculation")]
		public int? Calculation { get; set; }

		[JsonProperty("volumeOrganic")]
		public int? VolumeOfOrganic { get; set; }

		[JsonProperty("volumeAnorganic")]
		public int? VolumeOfAnorganic { get; set; }
	}

	public class PerformDrainageDTO
	{
		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }

		[JsonProperty("employeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("employeeName")]
		public string EmployeeName { get; set; }

		[JsonProperty("zoneName")]
		public string ZoneName { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("dicipline")]
		public int? Dicipline { get; set; }

		[JsonProperty("completeness")]
		public int? Completeness { get; set; }

		[JsonProperty("cleanliness")]
		public int? Cleanliness { get; set; }

		[JsonProperty("sediment")]
		public int? Sediment { get; set; }

		[JsonProperty("weed")]
		public int? Weed { get; set; }
	}

	public class PerformHeadZoneDTO
	{
		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }

		[JsonProperty("employeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("employeeName")]
		public string EmployeeName { get; set; }

		[JsonProperty("zoneName")]
		public string ZoneName { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("diciplinePresence")]
		public int? DiciplinePresence { get; set; }

		[JsonProperty("firstSession")]
		public int? FirstSession { get; set; }

		[JsonProperty("secondSession")]
		public int? SecondSession { get; set; }

		[JsonProperty("thirdSession")]
		public int? ThirdSession { get; set; }

		[JsonProperty("cleanlinessZone")]
		public int? CleanlinessOfZone { get; set; }

		[JsonProperty("completenessTeam")]
		public int? CompletenessOfTeam { get; set; }

		[JsonProperty("dataOfGarbage")]
		public int? DataOfGarbage { get; set; }
	}

	public class ZonePerformDTO
	{
		[JsonProperty("zoneId")]
		public long? ZoneId { get; set; }

		[JsonProperty("codeZone")]
		public string CodeZone { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("totalEmployee")]
		public int TotalEmployee { get; set; }

		[JsonProperty("percentage")]
		public int? Percentage { get; set; }
	}

	public class RegionPerformDTO
	{
		[JsonProperty("regionId")]
		public long RegionId { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("zoneTotal")]
		public int ZoneTotal { get; set; }

		[JsonProperty("employeTotal")]
		public int EmployeeTotal { get; set; }
		
		[JsonProperty("percentage")]
		public int? Percentage { get; set; }
	}

	public class DashboardDTO
	{
		[JsonProperty("employees")]
		public int Employees { get; set; }

		[JsonProperty("presences")]
		public int Presences { get; set; }

		[JsonProperty("performances")]
		public int Performances { get; set; }

		[JsonProperty("score")]
		public int Score { get; set; }
	}
}
