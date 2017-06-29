using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Condition {

    [DataContract (Name = "ConditionClass")]
    public class ConditionClass : CoreConfigurationObject {
        
        #region Private Properties

        [DataMember (Name = "ConditionDomainId")]
        private Int64 conditionDomainId = 0; // RESERVED FOR FUTURE USE

        #endregion 


        #region Private Properties

        public Int64 ConditionDomainId { get { return conditionDomainId; } set { conditionDomainId = value; } }
        
        #endregion 

        
        #region Public Properties

        //public ConditionDomain ConditionDomain { get { return application.ConditionDomainGet (conditionDomainId); } }

        //public String ConditionDomainName { get { return ((ConditionDomain != null) ? ConditionDomain.Name : String.Empty); } }

        #endregion

        
        #region Constructors

        public ConditionClass (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public ConditionClass (Application applicationReference, Int64 forId) {

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            //CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ConditionDomainId", ConditionDomainId.ToString ());

            //CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ConditionDomainName", ConditionDomainName);

            #endregion


            #region Object Nodes

            //document.LastChild.AppendChild (document.ImportNode (ConditionDomain.XmlSerialize ().LastChild, true));

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

                        case "ConditionDomain":

                            //conditionDomainId = application.CoreObjectGetIdByName ("ConditionDomain", currentNode.Attributes["Name"].InnerText);

                            //if (conditionDomainId == 0) {

                            //    // DOES NOT EXIST, CREATE NEW FROM IMPORT

                            //    ConditionDomain conditionDomain = new ConditionDomain (application);

                            //    response.AddRange (conditionDomain.XmlImport (currentNode));

                            //    conditionDomain.Save ();

                            //    conditionDomainId = application.CoreObjectGetIdByName ("ConditionDomain", currentNode.Attributes["Name"].InnerText);

                            //    if (conditionDomainId == 0) {

                            //        throw new ApplicationException ("Unable to import Condition Domain: " + currentNode.Attributes["Name"].InnerText + ".");

                            //    }

                            //}

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

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            conditionDomainId = base.IdFromSql (currentRow, "ConditionDomainId");


            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            Boolean usingTransaction = (application.EnvironmentDatabase.OpenTransactions == 0);


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.ConditionManage)) { throw new ApplicationException ("Permission Denied [ConditionClass.Save]."); }

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

                sqlStatement.Append ("EXEC dbo.ConditionClass_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


                //sqlStatement.Append (conditionDomainId.ToString () + ", ");


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
