using System.Net.Http.Formatting;

namespace AptifyWebApi.App_Start {
    public class FormatterConfig {
        public static void RegisterFormatters(MediaTypeFormatterCollection formatters) {
            // Hiding the Jsonp formatter for now, need to determine if it's necessary
            //formatters.Insert(0, new Formatters.JsonpMediaTypeFormatter());
        }
    }
}