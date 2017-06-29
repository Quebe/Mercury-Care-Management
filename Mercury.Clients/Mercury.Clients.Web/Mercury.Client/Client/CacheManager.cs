using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client {

    [Serializable]
    public class CacheManager {

        #region Private Properties

        [NonSerialized]
        private System.Web.Caching.Cache cache;

        //static Int64 cacheRequests = 0;

        //static Int64 cacheHits = 0;

        #endregion


        #region Public Properties

        public System.Web.Caching.Cache Cache {

            get {

                if (cache == null) {

                    if (System.Web.HttpRuntime.Cache != null) { cache = System.Web.HttpRuntime.Cache; }

                    else if (System.Web.Hosting.HostingEnvironment.Cache != null) { cache = System.Web.Hosting.HostingEnvironment.Cache; }

                    else { cache = new System.Web.Caching.Cache (); }

                }

                return cache;

            }

            set { cache = value; }

        }

        public Boolean IsCacheValid {

            get {

                Boolean isCacheValid = false;

                Int32 itemCount = 0;

                try {

                    itemCount = Cache.Count;

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

        public Boolean IsObjectCached (String key) {

            try {

                Object cacheObject = Cache[key];

                if (cacheObject == null) {

                    return false;

                }

                else { return true; }

            }

            catch {

                // DO NOTHING

            }

            return false;

        }

        public Boolean CacheObject (String key, Object forObject, TimeSpan expirationTime) {

            if (expirationTime == new TimeSpan (0, 0, 0)) { return true; } 


            try { 

                if (IsCacheValid) {

                    // MCM-1167: CACHE NOT ALLOWING FOR UPDATING CACHED VALUES

                    if (Cache[key] != null) { Cache.Remove (key); } // REMOVE PREVIOUSLY CACHED OBJECT IF EXISTS

                    if (forObject != null) {

                        Cache.Add (key, forObject, null, System.Web.Caching.Cache.NoAbsoluteExpiration, expirationTime, System.Web.Caching.CacheItemPriority.Normal, null);

                    }

                    return true;

                }

            }

            catch {

                // DO NOTHING

            }

            return false;

        }

        public Object GetObject (String key) {


#if DEBUG

            if (Cache[key] == null) {

                System.Diagnostics.Debug.Write ("----> Cache Manager, Miss! [" + key + "]: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

                System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

                System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

                System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[4].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine (String.Empty);

            }

//            else {
                
//            //    System.Diagnostics.Debug.Write ("----> Cache Manager, Hit! [" + key + "]: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

//            //    System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

//            //    System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

//            //    System.Diagnostics.Debug.WriteLine (String.Empty);

//            }

#endif

            return Cache[key];

        }

        public void RemoveObject (String key) {

            if (IsObjectCached (key)) {

                Cache.Remove (key);

            }

        }

        #endregion


        #region Constructors 

        public CacheManager () { /* DO NOTHING */ }

        #endregion

    }

}
