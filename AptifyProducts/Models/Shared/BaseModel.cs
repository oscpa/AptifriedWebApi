using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models.Shared
{
    public interface IBaseModel
    {
        int Id { get; set; }
    }

    public class BaseModel : IBaseModel
    {
        public int Id { get; set; }

        protected BaseModel(int id)
        {
            Id = id;
        }
    }
}