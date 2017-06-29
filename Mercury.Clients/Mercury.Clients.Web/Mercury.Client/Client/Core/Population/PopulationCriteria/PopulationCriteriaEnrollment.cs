using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Population.PopulationCriteria {

    [Serializable]
    public class PopulationCriteriaEnrollment : CoreObject {

        #region Private Properties

        private Int64 populationId;

        private Int64 insurerId;

        private Int64 programId;

        private Int64 benefitPlanId;

        #endregion


        #region Public Properties

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 ProgramId { get { return programId; } set { programId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }

        #endregion


        #region Constructors
        
        public PopulationCriteriaEnrollment (Application applicationReference) { BaseConstructor (applicationReference); }

        public PopulationCriteriaEnrollment (Application applicationReference, Mercury.Server.Application.PopulationCriteriaEnrollment serverCriteria) {

            BaseConstructor (applicationReference, serverCriteria);


            populationId = serverCriteria.PopulationId;

            insurerId = serverCriteria.InsurerId;

            programId = serverCriteria.ProgramId;

            benefitPlanId = serverCriteria.BenefitPlanId;


            return;

        }
        
        #endregion


        #region Public Methods
        
        public virtual void MapToServerObject (Server.Application.PopulationCriteriaEnrollment serverPopulationCriteria) {

            base.MapToServerObject ((Server.Application.CoreObject)serverPopulationCriteria);


            serverPopulationCriteria.PopulationId = populationId;

            serverPopulationCriteria.InsurerId = insurerId;

            serverPopulationCriteria.ProgramId = programId;

            serverPopulationCriteria.BenefitPlanId = benefitPlanId;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.PopulationCriteriaEnrollment serverPopulationCriteria = new Server.Application.PopulationCriteriaEnrollment ();

            MapToServerObject (serverPopulationCriteria);

            return serverPopulationCriteria;

        }

        public PopulationCriteriaEnrollment Copy () {

            Server.Application.PopulationCriteriaEnrollment serverPopulationCriteria = (Server.Application.PopulationCriteriaEnrollment)ToServerObject ();

            PopulationCriteriaEnrollment copiedPopulationCriteria = new PopulationCriteriaEnrollment (application, serverPopulationCriteria);

            return copiedPopulationCriteria;

        }

        public Boolean IsEqual (PopulationCriteriaEnrollment compareCriteria) {

            Boolean isEqual = true;

            if (this.insurerId != compareCriteria.InsurerId) { isEqual = false; }

            if (this.programId != compareCriteria.ProgramId) { isEqual = false; }

            if (this.benefitPlanId != compareCriteria.BenefitPlanId) { isEqual = false; }

            return isEqual;

        }
        
        #endregion

    }

}
