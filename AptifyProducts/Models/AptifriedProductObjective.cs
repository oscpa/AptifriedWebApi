﻿namespace AptifyWebApi.Models
{
    public class AptifriedProductObjective
    {
        public virtual int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int Sequence { get; set; }
        public virtual string Objective { get; set; }
    }
}