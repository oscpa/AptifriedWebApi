#region using

using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using Aptify.Framework.BusinessLogic.Security;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Aptifried;
using AptifyWebApi.Models.Dto;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AuthorizationController : ApiController
    {
        private readonly ISession session;

        public AuthorizationController(ISession session)
        {
            this.session = session;
        }

        public AptifriedAuthroizedUserDto Get(string uniqueId)
        {
            return Get(uniqueId, true);
        }

        public AptifriedAuthroizedUserDto Get(string uniqueId, bool withPass)
        {
            AptifriedAuthroizedUserDto resultingUser = null;
            if (string.IsNullOrEmpty(uniqueId))
                throw new HttpException(500, "Need an id!", new ArgumentException("uniqueId"));

            var foundUser = session.QueryOver<AptifriedWebUser>()
                                   .Where(x => x.UniqueId == uniqueId)
                                   .SingleOrDefault();

            if (foundUser != null)
                resultingUser = Mapper.Map(foundUser, new AptifriedAuthroizedUserDto());

            // Just in case we want to get the password back too- found a use for this when we're forcing auth
            if (withPass && resultingUser != null)
            {
                string encryptedPassword = foundUser.EncryptedPassword;

                int aptifyEbizSecurityKeyId = -1;
                if (int.TryParse(ConfigurationManager.AppSettings["AptifyEbizSecurityId"], out aptifyEbizSecurityKeyId))
                {
                    var aptifriedSecurityKey = GetAptifySecurityKey(aptifyEbizSecurityKeyId);
                    resultingUser.Password = DecryptPassword(encryptedPassword, aptifriedSecurityKey.KeyValue);
                }
                else
                {
                    throw new ArgumentException("AptifyEbizSecurityId missing from web.config");
                }
            }
            return resultingUser;
        }

        [HttpPost]
        public AptifriedAuthroizedUserDto Post(AptifriedWebUserDto user)
        {
            var resultingUser = new AptifriedAuthroizedUserDto();

            var badLoginException = new HttpException(401, "Password does not match or user not found");

            if (user == null)
                throw new HttpException(401, "User is not present", new ArgumentException("user"));

            var foundUser = session.QueryOver<AptifriedWebUser>()
                                   .Where(x => x.UserName == user.UserName)
                                   .SingleOrDefault();

            if (foundUser != null)
            {
                string encryptedPassword = foundUser.EncryptedPassword;
                string passwordEnteredByUser = user.Password;
                int aptifyEbizSecurityKeyId = -1;
                if (int.TryParse(ConfigurationManager.AppSettings["AptifyEbizSecurityId"], out aptifyEbizSecurityKeyId))
                {
                    var aptifriedSecurityKey = GetAptifySecurityKey(aptifyEbizSecurityKeyId);

                    string decryptedPassword = DecryptPassword(encryptedPassword, aptifriedSecurityKey.KeyValue);
                    if (decryptedPassword == passwordEnteredByUser)
                    {
                        resultingUser = Mapper.Map(foundUser, new AptifriedAuthroizedUserDto());
                        resultingUser.Password = decryptedPassword;
                    }
                    else
                    {
                        throw badLoginException;
                    }
                }
                else
                {
                    throw new ArgumentException("AptifyEbizSecurityId missing from web.config");
                }
            }
            else
            {
                throw badLoginException;
            }

            return resultingUser;
        }

        private AptifriedSecurityKey GetAptifySecurityKey(int aptifyEbizSecurityKeyId)
        {
            var aptifriedSecurityKey = session.QueryOver<AptifriedSecurityKey>()
                                              .Where(x => x.Id == aptifyEbizSecurityKeyId)
                                              .SingleOrDefault();

            if (aptifriedSecurityKey == null)
                throw new HttpException(401, "Could not find Security Key to decrypt password.");
            return aptifriedSecurityKey;
        }

        private string DecryptPassword(string encryptedPassword, string keyValue)
        {
            var aptifyCryptograph = new AptifyCryptograph();
            string decryptedPassword = aptifyCryptograph.DecryptData(keyValue, encryptedPassword);
            return decryptedPassword;
        }
    }
}