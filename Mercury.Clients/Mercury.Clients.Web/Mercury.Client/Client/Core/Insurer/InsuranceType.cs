using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Insurer {

    [Serializable]
    public class InsuranceType : CoreObject {
        
        #region Constructors

        public InsuranceType (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public InsuranceType (Application applicationReference, Server.Application.InsuranceType serverInsuranceType) {

            BaseConstructor (applicationReference, serverInsuranceType);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.InsuranceType serverInsuranceType) {

            base.BaseConstructor (applicationReference, serverInsuranceType);
            
            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.InsuranceType serverInsuranceType) {

            base.MapToServerObject ((Server.Application.CoreObject)serverInsuranceType);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.InsuranceType serverInsuranceType = new Server.Application.InsuranceType ();

            MapToServerObject (serverInsuranceType);

            return serverInsuranceType;

        }

        public InsuranceType Copy () {

            Server.Application.InsuranceType serverInsuranceType = (Server.Application.InsuranceType)ToServerObject ();

            InsuranceType copiedInsuranceType = new InsuranceType (application, serverInsuranceType);

            return copiedInsuranceType;
            
        }

        public Boolean IsEqual (InsuranceType compareInsuranceType) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareInsuranceType);


            return isEqual;

        }

        #endregion 

    }

}
