using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi {
    public class AutomapperConfig {
        public static void InitializeAutoMapper() {
            Mapper.CreateMap<AptifriedAddress, AptifriedAddressDto>();
            Mapper.CreateMap<AptifriedClass, AptifriedClassDto>();
            Mapper.CreateMap<AptifriedCompany, AptifriedCompanyDto>();
            Mapper.CreateMap<AptifriedCourse, AptifriedCourseDto>();
            Mapper.CreateMap<AptifriedMemberType, AptifriedMemberTypeDto>();
            Mapper.CreateMap<AptifriedProduct, AptifriedProductDto>();
            Mapper.CreateMap<AptifriedProductPrice, AptifriedProductPriceDto>();
            Mapper.CreateMap<AptifriedCredit, AptifriedCreditDto>();
            


            Mapper.AssertConfigurationIsValid();
        }
    }
}