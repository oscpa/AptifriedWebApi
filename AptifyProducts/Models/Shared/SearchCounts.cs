using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aptify.Applications.Subscriptions;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
using AptifyWebApi.Models.Meeting;
using AptifyWebApi.Repository;
using Microsoft.Data.OData.Query.SemanticAst;
using NHibernate;

namespace AptifyWebApi.Models.Shared
{
    //TODO: Refactor
    public static class SearchCounts
    {
        public static class MeetingSearch
        {
            public const int UpdateIntervalInMinutes = 240;

            public static class Groups
            {
                        public static DateTime LastUpdated = DateTime.Now;
                public static Dictionary<int, int> Counts = new Dictionary<int, int>();

                public static bool NeedsUpdate(int id)
                {
                    return !Counts.ContainsKey(id) || (DateTime.Now - LastUpdated).TotalHours > UpdateIntervalInMinutes;
                }

                public static void Update(int id, int count)
                {
                    if (Counts.ContainsKey(id))
                        Counts[id] = count;

                    else
                        Counts.Add(id, count);
                }

                public static int GetCount(int id)
                {
                    int cnt;
                    Counts.TryGetValue(id, out cnt);

                    return cnt;
                }
            }

            public static class Types
            {
                public static DateTime LastUpdated = DateTime.Now;
                public static Dictionary<int, int> Counts = new Dictionary<int, int>();

                public static bool NeedsUpdate(int id)
                {
                    return !Counts.ContainsKey(id) || (DateTime.Now - LastUpdated).TotalHours > UpdateIntervalInMinutes;
                }

                public static void Update(int id, int count)
                {
                    if (Counts.ContainsKey(id))
                        Counts[id] = count;

                    else
                        Counts.Add(id, count);

                    LastUpdated = DateTime.Now;
                }

                public static int GetCount(int id)
                {
                    int cnt;
                    Counts.TryGetValue(id, out cnt);

                    return cnt;
                }
            }
        }
    }
}