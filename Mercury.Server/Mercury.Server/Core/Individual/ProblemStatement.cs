using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "ProblemStatement")]
    public class ProblemStatement : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "ProblemDomainId")]
        private Int64 problemDomainId = 0;

        [DataMember (Name = "ProblemDomainName")]
        private String problemDomainName = String.Empty;

        [DataMember (Name = "ProblemClassId")]
        private Int64 problemClassId = 0;

        [DataMember (Name = "ProblemClassName")]
        private String problemClassName = String.Empty;


        [DataMember (Name = "DefiningCharacteristics")]
        private String definingCharacteristics = String.Empty;

        [DataMember (Name = "RelatedFactors")]
        private String relatedFactors = String.Empty;


        [DataMember (Name = "DefaultCarePlanId")]
        private Int64 defaultCarePlanId = 0;

        #endregion 


        #region Public Properties 

        public Int64 ProblemDomainId { get { return problemDomainId; } set { problemDomainId = value; } }
        
        public Int64 ProblemClassId { get { return problemClassId; } set { problemClassId = value; } }


        public String DefiningCharacteristics { get { return definingCharacteristics; } set { definingCharacteristics = Server.CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Description8000); } }

        public String RelatedFactors { get { return relatedFactors; } set { relatedFactors = Server.CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Description8000); } }


        public Int64 DefaultCarePlanId { get { return defaultCarePlanId; } set { defaultCarePlanId = value; } }

        #endregion 

        
        #region Public Properties

        public ProblemDomain ProblemDomain { get { return application.ProblemDomainGet (problemDomainId); } }

        public String ProblemDomainName { get { return problemDomainName; } set { problemDomainName = value; } }

        public ProblemClass ProblemClass { get { return application.ProblemClassGet (problemClassId); } }

        public String ProblemClassName { get { return problemClassName; } set { problemClassName = value; } }

        public CarePlan DefaultCarePlan { get { return application.CarePlanGet (defaultCarePlanId); } }

        public String DefaultCarePlanName { get { return ((DefaultCarePlan != null) ? DefaultCarePlan.Name : String.Empty); } }
        
        #endregion 


        #region Constructors

        public ProblemStatement (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return;  
        
        }

        public ProblemStatement (Application applicationReference, Int64 forId) {

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

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProblemDomainId", ProblemDomainId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProblemDomainName", ProblemDomainName);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProblemClassId", ProblemClassId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProblemClassName", ProblemClassName);


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DefiningCharacteristics", DefiningCharacteristics);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "RelatedFactors", RelatedFactors);

            #endregion

            
            #region Object Nodes

            document.LastChild.AppendChild (document.ImportNode (ProblemClass.XmlSerialize ().LastChild, true));

            document.LastChild.AppendChild (document.ImportNode (DefaultCarePlan.XmlSerialize ().LastChild, true));

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

                                    case "ProblemDomainName":

                                        problemDomainName = currentPropertyNode.InnerText;

                                        problemDomainId = application.CoreObjectGetIdByName ("ProblemDomain", problemDomainName);

                                        break;

                                    case "DefiningCharacteristics": DefiningCharacteristics = currentPropertyNode.InnerText; break;

                                    case "RelatedFactors": RelatedFactors = currentPropertyNode.InnerText; break;

                                    default: break;

                                }

                            }

                            break;
                            
                        case "ProblemClass":

                            // USES THE DOMAIN ID CAPTURED UNDER PROPERTIES

                            problemClassName = currentNode.Attributes["Name"].InnerText;

                            ProblemClass problemClass = application.ProblemClassGetByName (problemDomainId, problemClassName);

                            if (problemClass == null) {

                                // DOES NOT EXIST, CREATE NEW FROM IMPORT

                                problemClass = new ProblemClass (application);

                                response.AddRange (problemClass.XmlImport (currentNode));

                                problemDomainId = problemClass.ProblemDomainId;

                                problemClassId = problemClass.Id;


                                if (problemClassId == 0) {

                                    throw new ApplicationException ("Unable to import Care Measure Class: " + currentNode.Attributes["Name"].InnerText + ".");

                                }

                            }

                            break;

                        case "CarePlan":

                            CarePlan defaultCarePlan = application.CarePlanGet (currentNode.Attributes["Name"].InnerText);

                            if (defaultCarePlan == null) { 

                                // DOES NOT EXIST, CREATE NEW FROM IMPORT

                                defaultCarePlan = new CarePlan (application);

                                response.AddRange (defaultCarePlan.XmlImport (currentNode));

                            }

                            if (defaultCarePlan != null) { defaultCarePlanId = defaultCarePlan.Id; }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // SAVE IMPORTED CLASS

                if (!Save ()) { throw new ApplicationException ("Unable to save " + ObjectType + ": " + Name + "."); }

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

            validationResponse.Remove ("Duplicate");



            // VALIDATE DOMAIN IS NOT EMPTY

            ProblemDomainName = ProblemDomainName;

            if (String.IsNullOrWhiteSpace (ProblemDomainName)) { validationResponse.Add ("Domain", "Empty or Null"); }


            // VALIDATE CLASS IS NOT EMPTY 

            ProblemClassName = ProblemClassName;

            if (String.IsNullOrWhiteSpace (ProblemClassName)) { validationResponse.Add ("Class", "Empty or Null"); }
            

            // VALIDATE DEFINING CHARACTERISTICS AND RELATED FACTORS 

            DefiningCharacteristics = DefiningCharacteristics;

            if (String.IsNullOrWhiteSpace (DefiningCharacteristics)) { validationResponse.Add ("Defining Characteristics", "Empty or Null"); }

            RelatedFactors = RelatedFactors;

            if (String.IsNullOrWhiteSpace (RelatedFactors)) { validationResponse.Add ("Related Factors", "Empty or Null"); }
            


            // VALIDATE DEFAULT CARE PLAN 

            if (defaultCarePlanId == 0) {

                validationResponse.Add ("Default Care Plan", "Default Care Plan is Required.");

            }


            // FOR EXISTING, IF ENABLED THE PROBLEM STATEMENT, ENSURE THAT THE CARE PLAN IS ENABLED 

            if ((Enabled) && (DefaultCarePlan != null)) {

                if (!DefaultCarePlan.Enabled) {

                    validationResponse.Add ("Enabled", "Default Care Plan is disabled and cannot be assigned to an enabled Problem Statement.");

                }

            }


            // TODO: VALIDATE DUPLICATE

            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (long forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable problemStatementTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT ProblemStatement.*, ");

            selectStatement.Append ("    ProblemDomain.ProblemDomainName, ProblemClass.ProblemClassName");

            // selectStatement.Append (", ProblemCategory.ProblemCategoryName, ProblemSubcategory.ProblemSubcategoryName ");

            selectStatement.Append ("  FROM ProblemStatement ");

            selectStatement.Append ("    LEFT JOIN ProblemDomain ON ProblemStatement.ProblemDomainId = ProblemDomain.ProblemDomainId");

            selectStatement.Append ("    LEFT JOIN ProblemClass ON ProblemStatement.ProblemClassId = ProblemClass.ProblemClassId");

            //selectStatement.Append ("    LEFT JOIN ProblemCategory ON ProblemStatement.ProblemCategoryId = ProblemCategory.ProblemCategoryId");

            //selectStatement.Append ("    LEFT JOIN ProblemSubcategory ON ProblemStatement.ProblemSubcategoryId = ProblemSubcategory.ProblemSubcategoryId");

            selectStatement.Append ("    WHERE ProblemStatementId = " + forId.ToString ());

            problemStatementTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (problemStatementTable.Rows.Count == 1) {

                MapDataFields (problemStatementTable.Rows[0]);

                success = true;

            }

            else { success = false; }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            problemDomainId = IdFromSql (currentRow, "ProblemDomainId");

            problemDomainName = StringFromSql (currentRow, "ProblemDomainName");

            problemClassId = IdFromSql (currentRow, "ProblemClassId");

            problemClassName = StringFromSql (currentRow, "ProblemClassName");


            definingCharacteristics = StringFromSql (currentRow, "DefiningCharacteristics");

            relatedFactors = StringFromSql (currentRow, "RelatedFactors");


            defaultCarePlanId = IdFromSql (currentRow, "DefaultCarePlanId");

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            

            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.ProblemStatementManage)) { throw new ApplicationException ("PermissionDenied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                ModifiedAccountInfo = new Data.AuthorityAccountStamp (application);

                application.EnvironmentDatabase.BeginTransaction ();


                problemDomainId = application.CoreObjectGetIdByName ("ProblemDomain", problemDomainName);

                if (problemDomainId == 0) {

                    ProblemDomain problemDomain = new ProblemDomain (application);

                    problemDomain.Name = problemDomainName;

                    problemDomain.Description = problemDomainName;

                    problemDomain.Enabled = true;

                    problemDomain.Visible = true;

                    problemDomain.Save ();

                    problemDomainId = problemDomain.Id;

                }

                problemClassId = application.ProblemClassGetIdByName (problemDomainId, problemClassName);

                if (problemClassId == 0) {

                    ProblemClass problemClass = new ProblemClass (application);

                    problemClass.Name = problemClassName;

                    problemClass.Description = problemClassName;

                    problemClass.ProblemDomainId = problemDomainId;

                    problemClass.Enabled = true;

                    problemClass.Visible = true;

                    problemClass.Save ();

                    problemClassId = problemClass.Id;

                }



                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("ProblemStatement_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@problemStatementId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@problemStatementName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@problemStatementDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@problemDomainId", problemDomainId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@problemClassId", problemClassId);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@definingCharacteristics", definingCharacteristics, Server.Data.DataTypeConstants.Description);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@relatedFactors", relatedFactors, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@defaultCarePlanId", defaultCarePlanId);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@enabled", Enabled);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@visible", Visible);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", modifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", modifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", modifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);


                success = (sqlCommand.ExecuteNonQuery () > 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

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
