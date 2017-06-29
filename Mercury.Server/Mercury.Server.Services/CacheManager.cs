using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Mercury.Server.Services {

    [Serializable]
    public class CacheManager {

        #region Private Properties

        [NonSerialized]
        private System.Web.Caching.Cache cache = System.Web.Hosting.HostingEnvironment.Cache;

        [NonSerialized]
        private Exception lastException;

        #endregion 


        #region Public Properties

        public Exception LastException { get { return lastException; } }

        public Boolean IsCacheValid {

            get {

                Boolean isCacheValid = false;

                Int32 itemCount = 0;

                try {

                    itemCount = cache.Count;

                    isCacheValid = true;

                }

                catch {

                    // DO NOTHING

                }

                return isCacheValid;

            }
        }

        #endregion 


        #region Public Methods

        public Mercury.Server.Application GetApplication (String token) {

            Mercury.Server.Application application = null;

            Mercury.Server.Session session = null;

            String sessionCacheKey = "Mercury.Server.Session." + token;

            String applicationCacheKey = "Mercury.Server.Application." + token;

            Boolean localCacheSuccessful = false;

            lastException = null;

            try {

                if (IsCacheValid) {

                    if (cache[sessionCacheKey] != null) {

                        // LEVEL 1 CACHE FROM LOCAL SERVER

                        session = (Mercury.Server.Session) cache.Get (sessionCacheKey);

                        // Expiration Time from the Cache is maintained by a Sliding Time 

                        application = (Mercury.Server.Application) cache.Get (applicationCacheKey);

                        application.ApplicationUpdated += new EventHandler (Application_OnUpdate);

                        // application = new Mercury.Server.Application (session);

                        localCacheSuccessful = true;

                    }

                }

                if (!localCacheSuccessful) { 

                    // LEVEL 2 CACHE FROM DATABASE, FIRST CACHING IS ALWAYS LEVEL 2 FIRST

                    application = new Mercury.Server.Application (token);

                    if (application != null) {

                        // CACHE TO LEVEL 1 

                        CacheObject (sessionCacheKey, application.Session, new TimeSpan (0, 20, 0));

                        CacheObject (applicationCacheKey, application, new TimeSpan (0, 20, 0));

                        application.ApplicationUpdated += new EventHandler (Application_OnUpdate);

                    }

                }

                if (application != null) {

                    // UPDATE SESSION AUDIT LAST ACTIVITY TIME WHEN UPDATE INTERVAL HAS PASSED

                    if (DateTime.Now.Subtract (application.Session.LastActivityDate).TotalSeconds >= (application.SessionLastActivityUpdateMinutes * 60)) {

                        application.SessionUpdateLastActivity ();

                    }

                }

            }

            catch (Exception cacheException) {

                lastException = cacheException;

                application = null;

            }

            if (application != null) { application.SetLastException (null); }

            return application;

        }

        public Boolean CacheObject (String key, Object forObject, TimeSpan expirationTime) {

            try { 

                if (IsCacheValid) {

                    cache.Add (key, forObject, null, System.Web.Caching.Cache.NoAbsoluteExpiration, expirationTime, System.Web.Caching.CacheItemPriority.Normal, null);

                    return true;
                }

            }

            catch {

                // DO NOTHING

            }

            return false;

        }

        protected void Application_OnUpdate (Object sender, EventArgs eventArgs) {

            if ((sender != null) && (sender is Server.Application)) {

                Server.Application application = (Server.Application) sender;

                String sessionCacheKey = "Mercury.Server.Session." + application.Session.Token;

                String applicationCacheKey = "Mercury.Server.Application." + application.Session.Token;

                CacheObject (sessionCacheKey, application.Session, new TimeSpan (0, 20, 0));

                CacheObject (applicationCacheKey, application, new TimeSpan (0, 20, 0));

                application.Session.SerializeToDatabase (application);

            }

            return;

        }

        #endregion

    }

}
