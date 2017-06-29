using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Mercury.Server.Data;


namespace Mercury.Server.Core.Insurer {

    [Serializable]
    [DataContract (Name = "Program")]
    public class Program : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "InsurerId")]
        private Int64 insurerId;

        [DataMember (Name = "InsuranceTypeId")]
        private Int64 insuranceTypeId = 0;

        [DataMember (Name = "BankAccountId")]
        private Int64 bankAccountId;

        [NonSerialized]
        private Insurer insurer = null;

        #endregion


        #region Public Properties

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 InsuranceTypeId { get { return insuranceTypeId; } set { insuranceTypeId = value; } }

        public Int64 BankAccountId { get { return bankAccountId; } set { bankAccountId = value; } }


        public Insurer Insurer {

            get {

                if ((insurer == null) && (application != null)) {

                    insurer = application.InsurerGet (insurerId);

                }

                return insurer;

            }

        }

        #endregion


        #region Constructors

        public Program (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public Program (Application applicationReference, Int64 forProgramId) {

            BaseConstructor (applicationReference, forProgramId);

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableProgram;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.Program_Select " + forId.ToString ());

            tableProgram = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableProgram.Rows.Count == 1) {

                MapDataFields (tableProgram.Rows[0]);

                // insurer = new Insurer (Application, insurerId);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            insurerId = (Int64) currentRow["InsurerId"];

            insuranceTypeId = (Int64)currentRow["InsuranceTypeId"];

            bankAccountId = (Int64) currentRow["BankAccountId"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion

    }

}
