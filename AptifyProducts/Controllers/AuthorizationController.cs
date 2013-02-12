using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AptifyWebApi.Attributes {
    public class AuthorizationController : ApiController {

        private ISession session;

        public AuthorizationController(ISession session) {
            this.session = session;
        }

        [HttpGet]
        public AptifriedAuthroizedUserDto Get(string uniqueId) {
            AptifriedAuthroizedUserDto resultingUser = null;
            if (string.IsNullOrEmpty(uniqueId))
                throw new HttpException(500, "Need an id!", new ArgumentException("uniqueId"));

            var foundUser = session.QueryOver<AptifriedWebUser>()
                .Where(x => x.UniqueId == uniqueId)
                .SingleOrDefault();

            if (foundUser != null)
                Mapper.Map(foundUser, resultingUser);

            return resultingUser;

        }

        [HttpPost]
        public AptifriedAuthroizedUserDto Post(AptifriedWebUserDto user) {
            AptifriedAuthroizedUserDto resultingUser = null;

            if (user == null)
                throw new HttpException(401, "User is not present", new ArgumentException("user"));

            var foundUser = session.QueryOver<AptifriedWebUser>()
                .Where(x => x.UserName == user.UserName)
                .SingleOrDefault();

            if (foundUser != null) {


                var aptifriedSecurityKey = session.QueryOver<AptifriedSecurityKey>()
                    .Where(x => x.Id == 1)
                    .SingleOrDefault();

                if (aptifriedSecurityKey == null)
                    throw new HttpException(401, "Could not find Security Key to decrypt password.");
                var aptifyCryptograph = new Aptify.Framework.BusinessLogic.Security.AptifyCryptograph();

                string decryptedPassword = aptifyCryptograph.DecryptData(aptifriedSecurityKey.KeyValue, foundUser.EncryptedPassword);

                if (decryptedPassword == user.Password) {
                    Mapper.Map(foundUser, resultingUser);
                    resultingUser.Password = decryptedPassword;
                }
                
                
            } else {
                throw new HttpException(401, "User not found");
            }

            return resultingUser;
        }
    }
}