using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Manager.ApplicantManager
{
	public class ApplicantCreator : AsistanceBase<ApplicantAdapter, Applicant>
	{
		public ApplicantCreator(ApplicantAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Applicant Save(ApplicantDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Applicant
				{
					ApplicantName = dto.ApplicantName,
					Email = dto.Email,
					Password = dto.Password
				};

				Manager.Database.Applicants.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}

		public Applicant Login(ApplicantDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Query.Value.Get()
					.FirstOrDefault(x => x.Email.Equals(dto.Email) && x.Password.Equals(dto.Password));

				var existEmail = Manager.PersonManager.Value.Query.Value.
					TransformApplicant()
					.Select(x => x.Email);

				foreach (var email in existEmail)
				{
					if (email == null)
					{
						continue;
					}
					else if (email.Equals(dto.Email))
					{
						throw new Exception("you have submitted an application letter");
					}
				}

				if (exist == null)
					throw new Exception("data not found");
				else
					return exist;
			}
		}
	}
}
