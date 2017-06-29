using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Insurer {

    [Serializable]
    public class BenefitPlan : CoreConfigurationObject {

        #region Private Properties

        private Int64 programId;

        private DateTime effectiveDate;

        private DateTime terminationDate;

        #endregion


        #region Public Properties

        public Int64 ProgramId { get { return programId; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        #endregion


        #region Public Properties

        public Boolean IsActive { get { return ((DateTime.Today >= effectiveDate) && (DateTime.Today <= terminationDate)); } }

        #endregion 


        #region Constructors

        public BenefitPlan (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public BenefitPlan (Application applicationReference, Server.Application.BenefitPlan serverObject) {

            BaseConstructor (applicationReference, serverObject);


            programId = serverObject.ProgramId;

            effectiveDate = serverObject.EffectiveDate;

            terminationDate = serverObject.TerminationDate;


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.BenefitPlan serverBenefitPlan) {

            base.MapToServerObject ((Server.Application.CoreObject)serverBenefitPlan);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.BenefitPlan serverBenefitPlan = new Server.Application.BenefitPlan ();

            MapToServerObject (serverBenefitPlan);

            return serverBenefitPlan;

        }

        public BenefitPlan Copy () {

            Server.Application.BenefitPlan serverBenefitPlan = (Server.Application.BenefitPlan)ToServerObject ();

            BenefitPlan copiedBenefitPlan = new BenefitPlan (application, serverBenefitPlan);

            return copiedBenefitPlan;

        }

        public Boolean IsEqual (BenefitPlan compareBenefitPlan) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareBenefitPlan);


            return isEqual;

        }

        #endregion

    }

}
