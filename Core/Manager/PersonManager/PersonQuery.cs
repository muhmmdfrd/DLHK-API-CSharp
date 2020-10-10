using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.PersonManager
{
	public class PersonQuery : AsistanceBase<PersonAdapter, Person>
	{
		public PersonQuery(PersonAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Person> Get()
		{
			return Manager.Database.People;
		}

		public List<PersonDTO> Transform()
		{
			return (from val in Get()
					where val.Jobdesk.Equals("Employee")
					select new PersonDTO()
					{
						PersonId = val.PersonId,
						NIK = val.NIK,
						PersonName = val.PersonName,
						Address = val.Address,
						DateOfBirth = val.DateOfBirth,
						PlaceOfBirth = val.PlaceOfBirth,
						Phone = val.Phone,
						Email = val.Email,
						LastDegree = val.LastDegree,
						PreviousJob = val.PreviousJob,
						NameOfCouple = val.NameOfCouple,
						JobOfCouple = val.JobOfCouple,
						TotalChild = val.TotalChild,
						AppLetter = val.AppLetter,
						Jobdesk = val.Jobdesk,
						KTP = val.KTP,
						Photo = val.Photo,
						Sertificate = val.Sertificate,
					}).ToList();
		}

		public PersonDTO TransformId(long id)
		{
			return (from val in Get()
					select new PersonDTO()
					{
						PersonId = val.PersonId,
						PersonName = val.PersonName,
						Address = val.Address,
						DateOfBirth = val.DateOfBirth,
						PlaceOfBirth = val.PlaceOfBirth,
						NIK = val.NIK,
						Phone = val.Phone,
						LastDegree = val.LastDegree,
						PreviousJob = val.PreviousJob,
						NameOfCouple = val.NameOfCouple,
						JobOfCouple = val.JobOfCouple,
						AppLetter = val.AppLetter,
						Jobdesk = val.Jobdesk,
						KTP = val.KTP,
						Photo = val.Photo,
						Sertificate = val.Sertificate,
						Email = val.Email,
						TotalChild = val.TotalChild
					}).FirstOrDefault(x => x.PersonId == id);
		}

		public PersonDTO TransformName(string name)
		{
			return (from val in Get()
					where val.PersonName.Equals(name)
					select new PersonDTO()
					{
						PersonId = val.PersonId,
						PersonName = val.PersonName,
						Address = val.Address,
						DateOfBirth = val.DateOfBirth,
						PlaceOfBirth = val.PlaceOfBirth,
						NIK = val.NIK,
						Phone = val.Phone,
						LastDegree = val.LastDegree,
						PreviousJob = val.PreviousJob,
						NameOfCouple = val.NameOfCouple,
						JobOfCouple = val.JobOfCouple,
						AppLetter = val.AppLetter,
						Jobdesk = val.Jobdesk,
						KTP = val.KTP,
						Photo = val.Photo,
						Sertificate = val.Sertificate,
						Email = val.Email,
						TotalChild = val.TotalChild
					}).FirstOrDefault();
		}

		public List<PersonDTO> TransformApplicant()
		{
			return (from val in Get()
					where val.Jobdesk != "Employee" && 
					val.Jobdesk != "Interview"
					select new PersonDTO()
					{
						PersonId = val.PersonId,
						PersonName = val.PersonName,
						Address = val.Address,
						DateOfBirth = val.DateOfBirth,
						PlaceOfBirth = val.PlaceOfBirth,
						Phone = val.Phone,
						LastDegree = val.LastDegree,
						PreviousJob = val.PreviousJob,
						NIK = val.NIK,
						NameOfCouple = val.NameOfCouple,
						JobOfCouple = val.JobOfCouple,
						AppLetter = val.AppLetter,
						Jobdesk = val.Jobdesk,
						KTP = val.KTP,
						Photo = val.Photo,
						Sertificate = val.Sertificate,
						Email = val.Email,
						TotalChild = val.TotalChild
					}).ToList();
		}

		public List<PersonDTO> TransformInterviewed()
		{
			return (from val in Get()
					where val.Jobdesk.Equals("Interview")
					select new PersonDTO()
					{
						PersonId = val.PersonId,
						PersonName = val.PersonName,
						Address = val.Address,
						DateOfBirth = val.DateOfBirth,
						PlaceOfBirth = val.PlaceOfBirth,
						Phone = val.Phone,
						LastDegree = val.LastDegree,
						PreviousJob = val.PreviousJob,
						NameOfCouple = val.NameOfCouple,
						JobOfCouple = val.JobOfCouple,
						NIK = val.NIK,
						AppLetter = val.AppLetter,
						Jobdesk = val.Jobdesk,
						KTP = val.KTP,
						Photo = val.Photo,
						Sertificate = val.Sertificate,
						Email = val.Email,
						TotalChild = val.TotalChild
					}).ToList();
		}

		public PersonDTO TransformApplicantId(long id)
		{
			return (from val in Get()
					where val.Jobdesk != "Employee" &&
					val.Jobdesk != "Interview"
					select new PersonDTO()
					{
						NIK = val.NIK,
						PersonId = val.PersonId,
						PersonName = val.PersonName,
						Address = val.Address,
						DateOfBirth = val.DateOfBirth,
						PlaceOfBirth = val.PlaceOfBirth,
						Phone = val.Phone,
						LastDegree = val.LastDegree,
						PreviousJob = val.PreviousJob,
						NameOfCouple = val.NameOfCouple,
						JobOfCouple = val.JobOfCouple,
						AppLetter = val.AppLetter,
						Jobdesk = val.Jobdesk,
						KTP = val.KTP,
						Photo = val.Photo,
						Sertificate = val.Sertificate,
						Email = val.Email,
						TotalChild = val.TotalChild
					}).FirstOrDefault(x => x.PersonId == id);
		}
	}
}
