using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Search {

    [DataContract (Name = "SearchResultFormHeader")]
    public class SearchResultFormHeader {

        #region Private Properties

        [DataMember (Name = "ObjectType")]
        private String objectType = "Form";

        [DataMember (Name = "Id")]
        private Int64 formId = 0;

        [DataMember (Name = "FormType")]
        private Mercury.Server.Core.Forms.Enumerations.FormType formType = Mercury.Server.Core.Forms.Enumerations.FormType.Template;

        [DataMember (Name = "FormInstanceId")]
        private Guid formInstanceId = Guid.Empty;

        [DataMember (Name = "Description")]
        private String formDescription = String.Empty;

        [DataMember (Name = "Enabled")]
        private Boolean enabled = true;

        [DataMember (Name = "Visible")]
        private Boolean visible = true;

        [DataMember (Name = "CreateAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

        [DataMember (Name = "ModifiedAccountInfo")]
        private Mercury.Server.Data.AuthorityAccountStamp modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();


        // FROM THE DOCUMENT CONTROL TABLE 

        [DataMember (Name = "Name")]
        protected String controlName;

        [DataMember (Name = "Title")]
        protected String controlTitle;

        #endregion


        #region Public Properities

        public String ObjectType { get { return objectType; } }

        public Mercury.Server.Core.Forms.Enumerations.FormType FormType { get { return formType; } set { formType = value; } }

        #endregion


        #region Database Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            formId = (Int64) currentRow["FormId"];

            this.formType = Mercury.Server.Core.Forms.Enumerations.FormType.Template;

            formDescription = (String) currentRow["FormDescription"];

            createAccountInfo.MapDataFields (currentRow, "Create");

            modifiedAccountInfo.MapDataFields (currentRow, "Modified");

            controlName = (String) currentRow["ControlName"];

            controlTitle = (String) currentRow["ControlTitle"];

            enabled = (Boolean) currentRow["Enabled"];

            visible = (Boolean) currentRow["Visible"];

            return;

        }

        #endregion

    }

}
