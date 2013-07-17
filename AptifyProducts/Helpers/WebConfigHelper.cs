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

        public static string AptifyDbServer
        {
            get { return ConfigurationManager.AppSettings["AptifyDBServer"]; }
        }

        public static string AptifyEntitiesDb
        {
            get { return ConfigurationManager.AppSettings["AptifyEntitiesDB"]; }
        }
    }
}