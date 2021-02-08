using Core.Manager.InterviewManager;
using DLHK_API.Extensions;
using ExcelDataReader;
using Repository;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DLHK_API.Utilities
{
	public class Util
	{
		public Util()
		{
			// do nothing
			// constructor
		}

		public byte[] UploadPhotoToBinary()
		{
			var result = null as byte[];
			var httpRequest = HttpContext.Current.Request.Files;

			if (httpRequest.Count > 0)
			{
				foreach (string file in httpRequest)
				{
					var postedFile = httpRequest[file];

					using (var fs = postedFile.InputStream)
					{
						using (var br = new BinaryReader(fs))
						{
							result = br.ReadBytes((int)fs.Length);
						}
					}
				}
			}

			return result;
		}

		public static byte[] Compress()
		{
			var image = HttpContext.Current.Request.Files;
			var result = null as byte[];

			foreach (string file in image)
			{
				var postedFile = image[file];

				if (postedFile != null && postedFile.ContentLength > 0)
				{
					result = ConvertToBlob(10, Image.FromStream(postedFile.InputStream, true, true));
				}
			}

			return result;
		}

		public static byte[] ConvertToBlob(int quality, Image image)
		{
			using (var ms = new MemoryStream())
			{
				var jpegEncoder = GetEncoder(ImageFormat.Jpeg);
				var encoderParameters = new EncoderParameters(1);
				encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
				image.Save(ms, jpegEncoder, encoderParameters);

				return ms.ToArray();
			}
		}

		private static ImageCodecInfo GetEncoder(ImageFormat format)
		{
			var codecs = ImageCodecInfo.GetImageDecoders();
			foreach (ImageCodecInfo codec in codecs)
			{
				if (codec.FormatID == format.Guid)
				{
					return codec;
				}
			}

			return null;
		}

		public static void SaveJpeg(string path, Image img, int quality)
		{
			if (quality < 0 || quality > 100)
				throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

			var qualityParam = new EncoderParameter(Encoder.Quality, quality);
			var jpegCodec = GetEncoderInfo("image/jpeg");
			var encoderParams = new EncoderParameters(1);
			encoderParams.Param[0] = qualityParam;
			img.Save(path, jpegCodec, encoderParams);
		}

		private static ImageCodecInfo GetEncoderInfo(string mimeType)
		{
			var codecs = ImageCodecInfo.GetImageEncoders();

			for (int i = 0; i < codecs.Length; i++)
				if (codecs[i].MimeType == mimeType)
					return codecs[i];

			return null;
		}

		public void SendEmail(InterviewDTO dto)
		{
			var fromAddress = new MailAddress("mfarid2121@gmail.com");
			var toAddress = new MailAddress(dto.Email);
			const string fromPassword = "calloftugasww2_";
			const string subject = "Undangan Interview";
			string[] months = new string[]
			{
				"Januari", "Februari", "Maret", "April", "Mei", "Juni",
				"Juli", "Agustus", "September", "Oktober", "November", "Desember"
			};

			// body
			string body = "<h3>Selamat Anda diundang untuk menghadiri interview</h3><br />";
			body += "<p>Saudara/i " + dto.Interviewer + ", Anda lolos untuk ke tahap interview dengan membawa berkas berupa:</p>";
			body += "<ul><li>KTP</li><li>Ijazah</li><li>Surat Lamaran</li></ul>";
			body += "<br />";
			body += "<p>Silakan datang ke kantor DLHK Kota Bandung pada " + GetDateFormatter(months, dto.DateOfInterview)
					+ " jam  " + dto.DateOfInterview.Value.ToShortTimeString() + " WIB.</p>";
			body += "<br /><br />";
			body += "<p>Homat kami,</p>";
			body += "<p>Kadis DLHK Kota Bandung</p>";

			SmtpClient smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
			};
			using (var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			})
			{
				smtp.Send(message);
			}
		}

		private string GetDateFormatter(string[] listOfMonths, DateTime? schedule)
		{
			var date = schedule.Value.Day.ToString();
			var month = listOfMonths[schedule.Value.Month - 1].ToString();
			var year = schedule.Value.Year.ToString();

			return $"{date} {month} {year}";
		}

		public void ExcelUpload()
		{
			using (var objEntity = new DLHKEntities())
			{
				var httpRequest = HttpContext.Current.Request;
				var typeParams = httpRequest.Params;
				string status = typeParams["type"].ToString();

				if (string.IsNullOrEmpty(status))
					throw new Exception("please insert type of file");

				if (httpRequest.Files.Count > 0)
				{
					HttpPostedFile file = httpRequest.Files[0];
					Stream stream = file.InputStream;

					IExcelDataReader reader = null;

					if (file.FileName.EndsWith(".xlsx"))
						reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
					else
						throw new Exception("This file format is not supported");

					DataSet excelRecords = reader.AsDataSet();
					reader.Close();

					var finalRecords = excelRecords.Tables[0];
					switch (status)
					{
						case "person":
						case "employee":
							for (int i = 1; i < finalRecords.Rows.Count; i++)
							{
								var val = finalRecords.Rows;

								var data = new Person
								{
									PersonName = val[i][0].ToString(),
									PlaceOfBirth = val[i][1].ToString(),
									DateOfBirth = val[i][2].ToDate(),
									Address = val[i][3].ToString(),
									Phone = val[i][4].ToString(),
									LastDegree = val[i][5].ToString(),
									PreviousJob = val[i][6].ToString(),
									NameOfCouple = val[i][7].ToString(),
									JobOfCouple = val[i][8].ToString(),
									Jobdesk = "Employee",
									TotalChild = val[i][9].ToInt(),
									Email = val[i][10].ToString(),
									NIK = val[i][11].ToString()
								};

								var resPerson = objEntity.People.Add(data);
								objEntity.SaveChanges();

								var roleName = val[i][16].ToString();
								var regionName = val[i][17].ToString();
								var zoneName = val[i][18].ToString();
								
								var roleId = objEntity.Roles.Select(x => new { x.RoleId, x.RoleName }).FirstOrDefault(s => s.RoleName.Contains(roleName)).RoleId;
								var zoneId = objEntity.Zones.Select(x => new { x.ZoneId, x.ZoneName }).FirstOrDefault(s => s.ZoneName.Contains(zoneName)).ZoneId;
								var regionId = objEntity.Regions.Select(x => new { x.RegionId, x.RegionName }).FirstOrDefault(s => s.RegionName.Contains(regionName)).RegionId;

								var emp = new Employee
								{
									EmployeeNumber = val[i][12].ToString(),
									FirstContract = val[i][13].ToDate(),
									LastContract = val[i][14].ToDate(),
									LocationContract = val[i][15].ToString(),
									PersonId = resPerson.PersonId,
									RoleId = roleId,
									RegionId = regionId,
									ZoneId = zoneId,
									Bank = val[i][19].ToString(),
									Shift = val[i][20].ToString()
								};
								objEntity.Employees.Add(emp);
							}
							
							break;
						case "item":
							for (int i = 1; i < finalRecords.Rows.Count; i++)
							{
								var val = finalRecords.Rows;

								var data = new Item
								{
									ItemName = val[i][0].ToString(),
									ItemQty = val[i][1].ToInt(),
									Note = val[i][2].ToString(),
									CategoryId = val[i][3].ToLong(),
									ItemCode = val[i][4].ToString(),
								};

								objEntity.Items.Add(data);

								var itemIn = new Transac
								{
									DateTransac = DateTime.Now,
									ItemCode = val[i][4].ToString(),
									ItemName = val[i][0].ToString(),
									Note = val[i][2].ToString(),
									Qty = val[i][1].ToInt(),
									TypeOfTransac = "IN",
									UserRecorder = "Admin",
									SuplierName = val[i][5].ToString()
								};

								objEntity.Transacs.Add(itemIn);
							}
							break;
						default:
							throw new Exception("can't identified status");
					}

					int output = objEntity.SaveChanges();
					if (output < 0)
					{
						throw new Exception("Excel file uploaded has failed");
					}

				}
				else
				{
					throw new Exception("be sure to including file!");
				}
			}
		}
	}
}