using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Provider {

    [DataContract (Name = "ProviderAffiliation")]
    public class ProviderAffiliation : CoreObject {

        #region Private Properties

        [DataMember (Name = "ProviderId")]
        private Int64 providerId = 0;

        [DataMember (Name = "AffiliateProviderId")]
        private Int64 affiliateProviderId = 0;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate = new DateTime (9999, 12, 31);


        private ProviderAffiliation previousAffiliation = null;

        private Provider provider = null;

        private Provider affiliateProvider = null;

        #endregion


        #region Public Properties

        public Int64 ProviderId { get { return providerId; } }

        public Int64 AffiliateProviderId { get { return affiliateProviderId; } }

        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }

        #endregion 


        #region Public Properties

        public Provider Provider {

            get {

                if (provider != null) { return provider; }

                provider = new Provider (base.application, providerId);

                return provider;

            }

        }

        public Provider AffiliateProvider {

            get {

                if (affiliateProvider != null) { return affiliateProvider; }

                affiliateProvider = new Provider (base.application, affiliateProviderId);

                return affiliateProvider;

            }

        }


        public Boolean HasPreviousAffiliation { get { return (PreviousAffiliation != null); } }

        public ProviderAffiliation PreviousAffiliation {

            get {

                if (previousAffiliation != null) { return previousAffiliation; }

                List<ProviderAffiliation> providerAffiliations = base.application.ProviderAffiliationsGet (providerId);

                foreach (ProviderAffiliation currentAffiliation in providerAffiliations) {

                    if (currentAffiliation.Id != Id) {

                        if (currentAffiliation.TerminationDate < effectiveDate) {

                            if (previousAffiliation == null) { previousAffiliation = currentAffiliation; }

                            else {

                                if (currentAffiliation.TerminationDate > previousAffiliation.TerminationDate) {

                                    previousAffiliation = currentAffiliation;

                                }

                            }

                        }

                    }

                }

                return previousAffiliation;

            }

        }

        #endregion


        #region Constructors

        public ProviderAffiliation (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public ProviderAffiliation (Application applicationReference, Int64 forProviderAffiliationId) {

            BaseConstructor (applicationReference, forProviderAffiliationId);

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableAffiliation;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.ProviderAffiliation_Select " + forId.ToString ());

            tableAffiliation = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableAffiliation.Rows.Count == 1) {

                MapDataFields (tableAffiliation.Rows[0]);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            providerId = (Int64) currentRow["ProviderId"];

            affiliateProviderId = (Int64) currentRow["AffiliateProviderId"];


            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods - Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Id", "Id|Affiliation");

                bindingContexts.Add ("ProviderId", "Id|Provider");

                bindingContexts.Add ("AffiliateProviderId", "Id|Provider");

                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");

                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "Id": dataValue = Id.ToString (); break;

                case "ProviderId": dataValue = providerId.ToString (); break;

                case "AffiliateProviderId": dataValue = affiliateProviderId.ToString (); break;

                case "EffectiveDate": dataValue = effectiveDate.ToString ("MM/dd/yyyy"); break;

                case "TerminationDate": dataValue = terminationDate.ToString ("MM/dd/yyyy"); break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        #endregion

    }

}
