using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Text;

namespace Mercury.Server.Core.Member {

    [Serializable]
    [DataContract (Name = "MemberEnrollmentPcp")]
    public class MemberEnrollmentPcp : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberEnrollmentId")]
        private Int64 memberEnrollmentId;

        [DataMember (Name = "PcpProviderId")]
        private Int64 pcpProviderId;

        [DataMember (Name = "ProviderAffiliationId")]
        private Int64 providerAffiliationId;

        [DataMember (Name = "PcpServiceLocationId")]
        private Int64 pcpServiceLocationId;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;


        [NonSerialized]
        private Provider.Provider pcpProvider = null;

        [NonSerialized]
        private Provider.ProviderAffiliation providerAffiliation = null;

        #endregion


        #region Public Properties

        public Int64 MemberEnrollmentId { get { return memberEnrollmentId; } set { memberEnrollmentId = value; } }

        public Int64 PcpProviderId { get { return pcpProviderId; } set { pcpProviderId = value; } }

        public Int64 ProviderAffiliationId { get { return providerAffiliationId; } set { providerAffiliationId = value; } }

        public Int64 PcpServiceLocationId { get { return pcpServiceLocationId; } set { pcpServiceLocationId = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }


        public Provider.Provider PcpProvider {

            get {

                if (pcpProvider != null) { return pcpProvider; }

                pcpProvider = new Provider.Provider (base.application, pcpProviderId);

                return pcpProvider;

            }

        }

        public Provider.ProviderAffiliation ProviderAffiliation {

            get {

                if (providerAffiliation != null) { return providerAffiliation; }

                providerAffiliation = new Provider.ProviderAffiliation (base.application, providerAffiliationId);

                return providerAffiliation;

            }

        }

        #endregion


        #region Constructors 

        public MemberEnrollmentPcp (Application applicationReference) {

            BaseConstructor (applicationReference);
        
            return; 
        
        }

        public MemberEnrollmentPcp (Application applicationReference, Int64 forEnrollmentPcpId) {

            BaseConstructor (applicationReference, forEnrollmentPcpId);

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tablePcpAssignment;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.MemberEnrollmentPcp_Select " + forId.ToString ());

            tablePcpAssignment = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tablePcpAssignment.Rows.Count == 1) {

                MapDataFields (tablePcpAssignment.Rows[0]);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            memberEnrollmentId = (Int64) currentRow["MemberEnrollmentId"];

            pcpProviderId = base.IdFromSql (currentRow, "PcpProviderId");

            providerAffiliationId = base.IdFromSql (currentRow, "ProviderAffiliationId");

            pcpServiceLocationId = base.IdFromSql (currentRow, "PcpServiceLocationId");

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];


            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods - Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Id", "Id|MemberEnrollmentPcp");

                bindingContexts.Add ("MemberEnrollmentId", "Id|MemberEnrollment");

                bindingContexts.Add ("PcpProviderId", "Id|Provider");

                bindingContexts.Add ("ProviderAffiliationId", "Id|ProviderAffiliation");

                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");


                Dictionary<String, String> providerBindingContexts = new Provider.Provider (base.application).DataBindingContexts;

                foreach (String currentKey in providerBindingContexts.Keys) {

                    bindingContexts.Add ("PcpProvider." + currentKey, providerBindingContexts[currentKey]);

                }

                foreach (String currentKey in providerBindingContexts.Keys) {

                    bindingContexts.Add ("ProviderAffiliation." + currentKey, providerBindingContexts[currentKey]);

                }

                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "EnrollmentId": // BACKWARDS COMPATIBILITY
                     
                case "MemberEnrollment": 
                    
                    dataValue = memberEnrollmentId.ToString (); break;

                case "PcpProviderId": dataValue = pcpProviderId.ToString (); break;

                case "ProviderAffiliationId": dataValue = providerAffiliationId.ToString (); break;

                case "EffectiveDate": dataValue = effectiveDate.ToString ("MM/dd/yyyy"); break;

                case "TerminationDate": dataValue = terminationDate.ToString ("MM/dd/yyyy"); break;

                case "PcpProvider":

                    #region PCP Provider

                    if (PcpProvider == null) { return "!Error"; }

                    dataValue = PcpProvider.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", ""));

                    #endregion

                    break;

                case "ProviderAffiliation":

                    #region PCP Affiliation

                    if (ProviderAffiliation == null) { return "!Error"; }

                    dataValue = ProviderAffiliation.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", ""));

                    #endregion

                    break;

                default: dataValue = base.EvaluateDataBinding (bindingContext); break;

            }

            return dataValue;

        }

        #endregion

    }

}
