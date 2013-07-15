using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace AptifyWebApi.Repository
{
    public class BaseRepository
    {
        protected readonly ISession Session;

        protected BaseRepository(ISession session)
        {
            Session = session;
        }


    }
}