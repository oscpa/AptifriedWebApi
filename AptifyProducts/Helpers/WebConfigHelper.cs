#region using

using System.Configuration;

#endregion

namespace AptifyWebApi.Helpers
{
    public static class WebConfigHelper
    {
        public static string DefaultConnectionString
        {
            get { return ConfigurationManager.AppSettings["DefaultConnection"]; }
        }
    }
}