using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CareMeasureScale")]
    public class CareMeasureScale : CoreConfigurationObject {
        
        #region Private Properties

        [DataMember (Name = "ScaleLabel1")]
        private String scaleLabel1 = String.Empty;

        [DataMember (Name = "ScaleLabel2")]
        private String scaleLabel2 = String.Empty;

        [DataMember (Name = "ScaleLabel3")]
        private String scaleLabel3 = String.Empty;

        [DataMember (Name = "ScaleLabel4")]
        private String scaleLabel4 = String.Empty;

        [DataMember (Name = "ScaleLabel5")]
        private String scaleLabel5 = String.Empty;
    
        #endregion


        #region Public Properties

        public String ScaleLabel1 { get { return scaleLabel1; } set { scaleLabel1 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String ScaleLabel2 { get { return scaleLabel2; } set { scaleLabel2 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String ScaleLabel3 { get { return scaleLabel3; } set { scaleLabel3 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String ScaleLabel4 { get { return scaleLabel4; } set { scaleLabel4 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String ScaleLabel5 { get { return scaleLabel5; } set { scaleLabel5 = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        #endregion 


        #region Constructors

        public CareMeasureScale (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public CareMeasureScale (Application applicationReference, Int64 forId) {

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScaleLabel1", scaleLabel1);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScaleLabel2", scaleLabel2);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScaleLabel3", scaleLabel3);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScaleLabel4", scaleLabel4);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScaleLabel5", scaleLabel5);
            
            #endregion


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode propertiesNode;


            try {

                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        case "ScaleLabel1": ScaleLabel1 = currentPropertyNode.InnerText; break;

                        case "ScaleLabel2": ScaleLabel2 = currentPropertyNode.InnerText; break;

                        case "ScaleLabel3": ScaleLabel3 = currentPropertyNode.InnerText; break;

                        case "ScaleLabel4": ScaleLabel4 = currentPropertyNode.InnerText; break;

                        case "ScaleLabel5": ScaleLabel5 = currentPropertyNode.InnerText; break;
                    
                    }

                }

                if (!Save ()) { throw new ApplicationException ("Unable to save Care Measure Scale: " + Name + "."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();


            if (String.IsNullOrWhiteSpace (scaleLabel1)) { validationResponse.Add ("Scale Label 1", "NULL or Empty Value."); }

            if (String.IsNullOrWhiteSpace (scaleLabel2)) { validationResponse.Add ("Scale Label 2", "NULL or Empty Value."); }

            if (String.IsNullOrWhiteSpace (scaleLabel3)) { validationResponse.Add ("Scale Label 3", "NULL or Empty Value."); }

            if (String.IsNullOrWhiteSpace (scaleLabel4)) { validationResponse.Add ("Scale Label 4", "NULL or Empty Value."); }

            if (String.IsNullOrWhiteSpace (scaleLabel5)) { validationResponse.Add ("Scale Label 5", "NULL or Empty Value."); }


            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            ScaleLabel1 = (String)currentRow["ScaleLabel1"];

            ScaleLabel2 = (String)currentRow["ScaleLabel2"];

            ScaleLabel3 = (String)currentRow["ScaleLabel3"];

            ScaleLabel4 = (String)currentRow["ScaleLabel4"];

            ScaleLabel5 = (String)currentRow["ScaleLabel5"];


            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            Boolean useTransactions = (application.EnvironmentDatabase.OpenTransactions == 0);

            System.Data.IDbCommand sqlCommand;

            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareMeasureScaleManage)) { throw new ApplicationException ("Permission Denied."); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }

                modifiedAccountInfo = new Data.AuthorityAccountStamp (application.Session);


                if (useTransactions) { application.EnvironmentDatabase.BeginTransaction (); }

                sqlCommand = application.EnvironmentDatabase.CreateCommand ("CareMeasureScale_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureScaleId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureScaleName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureScaleDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scaleLabel1", ScaleLabel1, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scaleLabel2", ScaleLabel2, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scaleLabel3", ScaleLabel3, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scaleLabel4", ScaleLabel4, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scaleLabel5", ScaleLabel5, Server.Data.DataTypeConstants.Name);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@extendedProperties", ExtendedPropertiesXml);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@enabled", Enabled);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@visible", Visible);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", modifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", modifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", modifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);


                success = (sqlCommand.ExecuteNonQuery () > 0);

                if (!success) { throw application.EnvironmentDatabase.LastException; }

                SetIdentity ();

                success = true;

                if (useTransactions) { application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                application.SetLastException (applicationException);

                if (useTransactions) { application.EnvironmentDatabase.RollbackTransaction (); }

            }


            return success;

        }

        #endregion 


    }

}
