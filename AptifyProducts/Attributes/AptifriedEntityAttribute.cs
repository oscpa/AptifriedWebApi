using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Attributes {


    /// <summary>
    /// This allows us to mark up a model's class declaration with
    /// the entity that it will transmit data to within Aptify
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class AptifriedEntityAttribute : Attribute {
        public string Name { get; set; }
    }
}