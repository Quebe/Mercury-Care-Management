using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Authorizations {

    [DataContract (Name = "AuthorizationType")]
    public class AuthorizationType : CoreConfigurationObject  {

        #region Private Properties

        [DataMember (Name = "CategoryId")]
        private Int64 categoryId;

        [DataMember (Name = "Category")]
        private String category;

        [DataMember (Name = "SubcategoryId")]
        private Int64 subcategoryId;

        [DataMember (Name = "Subcategory")]
        private String subcategory;

        [DataMember (Name = "ServiceType")]
        private String serviceType;

        #endregion


        #region Public Properties

        public Int64 CategoryId { get { return categoryId; } set { categoryId = value; } }

        public String Category { get { return category; } set { category = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }

        public Int64 SubcategoryId { get { return subcategoryId; } set { subcategoryId = value; } }

        public String Subcategory { get { return subcategory; } set { subcategory = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }

        public String ServiceType { get { return serviceType; } set { serviceType = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }


        public String AuthorizationTypeText { get { return category + " - " + subcategory + " - " + serviceType; } }

        #endregion


        #region Constructors

        public AuthorizationType (Application applicationReference) { base.BaseConstructor (applicationReference); }

        public AuthorizationType (Application applicationReference, Int64 forAuthorizationTypeId) { base.BaseConstructor (applicationReference, forAuthorizationTypeId); }

        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();

            // VALIDATE CATEGORY IS NOT EMPTY
            Category = Category;

            if (String.IsNullOrEmpty (Category)) { validationResponse.Add ("Category", "Empty or Null"); }


            // VALIDATE SUBCATEGORY IS NOT EMPTY
            Subcategory = Subcategory;

            if (String.IsNullOrEmpty (Subcategory)) { validationResponse.Add ("Subcategory", "Empty or Null"); }


            // VALIDATE PROBLEM IS NOT EMPTY
            ServiceType = ServiceType;

            if (String.IsNullOrEmpty (ServiceType)) { validationResponse.Add ("ServiceType", "Empty or Null"); }


            // VALIDATE DESCRIPTION IS NOT EMPTY
            Description = Description;

            if (String.IsNullOrEmpty (Description)) { validationResponse.Add ("Description", "Empty or Null."); }


            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (long forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable authorizationTypeTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT AuthorizationType.*, ServiceTypeCategory.CategoryName, ServiceTypeSubcategory.SubcategoryName ");

            selectStatement.Append ("  FROM AuthorizationType ");

            selectStatement.Append ("    JOIN ServiceTypeCategory ON AuthorizationType.CategoryId = ServiceTypeCategory.CategoryId");

            selectStatement.Append ("    JOIN ServiceTypeSubcategory ON AuthorizationType.SubcategoryId = ServiceTypeSubcategory.SubcategoryId");

            selectStatement.Append ("    WHERE AuthorizationTypeId = " + forId.ToString ());

            authorizationTypeTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (authorizationTypeTable.Rows.Count == 1) {

                MapDataFields (authorizationTypeTable.Rows[0]);

                success = true;

            }

            else { success = false; }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            categoryId = (Int64) currentRow["CategoryId"];

            category = (String) currentRow["CategoryName"];

            subcategoryId = (Int64) currentRow["SubcategoryId"];

            subcategory = (String) currentRow["SubcategoryName"];

            serviceType = (String) currentRow["ServiceTypeName"];

            return;

        }

        #endregion

    }
}
