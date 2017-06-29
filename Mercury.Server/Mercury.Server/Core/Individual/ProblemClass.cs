using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "ProblemClass")]
    public class ProblemClass : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "ProblemDomainId")]
        private Int64 problemDomainId = 0;

        #endregion 


        #region Private Properties

        public Int64 ProblemDomainId { get { return problemDomainId; } set { problemDomainId = value; } }
        
        #endregion 

        
        #region Public Properties

        public ProblemDomain ProblemDomain { get { return application.ProblemDomainGet (problemDomainId); } }

        public String ProblemDomainName { get { return ((ProblemDomain != null) ? ProblemDomain.Name : String.Empty); } }

        #endregion

        
        #region Constructors

        public ProblemClass (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public ProblemClass (Application applicationReference, Int64 forId) {

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

            #endregion


            #region Object Nodes

            document.LastChild.AppendChild (document.ImportNode (ProblemDomain.XmlSerialize ().LastChild, true));

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

                                    // DO NOTHING EXTRA, NO PROPERTIES BESIDES BASE PROPERTIES

                                    // DOMAIN IS HANDLED UNDER RELATED OBJECTS

                                    default: break;

                                }

                            }

                            break;

                        case "ProblemDomain":

                            problemDomainId = application.CoreObjectGetIdByName ("ProblemDomain", currentNode.Attributes["Name"].InnerText);

                            if (problemDomainId == 0) {

                                // DOES NOT EXIST, CREATE NEW FROM IMPORT

                                ProblemDomain problemDomain = new ProblemDomain (application);

                                response.AddRange (problemDomain.XmlImport (currentNode));

                                problemDomain.Save ();

                                problemDomainId = application.CoreObjectGetIdByName ("ProblemDomain", currentNode.Attributes["Name"].InnerText);

                                if (problemDomainId == 0) {

                                    throw new ApplicationException ("Unable to import Problem Domain: " + currentNode.Attributes["Name"].InnerText + ".");

                                }

                            }

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


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable dataTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT * FROM dbo.ProblemClass WHERE ProblemClassId = " + forId.ToString ());

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (dataTable.Rows.Count == 1) {

                MapDataFields (dataTable.Rows[0]);

                success = true;

            }

            else { success = false; }


            if (success) {

                // LOAD CHILD OBJECTS 

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            problemDomainId = (Int64) currentRow["ProblemDomainId"];


            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            Boolean usingTransaction = (application.EnvironmentDatabase.OpenTransactions == 0);


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.ProblemStatementManage)) { throw new ApplicationException ("Permission Denied [ProblemClass.Save]."); }

            Dictionary<String, String> validationResponse = Validate ();

            if (validationResponse.Count != 0) {

                foreach (String validationKey in validationResponse.Keys) {

                    throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                }

            }

            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                if (usingTransaction) { application.EnvironmentDatabase.BeginTransaction (); }

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.ProblemClass_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


                sqlStatement.Append (problemDomainId.ToString () + ", ");


                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

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
