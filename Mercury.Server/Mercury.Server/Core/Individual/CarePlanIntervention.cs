using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CarePlanIntervention")]
    public class CarePlanIntervention : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "CarePlanGoalId")]
        private Int64 carePlanGoalId = 0;
        
        [DataMember (Name = "CareInterventionId")]
        private Int64 careInterventionId = 0;

        [DataMember (Name = "Inclusion")]
        private Enumerations.CarePlanItemInclusion inclusion = Enumerations.CarePlanItemInclusion.Suggested;
        
        #endregion 


        #region Public Properties - Encapsulated

        public Int64 CarePlanGoalId { get { return carePlanGoalId; } set { carePlanGoalId = value; } }

        public Int64 CareInterventionId { get { return careInterventionId; } set { careInterventionId = value; } }

        public Enumerations.CarePlanItemInclusion Inclusion { get { return inclusion; } set { inclusion = value; } }

        #endregion 
        

        #region Public Properties
        
        public CareIntervention CareIntervention { get { return application.CareInterventionGet (careInterventionId, true); } }

        #endregion
        

        #region Constructors
        
        protected CarePlanIntervention () { /* DO NOTHING, FOR INHERITANCE */ }

        public CarePlanIntervention (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return;  
        
        }

        public CarePlanIntervention (Application applicationReference, Int64 forId) {

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

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InclusionInt32", ((Int32)Inclusion).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Inclusion", Inclusion.ToString ());

            #endregion
            

            #region Object Nodes

            document.LastChild.AppendChild (document.ImportNode (CareIntervention.XmlSerialize ().LastChild, true));

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

                                    case "InclusionInt32": Inclusion = (Enumerations.CarePlanItemInclusion)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    default: break;

                                }

                            }

                            break;
                            
                        case "CareIntervention":

                            // USES THE DOMAIN ID CAPTURED UNDER PROPERTIES

                            String careInterventionName = currentNode.Attributes["Name"].InnerText;

                            CareIntervention careIntervention = application.CareInterventionGet (careInterventionName);

                            if (careIntervention == null) {

                                // DOES NOT EXIST, CREATE NEW FROM IMPORT

                                careIntervention = new CareIntervention (application);

                                response.AddRange (careIntervention.XmlImport (currentNode));

                                careInterventionId = careIntervention.Id;


                                if (careInterventionId == 0) {

                                    throw new ApplicationException ("Unable to import Care Intervention : " + currentNode.Attributes["Name"].InnerText + ".");

                                }

                            }

                            else { careInterventionId = careIntervention.Id; }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // SAVED BY PARENT OBJECT 

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Validation 

        public override Dictionary<string, string> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<string, string> (); // DO NOT USE BASE METHODS


            // VALIDATE NAME IS NOT NULL OR EMPTY

            // if (String.IsNullOrWhiteSpace (Name)) { validationResponse.Add ("Name", "Empty or NULL."); }
                
            if (CareIntervention == null) { validationResponse.Add ("Care Intervention", "Empty or NULL."); }


            return validationResponse;

        }

        #endregion 


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            CarePlanGoalId = Convert.ToInt64 (currentRow["CarePlanGoalId"]);

            CareInterventionId = base.IdFromSql (currentRow, "CareInterventionId");

            Inclusion = (Enumerations.CarePlanItemInclusion)Convert.ToInt32 (currentRow["Inclusion"]);


            return;

        }

        public override Boolean Save () {

            Boolean usingTransaction = (application.EnvironmentDatabase.OpenTransactions == 0);

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CarePlanManage)) { throw new ApplicationException ("Permission Denied."); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }

                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


                if (usingTransaction) { application.EnvironmentDatabase.BeginTransaction (); }


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("CarePlanIntervention_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@carePlanInterventionId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@carePlanInterventionName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@carePlanInterventionDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@carePlanGoalId", CarePlanGoalId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionId", CareInterventionId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@inclusion", ((Int32)Inclusion));


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

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }



                success = true;

                if (usingTransaction) { application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                if (usingTransaction) { application.EnvironmentDatabase.RollbackTransaction (); }

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion

    }

}
