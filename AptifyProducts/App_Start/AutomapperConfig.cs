using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi {
    public class AutomapperConfig {
        public static void InitializeAutoMapper() {
            Mapper.CreateMap<AptifriedAddress, AptifriedAddressDto>();

			Mapper.CreateMap<AptifriedAttendeeStatus, AptifriedAttendeeStatusDto>();

            Mapper.CreateMap<AptifriedClass, AptifriedClassDto>();
			Mapper.CreateMap<AptifriedClassExtended, AptifriedClassExtendedDto>();

			Mapper.CreateMap<AptifriedCredit, AptifriedCreditDto>();

            Mapper.CreateMap<AptifriedCompany, AptifriedCompanyDto>();
            Mapper.CreateMap<AptifriedCourse, AptifriedCourseDto>();
            
            Mapper.CreateMap<AptifriedExam, AptifriedExamDto>();
            Mapper.CreateMap<AptifriedExamQuestion, AptifriedExamQuestionDto>();

			Mapper.CreateMap<AptifriedLicenseStatus, AptifriedLicenseStatusDto>();

            Mapper.CreateMap<AptifriedMeeting, AptifriedMeetingDto>()
               .ForMember(dto => dto.MeetingStatusName, m => m.ResolveUsing(ao => ao.Status.Name))
               .ForMember(dto => dto.MeetingTypeName, m => m.ResolveUsing(ao => ao.Type.Name));
			Mapper.CreateMap<AptifriedMeetingDetail, AptifriedMeetingDetailDto>();
            Mapper.CreateMap<AptifriedMeetingEductionUnits, AptifriedMeetingEductionUnitsDto>()
                .ForMember(dto => dto.Code, m => m.ResolveUsing(ao => ao.Category.Code))
                .ForMember(dto => dto.Name, m => m.ResolveUsing(ao => ao.Category.Name));
			Mapper.CreateMap<AptifriedMeetingStatus, AptifriedMeetingStatusDto>();

			Mapper.CreateMap<AptifriedMemberClassificationType, AptifriedMemberClassificiationTypeDto>();
			Mapper.CreateMap<AptifriedMemberStatusType, AptifriedMemberStatusTypeDto>();
			Mapper.CreateMap<AptifriedMemberType, AptifriedMemberTypeDto>();

			Mapper.CreateMap<AptifriedPaymentType, AptifriedPaymentTypeDto>();

			Mapper.CreateMap<AptifriedPerson, AptifriedPersonDto>();

            Mapper.CreateMap<AptifriedProduct, AptifriedProductDto>();
            Mapper.CreateMap<AptifriedProductPrice, AptifriedProductPriceDto>();
            
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
            Mapper.CreateMap<AptifriedWebShoppingCartType, AptifriedWebShoppingCartTypeDto>();

			Mapper.CreateMap<Aptify.Applications.OrderEntry.OrdersEntity, AptifriedOrderDto>()
				.ForMember(dto => dto.Lines, m => m.ResolveUsing<OrderLinesResolver>()
					.FromMember(ao => ao.SubTypes["OrderLines"]))
				.ForMember(dto => dto.ShipToPerson, m => m.ResolveUsing<OrderShipToPersonResolver>())
				.ForMember(dto => dto.ShippingAddress, m => m.ResolveUsing<OrderShipToAddressResolver>())
				.ForMember(dto => dto.Balance, m => m.MapFrom(ao => ao.Balance))
				.ForMember(dto => dto.SubTotal, m => m.MapFrom(ao => ao.CALC_SubTotal))
				.ForMember(dto => dto.ShippingTotal, m => m.MapFrom(ao => ao.CALC_ShippingCharge))
				.ForMember(dto => dto.Tax, m => m.MapFrom(ao => ao.CALC_SalesTax))
				.ForMember(dto => dto.GrandTotal, m => m.MapFrom(ao => ao.CALC_GrandTotal));
            
            Mapper.AssertConfigurationIsValid();
        }

        internal class OrderLinesResolver :
            ValueResolver<Aptify.Framework.BusinessLogic.GenericEntity.AptifySubType, IEnumerable<AptifriedOrderLineDto>> {

            protected override IEnumerable<AptifriedOrderLineDto> ResolveCore(
                Aptify.Framework.BusinessLogic.GenericEntity.AptifySubType source) {
                IList<AptifriedOrderLineDto> convertedOrderLines = new List<AptifriedOrderLineDto>();
                var orderLineEnumerator =
                    ((IEnumerable)source).GetEnumerator();

                while (orderLineEnumerator.MoveNext()) {
                    var currentOrderline = (Aptify.Applications.OrderEntry.OrderLinesEntity)orderLineEnumerator.Current;
                    convertedOrderLines.Add(new AptifriedOrderLineDto() {
                        Product = new AptifriedProductDto() {
                            Id = Convert.ToInt32(currentOrderline.ProductID)
                        },
                        Price = currentOrderline.Price,
                        Discount = currentOrderline.Discount,
                        Extended = currentOrderline.Extended
                    });
                }

                return convertedOrderLines.AsEnumerable();
            }


        }

        internal class OrderShipToPersonResolver :
            ValueResolver<Aptify.Applications.OrderEntry.OrdersEntity, AptifriedPersonDto> {

            protected override AptifriedPersonDto ResolveCore(Aptify.Applications.OrderEntry.OrdersEntity source) {
                AptifriedPersonDto resulingPerson = new AptifriedPersonDto() {
                    Id = Convert.ToInt32(source.ShipToID),
                    FirstName = source.ShipToName
                };

                return resulingPerson;
            }

        }

        internal class OrderShipToAddressResolver :
            ValueResolver<Aptify.Applications.OrderEntry.OrdersEntity, AptifriedAddressDto> {

            protected override AptifriedAddressDto ResolveCore(Aptify.Applications.OrderEntry.OrdersEntity source) {
                AptifriedAddressDto resultingAddress = new AptifriedAddressDto() {
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

    }
}
