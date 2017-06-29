using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Search {

    [DataContract (Name = "SearchResultCarePlanHeader")]
    public class SearchResultCarePlanHeader {

        #region Private Properties

        [DataMember (Name = "CarePlanId")]
        private Int64 carePlanId = 0;

        [DataMember (Name = "Name")]
        private String carePlanName = String.Empty;

        [DataMember (Name = "Description")]
        private String description;

        [DataMember (Name = "Enabled")]
        private Boolean enabled = false;

        [DataMember (Name = "Visible")]
        private Boolean visible = false;

        [DataMember (Name = "CreateAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        [DataMember (Name = "ModifiedAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        #endregion


        #region Public Properties

        public Int64 CarePlanId { get { return carePlanId; } set { carePlanId = value; } }

        public String Name { get { return carePlanName; } set { carePlanName = value; } }

        public String Description { get { return description; } set { description = value; } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        public Boolean Visible { get { return visible; } set { visible = value; } }

        public Mercury.Server.Data.AuthorityAccountStamp CreateAccountInfo { get { return createAccountInfo; } set { createAccountInfo = value; } }

        public Mercury.Server.Data.AuthorityAccountStamp ModifiedAccountInfo { get { return modifiedAccountInfo; } set { modifiedAccountInfo = value; } }

        #endregion


        #region Database Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            carePlanId = (Int64) currentRow["CarePlanId"];

            carePlanName = (String) currentRow["CarePlanName"];

            description = (String) currentRow["CarePlanDescription"];

            enabled = (Boolean) currentRow["Enabled"];

            visible = (Boolean) currentRow["Visible"];

            createAccountInfo.MapDataFields (currentRow, "Create");

            modifiedAccountInfo.MapDataFields (currentRow, "Modified");

            return;

        }

        #endregion

    }

}
