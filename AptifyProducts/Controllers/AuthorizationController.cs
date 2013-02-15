using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AptifyWebApi.Attributes {
    public class AuthorizationController : ApiController {

        private ISession session;

        public AuthorizationController(ISession session) {
            this.session = session;
        }

        public AptifriedAuthroizedUserDto Get(string uniqueId) {
            AptifriedAuthroizedUserDto resultingUser = null;
            if (string.IsNullOrEmpty(uniqueId))
                throw new HttpException(500, "Need an id!", new ArgumentException("uniqueId"));

            var foundUser = session.QueryOver<AptifriedWebUser>()
                .Where(x => x.UniqueId == uniqueId)
                .SingleOrDefault();

            if (foundUser != null)
               resultingUser =  Mapper.Map(foundUser, new AptifriedAuthroizedUserDto());
            
            return resultingUser;

        }

        [HttpPost]
        public AptifriedAuthroizedUserDto Post(AptifriedWebUserDto user) {
            AptifriedAuthroizedUserDto resultingUser = new AptifriedAuthroizedUserDto();

            if (user == null)
                throw new HttpException(401, "User is not present", new ArgumentException("user"));

            var foundUser = session.QueryOver<AptifriedWebUser>()
                .Where(x => x.UserName == user.UserName)
                .SingleOrDefault();

            if (foundUser != null) {
                int aptifyEbizSecurityKeyId = -1;
                if (int.TryParse(ConfigurationManager.AppSettings["AptifyEbizSecurityId"], out aptifyEbizSecurityKeyId)) {

                    var aptifriedSecurityKey = session.QueryOver<AptifriedSecurityKey>()
                        .Where(x => x.Id == aptifyEbizSecurityKeyId)
                        .SingleOrDefault();

                    if (aptifriedSecurityKey == null)
                        throw new HttpException(401, "Could not find Security Key to decrypt password.");

                    string decryptedPassword = DecryptPassword(foundUser.EncryptedPassword, aptifriedSecurityKey.KeyValue);
                    if (decryptedPassword == user.Password) {
                        resultingUser = Mapper.Map(foundUser, new AptifriedAuthroizedUserDto());
                        resultingUser.Password = decryptedPassword;
                    } else {
                        throw new ArgumentException("AptifyEbizSecurityId missing from web.config");
                    }
                }
            }

            return resultingUser;
        }

        private string DecryptPassword(string encryptedPassword, string keyValue) {
                var aptifyCryptograph = new Aptify.Framework.BusinessLogic.Security.AptifyCryptograph();
                string decryptedPassword = aptifyCryptograph.DecryptData(keyValue, encryptedPassword);
                return decryptedPassword;
        }

    }
}