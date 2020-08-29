using Repository;
using System;
using System.Linq;
using System.Transactions;

namespace Core.Manager.PersonManager
{
	public class PersonUpdater : AsistanceBase<PersonAdapter, Person>
	{
		public PersonUpdater(PersonAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Person Update(PersonDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == dto.PersonId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Address = dto.Address;
				exist.DateOfBirth = dto.DateOfBirth;
				exist.JobOfCouple = dto.JobOfCouple;
				exist.LastDegree = dto.LastDegree;
				exist.NameOfCouple = dto.NameOfCouple;
				exist.PersonName = dto.PersonName;
				exist.Phone = dto.Phone;
				exist.PlaceOfBirth = dto.PlaceOfBirth;
				exist.PreviousJob = dto.PreviousJob;
				exist.Jobdesk = dto.Jobdesk ?? exist.Jobdesk;
				exist.TotalChild = dto.TotalChild;
				exist.Email = dto.Email;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}

		public void KTP(PersonDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == dto.PersonId);

				if (exist == null)
					throw new Exception("data not found");

				exist.KTP = dto.KTP;

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}


		public void Letter(PersonDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == dto.PersonId);

				if (exist == null)
					throw new Exception("data not found");

				exist.AppLetter = dto.AppLetter;

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}

		public void Sertificate(PersonDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == dto.PersonId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Sertificate = dto.Sertificate;

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}

		public void Photo(PersonDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == dto.PersonId);

				if (exist == null)
					throw new Exception("data not found");

				exist.Photo = dto.Photo;

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}

		public void Accepted(long? personIdParams)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonId == personIdParams);

				if (exist == null)
					throw new Exception("data not found");

				exist.Jobdesk = "Employee";

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}

		public void InterviewPerson(string nameParams)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get().FirstOrDefault(x => x.PersonName.Equals(nameParams));

				if (exist == null)
					throw new Exception("data not found");

				exist.Jobdesk = "Interview";

				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
	
}
