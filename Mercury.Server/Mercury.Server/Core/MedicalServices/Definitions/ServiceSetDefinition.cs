using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.MedicalServices.Definitions {

    [DataContract (Name = "ServiceSetDefinition")]
    public class ServiceSetDefinition : CoreObject {

        #region Private Properties

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId; // PARENT SERVICESET OBJECT THAT OWNS THE DEFINITION

        [DataMember (Name = "DefinitionServiceId")]
        private Int64 definitionServiceId; // THE CHILD SERVICE THAT THE DEFINITION POINTS TO

        [DataMember (Name = "Enabled")]
        private Boolean enabled = true;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public Int64 DefinitionServiceId { get { return definitionServiceId; } set { definitionServiceId = value; } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }            

        #endregion


        #region Public Properties

        public Service DefinitionService { get { return application.MedicalServiceGet (definitionServiceId); } }

        #endregion 


        #region Constructors

        public ServiceSetDefinition (Application application) { base.BaseConstructor (application); }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceId", serviceId.ToString ());

            //CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DefinitionServiceId", definitionServiceId.ToString ());

            //CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DefinitionServiceName", application.CoreObjectGetNameById ("Service", definitionServiceId));

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Enabled", Enabled.ToString ());


            // TODO: UPDATE V2, ADD SERVICE DEFINITION AS CHILD PROPERTY


            System.Xml.XmlElement definitionServicePropertyNode = CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DefinitionService", String.Empty);

            definitionServicePropertyNode.AppendChild (document.ImportNode (DefinitionService.XmlSerialize ().ChildNodes[1], true));


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            try {

                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        case "ServiceId": ServiceId = Convert.ToInt64 (currentPropertyNode.InnerText); break;

                        case "Enabled": enabled = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                        case "DefinitionService":
                            
                            if (currentPropertyNode.ChildNodes.Count > 0) {
                                
                                System.Xml.XmlNode definitionServiceNode = currentPropertyNode.ChildNodes[0];

                                String definitionServiceName = definitionServiceNode.Attributes["Name"].InnerText;

                                String definitionServiceType = definitionServiceNode.Name;

                                definitionServiceId = base.application.CoreObjectGetIdByName ("Service", definitionServiceName);

                                Core.MedicalServices.Service definitionService = application.MedicalServiceGet (definitionServiceId);

                                if (definitionService == null) {

                                    definitionService = ((definitionServiceType == "ServiceSingleton") ? (Core.MedicalServices.Service)new ServiceSingleton (application) : (Core.MedicalServices.Service) new ServiceSet (application));

                                    response.AddRange (definitionService.XmlImport (definitionServiceNode));

                                    definitionServiceId = definitionService.Id;

                                }

                                if (definitionServiceId == 0) { throw new ApplicationException ("Unable to import Service for Set."); }

                            }
                            
                            break; 

                    }

                }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        //#region XML Serialization

        //public override System.Xml.XmlDocument XmlSerialize () {

        //    System.Xml.XmlDocument serviceDocument = base.XmlSerialize ();

        //    System.Xml.XmlNode serviceNode = serviceDocument.GetElementsByTagName ("Service")[0];

        //    System.Xml.XmlElement propertiesNode = (System.Xml.XmlElement) serviceDocument.ChildNodes[1].ChildNodes[0];

        //    System.Xml.XmlElement definitionsNode;



        //    #region Properties

        //    CommonFunctions.XmlDocumentAppendPropertyNode (serviceDocument, propertiesNode, "SetType", ((Int32) setType).ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (serviceDocument, propertiesNode, "WithinDays", withinDays.ToString ());

        //    #endregion


        //    definitionsNode = serviceDocument.CreateElement ("Definitions");

        //    serviceNode.AppendChild (definitionsNode);


        //    foreach (MedicalServices.Definitions.SetDefinition currentDefinition in definitions) {

        //        System.Xml.XmlElement definitionNode = serviceDocument.CreateElement ("Definition");

        //        definitionNode.SetAttribute ("SetDefinitionId", currentDefinition.DefinitionId.ToString ());

        //        definitionNode.SetAttribute ("ServiceId", currentDefinition.ServiceId.ToString ());

        //        definitionNode.SetAttribute ("DefinitionServiceId", currentDefinition.DefinitionServiceId.ToString ());

        //        definitionNode.SetAttribute ("Enabled", currentDefinition.Enabled.ToString ());


        //        Core.MedicalServices.Service definitionService = new Service (base.application, currentDefinition.DefinitionServiceId);

        //        switch (definitionService.ServiceType) {

        //            case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton: definitionService = new ServiceSingleton (base.application, currentDefinition.DefinitionServiceId); break;

        //            case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set: definitionService = new ServiceSet (base.application, currentDefinition.DefinitionServiceId); break;

        //        }

        //        definitionNode.AppendChild (serviceDocument.ImportNode (definitionService.XmlSerialize ().GetElementsByTagName ("Service")[0], true));

        //        definitionsNode.AppendChild (definitionNode);

        //    }


        //    return serviceDocument;

        //}

        //public override List<Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = objectNode.Attributes["Name"].InnerText;

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "Service") {

        //        if ((MedicalServices.Enumerations.MedicalServiceType) Convert.ToInt32 (objectNode.Attributes["ServiceType"].InnerText) == Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set) { 

        //            XmlReadBaseServiceProperties (objectNode);


        //            foreach (System.Xml.XmlNode currentProperty in objectNode.ChildNodes[0]) {

        //                switch (currentProperty.Attributes["Name"].InnerText) {

        //                    case "SetType": setType = (Mercury.Server.Core.MedicalServices.Enumerations.ServiceSetType) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "WithinDays": withinDays = Convert.ToInt32 (currentProperty.InnerText); break;

        //                }

        //            }


        //            foreach (System.Xml.XmlNode currentDefinitionNode in objectNode.ChildNodes[1]) {

        //                MedicalServices.Definitions.SetDefinition definition = new Mercury.Server.Core.MedicalServices.Definitions.SetDefinition ();

        //                definition.ServiceId = this.ServiceId;

        //                foreach (System.Xml.XmlAttribute currentAttribute in currentDefinitionNode.Attributes) {

        //                    switch (currentAttribute.Name) {

        //                        case "DefinitionServiceId":

        //                            String serviceName = currentDefinitionNode.ChildNodes[0].Attributes["Name"].InnerText;

        //                            Service medicalService = base.application.MedicalServiceGet (serviceName);

        //                            if (medicalService != null) {

        //                                definition.DefinitionServiceId = medicalService.ServiceId;

        //                            }

        //                            else {

        //                                System.Xml.XmlNode serviceNode = currentDefinitionNode.ChildNodes[0];

        //                                Enumerations.MedicalServiceType definitionServiceType = (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) Convert.ToInt32 (serviceNode.Attributes["ServiceType"].InnerText);

        //                                switch (definitionServiceType) {

        //                                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton:

        //                                        ServiceSingleton singleton = new ServiceSingleton (base.application);

        //                                        response.AddRange (singleton.XmlImport (serviceNode));

        //                                        definition.DefinitionServiceId = singleton.ServiceId;

        //                                        break;

        //                                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set:

        //                                        ServiceSet serviceSet = new ServiceSet (base.application);

        //                                        response.AddRange (serviceSet.XmlImport (serviceNode));

        //                                        definition.DefinitionServiceId = serviceSet.ServiceId;

        //                                        break;

        //                                }

        //                                if (definition.DefinitionServiceId == 0) {

        //                                    importResponse.SetException (new ApplicationException ("Unable to create dependent Service: " + serviceNode.Attributes["Name"].InnerText));

        //                                }


        //                            }

        //                            break;

        //                        case "Enabled": definition.Enabled = Convert.ToBoolean (currentAttribute.InnerText); break;

        //                    }

        //                }

        //                if (!importResponse.Success) { break; }

        //                definitions.Add (definition);

        //            }

        //            importResponse.Success = Save ();

        //            importResponse.Id = Id;

        //            if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }

        //        }

        //        else { importResponse.SetException (new ApplicationException ("Invalid Service Type Specified.")); }

        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as Service.")); }


        //    response.Add (importResponse);


        //    return response;

        //}

        //#endregion

        #endregion


        #region Validation

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();

            // Validate No Circular References

            //if (serviceId != 0) {

            //    if (HasCircularReference (application, serviceId)) {

            //        if (!validationResponse.ContainsKey ("Circular Reference")) {

            //            validationResponse.Add ("Circular Reference", serviceId.ToString ());

            //        }

            //    }

            //}

            return validationResponse;

        }

        #endregion 


        #region Database Functions

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;


            try {

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC ServiceSetDefinition_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (serviceId.ToString () + ", ");

                sqlStatement.Append (definitionServiceId.ToString () + ", ");

                sqlStatement.Append ("'" + Convert.ToInt32 (enabled).ToString () + "'");

                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();


                success = true;

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            serviceId = (Int64) currentRow["ServiceId"];

            definitionServiceId = (Int64) currentRow["DefinitionServiceId"];

            enabled = (Boolean) currentRow["Enabled"];

            return;

        }

        #endregion


        #region Public Methods

        public Boolean HasCircularReference (Application application, Int64 forServiceId) {

            Boolean circularReferenceFound = false;

            Service medicalService;

            ServiceSet serviceSet;

            if (definitionServiceId == forServiceId) { return true; }

            medicalService = new Service (application, forServiceId);

            if (medicalService.ServiceType == Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set) {

                serviceSet = new ServiceSet (application, forServiceId);

                foreach (ServiceSetDefinition currentDefinition in serviceSet.Definitions) {

                    circularReferenceFound = circularReferenceFound || (currentDefinition.HasCircularReference (application, forServiceId));

                    if (circularReferenceFound) { break; }

                }

            }

            return circularReferenceFound;

        }

        public String SqlStatement {

            get {

                String sqlStatement = String.Empty;

                String selectClause = String.Empty;

                String fromClause = String.Empty;

                String whereClause = String.Empty;


                selectClause = selectClause + "SELECT ";

                selectClause = selectClause + "\r\n      CAST (" + serviceId.ToString () + " AS BIGINT) AS ParentServiceId, CAST (" + Id.ToString () + " AS BIGINT) AS ServiceSetDefinitionId, ";

                selectClause = selectClause + "\r\n      MemberService.MemberId, MemberService.MemberServiceId AS DetailMemberServiceId, MemberService.EventDate, ";
                
                selectClause = selectClause + "\r\n      Service.ServiceId AS ServiceId, Service.ServiceName AS ServiceName, Service.ServiceType AS ServiceType ";

                fromClause = "\r\n  FROM MemberService";

                fromClause = fromClause + "\r\n      JOIN Service ON MemberService.ServiceId = Service.ServiceId";

                // TODO: JOIN TO MAKE SURE THAT IT WASN'T PREVIOUS USED BY OTHER OF THE SAME TYPE

                fromClause = fromClause + "\r\n      LEFT JOIN MemberServiceDetailSet ON MemberService.MemberServiceId = MemberServiceDetailSet.DetailMemberServiceId";

                fromClause = fromClause + "\r\n        AND MemberServiceDetailSet.ParentServiceId = " + ServiceId.ToString ();

                whereClause = "\r\n  WHERE MemberService.ServiceId = " + definitionServiceId.ToString ();

                whereClause = whereClause + "\r\n    AND MemberServiceDetailSet.MemberServiceId IS NULL";


                sqlStatement = selectClause + fromClause + whereClause;
                
                
                return sqlStatement;

            }

        }

        #endregion

    }

}
