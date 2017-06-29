using System;
using System.Net;
using System.Collections.Generic;

namespace Mercury.Server.Caching {

    public class CacheManager {

        #region Private Properties

        private Int32 dataCacheDurationInitial = 30;

        private Int32 dataCacheDurationMultiplier = 2;

        private Dictionary<String, CacheObject> cache = new Dictionary<String, CacheObject> ();

        private Object syncRoot = new Object ();

        #endregion 


        #region Public Properties

        public Int32 DataCacheDurationInitial { get { return dataCacheDurationInitial; } set { dataCacheDurationInitial = value; } }

        public Int32 DataCacheDurationMultiplier { get { return dataCacheDurationMultiplier; } set { dataCacheDurationMultiplier = value; } }

        public Object SyncRoot { get { return syncRoot; } }

        #endregion 


        #region Constructors

        public CacheManager () { return; }

        public CacheManager (Int32 forDurationInitial, Int32 forDurationMultipler) {

            dataCacheDurationInitial = forDurationInitial;

            dataCacheDurationMultiplier = forDurationMultipler;

            return;

        }

        #endregion 

    
        #region Public Methods

        public void CacheObject (String key, Object forObject, CacheDuration duration, Boolean isSlidingTime) {

            lock (syncRoot) {

                if (cache.ContainsKey (key)) { cache.Remove (key); } // REMOVE EXISTING

                CacheObject cacheObject;

                Double durationSeconds = dataCacheDurationInitial * Math.Pow (dataCacheDurationMultiplier, ((Int32) duration));

                cacheObject = new CacheObject (forObject, durationSeconds, isSlidingTime);

                cache.Add (key, cacheObject);

            }

            return;

        }

        public void CacheObject (String key, Object forObject, TimeSpan duration) {

            if (duration == TimeSpan.Zero) { return; }

            lock (syncRoot) {

                if (cache.ContainsKey (key)) { cache.Remove (key); } // REMOVE EXISTING

                CacheObject cacheObject;

                cacheObject = new CacheObject (forObject, duration.TotalSeconds, false);

                cache.Add (key, cacheObject);

            }

            return;

        }

        public Object GetObject (String key) {

            Object objectCached = null; // SET TO EMPTY OBJECT

            lock (syncRoot) {

                if (cache.ContainsKey (key)) { // CACHE CONTAINS OBJECT BY KEY

                    CacheObject cacheObject = cache[key];

                    if (cacheObject.Expiration >= DateTime.Now) { // CHECK EXPIRATION TIME OF CACHE OBJECT

                        objectCached = cacheObject.ObjectCached;

                        cacheObject.Slide (); // SLIDE OBJECT IF USING SLIDING CACHE TIME

                    }

                    else { cache.Remove (key); } // REMOVE EXPIRED OBJECT

                }

            }

            return objectCached;

        }

        public void RemoveObject (String key) {

            lock (syncRoot) {

                if (cache.ContainsKey (key)) { cache.Remove (key); } // REMOVE EXISTING

            }

            return;

        }

        public Boolean ContainsKey (String key) {

            Boolean containsKey = false;

            lock (syncRoot) {

                containsKey = cache.ContainsKey (key);

            }

            return containsKey; 
        
        }

        #endregion 

    }

}
