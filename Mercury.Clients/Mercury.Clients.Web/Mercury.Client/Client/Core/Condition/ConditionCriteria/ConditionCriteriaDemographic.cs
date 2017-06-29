using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Condition.ConditionCriteria {

    [Serializable]
    public class ConditionCriteriaDemographic : CoreObject {
        
        #region Private Properties
        
        private Int64 conditionId;

        private Mercury.Server.Application.Gender gender = Mercury.Server.Application.Gender.Both;

        private Boolean useAgeCriteria = false;

        private Int32 ageMinimum = 0;

        private Int32 ageMaximum = 0;

        private Int64 ethnicityId;

        private String ethnicityName;

        #endregion


        #region Public Properties

        public Int64 ConditionId { get { return conditionId; } set { conditionId = value; } }

        public Mercury.Server.Application.Gender Gender { get { return gender; } set { gender = value; } }

        public Boolean UseAgeCriteria { get { return useAgeCriteria; } set { useAgeCriteria = value; } }

        public Int32 AgeMinimum { get { return ageMinimum; } set { ageMinimum = value; } }

        public Int32 AgeMaximum { get { return ageMaximum; } set { ageMaximum = value; } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public String EthnicityName { get { return ethnicityName; } set { ethnicityName = value; } }

        #endregion


        #region Constructors

        public ConditionCriteriaDemographic (Application applicationReference) { BaseConstructor (applicationReference); }

        public ConditionCriteriaDemographic (Application applicationReference, Mercury.Server.Application.ConditionCriteriaDemographic serverCriteria) {

            BaseConstructor (applicationReference, serverCriteria);


            conditionId = serverCriteria.ConditionId;

            gender = serverCriteria.Gender;

            useAgeCriteria = serverCriteria.UseAgeCriteria;

            ageMinimum = serverCriteria.AgeMinimum;

            ageMaximum = serverCriteria.AgeMaximum;

            ethnicityId = serverCriteria.EthnicityId;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.ConditionCriteriaDemographic serverConditionCriteria) {

            base.MapToServerObject ((Server.Application.CoreObject)serverConditionCriteria);


            serverConditionCriteria.ConditionId = conditionId;

            serverConditionCriteria.Gender = gender;

            serverConditionCriteria.UseAgeCriteria = useAgeCriteria;

            serverConditionCriteria.AgeMinimum = ageMinimum;

            serverConditionCriteria.AgeMaximum = ageMaximum;

            serverConditionCriteria.EthnicityId = ethnicityId;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.ConditionCriteriaDemographic serverConditionCriteria = new Server.Application.ConditionCriteriaDemographic ();

            MapToServerObject (serverConditionCriteria);

            return serverConditionCriteria;

        }

        public ConditionCriteriaDemographic Copy () {

            Server.Application.ConditionCriteriaDemographic serverConditionCriteria = (Server.Application.ConditionCriteriaDemographic)ToServerObject ();

            ConditionCriteriaDemographic copiedConditionCriteria = new ConditionCriteriaDemographic (application, serverConditionCriteria);

            return copiedConditionCriteria;

        }

        public Boolean IsEqual (ConditionCriteriaDemographic compareCriteria) {

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
