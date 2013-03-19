using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AptifyWebApi.Dto;
using NHibernate;
using AptifyWebApi.Models;
using AutoMapper;

namespace AptifyWebApi.Controllers {
    public class AptifriedCarouselController : AptifyEnabledApiController {

        public AptifriedCarouselController(ISession session) : base(session) { }


        public IList<AptifriedCarouselElementDto> Get() {
            List<AptifriedCarouselElementDto> resultingCarouselElements = new List<AptifriedCarouselElementDto>();

            var carouselProducts = session.CreateSQLQuery("exec spGetCarouselProducts")
                .AddEntity(typeof(Models.AptifriedCarouselElement))
                .List<AptifriedCarouselElement>();

            foreach (var carouselElement in carouselProducts) {
                var meeting = session.QueryOver<AptifriedMeeting>()
                    .Where(x => x.Product.Id == carouselElement.ProductId)
                    .SingleOrDefault();

                var meetingDto = Mapper.Map(meeting, new AptifriedMeetingDto());
                resultingCarouselElements.Add(new AptifriedCarouselElementDto() {
                    Meeting = meetingDto,
                    BackgroundImageUrl = carouselElement.BackgroundImageUrl
                });

            }

            return resultingCarouselElements;

        }


    }
}