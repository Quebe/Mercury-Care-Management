using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Client.Core.Population.PopulationCriteria {

    [Serializable]
    public class PopulationCriteriaDemographic : CoreObject {

        #region Private Properties
        
        private Int64 populationId;

        private Mercury.Server.Application.Gender gender = Mercury.Server.Application.Gender.Both;

        private Boolean useAgeCriteria = false;

        private Int32 ageMinimum = 0;

        private Int32 ageMaximum = 0;

        private Int64 ethnicityId;

        private String ethnicityName;

        #endregion


        #region Public Properties

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Mercury.Server.Application.Gender Gender { get { return gender; } set { gender = value; } }

        public Boolean UseAgeCriteria { get { return useAgeCriteria; } set { useAgeCriteria = value; } }

        public Int32 AgeMinimum { get { return ageMinimum; } set { ageMinimum = value; } }

        public Int32 AgeMaximum { get { return ageMaximum; } set { ageMaximum = value; } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public String EthnicityName { get { return ethnicityName; } set { ethnicityName = value; } }

        #endregion


        #region Constructors

        public PopulationCriteriaDemographic (Application applicationReference) { BaseConstructor (applicationReference); }

        public PopulationCriteriaDemographic (Application applicationReference, Mercury.Server.Application.PopulationCriteriaDemographic serverCriteria) {

            BaseConstructor (applicationReference, serverCriteria);


            populationId = serverCriteria.PopulationId;

            gender = serverCriteria.Gender;

            useAgeCriteria = serverCriteria.UseAgeCriteria;

            ageMinimum = serverCriteria.AgeMinimum;

            ageMaximum = serverCriteria.AgeMaximum;

            ethnicityId = serverCriteria.EthnicityId;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.PopulationCriteriaDemographic serverPopulationCriteria) {

            base.MapToServerObject ((Server.Application.CoreObject)serverPopulationCriteria);


            serverPopulationCriteria.PopulationId = populationId;

            serverPopulationCriteria.Gender = gender;

            serverPopulationCriteria.UseAgeCriteria = useAgeCriteria;

            serverPopulationCriteria.AgeMinimum = ageMinimum;

            serverPopulationCriteria.AgeMaximum = ageMaximum;

            serverPopulationCriteria.EthnicityId = ethnicityId;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.PopulationCriteriaDemographic serverPopulationCriteria = new Server.Application.PopulationCriteriaDemographic ();

            MapToServerObject (serverPopulationCriteria);

            return serverPopulationCriteria;

        }

        public PopulationCriteriaDemographic Copy () {

            Server.Application.PopulationCriteriaDemographic serverPopulationCriteria = (Server.Application.PopulationCriteriaDemographic)ToServerObject ();

            PopulationCriteriaDemographic copiedPopulationCriteria = new PopulationCriteriaDemographic (application, serverPopulationCriteria);

            return copiedPopulationCriteria;

        }

        public Boolean IsEqual (PopulationCriteriaDemographic compareCriteria) {

            Boolean isEqual = true;

            if (this.gender != compareCriteria.Gender) { isEqual = false; }

            if (this.useAgeCriteria != compareCriteria.UseAgeCriteria) { isEqual = false; }

            if (this.ageMinimum != compareCriteria.AgeMinimum) { isEqual = false; }

            if (this.ageMaximum != compareCriteria.AgeMaximum) { isEqual = false; }

            if (this.ethnicityId != compareCriteria.EthnicityId) { isEqual = false; }

            return isEqual;

        }

        #endregion


    }

}
