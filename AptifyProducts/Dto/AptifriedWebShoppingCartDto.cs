﻿#region using

using System;
using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedWebShoppingCartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AptifriedWebShoppingCartTypeDto Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public AptifriedOrderDto Order { get; set; }
        public IList<AptifriedWebShoppingCartProductRequestDto> RequestedLines { get; set; }
        public int OrderId { get; set; }
    }
}