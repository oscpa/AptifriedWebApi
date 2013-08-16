#region using

using System.Collections.Generic;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
using AutoMapper;
using NHibernate;

#endregion

namespace AptifyWebApi.Controllers
{
    public class AptifriedCarouselController : AptifyEnabledApiController
    {
        public AptifriedCarouselController(ISession session) : base(session)
        {
        }


        public IList<AptifriedCarouselElementDto> Get()
        {
            var resultingCarouselElements = new List<AptifriedCarouselElementDto>();

            var carouselProducts = session.CreateSQLQuery("exec spGetCarouselProducts")
                                          .AddEntity(typeof (AptifriedCarouselElement))
                                          .List<AptifriedCarouselElement>();

            foreach (var carouselElement in carouselProducts)
            {
                var meeting = session.QueryOver<AptifriedMeeting>()
                                     .Where(x => x.Product.Id == carouselElement.ProductId)
                                     .SingleOrDefault();

                var meetingDto = Mapper.Map(meeting, new AptifriedMeetingDto());
                resultingCarouselElements.Add(new AptifriedCarouselElementDto
                    {
                        Meeting = meetingDto,
                        BackgroundImageUrl = carouselElement.BackgroundImageUrl
                    });
            }

            return resultingCarouselElements;
        }
    }
}