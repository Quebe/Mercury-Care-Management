using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Provider {

    [DataContract (Name = "ProviderEnrollment")]
    public class ProviderEnrollment : CoreObject {

        #region Private Properties

        [DataMember (Name = "ProviderId")]
        private Int64 providerId = 0;

        [DataMember (Name = "ProgramId")]
        private Int64 programId = 0;

        [DataMember (Name = "ProgramProviderId")]
        private String programProviderId = String.Empty;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate = new DateTime (9999, 12, 31);


        private Insurer.Program program = null;

        private ProviderEnrollment previousEnrollment = null;

        #endregion


        #region Public Properties

        public Int64 ProviderId { get { return providerId; } }

        public Int64 ProgramId { get { return programId; } }

        public String ProgramProviderId { get { return programProviderId; } set { programProviderId = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.UniqueId); } }

        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }

        #endregion 


        #region Public Properties

        public Int64 InsurerId { get { return ((Program != null) ? Program.InsurerId : 0); } }

        public Insurer.Program Program {

            get {

                if (program != null) { return program; }

                if (base.application == null) { program = new Insurer.Program (base.application); return program; }

                if (programId == 0) { program = new Insurer.Program (base.application); return program; }

                program = new Insurer.Program (base.application, programId);

                return program;

            }

        }

        public Boolean HasPreviousEnrollment { get { return (PreviousEnrollment != null); } }

        public ProviderEnrollment PreviousEnrollment {

            get {

                if (previousEnrollment != null) { return previousEnrollment; }

                List<ProviderEnrollment> providerEnrollments = base.application.ProviderEnrollmentsGet (providerId);

                foreach (ProviderEnrollment currentEnrollment in providerEnrollments) {

                    if (currentEnrollment.Id != Id) {

                        if (currentEnrollment.TerminationDate < effectiveDate) {

                            if (previousEnrollment == null) { previousEnrollment = currentEnrollment; }

                            else {

                                if (currentEnrollment.TerminationDate > previousEnrollment.TerminationDate) {

                                    previousEnrollment = currentEnrollment;

                                }

                            }

                        }

                    }

                }

                return previousEnrollment;

            }

        }

        #endregion

        
        #region Constructors 

        public ProviderEnrollment (Application applicationReference) {

            BaseConstructor (applicationReference);
        
            return; 
        
        }

        public ProviderEnrollment (Application applicationReference, Int64 forEnrollmentId) {

            BaseConstructor (applicationReference);

            if (!Load (forEnrollmentId)) {

                throw new ApplicationException ("Unable to load Enrollment from the database for " + forEnrollmentId.ToString () + ".");

            }

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) { return base.LoadFromDalSp (forId); }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            providerId = (Int64) currentRow["ProviderId"];

            programId = (Int64) currentRow["ProgramId"];

            programProviderId = (String) currentRow["ProgramProviderId"];


            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods - Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = base.DataBindingContexts;

                bindingContexts.Add ("ProviderId", "Id|Provider");

                bindingContexts.Add ("ProgramId", "Id|Program");

                bindingContexts.Add ("ProgramProviderId", "String");

                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");

                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "ProviderId": dataValue = providerId.ToString (); break;

                case "ProgramId": dataValue = programId.ToString (); break;

                case "ProgramProviderId": dataValue = programProviderId; break;

                case "EffectiveDate": dataValue = effectiveDate.ToString ("MM/dd/yyyy"); break;

                case "TerminationDate": dataValue = terminationDate.ToString ("MM/dd/yyyy"); break;

                default: dataValue = base.EvaluateDataBinding (bindingContext); break;

            }

            return dataValue;

        }

        #endregion

    }

}
