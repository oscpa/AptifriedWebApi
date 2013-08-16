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
            public const int UpdateIntervalInHours = 24;

            public static class Groups
            {
                        public static DateTime LastUpdated = DateTime.Now;
                public static Dictionary<int, int> Counts = new Dictionary<int, int>();

                public static bool NeedsUpdate(int id)
                {
                    return !Counts.ContainsKey(id) || (DateTime.Now - LastUpdated).TotalHours > UpdateIntervalInHours;
                }

                public static void Update(int id, int count)
                {
                    lock (Counts)
                    {
                        
                        if (Counts.ContainsKey(id))
                            Counts[id] = count;

                        else
                            Counts.Add(id, count);
                    }

                    LastUpdated = DateTime.Now;
                }

                public static int GetCount(int id)
                {
                    lock (Counts)
                    {
                        int cnt;
                        Counts.TryGetValue(id, out cnt);

                        return cnt;
                    }
                }
            }

            public static class Types
            {
                public static DateTime LastUpdated = DateTime.Now;
                public static Dictionary<int, int> Counts = new Dictionary<int, int>();

                public static bool NeedsUpdate(int id)
                {
                    lock (Counts)
                    {
                        lock (Counts)
                        {
                            
                        return !Counts.ContainsKey(id) || (DateTime.Now - LastUpdated).TotalHours > UpdateIntervalInHours;    
                            }}
                }

                public static void Update(int id, int count)
                {
                    lock (Counts)
                    {

                        if (Counts.ContainsKey(id))
                            Counts[id] = count;

                        else
                            Counts.Add(id, count);
                    }

                    LastUpdated = DateTime.Now;
                }

                public static int GetCount(int id)
                {
                    lock (Counts)
                    {

                        int cnt;
                        Counts.TryGetValue(id, out cnt);

                        return cnt;
                    }
                }
            }
        }
    }
}