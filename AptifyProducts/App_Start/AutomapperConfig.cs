#region using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aptify.Applications.OrderEntry;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using AptifyWebApi.Dto.SessionSelection;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Meeting;
using AptifyWebApi.Models.SessionSelection;
using AutoMapper;

#endregion

namespace AptifyWebApi.App_Start
{
    public class AutomapperConfig
    {
        public static void InitializeAutoMapper()
        {
            Mapper.CreateMap<AptifriedAddress, AptifriedAddressDto>();

            Mapper.CreateMap<AptifriedAttendeeStatus, AptifriedAttendeeStatusDto>();
            Mapper.CreateMap<AptifriedAttachment, AptifriedAttachmentDto>();
            Mapper.CreateMap<AptifriedAttachmentCategory, AptifriedAttachmentCategoryDto>();
            Mapper.CreateMap<AptifriedAnswerSheet, AptifriedAnswerSheetDto>();
            Mapper.CreateMap<AptifriedAnswerSheetAnswer, AptifriedAnswerSheetAnswerDto>();

            Mapper.CreateMap<AptifriedCampaign, AptifriedCampaignDto>();
            Mapper.CreateMap<AptifriedCampaignListDetail, AptifriedCampaignListDetailDto>();

            Mapper.CreateMap<AptifriedClass, AptifriedClassDto>();
            Mapper.CreateMap<AptifriedClassExtended, AptifriedClassExtendedDto>();
            Mapper.CreateMap<AptifriedCpeCreditAdjustmentType, AptifriedCpeCreditAdjustmentTypeDto>();

            Mapper.CreateMap<AptifriedCredit, AptifriedCreditDto>();

            Mapper.CreateMap<AptifriedCompany, AptifriedCompanyDto>();
            Mapper.CreateMap<AptifriedCourse, AptifriedCourseDto>();
            Mapper.CreateMap<AptifriedEducationCategory, AptifriedEducationCategoryDto>();

            Mapper.CreateMap<AptifriedEducationUnit, AptifriedEducationUnitDto>();
            Mapper.CreateMap<AptifriedEducationUnitAttachment, AptifriedEducationUnitAttachmentDto>();

            Mapper.CreateMap<AptifriedExam, AptifriedExamDto>();
            Mapper.CreateMap<AptifriedExamQuestion, AptifriedExamQuestionDto>();
            Mapper.CreateMap<AptifriedExamQuestionPossibleAnswers, AptifriedExamQuestionPossibleAnswersDto>();

			Mapper.CreateMap<AptifriedKnowledgeCaptureMode, AptifriedKnowledgeCaptureModeDto>();
			Mapper.CreateMap<AptifriedKnowledgeCategory, AptifriedKnowledgeCategoryDto>();

            Mapper.CreateMap<AptifriedLicenseStatus, AptifriedLicenseStatusDto>();

            Mapper.CreateMap<AptifriedMeeting, AptifriedMeetingDto>();
            Mapper.CreateMap<AptifriedMeetingT, AptifriedMeetingTDto>();
  
            Mapper.CreateMap<AptifriedMeetingDetail, AptifriedMeetingDetailDto>();

            Mapper.CreateMap<AptifriedMeetingEductionUnits, AptifriedMeetingEductionUnitsDto>()
                  .ForMember(dto => dto.Code, m => m.ResolveUsing(ao => ao.Category.Code))
                  .ForMember(dto => dto.Name, m => m.ResolveUsing(ao => ao.Category.Name));
            Mapper.CreateMap<AptifriedMeetingExternalWebMediaContent, AptifriedMeetingExternalWebMediaContentDto>();
            Mapper.CreateMap<AptifriedMeetingWebMediaType, AptifriedMeetingWebMediaTypeDto>();
            Mapper.CreateMap<AptifriedMeetingStatus, AptifriedMeetingStatusDto>();
            Mapper.CreateMap<AptifriedMeetingType, AptifriedMeetingTypeDto>();
            Mapper.CreateMap<AptifriedMeetingTypeGroup, AptifriedMeetingTypeGroupDto>();
            Mapper.CreateMap<AptifriedMeetingTypeItem, AptifriedMeetingTypeItemDto>();
            
            Mapper.CreateMap<AptifriedMeetingTopicTrack, AptifriedMeetingTopicTrackDto>();
            Mapper.CreateMap<AptifriedMeetingTopicTrackProduct, AptifriedMeetingTopicTrackProductDto>();

            Mapper.CreateMap<AptifriedMeetingSessionLog,
                AptifriedMeetingSessionLogDto>();
            Mapper.CreateMap<AptifriedCancelledMeetingSessions,
                AptifriedCancelledMeetingSessionsDto>();
            Mapper.CreateMap<AptifriedNewMeetingSession,
                AptifriedNewMeetingSessionDto>();

            Mapper.CreateMap<AptifriedMeetingTimeSpan, AptifriedMeetingTimeSpanDto>();
            Mapper.CreateMap<AptifriedMeetingTimeSpanProduct, AptifriedMeetingTimeSpanProductDto>();


            Mapper.CreateMap<AptifriedMemberClassificationType, AptifriedMemberClassificiationTypeDto>();
            Mapper.CreateMap<AptifriedMemberStatusType, AptifriedMemberStatusTypeDto>();
            Mapper.CreateMap<AptifriedMemberType, AptifriedMemberTypeDto>();

            Mapper.CreateMap<AptifriedWebNotification, AptifriedWebNotificationDto>();

            Mapper.CreateMap<AptifriedPaymentType, AptifriedPaymentTypeDto>();

            Mapper.CreateMap<AptifriedPerson, AptifriedPersonDto>();

            Mapper.CreateMap<AptifriedProduct, AptifriedProductDto>();
            Mapper.CreateMap<AptifriedProductObjective, AptifriedProductObjectiveDto>();
			Mapper.CreateMap<AptifriedProductPersonNote, AptifriedProductPersonNoteDto>();
            Mapper.CreateMap<AptifriedProductPrice, AptifriedProductPriceDto>();
            Mapper.CreateMap<AptifriedProductRelation, AptifriedProductRelationDto>();
            Mapper.CreateMap<AptifriedProductRelationshipType, AptifriedProductRelationshipTypeDto>();
            Mapper.CreateMap<AptifriedProductType, AptifriedProductTypeDto>();
            Mapper.CreateMap<AptifriedProductAttachmentWithThumbnail, AptifriedProductAttachmentWithThumbnailDto>();

            Mapper.CreateMap<AptifriedVenue, AptifriedVenueDto>();

            Mapper.CreateMap<AptifriedWebUser, AptifriedWebUserDto>()
                  .ForMember(x => x.Password, y => y.Ignore());
            Mapper.CreateMap<AptifriedWebUser, AptifriedAuthroizedUserDto>()
                  .ForMember(x => x.Password, y => y.Ignore());

            Mapper.CreateMap<AptifriedWebRole, AptifriedWebRoleDto>();
            Mapper.CreateMap<AptifriedWebRole, AptifriedAuthorizedRoleDto>();

            Mapper.CreateMap<AptifriedWebShoppingCart, AptifriedWebShoppingCartDto>()
                  .ForMember(dto => dto.Order, m => m.Ignore())
                  .ForMember(dto => dto.RequestedLines, m => m.MapFrom(ao => ao.Lines));
            Mapper.CreateMap<AptifriedWebShoppingCartDetails, AptifriedWebShoppingCartProductRequestDto>();
			Mapper.CreateMap<AptifriedWebShoppingCartDetailsCount, AptifriedWebShoppingCartDetailsCountDto>();
            Mapper.CreateMap<AptifriedWebShoppingCartType, AptifriedWebShoppingCartTypeDto>();

            Mapper.CreateMap<AptifriedOrder, AptifriedCompletedOrderDto>();
            Mapper.CreateMap<AptifriedOrderLine, AptifriedCompletedOrderLineDto>();

            Mapper.CreateMap<OrdersEntity, AptifriedOrderDto>()
                  .ForMember(dto => dto.Id, m => m.MapFrom(ao => ao.RecordID))
                  .ForMember(dto => dto.Lines, m => m.ResolveUsing<OrderLinesResolver>()
                                                     .FromMember(ao => ao.SubTypes["OrderLines"]))
                  .ForMember(dto => dto.ShipToPerson, m => m.ResolveUsing<OrderShipToPersonResolver>())
                  .ForMember(dto => dto.ShippingAddress, m => m.ResolveUsing<OrderShipToAddressResolver>())
                  .ForMember(dto => dto.Balance, m => m.MapFrom(ao => ao.Balance))
                  .ForMember(dto => dto.SubTotal, m => m.MapFrom(ao => ao.CALC_SubTotal))
                  .ForMember(dto => dto.ShippingTotal, m => m.MapFrom(ao => ao.CALC_ShippingCharge))
                  .ForMember(dto => dto.Tax, m => m.MapFrom(ao => ao.CALC_SalesTax))
                  .ForMember(dto => dto.GrandTotal, m => m.MapFrom(ao => ao.CALC_GrandTotal))
                  .ForMember(dto => dto.AptifyLastError, m => m.MapFrom(ao => ao.LastError));

            Mapper.AssertConfigurationIsValid();
        }

