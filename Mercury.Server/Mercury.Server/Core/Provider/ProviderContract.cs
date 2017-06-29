using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Provider {

    [DataContract (Name="ProviderContract")]
    public class ProviderContract : CoreObject {
        
        #region Private Properties
        
        [DataMember (Name = "ProviderId")]
        private Int64 providerId = 0;

        [DataMember (Name = "ProviderAffiliationId")]
        private Int64 providerAffiliationId = 0;
        
        [DataMember (Name = "ProgramId")]
        private Int64 programId = 0;

        [DataMember (Name = "ContractId")]
        private Int64 contractId = 0;

        [DataMember (Name = "IsContracted")]
        private Boolean isContracted = false;

        [DataMember (Name = "IsParticipating")]
        private Boolean isParticipating = false;

        [DataMember (Name = "IsCapitated")]
        private Boolean isCapitated = false;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate = new DateTime (9999, 12, 31);


        private Provider provider = null;

        private ProviderAffiliation providerAffiliation = null;

        private Insurer.Program program = null;

        private Insurer.Contract contract = null;

        #endregion


        #region Public Properties
            
        public Int64 ProviderId { get { return providerId; } }

        public Int64 ProviderAffiliationId { get { return providerAffiliationId; } }
        
        public Int64 ProgramId { get { return programId; } }
       
        public Int64 ContractId { get { return contractId; } }


        public Boolean IsContracted { get { return isContracted; } }

        public Boolean IsParticipating { get { return isParticipating; } }

        public Boolean IsCapitated { get { return isCapitated; } }


        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }


        public Provider Provider {

            get {

                if (provider != null) { return null; }

                provider = base.application.ProviderGet (providerId);

                return provider;

            }

        }

        public ProviderAffiliation ProviderAffiliation {

            get {

                if (providerAffiliation != null) { return null; }

                providerAffiliation = base.application.ProviderAffiliationGet (providerAffiliationId);

                return providerAffiliation;

            }

        }

        public Insurer.Program Program {

            get {

                if (program != null) { return null; }

                program = base.application.ProgramGet (programId);

                return program;

            }

        }

        public Insurer.Contract Contract {

            get {

                if (contract != null) { return null; }

                contract = base.application.ContractGet (contractId);

                return contract;

            }

        }

        #endregion


        #region Constructors

        public ProviderContract (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public ProviderContract (Application applicationReference, Int64 forProviderContractId) {

            BaseConstructor (applicationReference);

            if (!Load (forProviderContractId)) {

                throw new ApplicationException ("Unable to load Provider Contract from the database for " + forProviderContractId.ToString () + ".");

            }

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) { return LoadFromDalSp (forId); } 

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            providerId = (Int64) currentRow["ProviderId"];

            providerAffiliationId = (Int64) currentRow["ProviderAffiliationId"];

            programId = (Int64) currentRow["ProgramId"];

            contractId = (Int64) currentRow["ContractId"];


            isContracted = (Boolean)currentRow["IsContracted"];

            isParticipating = (Boolean) currentRow["IsParticipating"];

            isCapitated = (Boolean) currentRow["IsCapitated"];


            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods - Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Id", "Id|ProviderContract");

                bindingContexts.Add ("ProviderId", "Id|Provider");

                bindingContexts.Add ("ProviderAffiliationId", "Id|ProviderAffiliation");

                bindingContexts.Add ("InsurerId", "Id|Insurer");

                bindingContexts.Add ("ProgramId", "Id|Program");

                bindingContexts.Add ("ContractId", "Id|Contract");

                bindingContexts.Add ("IsContracted", "Boolean");

                bindingContexts.Add ("IsParticipating", "Boolean");

                bindingContexts.Add ("IsCapitated", "Boolean");
                
                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");



                Dictionary<String, String> providerBindingContexts = new Provider (base.application).DataBindingContexts;

                foreach (String currentKey in providerBindingContexts.Keys) {

                    bindingContexts.Add ("Provider." + currentKey, providerBindingContexts[currentKey]);

                }


                Dictionary<String, String> providerAffiliationBindingContexts = new ProviderAffiliation (base.application).DataBindingContexts;

                foreach (String currentKey in providerAffiliationBindingContexts.Keys) {

                    bindingContexts.Add ("ProviderAffiliation." + currentKey, providerAffiliationBindingContexts[currentKey]);

                }


                Dictionary<String, String> contractBindingContexts = new Insurer.Contract (base.application).DataBindingContexts;

                foreach (String currentKey in contractBindingContexts.Keys) {

                    bindingContexts.Add ("Contract." + currentKey, contractBindingContexts[currentKey]);

                }


                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "Id": dataValue = Id.ToString (); break;

                case "ProviderId": dataValue = providerId.ToString (); break;

                case "ProviderAffiliationId": dataValue = providerAffiliationId.ToString (); break;

                case "InsurerId": dataValue = (Program != null) ? Program.InsurerId.ToString () : "0"; break;

                case "ProgramId": dataValue = programId.ToString (); break;

                case "ContractId": dataValue = contractId.ToString (); break;

                case "IsContracted": dataValue = isContracted.ToString (); break;

                case "IsParticipating": dataValue = isParticipating.ToString (); break;

                case "IsCapitated": dataValue = isCapitated.ToString (); break;

                case "EffectiveDate": dataValue = effectiveDate.ToString ("MM/dd/yyyy"); break;

                case "TerminationDate": dataValue = terminationDate.ToString ("MM/dd/yyyy"); break;

                case "Provider":

                    #region Provider

                    if (Provider == null) { return "!Error"; }

                    dataValue = Provider.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", ""));

                    #endregion

                    break;

                case "ProviderAffiliation":

                    #region ProviderAffiliation

                    if (ProviderAffiliation == null) { return "!Error"; }

                    dataValue = ProviderAffiliation.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", ""));

                    #endregion

                    break;

                case "Contract":

                    #region Contract

                    if (Contract == null) { return "!Error"; }

                    dataValue = Contract.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", ""));

                    #endregion

                    break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        #endregion

    }

}
