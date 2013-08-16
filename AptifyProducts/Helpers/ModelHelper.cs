using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using AptifyWebApi.Models.Shared;

namespace AptifyWebApi.Helpers
{
    public static class ModelHelper
    {
        public static bool IsActive(this AptifriedEducationCategory ec)
        {
            return ec.Status.ToLowerInvariant().Equals(EnumsAndConstants.EducationCategoryStatusActive.ToLowerInvariant());
        }
    }
}