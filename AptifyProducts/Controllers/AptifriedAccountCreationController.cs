using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using AptifyWebApi.Dto;
using NHibernate;

namespace AptifyWebApi.Controllers {
	public class AptifriedAccountCreationController : AptifyEnabledApiController {
		public AptifriedAccountCreationController(ISession session) : base(session) { }

		public AptifriedPersonDto Put(AptifriedPersonDto personDto) {
			return createPersonAndWebUser(personDto);
		}

		private AptifriedPersonDto createPersonAndWebUser(AptifriedPersonDto personDto) {
			AptifriedPersonController personController = new AptifriedPersonController(session);
			AptifriedPersonDto createdPersonDto = personController.Put(personDto);

			if (createdPersonDto != null) {
				AptifriedWebUserController webUserController = new AptifriedWebUserController(session);

				String password = getNewPassword(16);

				AptifriedWebUserDto createdWebUserDto = webUserController.Put(
					new AptifriedWebUserDto() {
						LinkId = createdPersonDto.Id,
						Email = createdPersonDto.Email,
						UserName = createdPersonDto.Email,
						Password = password,
						FirstName = createdPersonDto.FirstName,
						LastName = createdPersonDto.LastName
					}
				);

				if (createdPersonDto != null) {
					if (sendEmail(createdPersonDto.Email, password)) {
						return createdPersonDto;
					}
				}
			}

			return null;
		}

		private bool sendEmail(string email, string password) {
			SmtpClient client = new SmtpClient();
			MailMessage msg = new MailMessage();

			msg.To.Add(new MailAddress(email));
			msg.From = new MailAddress("website@ohiocpa.net");
			msg.Subject = "OSCPA Store Account Successfully Created!";
			msg.Body = "Thank you for creating an account at the OSCPA Store. Please log in using your email address as your username and your temporary password, which should be changed immediately: " + password;
			msg.IsBodyHtml = false;

			string defCreds = System.Configuration.ConfigurationManager.AppSettings["UseDefaultCredentials"];
			if (string.Compare(defCreds, string.Empty) == 0) {
				client.UseDefaultCredentials = false;
			} else {
				client.UseDefaultCredentials = Convert.ToBoolean(defCreds);
			}

			if (!client.UseDefaultCredentials) {
				System.Net.NetworkCredential basicAuth = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["MailUserName"], System.Configuration.ConfigurationManager.AppSettings["MailPassword"]);
				client.Credentials = basicAuth;
			}

			client.Host = "mail.ohiocpa.net";

			try {
				client.Send(msg);
			} catch (Exception e) {
				throw new HttpException(500, e.Message);
			}

			return true;
		}

		private string getNewPassword(int length) {
			return System.Web.Security.Membership.GeneratePassword(length, (int) Math.Floor((double) length / 4));
		}
	}
}