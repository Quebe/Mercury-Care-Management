using System;
using System.Net;


namespace Mercury.Server.Caching {

    public class CacheObject {

        #region Private Properties

        private Object objectCached = null;

        private DateTime dateAdded = DateTime.Now;

        private DateTime expiration = DateTime.Now;

        private Boolean isSliding = false;

        private Double slideDuration = 0;

        #endregion 


        #region Public Properties

        public Object ObjectCached { get { return objectCached; } set { objectCached = value; } }

        public DateTime Expiration { get { return expiration; } }

        #endregion 


        #region Constructors

        public CacheObject (Object forObject, Double forDurationSeconds, Boolean forIsSliding) {

            objectCached = forObject;

            dateAdded = DateTime.Now;

            slideDuration = forDurationSeconds;

            expiration = dateAdded.AddSeconds (slideDuration);

            isSliding = forIsSliding;

            return;

        }

        #endregion 


        #region Public Methods

        public void Slide () {

            if (isSliding) {

                expiration = DateTime.Now.AddSeconds (slideDuration);

            }

            return;

        }

        #endregion

    }

}
