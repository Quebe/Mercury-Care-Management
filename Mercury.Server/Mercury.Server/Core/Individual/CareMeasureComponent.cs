using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CareMeasureComponent")]
    public class CareMeasureComponent : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "CareMeasureId")]
        private Int64 careMeasureId = 0;

        [DataMember (Name = "CareMeasureScaleId")]
        private Int64 careMeasureScaleId = 0;

        [DataMember (Name = "Tag")]
        private String tag = String.Empty;
        
        #endregion 


        #region Public Properties

        public override String Name { get { return base.Name; } set { name = Server.CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Description99); } }

        public Int64 CareMeasureId { get { return careMeasureId; } set { careMeasureId = value; } }

        public Int64 CareMeasureScaleId { get { return careMeasureScaleId; } set { careMeasureScaleId = value; } }

        public String Tag { get { return tag; } set { tag = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.UniqueId); } }

        #endregion 

        
        #region Public Properties

        public CareMeasureScale CareMeasureScale { get { return application.CareMeasureScaleGet (careMeasureScaleId); } }

        public String CareMeasureScaleName { get { return ((CareMeasureScale != null) ? CareMeasureScale.Name : String.Empty); } }

        #endregion


        #region Constructors

        public CareMeasureComponent (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareMeasureComponent (Application applicationReference, Int64 forId) {

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

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureId", CareMeasureId.ToString ());
            
            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureScaleId", CareMeasureScaleId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureScaleName", CareMeasureScaleName);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Tag", Tag);

            #endregion
            

            #region Object Nodes

            document.LastChild.AppendChild (document.ImportNode (CareMeasureScale.XmlSerialize ().LastChild, true));

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

                                    case "Tag":

                                        tag = currentPropertyNode.InnerText;

                                        break;

                                    default: break;

                                }

                            }

                            break;

                        case "CareMeasureScale":
                            
                            careMeasureScaleId = application.CoreObjectGetIdByName ("CareMeasureScale", currentNode.Attributes["Name"].InnerText);

                            if (careMeasureScaleId == 0) {

                                // DOES NOT EXIST, CREATE NEW FROM IMPORT

                                CareMeasureScale careMeasureScale = new CareMeasureScale (application);

                                response.AddRange (careMeasureScale.XmlImport (currentNode));

                                careMeasureScaleId = application.CoreObjectGetIdByName ("CareMeasureScale", currentNode.Attributes["Name"].InnerText);

                                if (careMeasureScaleId == 0) {

                                    throw new ApplicationException ("Unable to import Care Measure Scale: " + currentNode.Attributes["Name"].InnerText + ".");

                                }

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // DO NOT CALL THE SAVE FUNCTION, THIS IS A CHILD OBJECT THAT SHOULD BE SAVED THROUGH THE PARENT

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Validations

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate (((CoreObject) this));

            // TODO: VALIDATE DUPLICATE

            return validationResponse;

        }

        #endregion 


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            careMeasureId = IdFromSql (currentRow, "CareMeasureId");

            careMeasureScaleId = IdFromSql (currentRow, "CareMeasureScaleId");

            tag = StringFromSql (currentRow, "Tag");


            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();



            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareMeasureManage)) { throw new ApplicationException ("PermissionDenied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                ModifiedAccountInfo = new Data.AuthorityAccountStamp (application);

                
                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("CareMeasureComponent_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureComponentId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureComponentName", Name, Server.Data.DataTypeConstants.Description99); // EXTENDED NAME

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureComponentDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureId", careMeasureId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureScaleId", careMeasureScaleId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@tag", tag, Server.Data.DataTypeConstants.UniqueId);


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

                success = true;

            }

            catch (Exception applicationException) {

                success = false;

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion 

    }

}
