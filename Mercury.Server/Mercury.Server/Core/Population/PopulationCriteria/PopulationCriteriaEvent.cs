using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.PopulationCriteria {

    [DataContract (Name = "PopulationCriteriaEvent")]
    public class PopulationCriteriaEvent : CoreObject {

        #region Private Properties

        [DataMember (Name = "PopulationId")]
        private Int64 populationId = 0;

        [DataMember (Name = "EventType")]
        private Enumerations.PopulationCriteriaEventType eventType = Mercury.Server.Core.Population.Enumerations.PopulationCriteriaEventType.Identifying;

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId;

        #endregion


        #region Public Properties - Encapsulated
        
        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Enumerations.PopulationCriteriaEventType EventType { get { return eventType; } set { eventType = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        #endregion


        #region Public Properties 

        public MedicalServices.Service Service { get { return application.MedicalServiceGet (serviceId); } }

        #endregion 


        #region Constructors

        public PopulationCriteriaEvent (Application applicationReference) { base.BaseConstructor (applicationReference); }

        public PopulationCriteriaEvent (Application applicationReference, Int64 forId) { base.BaseConstructor (applicationReference, forId); }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EventTypeInt32", ((Int32)EventType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EventType", EventType.ToString ());

            #endregion


            #region Object Nodes

            if (Service != null) { document.LastChild.AppendChild (document.ImportNode (Service.XmlSerialize ().LastChild, true)); }

            #endregion 


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            MedicalServices.Service service = null;

            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Properties":

                            #region Properties

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].Value) {

                                    case "EventTypeInt32": EventType = (Enumerations.PopulationCriteriaEventType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                }

                            }

                            #endregion

                            break;

                        case "ServiceSet":

                            service = application.MedicalServiceGet (currentNode.Attributes["Name"].Value);

                            if (service == null) {

                                MedicalServices.ServiceSet serviceSet = new MedicalServices.ServiceSet (application);

                                response.AddRange (serviceSet.XmlImport (currentNode));

                                service = serviceSet;

                            }

                            if (service != null) { serviceId = service.Id; }

                            break;

                        case "ServiceSingleton":
                            
                            service = application.MedicalServiceGet (currentNode.Attributes["Name"].Value);

                            if (service == null) {

                                MedicalServices.ServiceSingleton serviceSingleton = new MedicalServices.ServiceSingleton (application);

                                response.AddRange (serviceSingleton.XmlImport (currentNode));

                                service = serviceSingleton;

                            }

                            if (service != null) { serviceId = service.Id; }

                            break;

                    } // switch (currentNode.Name) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                // SAVE IS PERFORMED BY PARENT OBJECT

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion
               
        
        #region Database Functions

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;


            try {

                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.PopulationCriteriaEvent_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (populationId.ToString () + ", ");

                sqlStatement.Append (((Int32) eventType).ToString () + ", ");

                sqlStatement.Append (serviceId.ToString () + ", ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

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


            populationId = (Int64) currentRow["PopulationId"];

            eventType = (Mercury.Server.Core.Population.Enumerations.PopulationCriteriaEventType) (Int32) currentRow["EventType"];

            serviceId = (Int64) currentRow["ServiceId"];

            return;

        }

        #endregion


    }

}