        internal class OrderLinesResolver :
            ValueResolver<AptifySubType, IEnumerable<AptifriedOrderLineDto>>
        {
            protected override IEnumerable<AptifriedOrderLineDto> ResolveCore(
                AptifySubType source)
            {
                IList<AptifriedOrderLineDto> convertedOrderLines = new List<AptifriedOrderLineDto>();
                var orderLineEnumerator =
                    ((IEnumerable) source).GetEnumerator();

                while (orderLineEnumerator.MoveNext())
                {
                    var currentOrderline = (OrderLinesEntity) orderLineEnumerator.Current;
                    convertedOrderLines.Add(new AptifriedOrderLineDto
                        {
                            Product = new AptifriedProductDto
                                {
                                    Id = Convert.ToInt32(currentOrderline.ProductID)
                                },
                            Price = currentOrderline.Price,
                            Discount = currentOrderline.Discount,
                            Extended = currentOrderline.Extended,
                            RequestedLineId = Convert.ToInt32(currentOrderline.GetValue("__requestedLineId")),
                            RequestedRegistrantId =
                                Convert.ToInt32(currentOrderline.GetValue("__requestedLineRegistrantId")),
                            Campaign = new AptifriedCampaignDto
                                {
                                    Id = Convert.ToInt32(currentOrderline.GetValue("__requestedLineCampaignId"))
                                }
                        });
                }

                return convertedOrderLines.AsEnumerable();
            }
        }

        internal class OrderShipToAddressResolver :
            ValueResolver<OrdersEntity, AptifriedAddressDto>
        {
            protected override AptifriedAddressDto ResolveCore(OrdersEntity source)
            {
                var resultingAddress = new AptifriedAddressDto
                    {
                        Id = source.ShipToAddressID,
                        Line1 = source.ShipToAddrLine1,
                        Line2 = source.ShipToAddrLine2,
                        Line3 = source.ShipToAddrLine3,
                        City = source.ShipToCity,
                        StateProvince = source.ShipToState,
                        PostalCode = source.ShipToZipCode
                    };

                return resultingAddress;
            }
        }

        internal class OrderShipToPersonResolver :
            ValueResolver<OrdersEntity, AptifriedPersonDto>
        {
            protected override AptifriedPersonDto ResolveCore(OrdersEntity source)
            {
                var resulingPerson = new AptifriedPersonDto
                    {
                        Id = Convert.ToInt32(source.ShipToID),
                        FirstName = source.ShipToName
                    };

                return resulingPerson;
            }
        }
    }
}
