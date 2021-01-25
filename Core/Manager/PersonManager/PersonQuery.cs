using ClosedXML.Excel;
using Core.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Manager.PersonManager
{
	public class PersonFilter : TableFilter
	{
		// filter
	}

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

		public IQueryable<PersonDTO> GetQuery(bool withAttachment = false)
		{
			return Manager.Database.People.Select(val => new PersonDTO()
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
				Jobdesk = val.Jobdesk,
				AppLetter = withAttachment ? val.AppLetter : null,
				KTP = withAttachment ? val.KTP : null,
				Photo = withAttachment ? val.Photo : null,
				Sertificate = withAttachment ? val.Sertificate : null,
			});
		}

		public List<PersonDTO> Transform()
		{
			return GetQuery().Where(x => x.Jobdesk.Equals("Employee")).ToList();
		}

		public PersonDTO TransformId(long id)
		{
			return GetQuery().FirstOrDefault(x => x.PersonId == id);
		}

		public PersonDTO TransformName(string name)
		{
			return GetQuery().FirstOrDefault(x => x.PersonName.Equals(name));
		}

		public Pagination<PersonDTO> GetApplicantPagination(PersonFilter filter)
		{
			var query = GetQuery()
				.Where(x =>
					!x.Jobdesk.Equals("Employee") &&
					!x.Jobdesk.Equals("Interview"));

			int total = query.Count();
			int filtered = total;

			if (!string.IsNullOrEmpty(filter.Keyword))
			{
				query = query.Where(x => x.PersonName.Contains(filter.Keyword));
				filtered = query.Count();

				if (filtered == 0)
					throw new Exception("data not found");
			}

			query = query
				.OrderBy(x => x.PersonId)
				.Skip(filter.Skip)
				.Take(filter.PageSize);

			return new Pagination<PersonDTO>()
			{
				ActivePage = filter.ActivePage,
				PageSize = filter.PageSize,
				Data = query.ToList(),
				RecordsTotal = total,
				RecordsFiltered = filtered
			};
		}

		public List<PersonDTO> TransformApplicant()
		{
			return GetQuery()
				.Where(x => 
					!x.Jobdesk.Equals("Employee") &&
					!x.Jobdesk.Equals("Interview"))
				.ToList();
		}

		public List<PersonDTO> TransformInterviewed()
		{
			return GetQuery().Where(x => x.Jobdesk.Equals("Interview")).ToList();
		}

		public PersonDTO TransformApplicantId(long id)
		{
			return GetQuery(true)
				.FirstOrDefault(x =>
					!x.Jobdesk.Equals("Employee") &&
					!x.Jobdesk.Equals("Interview") &&
					x.PersonId == id);
		}

		public string ExportExcel()
		{
			using (var memory = new MemoryStream())
			{
				using (var workbook = new XLWorkbook())
				{
					var worksheet = workbook.Worksheets.Add("applicant");
					worksheet.Cell("A2").Value = NullableValue("No");
					worksheet.Cell("B2").Value = NullableValue("NIK");
					worksheet.Cell("C2").Value = NullableValue("Nama");
					worksheet.Cell("D2").Value = NullableValue("Tempat Lahir");
					worksheet.Cell("E2").Value = NullableValue("Tanggal Lahir");
					worksheet.Cell("F2").Value = NullableValue("Alamat");
					worksheet.Cell("G2").Value = NullableValue("Emaiil");
					worksheet.Cell("H2").Value = NullableValue("HP");
					worksheet.Cell("I2").Value = NullableValue("Pendidikan");
					worksheet.Cell("J2").Value = NullableValue("Nama Pasangan");
					worksheet.Cell("K2").Value = NullableValue("Pekerjaan Pasangan");
					worksheet.Cell("L2").Value = NullableValue("Pekerjaan Terakhir");
					worksheet.Cell("M2").Value = NullableValue("Jumlah Anak");
					worksheet.Cell("N2").Value = NullableValue("Posisi Lamaran");
					worksheet.Cells("A2:N2").Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

					worksheet.Column("B").Width = 20.71;
					worksheet.Column("C").Width = 30;
					worksheet.Column("D").Width = 18;
					worksheet.Column("E").Width = 15;
					worksheet.Column("F").Width = 60;
					worksheet.Column("G").Width = 25;
					worksheet.Column("H").Width = 15;
					worksheet.Column("I").Width = 13;
					worksheet.Column("J").Width = 30;
					worksheet.Column("K").Width = 21;
					worksheet.Column("L").Width = 20;
					worksheet.Column("M").Width = 15;
					worksheet.Column("N").Width = 18;

					// data
					int no = 0;
					var data = TransformApplicant();
					for (int i = 0; i < data.Count(); i++)
					{
						no = i + 3;
						worksheet.Cell($"A{no}").Value = i + 1;
						worksheet.Cell($"B{no}").SetDataType(XLDataType.Text).Value = NullableValue(data[i].NIK);
						worksheet.Cell($"C{no}").Value = NullableValue(data[i].PersonName);
						worksheet.Cell($"D{no}").Value = NullableValue(data[i].PlaceOfBirth);
						worksheet.Cell($"E{no}").Value = NullableValue(data[i].DateOfBirth.GetValueOrDefault().ToShortDateString());
						worksheet.Cell($"F{no}").Value = NullableValue(data[i].Address);
						worksheet.Cell($"G{no}").Value = NullableValue(data[i].Email);
						worksheet.Cell($"H{no}").SetDataType(XLDataType.Text).Value = NullableValue(data[i].Phone);
						worksheet.Cell($"I{no}").Value = NullableValue(data[i].LastDegree);
						worksheet.Cell($"J{no}").Value = NullableValue(data[i].NameOfCouple);
						worksheet.Cell($"K{no}").Value = NullableValue(data[i].JobOfCouple);
						worksheet.Cell($"L{no}").Value = NullableValue(data[i].PreviousJob);
						worksheet.Cell($"M{no}").Value = NullableValue(data[i].TotalChild);
						worksheet.Cell($"N{no}").Value = NullableValue(data[i].Jobdesk);
					}

					workbook.SaveAs(memory);

					return Convert.ToBase64String(memory.ToArray());
				}
			}
		}

		private object NullableValue(object val)
		{
			return val ?? "-";
		}
	}
}
