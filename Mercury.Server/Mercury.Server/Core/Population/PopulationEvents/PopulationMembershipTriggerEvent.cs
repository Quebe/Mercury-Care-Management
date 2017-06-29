using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.PopulationEvents {

    [DataContract (Name = "PopulationMembershipTriggerEvent")]
    public class PopulationMembershipTriggerEvent : CoreObject {

        #region Private Properties
        
        [DataMember (Name = "PopulationMembershipId")]
        private Int64 populationMembershipId;

        [DataMember (Name = "PopulationTriggerEventId")]
        private Int64 populationTriggerEventId;

        [DataMember (Name = "TriggerDate")]
        private DateTime triggerDate;

        [DataMember (Name = "EventDate")]
        private DateTime eventDate;

        [DataMember (Name = "MemberServiceId")]
        private Int64 memberServiceId = 0;

        [DataMember (Name = "MemberMetricId")]
        private Int64 memberMetricId = 0;

        [DataMember (Name = "MemberAuthorizedServiceId")]
        private Int64 memberAuthorizedServiceId = 0;

        [DataMember (Name = "ProblemStatementId")]
        private Int64 problemStatementId = 0;

        [DataMember (Name = "ActionDescription")]
        private String actionDescription;

        #endregion


        #region Public Properties
        
        public Int64 PopulationMembershipId { get { return populationMembershipId; } set { populationMembershipId = value; } }

        public Int64 PopulationTriggerEventId { get { return populationTriggerEventId; } set { populationTriggerEventId = value; } }

        public DateTime TriggerDate { get { return triggerDate; } set { triggerDate = value; } }

        public DateTime EventDate { get { return eventDate; } set { eventDate = value; } }

        public Int64 MemberServiceId { get { return memberServiceId; } set { memberServiceId = value; } }

        public Int64 MemberMetricId { get { return memberMetricId; } set { memberMetricId = value; } }

        public Int64 MemberAuthorizedServiceId { get { return memberAuthorizedServiceId; } set { memberAuthorizedServiceId = value; } }

        public Int64 ProblemStatementId { get { return problemStatementId; } set { problemStatementId = value; } }

        public String ActionDescription { get { return actionDescription; } set { actionDescription = value; } }

        #endregion


        #region Constructors

        protected void ObjectConstructor (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public PopulationMembershipTriggerEvent (Application applicationReference) { ObjectConstructor (applicationReference); return; }

        public PopulationMembershipTriggerEvent (Application applicationReference, Int64 forPopulationMembershipTriggerEventId) {

            ObjectConstructor (applicationReference);

            base.BaseConstructor (applicationReference, forPopulationMembershipTriggerEventId);

            return;

        }

        #endregion


        #region Database Functions
        
        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            PopulationMembershipId = (Int64) currentRow["PopulationMembershipId"];

            PopulationTriggerEventId = (Int64) currentRow["PopulationTriggerEventId"];


            TriggerDate = (DateTime) currentRow["TriggerDate"];

            EventDate = (DateTime)currentRow["EventDate"];

            MemberServiceId = IdFromSql (currentRow, "MemberServiceId");

            MemberMetricId = IdFromSql (currentRow, "MemberMetricId");

            MemberAuthorizedServiceId = IdFromSql (currentRow, "MemberAuthorizedServiceId");

            ProblemStatementId = IdFromSql (currentRow, "ProblemStatementId");

            ActionDescription = (String) currentRow["ActionDescription"];


            return;

        }

        #endregion


        #region Virtual - Data Bindings

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "PopulationMembershipTriggerEvent":

                    bindingContextPart = bindingContext.Split ('.')[1];

                    switch (bindingContextPart) {

                        case "TriggerDate": dataValue = triggerDate.ToString ("MM/dd/yyyy"); break;

                        default: dataValue = "!Error"; break;

                    }

                    break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        override public Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> dataBindings = new Dictionary<String, String> ();


                foreach (String bindingName in new Member.Member (base.application).DataBindingContexts.Keys) {

                    dataBindings.Add ("PopulationMembership.Member." + bindingName, new Member.Member (base.application).DataBindingContexts[bindingName]);

                }


                foreach (String bindingName in new Member.MemberEnrollment (base.application).DataBindingContexts.Keys) {

                    dataBindings.Add ("PopulationMembership.Member.CurrentEnrollment." + bindingName, new Member.MemberEnrollment (base.application).DataBindingContexts[bindingName]);

                }

                foreach (String bindingName in new Member.MemberEnrollmentPcp (base.application).DataBindingContexts.Keys) {

                    dataBindings.Add ("PopulationMembership.Member.CurrentPcpAssignment." + bindingName, new Member.MemberEnrollmentPcp (base.application).DataBindingContexts[bindingName]);

                }

                return dataBindings;

            }

        }

        #endregion

    }

}
