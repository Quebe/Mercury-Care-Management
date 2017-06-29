using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Insurer {

    [DataContract (Name = "Contract")]
    public class Contract : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate;

        #endregion


        #region Public Properties

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        #endregion


        #region Constructors

        public Contract (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public Contract (Application applicationReference, Int64 forContractId) {

            BaseConstructor (applicationReference, forContractId);

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableContract;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.Contract_Select " + forId.ToString ());

            tableContract = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableContract.Rows.Count == 1) {

                MapDataFields (tableContract.Rows[0]);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion

    }

}
