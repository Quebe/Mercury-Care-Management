using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CareMeasureClass")]
    public class CareMeasureClass : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "CareMeasureDomainId")]
        private Int64 careMeasureDomainId = 0;

        #endregion


        #region Public Properties

        public Int64 CareMeasureDomainId { get { return careMeasureDomainId; } set { careMeasureDomainId = value; } }

        #endregion


        #region Public Properties

        public CareMeasureDomain CareMeasureDomain { get { return application.CareMeasureDomainGet (careMeasureDomainId); } }

        public String CareMeasureDomainName { get { return ((CareMeasureDomain != null) ? CareMeasureDomain.Name : String.Empty); } }

        #endregion


        #region Constructors

        public CareMeasureClass (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public CareMeasureClass (Application applicationReference, Int64 forId) {

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureDomainId", CareMeasureDomainId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureDomainName", CareMeasureDomainName);

            #endregion
            

            #region Object Nodes

            document.LastChild.AppendChild (document.ImportNode (CareMeasureDomain.XmlSerialize ().LastChild, true));
           
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

                        case "CareMeasureDomain":

                            careMeasureDomainId = application.CoreObjectGetIdByName ("CareMeasureDomain", currentNode.Attributes["Name"].InnerText);

                            if (careMeasureDomainId == 0) {

                                // DOES NOT EXIST, CREATE NEW FROM IMPORT

                                CareMeasureDomain careMeasureDomain = new CareMeasureDomain (application);

                                response.AddRange (careMeasureDomain.XmlImport (currentNode));

                                careMeasureDomain.Save ();

                                careMeasureDomainId = application.CoreObjectGetIdByName ("CareMeasureDomain", currentNode.Attributes["Name"].InnerText);

                                if (careMeasureDomainId == 0) {

                                    throw new ApplicationException ("Unable to import Care Measure Domain: " + currentNode.Attributes["Name"].InnerText + ".");

                                }

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // SAVE IMPORTED CLASS

                if (!Save ()) { throw new ApplicationException ("Unable to save Care Measure Class: " + Name + "."); }

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


            careMeasureDomainId = (Int64)currentRow["CareMeasureDomainId"];


            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            Boolean usingTransaction = (application.EnvironmentDatabase.OpenTransactions == 0);


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareMeasureManage)) { throw new ApplicationException ("Permission Denied [CareMeasureClass.Save]."); }

            Dictionary<String, String> validationResponse = Validate ();

            if (validationResponse.Count != 0) {

                foreach (String validationKey in validationResponse.Keys) {

                    throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                }

            }

            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                if (usingTransaction) { application.EnvironmentDatabase.BeginTransaction (); }


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("CareMeasureClass_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureClassId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureClassName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureClassDescription", Description, Server.Data.DataTypeConstants.Description);
                

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureDomainId", careMeasureDomainId);


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
