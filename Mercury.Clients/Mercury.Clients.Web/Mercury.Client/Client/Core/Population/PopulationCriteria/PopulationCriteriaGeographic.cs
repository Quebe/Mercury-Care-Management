using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population.PopulationCriteria {

    [Serializable]
    public class PopulationCriteriaGeographic : CoreObject {

        #region Private Properties

        private Int64 populationId;

        private String state;

        private String city;

        private String county;

        private String zipCode;

        #endregion


        #region Public Properties

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public String State { get { return state; } set { state = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressState); } }

        public String City { get { return city; } set { city = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressCity); } }

        public String County { get { return county; } set { county = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressCounty); } }

        public String ZipCode { get { return zipCode; } set { zipCode = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressZipCode); } }

        #endregion


        #region Constructors
        
        public PopulationCriteriaGeographic (Application applicationReference) { BaseConstructor (applicationReference); }

        public PopulationCriteriaGeographic (Application applicationReference, Mercury.Server.Application.PopulationCriteriaGeographic serverCriteria) {

            BaseConstructor (applicationReference, serverCriteria);


            populationId = serverCriteria.PopulationId;

            state = serverCriteria.State;

            city = serverCriteria.City;

            county = serverCriteria.County;

            zipCode = serverCriteria.ZipCode;

            createAccountInfo = serverCriteria.CreateAccountInfo;

            modifiedAccountInfo = serverCriteria.ModifiedAccountInfo;

            return;

        }

        #endregion


        #region Public Methods
        
        public virtual void MapToServerObject (Server.Application.PopulationCriteriaGeographic serverPopulationCriteria) {

            base.MapToServerObject ((Server.Application.CoreObject)serverPopulationCriteria);


            serverPopulationCriteria.PopulationId = populationId;

            serverPopulationCriteria.State = state;

            serverPopulationCriteria.City = city;

            serverPopulationCriteria.County = county;

            serverPopulationCriteria.ZipCode = zipCode;

            
            return;

        }

        public override Object ToServerObject () {

            Server.Application.PopulationCriteriaGeographic serverPopulationCriteria = new Server.Application.PopulationCriteriaGeographic ();

            MapToServerObject (serverPopulationCriteria);

            return serverPopulationCriteria;

        }

        public PopulationCriteriaGeographic Copy () {

            Server.Application.PopulationCriteriaGeographic serverPopulationCriteria = (Server.Application.PopulationCriteriaGeographic)ToServerObject ();

            PopulationCriteriaGeographic copiedPopulationCriteria = new PopulationCriteriaGeographic (application, serverPopulationCriteria);

            return copiedPopulationCriteria;

        }

        
        public Boolean IsEqual (PopulationCriteriaGeographic compareCriteria) {

            Boolean isEqual = true;

            if (this.state != compareCriteria.State) { isEqual = false; }

            if (this.city != compareCriteria.City) { isEqual = false; }

            if (this.county != compareCriteria.County) { isEqual = false; }

            if (this.zipCode != compareCriteria.ZipCode) { isEqual = false; }

            return isEqual;

        }
        
        #endregion

    }
}
