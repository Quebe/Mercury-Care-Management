using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Reference {

    [Serializable]
    public class ContactRegarding : CoreConfigurationObject {

        #region Constructors

        public ContactRegarding (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public ContactRegarding (Application applicationReference, Server.Application.ContactRegarding serverContactRegarding) {

            BaseConstructor (applicationReference, serverContactRegarding);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.ContactRegarding serverContactRegarding) {

            base.BaseConstructor (applicationReference, serverContactRegarding);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.ContactRegarding serverContactRegarding) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverContactRegarding);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.ContactRegarding serverContactRegarding = new Server.Application.ContactRegarding ();

            MapToServerObject (serverContactRegarding);

            return serverContactRegarding;

        }

        public ContactRegarding Copy () {

            Server.Application.ContactRegarding serverContactRegarding = (Server.Application.ContactRegarding)ToServerObject ();

            ContactRegarding copiedContactRegarding = new ContactRegarding (application, serverContactRegarding);

            return copiedContactRegarding;

        }

        public Boolean IsEqual (ContactRegarding compareContactRegarding) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareContactRegarding);


            return isEqual;

        }

        #endregion

    }

}
