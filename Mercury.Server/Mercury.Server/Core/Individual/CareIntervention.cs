using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {
    
    [DataContract (Name = "CareIntervention")]
    public class CareIntervention : CoreConfigurationObject {
        
        #region Private Properties

        [DataMember (Name = "Activities")]
        private List<CareInterventionActivity> activities = new List<CareInterventionActivity> ();

        #endregion


        #region Public Properties - Encapsulated

        public List<CareInterventionActivity> Activities { get { return activities; } set { activities = value; } }

        #endregion 
        

        #region Public Properties

        public override Application Application { 
            
            get { return base.Application; }

            set {

                base.Application = value;

                foreach (CareInterventionActivity currentActivity in activities) {

                    currentActivity.Application = value;

                }

            }

        }

        #endregion 


        #region Constructors

        protected CareIntervention () { /* DO NOTHING, FOR INHERITANCE */ }

        public CareIntervention (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareIntervention (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference);


            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties


            #endregion


            #region Components

            System.Xml.XmlNode activitiesNode = document.CreateElement ("Activities");

            document.LastChild.AppendChild (activitiesNode);

            foreach (CareInterventionActivity currentActivity in activities) {

                activitiesNode.AppendChild (document.ImportNode (currentActivity.XmlSerialize ().LastChild, true));

            }

            #endregion 


            #region Object Nodes


            #endregion 


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);


            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Properties":

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                                    default: break;

                                }

                            }

                            break;

                        case "Activities":

                            foreach (System.Xml.XmlNode currentActivityNode in currentNode.ChildNodes) {

                                CareInterventionActivity activity = new CareInterventionActivity (application);

                                response.AddRange (activity.XmlImport (currentActivityNode));

                                activities.Add (activity);

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // SAVE IMPORTED CLASS

                if (!Save ()) { throw new ApplicationException ("Unable to save Care Intervention: " + Name + "."); }

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


            if (activities == null) { activities = new List<CareInterventionActivity> (); }

            if (activities.Count == 0) { validationResponse.Add ("Activities", "No activities specified for Intervention."); }


            // TODO: VALIDATE DUPLICATE

            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (long forId) {

            Boolean success = base.Load (forId);

            StringBuilder selectStatement = new StringBuilder ();


            if (success) {

                activities = new List<CareInterventionActivity> ();

                System.Data.DataTable activitiesTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM dbo.CareInterventionActivity WHERE CareInterventionId = " + Id.ToString (), 0);

                foreach (System.Data.DataRow currentActivityRow in activitiesTable.Rows) {

                    CareInterventionActivity activity = new CareInterventionActivity (application);

                    activity.MapDataFields (currentActivityRow);

                    activities.Add (activity);

                }

                success = true;

            }


            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            String childIds = String.Empty;


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareInterventionManage)) { throw new ApplicationException ("PermissionDenied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                ModifiedAccountInfo = new Data.AuthorityAccountStamp (application);

                application.EnvironmentDatabase.BeginTransaction ();
                

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("CareIntervention_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@extendedProperties", ExtendedPropertiesXml);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@enabled", Enabled);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@visible", Visible);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", ModifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", ModifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", ModifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);


                success = (sqlCommand.ExecuteNonQuery () > 0);


                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();


                foreach (CareInterventionActivity currentActivity in activities) {

                    if (currentActivity.Id != 0) {

                        childIds += currentActivity.Id.ToString () + ", ";

                    }

                }


                childIds += "0";

                String deleteStatement = "DELETE FROM dbo.CareInterventionActivity WHERE CareInterventionId = " + Id.ToString () + " AND CareInterventionActivityId NOT IN (" + childIds + ")";

                success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement);

                if (!success) { throw new ApplicationException ("Unable to propertly delete child Activities from database."); }


                foreach (CareInterventionActivity currentActivity in activities) {

                    currentActivity.Application = application;

                    currentActivity.CareInterventionId = id;

                    success = currentActivity.Save ();

                    if (!success) { throw new ApplicationException ("Unable to save Intervention Activity."); }

                }


                application.EnvironmentDatabase.CommitTransaction ();

                success = true;

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion

    }

}
