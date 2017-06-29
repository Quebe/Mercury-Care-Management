using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Caching {

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
