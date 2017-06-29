using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Work {

    [Serializable]
    public class RoutingRuleDefinition : CoreObject {

        #region Private Properties

        private Int64 routingRuleId;

        private Int32 sequence;

        private Int64 insurerId;

        private Int64 programId;

        private Int64 benefitPlanId;

        private Server.Application.Gender gender = Server.Application.Gender.Both;

        private Boolean useAgeCriteria = false;

        private Int32 ageMinimum = 0;

        private Int32 ageMaximum = 0;

        private Boolean isAgeInMonths = false;

        private Int64 ethnicityId = 0;

        private Int64 languageId = 0;

        private String state;

        private String city;

        private String county;

        private String zipCode;

        private Int64 workQueueId;

        #endregion


        #region Public Properties

        public Int64 RoutingRuleId { get { return routingRuleId; } set { routingRuleId = value; } }

        public Int32 Sequence { get { return sequence; } set { sequence = value; } }

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 ProgramId { get { return programId; } set { programId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }

        public Server.Application.Gender Gender { get { return gender; } set { gender = value; } }

        public Boolean UseAgeCriteria { get { return useAgeCriteria; } set { useAgeCriteria = value; } }

        public Int32 AgeMinimum { get { return ageMinimum; } set { ageMinimum = value; } }

        public Int32 AgeMaximum { get { return ageMaximum; } set { ageMaximum = value; } }

        public Boolean IsAgeInMonths { get { return isAgeInMonths; } set { isAgeInMonths = value; } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public Int64 LanguageId { get { return languageId; } set { languageId = value; } }

        public String State { get { return state; } set { state = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressState); } }

        public String City { get { return city; } set { city = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressCity); } }

        public String County { get { return county; } set { county = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressCounty); } }

        public String ZipCode { get { return zipCode; } set { zipCode = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.AddressZipCode); } }

        public Int64 WorkQueueId { get { return workQueueId; } set { workQueueId = value; } }



        public override String Description {

            get {

                String description = String.Empty;

                if (insurerId != 0) { description = description + "Insurer: " + application.CoreObjectGetNameById ("Insurer", insurerId, true) + "; "; }

                if (programId != 0) { description = description + "Program: " + application.CoreObjectGetNameById ("Program", programId, true) + "; "; }

                if (benefitPlanId != 0) { description = description + "Benefit Plan: " + application.CoreObjectGetNameById ("BenefitPlan", benefitPlanId, true) + "; "; }


                if (gender != Server.Application.Gender.Both) { description = description + "Gender: " + gender.ToString () + "; "; }

                if (useAgeCriteria) { description = description + "Age: " + ageMinimum.ToString () + " - " + ageMaximum.ToString () + ((IsAgeInMonths) ? " Months" : " Years") + "; "; }

                if (ethnicityId != 0) { description = description + "Ethinicity: " + application.CoreObjectGetNameById ("Ethnicity", ethnicityId, true) + "; "; }

                if (languageId != 0) { description = description + "Language: " + application.CoreObjectGetNameById ("Language", languageId, true) + "; "; }


                if (!String.IsNullOrEmpty (state)) { description = description + "State: " + state + "; "; }

                if (!String.IsNullOrEmpty (city)) { description = description + "City: " + city + "; "; }

                if (!String.IsNullOrEmpty (county)) { description = description + "County: " + county + "; "; }

                if (!String.IsNullOrEmpty (zipCode)) { description = description + "Zip Code: " + zipCode + "; "; }


                if (String.IsNullOrEmpty (description)) { description = "* ALL"; }

                return description;

            }

        }

        #endregion


        #region Constructors

        public RoutingRuleDefinition (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public RoutingRuleDefinition (Application applicationReference, Server.Application.RoutingRuleDefinition serverDefinition) {

            base.BaseConstructor (applicationReference, serverDefinition);


            routingRuleId = serverDefinition.RoutingRuleId;

            sequence = serverDefinition.Sequence;


            insurerId = serverDefinition.InsurerId;

            programId = serverDefinition.ProgramId;

            benefitPlanId = serverDefinition.BenefitPlanId;
            

            gender = serverDefinition.Gender;

            useAgeCriteria = serverDefinition.UseAgeCriteria;

            ageMinimum = serverDefinition.AgeMinimum;

            ageMaximum = serverDefinition.AgeMaximum;

            isAgeInMonths = serverDefinition.IsAgeInMonths;

            
            ethnicityId = serverDefinition.EthnicityId;

            languageId = serverDefinition.LanguageId;


            state = serverDefinition.State;

            city = serverDefinition.City;

            county = serverDefinition.County;

            zipCode = serverDefinition.ZipCode;

            workQueueId = serverDefinition.WorkQueueId;

            return;

        }

        #endregion 
        

        #region Public Methods

        public virtual void MapToServerObject (Server.Application.RoutingRuleDefinition serverRoutingRuleDefinition) {

            base.MapToServerObject ((Server.Application.CoreObject)serverRoutingRuleDefinition);


            serverRoutingRuleDefinition.RoutingRuleId = routingRuleId;

            serverRoutingRuleDefinition.Sequence = sequence;


            serverRoutingRuleDefinition.InsurerId = insurerId;

            serverRoutingRuleDefinition.ProgramId = programId;

            serverRoutingRuleDefinition.BenefitPlanId = benefitPlanId;


            serverRoutingRuleDefinition.Gender = gender;

            serverRoutingRuleDefinition.UseAgeCriteria = useAgeCriteria;

            serverRoutingRuleDefinition.AgeMinimum = ageMinimum;

            serverRoutingRuleDefinition.AgeMaximum = ageMaximum;

            serverRoutingRuleDefinition.IsAgeInMonths = isAgeInMonths;


            serverRoutingRuleDefinition.EthnicityId = ethnicityId;

            serverRoutingRuleDefinition.LanguageId = languageId;


            serverRoutingRuleDefinition.State = state;

            serverRoutingRuleDefinition.City = city;

            serverRoutingRuleDefinition.County = county;

            serverRoutingRuleDefinition.ZipCode = zipCode;

            serverRoutingRuleDefinition.WorkQueueId = workQueueId;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.RoutingRuleDefinition serverRoutingRuleDefinition = new Server.Application.RoutingRuleDefinition ();

            MapToServerObject (serverRoutingRuleDefinition);

            return serverRoutingRuleDefinition;

        }

        public RoutingRuleDefinition Copy () {

            Server.Application.RoutingRuleDefinition serverRoutingRuleDefinition = (Server.Application.RoutingRuleDefinition)ToServerObject ();

            RoutingRuleDefinition copiedRoutingRuleDefinition = new RoutingRuleDefinition (application, serverRoutingRuleDefinition);

            return copiedRoutingRuleDefinition;

        }

        public Boolean IsEqual (RoutingRuleDefinition compareDefinition) {

            Boolean isEqual = true;

            if (insurerId != compareDefinition.InsurerId) { isEqual = false; }

            if (programId != compareDefinition.ProgramId) { isEqual = false; }

            if (benefitPlanId != compareDefinition.BenefitPlanId) { isEqual = false; }


            if (gender != compareDefinition.Gender) { isEqual = false; }


            if (useAgeCriteria != compareDefinition.UseAgeCriteria) { isEqual = false; }

            if (ageMinimum != compareDefinition.AgeMinimum) { isEqual = false; }

            if (ageMaximum != compareDefinition.AgeMaximum) { isEqual = false; }

            if (isAgeInMonths != compareDefinition.IsAgeInMonths) { isEqual = false; }


            if (ethnicityId != compareDefinition.EthnicityId) { isEqual = false; }

            isEqual &= (languageId == compareDefinition.languageId);


            if (state != compareDefinition.State) { isEqual = false; }

            if (city != compareDefinition.City) { isEqual = false; }


            if (county != compareDefinition.County) { isEqual = false; }

            if (zipCode != compareDefinition.ZipCode) { isEqual = false; }

            if (workQueueId != compareDefinition.WorkQueueId) { isEqual = false; }

            return isEqual;

        }

        #endregion 


    }

}
