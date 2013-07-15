#region using

using System;

#endregion

namespace AptifyWebApi.Attributes
{
    /// <summary>
    /// This will allow us to store metadata about the dto's 
    /// matched entity in the markup of a declaration if available.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Delegate | AttributeTargets.Property)]
    internal class AptifriedEntityFieldAttribute : Attribute
    {
        public string FieldName { get; set; }
    }
}