using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;

namespace AptifyWebApi {
    public class FormatterConfig {
        public static void RegisterFormatters(MediaTypeFormatterCollection formatters) {
            // Hiding the Jsonp formatter for now, need to determine if it's necessary
            //formatters.Insert(0, new Formatters.JsonpMediaTypeFormatter());
        }
    }
}