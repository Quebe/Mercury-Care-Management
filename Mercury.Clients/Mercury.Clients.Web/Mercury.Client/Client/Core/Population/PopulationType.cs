using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population {

    [Serializable]
    public class PopulationType : CoreConfigurationObject {
        
        #region Constructors

        public PopulationType (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public PopulationType (Application applicationReference, Mercury.Server.Application.PopulationType serverPopulationType) {

            base.BaseConstructor (applicationReference, serverPopulationType);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.PopulationType serverPopulationType) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverPopulationType);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.PopulationType serverPopulationType = new Server.Application.PopulationType ();

            MapToServerObject (serverPopulationType);

            return serverPopulationType;

        }

        public PopulationType Copy () {

            Server.Application.PopulationType serverPopulationType = (Server.Application.PopulationType)ToServerObject ();

            PopulationType copiedPopulationType = new PopulationType (application, serverPopulationType);

            return copiedPopulationType;

        }

        public Boolean IsEqual (PopulationType comparePopulationType) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)comparePopulationType);


            return isEqual;

        }
        #endregion

    }

}
