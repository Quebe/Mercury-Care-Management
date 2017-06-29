using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.PopulationEvents {

    [DataContract (Name = "PopulationTriggerEvent")]
    public class PopulationTriggerEvent : CoreObject {

        #region Private Properties

        [DataMember (Name = "PopulationId")]
        private Int64 populationId;

        [DataMember (Name = "EventType")]
        private Enumerations.PopulationTriggerEventType eventType = Mercury.Server.Core.Population.Enumerations.PopulationTriggerEventType.Service;

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId;

        [DataMember (Name = "MetricType")]
        private Core.Metrics.Enumerations.MetricType metricType = Mercury.Server.Core.Metrics.Enumerations.MetricType.Health;

        [DataMember (Name = "MetricId")]
        private Int64 metricId;

        [DataMember (Name = "MetricMinimum")]
        private Decimal metricMinimum;

        [DataMember (Name = "MetricMaximum")]
        private Decimal metricMaximum;

        [DataMember (Name = "AuthorizedServiceId")]
        private Int64 authorizedServiceId;

        [DataMember (Name = "ProblemStatementId")]
        private Int64 problemStatementId;

        [DataMember (Name = "Action")]
        private Core.Action.Action action;


        private Population population = null;

        #endregion


        #region Public Properties

        public override String Description {

            get {

                if (application == null) { return String.Empty; }


                String triggerText = String.Empty;

                switch (eventType) {

                    case Mercury.Server.Core.Population.Enumerations.PopulationTriggerEventType.Service:

                        Core.MedicalServices.Service medicalService = application.MedicalServiceGet (serviceId);

                        if (medicalService != null) {

                            triggerText = medicalService.Name;

                        }

                        break;

                    case Mercury.Server.Core.Population.Enumerations.PopulationTriggerEventType.Metric:

                        Core.Metrics.Metric metric = application.MetricGet (metricId);

                        if (metric != null) {

                            triggerText = metric.MetricType.ToString () + ": " + metric.Name + " (" + metricMinimum.ToString () + " - " + MetricMaximum.ToString () + ")";

                        }

                        break;

                    case Mercury.Server.Core.Population.Enumerations.PopulationTriggerEventType.AuthorizedService:

                        Core.AuthorizedServices.AuthorizedService authorizedService = application.AuthorizedServiceGet (authorizedServiceId);

                        if (authorizedService != null) {

                            triggerText = authorizedService.Name;

                        }

                        break;

                }

                return triggerText;

            }

        }

        
        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Enumerations.PopulationTriggerEventType EventType { get { return eventType; } set { eventType = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public Metrics.Enumerations.MetricType MetricType { get { return metricType; } set { metricType = value; } }

        public Int64 MetricId { get { return metricId; } set { metricId = value; } }

        public Decimal MetricMinimum { get { return metricMinimum; } set { metricMinimum = value; } }

        public Decimal MetricMaximum { get { return metricMaximum; } set { metricMaximum = value; } }

        public Int64 AuthorizedServiceId { get { return authorizedServiceId; } set { authorizedServiceId = value; } }

        public Int64 ProblemStatementId { get { return problemStatementId; } set { problemStatementId = value; } }

        public Core.Action.Action Action { get { return action; } set { action = value; } }


        public Population Population { get { return population; } set { population = value; } }

        public override Application Application {

            set {

                base.Application = value;

                // PROPOGATE: SET ALL CHILD REFERENCES

                action.Application = value;

            }

        }

        #endregion


        #region Public Properties
        
        public AuthorizedServices.AuthorizedService AuthorizedService { get { return application.AuthorizedServiceGet (authorizedServiceId); } }

        public MedicalServices.Service Service { get { return application.MedicalServiceGet (serviceId); } }

        public Metrics.Metric Metric { get {return application.MetricGet (metricId); } }

        public Individual.ProblemStatement ProblemStatement { get { return application.ProblemStatementGet (problemStatementId); } }

        #endregion 


        #region Constructors

        protected void ObjectConstructor (Application applicationReference) {

            BaseConstructor (applicationReference);

            action = new Mercury.Server.Core.Action.Action (applicationReference);

            return;

        }

        public PopulationTriggerEvent (Application applicationReference) {

            ObjectConstructor (applicationReference);

            action = new Mercury.Server.Core.Action.Action (applicationReference);
            
            return;  
        
        }

        public PopulationTriggerEvent (Application applicationReference, Int64 forTriggerEvent) {

            ObjectConstructor (applicationReference);

            BaseConstructor (applicationReference, forTriggerEvent);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EventTypeInt32", ((Int32) EventType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EventType", EventType.ToString ());
            
            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MetricTypeInt32", ((Int32) MetricType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MetricType", MetricType.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MetricMinimum", MetricMinimum.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MetricMaximum", MetricMaximum.ToString ());


            
            if (AuthorizedService != null) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AuthorizedServiceId", AuthorizedService.Id.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AuthorizedServiceName", AuthorizedService.Name);

            }

            if (Service != null) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceId", Service.Id.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceName", Service.Name);

            }

            if (Metric != null) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MetricId", Metric.Id.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "MetricName", Metric.Name);

            }
            

            if (ProblemStatement != null) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProblemStatementId", ProblemStatement.Id.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProblemStatementName", ProblemStatement.Name);

            }

            #endregion 


            #region Object Nodes

            if (Action != null) { document.LastChild.AppendChild (document.ImportNode (Action.XmlSerialize ().LastChild, true)); }

            #endregion


            #region Dependencies Nodes

            System.Xml.XmlNode dependenciesNode = document.CreateElement ("Dependencies");

            document.LastChild.InsertBefore (dependenciesNode, propertiesNode);

            if (AuthorizedService != null) { dependenciesNode.AppendChild (document.ImportNode (AuthorizedService.XmlSerialize ().LastChild, true)); }

            if (Service != null) { dependenciesNode.AppendChild (document.ImportNode (Service.XmlSerialize ().LastChild, true)); }

            if (Metric != null) { dependenciesNode.AppendChild (document.ImportNode (Metric.XmlSerialize ().LastChild, true)); }

            if (ProblemStatement != null) { dependenciesNode.AppendChild (document.ImportNode (ProblemStatement.XmlSerialize ().LastChild, true)); }

            #endregion 


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            MedicalServices.Service service = null;

            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Dependencies":

                            #region Dependencies

                            foreach (System.Xml.XmlNode currentDependencyNode in currentNode.ChildNodes) {

                                switch (currentDependencyNode.Name) {

                                    case "ServiceSet":

                                        service = application.MedicalServiceGet (currentNode.Attributes["Name"].Value);

                                        if (service == null) {

                                            MedicalServices.ServiceSet serviceSet = new MedicalServices.ServiceSet (application);

                                            response.AddRange (serviceSet.XmlImport (currentNode));

                                        }

                                        break;

                                    case "ServiceSingleton":

                                        service = application.MedicalServiceGet (currentNode.Attributes["Name"].Value);

                                        if (service == null) {

                                            MedicalServices.ServiceSingleton serviceSingleton = new MedicalServices.ServiceSingleton (application);

                                            response.AddRange (serviceSingleton.XmlImport (currentNode));

                                        }

                                        break;

                                    case "AuthorizedService":

                                        Core.AuthorizedServices.AuthorizedService authorizedService = application.AuthorizedServiceGet (currentNode.Attributes["Name"].Value);

                                        if (authorizedService == null) { 

                                            authorizedService = new AuthorizedServices.AuthorizedService (application);

                                            response.AddRange (authorizedService.XmlImport (currentDependencyNode));

                                        }
                                        
                                        break;

                                    case "Metric":
                                        
                                        Core.Metrics.Metric metric = application.MetricGet (currentNode.Attributes["Name"].Value);

                                        if (metric == null) { 

                                            metric = new Metrics.Metric (application);

                                            response.AddRange (metric.XmlImport (currentDependencyNode));

                                        }
                                        
                                        break;

                                    case "ProblemStatement":
                                                     
                                        // TODO: 
                                        
                                        break;

                                }

                            }

                            #endregion 

                            break;

                        case "Properties":

                            #region Properties

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].Value) {

                                    case "EventTypeInt32": EventType = (Enumerations.PopulationTriggerEventType) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "MetricTypeInt32": MetricType = (Metrics.Enumerations.MetricType) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "MetricMinimum": MetricMinimum = Convert.ToDecimal (currentPropertyNode.InnerText); break;

                                    case "MetricMaximum": MetricMaximum = Convert.ToDecimal (currentPropertyNode.InnerText); break;

                                    case "AuthorizedServiceName": authorizedServiceId = application.AuthorizedServiceGetIdByName (currentPropertyNode.InnerText); break;

                                    case "ServiceName": serviceId = application.CoreObjectGetIdByName ("Service", currentPropertyNode.InnerText); break;

                                    case "MetricName": metricId = application.MetricGetIdByName (currentPropertyNode.InnerText); break;

                                    case "ProblemStatementName": /* TODO */ break;

                                }

                            }

                            #endregion

                            break;

                        case "Action":

                            action = new Action.Action (application);

                            response.AddRange (action.XmlImport (currentNode));

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

        //public override List<Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = "TriggerEvent";

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "TriggerEvent") {

        //        try {

        //            #region Trigger Properties

        //            foreach (System.Xml.XmlNode currentProperty in objectNode.ChildNodes[0]) {

        //                String propertyName = currentProperty.Attributes["Name"].InnerText;

        //                switch (propertyName) {

        //                    case "EventType": eventType = (Mercury.Server.Core.Population.Enumerations.TriggerEventType) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "MetricType": metricType = (Mercury.Server.Core.Metrics.Enumerations.MetricType) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "MetricMinimum": metricMinimum = Convert.ToDecimal (currentProperty.InnerText); break;

        //                    case "MetricMaximum": metricMaximum = Convert.ToDecimal (currentProperty.InnerText); break;

        //                    case "ProblemStatementId": problemStatementId = Convert.ToInt64 (currentProperty.InnerText); break;

        //                    case "Service":

        //                        #region Service

        //                        if (currentProperty.ChildNodes.Count > 0) {

        //                            System.Xml.XmlNode serviceNode = currentProperty.ChildNodes[0];

        //                            String serviceName = serviceNode.Attributes["Name"].InnerText;

        //                            Core.MedicalServices.Service medicalService = base.application.MedicalServiceGet (serviceName);

        //                            if (medicalService != null) {

        //                                serviceId = medicalService.ServiceId;

        //                            }

        //                            else {

        //                                Core.MedicalServices.Enumerations.MedicalServiceType serviceType = (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) Convert.ToInt32 (serviceNode.Attributes["ServiceType"].InnerText);

        //                                switch (serviceType) {

        //                                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton:

        //                                        Core.MedicalServices.ServiceSingleton singleton = new Core.MedicalServices.ServiceSingleton (base.application);

        //                                        response.AddRange (singleton.XmlImport (serviceNode));

        //                                        serviceId = singleton.ServiceId;

        //                                        break;

        //                                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set:

        //                                        Core.MedicalServices.ServiceSet serviceSet = new Core.MedicalServices.ServiceSet (base.application);

        //                                        response.AddRange (serviceSet.XmlImport (serviceNode));

        //                                        serviceId = serviceSet.ServiceId;

        //                                        break;

        //                                }

        //                            }

        //                            if ((currentProperty.Attributes["Name"].InnerText == "Service") && (serviceId == 0)) { throw new ApplicationException ("Unable to load Service."); }

        //                        }

        //                        #endregion

        //                        break;

        //                    case "Metric":

        //                        #region Metric

        //                        if (currentProperty.ChildNodes.Count > 0) {

        //                            System.Xml.XmlNode metricNode = currentProperty.ChildNodes[0];

        //                            String metricName = metricNode.Attributes["Name"].InnerText;

        //                            Core.Metrics.Metric metric = base.application.MetricGet (metricName);

        //                            if (metric != null) {

        //                                metricId = metric.MetricId;

        //                            }

        //                            else {

        //                                metric = new Mercury.Server.Core.Metrics.Metric (base.application);

        //                                response.AddRange (metric.XmlImport (metricNode));

        //                                metricId = metric.MetricId;

        //                            }

        //                            if (metricId == 0) { throw new ApplicationException ("Unable to import Metric."); }

        //                        }

        //                        #endregion 

        //                        break;

        //                    case "AuthorizedService":

        //                        #region AuthorizedService

        //                        if (currentProperty.ChildNodes.Count > 0) {

        //                            System.Xml.XmlNode authorizedServiceNode = currentProperty.ChildNodes[0];

        //                            String authorizedServiceName = authorizedServiceNode.Attributes["Name"].InnerText;

        //                            Core.AuthorizedServices.AuthorizedService authorizedService = base.application.AuthorizedServiceGet (authorizedServiceId);

        //                            if (authorizedService != null) {

        //                                authorizedServiceId = authorizedService.AuthorizedServiceId;

        //                            }

        //                            else {

        //                                authorizedService = new Mercury.Server.Core.AuthorizedServices.AuthorizedService (base.application);

        //                                response.AddRange (authorizedService.XmlImport (authorizedServiceNode));

        //                                authorizedServiceId = authorizedService.AuthorizedServiceId;

        //                            }

        //                            if (authorizedServiceId == 0) { throw new ApplicationException ("Unable to import Authorized Service."); }

        //                        }

        //                        #endregion

        //                        break;

        //                    case "Action":

        //                        #region Action

        //                        if (currentProperty.ChildNodes.Count > 0) {

        //                            System.Xml.XmlNode actionNode = currentProperty.ChildNodes[0];

        //                            Int32 actionId = Convert.ToInt32 (actionNode.Attributes["ActionId"].InnerText);

        //                            String actionName = actionNode.Attributes["Name"].InnerText;

        //                            if (actionId != 0) {

        //                                action = new Mercury.Server.Core.Action.Action (base.application, actionId, actionName);

        //                                response.AddRange (action.XmlImport (actionNode));

        //                            }

        //                        }

        //                        #endregion 

        //                        break;

        //                }

        //            }

        //            #endregion


        //            if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }

        //        }

        //        catch (Exception importException) {

        //            importResponse.SetException (importException);

        //        }


        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as Population.")); }


        //    response.Add (importResponse);

        //    return response;

        //}

        //#endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            if (action == null) { action = new Mercury.Server.Core.Action.Action (application); }

            switch (eventType) {

                case Mercury.Server.Core.Population.Enumerations.PopulationTriggerEventType.Metric:

                    if (metricMinimum > metricMaximum) {

                        validationResponse.Add ("Metric Value Range", "Minimum (Floor) is greater than Maximum (Ceiling).");

                    }

                    break;

            }

            if ((problemStatementId == 0) && (action.Id == 0)) {

                validationResponse.Add ("Problem Statement or Action", "Triggers Requires a Problem Statement or Action.");

            }

            else if (action.Id != 0) {

                Dictionary<String, String> actionValidation = action.Validate ();

                foreach (String currentKey in actionValidation.Keys) {

                    if (!validationResponse.ContainsKey (currentKey)) {

                        validationResponse.Add (currentKey, actionValidation[currentKey]);

                    }

                }

            }

            return validationResponse;

        }

        #endregion


        #region Database Functions

        override public Boolean Save () {

            Boolean success = false;

            try {

                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("PopulationTriggerEvent_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@triggerEventId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@populationId", populationId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@eventType", ((Int32)eventType));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@serviceId", base.IdSqlAllowNullInt64 (serviceId));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@metricType", ((Int32)metricType));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@metricId", base.IdSqlAllowNullInt64 (metricId));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@metricMinimum", metricMinimum);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@metricMaximum", metricMaximum);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@authorizedServiceId", base.IdSqlAllowNullInt64 (authorizedServiceId));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@problemStatementId", base.IdSqlAllowNullInt64 (problemStatementId));


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@actionId", Action.Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@actionParameters", Action.ActionParametersXml);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@actionDescription", Action.Description, Server.Data.DataTypeConstants.Description99);


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

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            populationId = (Int64) currentRow["PopulationId"];

            eventType = (Mercury.Server.Core.Population.Enumerations.PopulationTriggerEventType) (Int32) currentRow ["TriggerEventType"];

            serviceId = base.IdFromSql (currentRow, "ServiceId");

            metricType = (Mercury.Server.Core.Metrics.Enumerations.MetricType) (Int32) currentRow ["MetricType"];

            metricId = base.IdFromSql (currentRow, "MetricId");

            metricMinimum = (Decimal) currentRow["MetricMinimum"];

            metricMaximum = (Decimal) currentRow["MetricMaximum"];

            authorizedServiceId = base.IdFromSql (currentRow, "AuthorizedServiceId");

            problemStatementId = base.IdFromSql (currentRow, "ProblemStatementId");


            action = new Mercury.Server.Core.Action.Action (base.application);

            action.MapDataFields (String.Empty, currentRow);

            return;

        }

        #endregion


        #region Virtual - Data Bindings

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            return dataValue;

        }

        override public Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> dataBindings = new Dictionary<String, String> ();


                dataBindings.Add ("TriggerEvent.Id", "Id|TriggerEvent");

                dataBindings.Add ("TriggerEvent.EventType", "String");

                dataBindings.Add ("TriggerEvent.ServiceId", "Id|Service");

                dataBindings.Add ("TriggerEvent.MetricType", "String");

                dataBindings.Add ("TriggerEvent.MetricId", "Id|Metric");

                dataBindings.Add ("TriggerEvent.AuthorizedServiceId", "Id|AuthorizedService");

                dataBindings.Add ("TriggerEvent.ProblemStatementId", "Id|ProblemStatement");

                foreach (String bindingName in new Population (base.application).DataBindingContexts.Keys) {

                    dataBindings.Add (bindingName, new Population (base.application).DataBindingContexts[bindingName]);

                }

                return dataBindings;

            }

        }

        #endregion

    }

}
