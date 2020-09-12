using Newtonsoft.Json;
using System;

namespace Core.Manager.PersonManager
{
	public class PersonDTO
	{
		[JsonProperty("personId")]
		public long PersonId { get; set; }

		[JsonProperty("nik")]
		public string NIK { get; set; }

		[JsonProperty("personName")]
		public string PersonName { get; set; }

		[JsonProperty("placeOfBirth")]
		public string PlaceOfBirth { get; set; }

		[JsonProperty("dateOfBirth")]
		public DateTime? DateOfBirth { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("lastDegree")]
		public string LastDegree { get; set; }

		[JsonProperty("previousJob")]
		public string PreviousJob { get; set; }

		[JsonProperty("nameOfCouple")]
		public string NameOfCouple { get; set; }

		[JsonProperty("jobOfCouple")]
		public string JobOfCouple { get; set; }

		[JsonProperty("totalChild")]
		public int? TotalChild { get; set; }

		[JsonProperty("photo")]
		public byte[] Photo { get; set; }

		[JsonProperty("ktp")]
		public byte[] KTP { get; set; }

		[JsonProperty("sertificate")]
		public byte[] Sertificate { get; set; }

		[JsonProperty("appLetter")]
		public byte[] AppLetter { get; set; }

		[JsonProperty("jobDesc")]
		public string Jobdesk { get; set; }
	}
}
