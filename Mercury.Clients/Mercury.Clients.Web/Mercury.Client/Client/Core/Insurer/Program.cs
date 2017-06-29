using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Insurer {

    [Serializable]
    public class Program : CoreObject {

        #region Private Properties

        private Int64 insurerId = 0;

        private Int64 insuranceTypeId = 0;

        private Int64 bankAccountId = 0;

        #endregion 
        

        #region Public Properties

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 InsuranceTypeId { get { return insuranceTypeId; } set { insuranceTypeId = value; } }

        public Int64 BankAccountId { get { return bankAccountId; } set { bankAccountId = value; } }

        #endregion 


        #region Public Object Properties

        public Insurer Insurer { get { return application.InsurerGet (insurerId, true); } }

        public InsuranceType InsuranceType { get { return application.InsuranceTypeGet (insuranceTypeId, true); } }

        public String InsuranceTypeName { get { return ((InsuranceType != null) ? InsuranceType.Name : String.Empty); } }

        #endregion


        
        #region Constructors

        public Program (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Program (Application applicationReference, Server.Application.Program serverProgram) {

            BaseConstructor (applicationReference, serverProgram);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Program serverProgram) {

            base.BaseConstructor (applicationReference, serverProgram);


            insurerId = serverProgram.InsurerId;

            insuranceTypeId = serverProgram.InsuranceTypeId;

            bankAccountId = serverProgram.BankAccountId;

            
            return;

        }

        #endregion

    }

}
