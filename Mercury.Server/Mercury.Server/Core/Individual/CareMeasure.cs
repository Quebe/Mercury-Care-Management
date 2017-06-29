using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CareMeasure")]
    public class CareMeasure : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "CareMeasureDomainId")]
        private Int64 careMeasureDomainId = 0;

        [DataMember (Name = "CareMeasureDomainName")]
        private String careMeasureDomainName = String.Empty;

        [DataMember (Name = "CareMeasureClassId")]
        private Int64 careMeasureClassId = 0;

        [DataMember (Name = "CareMeasureClassName")]
        private String careMeasureClassName = String.Empty;

        [DataMember (Name = "Components")]
        private List<CareMeasureComponent> components = new List<CareMeasureComponent> ();

        #endregion


        #region Public Properties

        public Int64 CareMeasureDomainId { get { return careMeasureDomainId; } set { careMeasureDomainId = value; } }

        public String CareMeasureDomainName { get { return careMeasureDomainName; } set { careMeasureDomainName = value; } }


        public Int64 CareMeasureClassId { get { return careMeasureClassId; } set { careMeasureClassId = value; } }

        public String CareMeasureClassName { get { return careMeasureClassName; } set { careMeasureClassName = value; } }


        public List<CareMeasureComponent> Components { get { return components; } set { components = value; } }

        public override Application Application { 
            
            get { return base.Application; }

            set {

                base.Application = value;

                foreach (CareMeasureComponent currentComponent in components) {

                    currentComponent.Application = value;

                }

            }

        }

        #endregion


        #region Public Properties

        public CareMeasureDomain CareMeasureDomain { get { return application.CareMeasureDomainGet (careMeasureDomainId); } }

        public CareMeasureClass CareMeasureClass { get { return application.CareMeasureClassGet (careMeasureClassId); } }

        #endregion 


        #region Constructors

        public CareMeasure (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareMeasure (Application applicationReference, Int64 forId) {

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

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureDomainId", CareMeasureDomainId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureDomainName", CareMeasureDomainName);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureClassId", CareMeasureClassId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureClassName", CareMeasureClassName);

            #endregion


            #region Components

            System.Xml.XmlNode componentsNode = document.CreateElement ("Components");

            document.LastChild.AppendChild (componentsNode);

            foreach (CareMeasureComponent currentComponent in components) {

                componentsNode.AppendChild (document.ImportNode (currentComponent.XmlSerialize ().LastChild, true));

            }

            #endregion 


            #region Object Nodes

            document.LastChild.AppendChild (document.ImportNode (CareMeasureClass.XmlSerialize ().LastChild, true));

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

                                    case "CareMeasureDomainName": 
                                        
                                        careMeasureDomainName = currentPropertyNode.InnerText;

                                        careMeasureDomainId = application.CoreObjectGetIdByName ("CareMeasureDomain", careMeasureDomainName);

                                        break;

                                    default: break;

                                }

                            }

                            break;

                        case "Components":

                            foreach (System.Xml.XmlNode currentComponentNode in currentNode.ChildNodes) {

                                CareMeasureComponent component = new CareMeasureComponent (application);

                                response.AddRange (component.XmlImport (currentComponentNode));

                                components.Add (component);

                            }

                            break;

                        case "CareMeasureClass":

                            // USES THE DOMAIN ID CAPTURED UNDER PROPERTIES

                            careMeasureClassName = currentNode.Attributes["Name"].InnerText;

                            CareMeasureClass careMeasureClass = application.CareMeasureClassGetByName (careMeasureDomainId, careMeasureClassName);

                            if (careMeasureClass == null) {

                                // DOES NOT EXIST, CREATE NEW FROM IMPORT

                                careMeasureClass = new CareMeasureClass (application);

                                response.AddRange (careMeasureClass.XmlImport (currentNode));

                                careMeasureDomainId = careMeasureClass.CareMeasureDomainId;

                                careMeasureClassId = careMeasureClass.Id;


                                if (careMeasureClassId == 0) {

                                    throw new ApplicationException ("Unable to import Care Measure Class: " + currentNode.Attributes["Name"].InnerText + ".");

                                }

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // SAVE IMPORTED CLASS

                if (!Save ()) { throw new ApplicationException ("Unable to save Care Measure: " + Name + "."); }

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



            // VALIDATE DOMAIN IS NOT EMPTY

            CareMeasureDomainName = CareMeasureDomainName;

            if (String.IsNullOrWhiteSpace (CareMeasureDomainName)) { validationResponse.Add ("Domain", "Empty or Null"); }


            // VALIDATE CLASS IS NOT EMPTY 

            CareMeasureClassName = CareMeasureClassName;

            if (String.IsNullOrWhiteSpace (CareMeasureClassName)) { validationResponse.Add ("Class", "Empty or Null"); }


            if (components == null) { components = new List<CareMeasureComponent> (); }

            if (components.Count == 0) { validationResponse.Add ("Components", "No components specified for Measure."); }


            // TODO: VALIDATE DUPLICATE

            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (long forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable careMeasureTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT CareMeasure.*, ");

            selectStatement.Append ("    CareMeasureDomain.CareMeasureDomainId, CareMeasureDomain.CareMeasureDomainName, CareMeasureClass.CareMeasureClassName ");

            selectStatement.Append ("  FROM CareMeasure ");

            selectStatement.Append ("    LEFT JOIN CareMeasureClass ON CareMeasure.CareMeasureClassId = CareMeasureClass.CareMeasureClassId");

            selectStatement.Append ("    LEFT JOIN CareMeasureDomain ON CareMeasureClass.CareMeasureDomainId = CareMeasureDomain.CareMeasureDomainId");

            selectStatement.Append ("    WHERE CareMeasureId = " + forId.ToString ());

            careMeasureTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (careMeasureTable.Rows.Count == 1) {

                MapDataFields (careMeasureTable.Rows[0]);


                components = new List<CareMeasureComponent> ();

                System.Data.DataTable componentsTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM dbo.CareMeasureComponent WHERE CareMeasureId = " + Id.ToString (), 0);

                foreach (System.Data.DataRow currentComponentRow in componentsTable.Rows) {

                    CareMeasureComponent component = new CareMeasureComponent (application);

                    component.MapDataFields (currentComponentRow);

                    components.Add (component);

                }

                success = true;

            }

            else { success = false; }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            careMeasureDomainId = IdFromSql (currentRow, "CareMeasureDomainId");

            careMeasureDomainName = StringFromSql (currentRow, "CareMeasureDomainName");

            careMeasureClassId = IdFromSql (currentRow, "CareMeasureClassId");

            careMeasureClassName = StringFromSql (currentRow, "CareMeasureClassName");


            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            String childIds = String.Empty;


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareMeasureManage)) { throw new ApplicationException ("PermissionDenied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                ModifiedAccountInfo = new Data.AuthorityAccountStamp (application);

                application.EnvironmentDatabase.BeginTransaction ();


                careMeasureDomainId = application.CoreObjectGetIdByName ("CareMeasureDomain", careMeasureDomainName);

                if (careMeasureDomainId == 0) {

                    CareMeasureDomain careMeasureDomain = new CareMeasureDomain (application);

                    careMeasureDomain.Name = careMeasureDomainName;

                    careMeasureDomain.Description = careMeasureDomainName;

                    careMeasureDomain.Enabled = true;

                    careMeasureDomain.Visible = true;

                    careMeasureDomain.Save ();

                    careMeasureDomainId = careMeasureDomain.Id;

                }

                careMeasureClassId = application.CareMeasureClassGetIdByName (careMeasureDomainId, careMeasureClassName);

                if (careMeasureClassId == 0) {

                    CareMeasureClass careMeasureClass = new CareMeasureClass (application);

                    careMeasureClass.Name = careMeasureClassName;

                    careMeasureClass.Description = careMeasureClassName;

                    careMeasureClass.CareMeasureDomainId = careMeasureDomainId;

                    careMeasureClass.Enabled = true;

                    careMeasureClass.Visible = true;

                    careMeasureClass.Save ();

                    careMeasureClassId = careMeasureClass.Id;

                }



                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("CareMeasure_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureClassId", careMeasureClassId);


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


                foreach (CareMeasureComponent currentComponent in components) {

                    if (currentComponent.Id != 0) {

                        childIds += currentComponent.Id.ToString () + ", ";

                    }

                }

                if (!String.IsNullOrWhiteSpace (childIds)) { // DELETE ALL REMOVED CHILD IDS FROM DATABASE

                    childIds += "0";

                    String deleteStatement = "DELETE FROM dbo.CareMeasureComponent WHERE CareMeasureId = " + Id.ToString () + " AND CareMeasureComponentId NOT IN (" + childIds + ")";

                    success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement);

                    if (!success) { throw new ApplicationException ("Unable to propertly delete child Components from database."); }

                }

                foreach (CareMeasureComponent currentComponent in components) {

                    currentComponent.Application = application;

                    currentComponent.CareMeasureId = id;

                    success = currentComponent.Save ();

                    if (!success) { throw new ApplicationException ("Unable to save Measure Component."); }

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
