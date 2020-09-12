using Repository;
using System.Transactions;

namespace Core.Manager.PersonManager
{
	public class PersonCreator : AsistanceBase<PersonAdapter, Person>
	{
		public PersonCreator(PersonAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Person Save(PersonDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Person
				{
					Address = dto.Address,
					DateOfBirth = dto.DateOfBirth,
					JobOfCouple = dto.JobOfCouple,
					PreviousJob = dto.PreviousJob,
					PlaceOfBirth = dto.PlaceOfBirth,
					Phone = dto.Phone,
					PersonName = dto.PersonName,
					NIK = dto.NIK,
					LastDegree = dto.LastDegree,
					NameOfCouple = dto.NameOfCouple,
					Jobdesk = dto.Jobdesk,
					Email = dto.Email,
					TotalChild = dto.TotalChild
				};

				Manager.Database.People.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
