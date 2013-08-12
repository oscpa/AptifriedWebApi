#region using

using System;

#endregion

namespace AptifyWebApi.Attributes
{
    /// <summary>
    /// This allows us to mark up a model's class declaration with
    /// the entity that it will transmit data to within Aptify
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class AptifriedEntityAttribute : Attribute
    {
        public string Name { get; set; }
    }

    public class IgnoreAttribute : Attribute
    {
    }
}