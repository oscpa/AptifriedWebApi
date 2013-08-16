#region using

using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;
using AptifyWebApi.Dto;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedAccountCreationController : AptifyEnabledApiController
    {
        public AptifriedAccountCreationController(ISession session) : base(session)
        {
        }

        public AptifriedPersonDto Put(AptifriedPersonDto personDto)
        {
            return createPersonAndWebUser(personDto);
        }

        private AptifriedPersonDto createPersonAndWebUser(AptifriedPersonDto personDto)
        {
            var personController = new AptifriedPersonController(session);
            AptifriedPersonDto createdPersonDto = personController.Put(personDto);

            if (createdPersonDto != null)
            {
                var webUserController = new AptifriedWebUserController(session);

                String password = getNewPassword(16);

                AptifriedWebUserDto createdWebUserDto = webUserController.Put(
                    new AptifriedWebUserDto
                        {
                            LinkId = createdPersonDto.Id,
                            Email = createdPersonDto.Email,
                            UserName = createdPersonDto.Email,
                            Password = password,
                            FirstName = createdPersonDto.FirstName,
                            LastName = createdPersonDto.LastName
                        }
                    );

                if (createdPersonDto != null)
                {
                    if (sendEmail(createdPersonDto.Email, password))
                    {
                        return createdPersonDto;
                    }
                }
            }

            return null;
        }

        private bool sendEmail(string email, string password)
        {
            var client = new SmtpClient();
            var msg = new MailMessage();

            msg.To.Add(new MailAddress(email));
            msg.From = new MailAddress("website@ohiocpa.net");
            msg.Subject = "OSCPA Store Account Successfully Created!";
            msg.Body =
                "Thank you for creating an account at the OSCPA Store. Please log in using your email address as your username and your temporary password, which should be changed immediately: " +
                password;
            msg.IsBodyHtml = false;

            string defCreds = ConfigurationManager.AppSettings["UseDefaultCredentials"];
            if (string.Compare(defCreds, string.Empty) == 0)
            {
                client.UseDefaultCredentials = false;
            }
            else
            {
                client.UseDefaultCredentials = Convert.ToBoolean(defCreds);
            }

            if (!client.UseDefaultCredentials)
            {
                var basicAuth = new NetworkCredential(ConfigurationManager.AppSettings["MailUserName"],
                                                      ConfigurationManager.AppSettings["MailPassword"]);
                client.Credentials = basicAuth;
            }

            client.Host = "mail.ohiocpa.net";

            try
            {
                client.Send(msg);
            }
            catch (Exception e)
            {
                throw new HttpException(500, e.Message);
            }

            return true;
        }

        private string getNewPassword(int length)
        {
            return System.Web.Security.Membership.GeneratePassword(length, (int) Math.Floor((double) length/4));
        }
    }
}