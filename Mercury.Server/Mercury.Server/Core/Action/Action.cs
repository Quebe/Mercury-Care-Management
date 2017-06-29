using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Action {

    [Serializable]
    [DataContract (Name = "Action")]
    public class Action : CoreObject {

        #region Private Properties

        [DataMember (Name = "DescribingParameterName")]
        private String describingParameterName = String.Empty;

        [DataMember (Name = "ActionParameters")]
        private Dictionary<String, ActionParameter> actionParameters = new Dictionary<String, ActionParameter> ();


        private Exception lastProcessException = null;

        private Mercury.Server.Workflows.WorkflowResponse lastWorkflowResponse = null;

        #endregion


        #region Public Properties
        
        public String DescribingParameterName { get { return describingParameterName; } set { describingParameterName = value; } }

        public Dictionary<String, ActionParameter> ActionParameters { get { return actionParameters; } set { actionParameters = value; } }


        public Exception LastProcessException { get { return lastProcessException; } }

        public Mercury.Server.Workflows.WorkflowResponse LastWorkflowResponse { get { return lastWorkflowResponse; } }

        #endregion


        #region Constructors

        public Action (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public Action (Application applicationReference, Int64 forActionId, String forActionName) {

            base.BaseConstructor (applicationReference);

            id = forActionId;

            Name = forActionName;

            return;

        }

        #endregion

        
        
        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "DescribingParameterName", describingParameterName);


            #region Action Parameters

            System.Xml.XmlElement parametersRoot = CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ActionParemeters", String.Empty);

            foreach (ActionParameter currentParameter in actionParameters.Values) {

                System.Xml.XmlElement parameterNode;


                parameterNode = document.CreateElement ("ActionParameter");

                parameterNode.SetAttribute ("Name", currentParameter.Name);

                parameterNode.SetAttribute ("DataType", ((Int32)currentParameter.DataType).ToString ());

                parameterNode.SetAttribute ("AllowFixedValue", (currentParameter.AllowFixedValue.ToString ()));

                parameterNode.SetAttribute ("Required", (currentParameter.Required.ToString ()));

                parameterNode.SetAttribute ("ValueType", ((Int32)currentParameter.ValueType).ToString ());

                parameterNode.SetAttribute ("Description", currentParameter.ValueDescription);

                parameterNode.InnerText = currentParameter.Value;

                parametersRoot.AppendChild (parameterNode);

            }

            #endregion
            
            #endregion

            #region Object Nodes

            // INSERT DEPENDENCIES BEFORE THE PROPERTIES NODE TO ENSURE THEY ARE CREATED FIRST

            //if ((Id == (Int64)Enumerations.StandardActions.Workflow) && (actionParameters.ContainsKey ("Workflow"))) {

            //    Core.Work.Workflow workflow = application.WorkflowGet (actionParameters["Workflow"].ValueDescription);

            //    if (workflow != null) { document.LastChild.InsertBefore (document.ImportNode (workflow.XmlSerialize ().LastChild, true), propertiesNode); }

            //}

            foreach (ActionParameter currentActionParameter in actionParameters.Values) {

                Int64 currentCoreObjectId = 0;

                CoreObject currentCoreObject = null;

                if (Int64.TryParse (currentActionParameter.Value, out currentCoreObjectId)) {

                    switch (currentActionParameter.DataType) {

                        case Enumerations.ActionParameterDataType.CareOutcome: currentCoreObject = application.CareOutcomeGet (currentCoreObjectId); break;

                        case Enumerations.ActionParameterDataType.Correspondence: currentCoreObject = application.CorrespondenceGet (currentCoreObjectId); break;

                        case Enumerations.ActionParameterDataType.RoutingRule: currentCoreObject = application.RoutingRuleGet (currentCoreObjectId); break;

                        case Enumerations.ActionParameterDataType.Workflow: currentCoreObject = application.WorkflowGet (currentCoreObjectId); break;

                        case Enumerations.ActionParameterDataType.WorkOutcome: currentCoreObject = application.WorkOutcomeGet (currentCoreObjectId); break;

                        case Enumerations.ActionParameterDataType.WorkQueue: currentCoreObject = application.WorkQueueGet (currentCoreObjectId); break;

                    }

                    if (currentCoreObject != null) { document.LastChild.InsertBefore (document.ImportNode (currentCoreObject.XmlSerialize ().LastChild, true), propertiesNode); }

                }

            }

            #endregion


            return document;

        }

        #endregion 


        //public override List<Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = objectNode.Attributes["Name"].InnerText;

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "Action") {

        //        try {

        //            foreach (System.Xml.XmlNode currentProperty in objectNode.ChildNodes[0]) {

        //                switch (currentProperty.Attributes["Name"].InnerText) {

        //                    case "ActionId": actionId = Convert.ToInt64 (currentProperty.InnerText); break;

        //                    case "ActionName": actionName = currentProperty.InnerText; break;

        //                    case "DescribingParameterName": describingParameterName = currentProperty.InnerText; break;

        //                    case "Description": description = currentProperty.InnerText; break;

        //                    case "ParameterObjects":

        //                        foreach (System.Xml.XmlNode currentObject in currentProperty.ChildNodes) {

        //                            switch (currentObject.Name) {

        //                                case "Correspondence":

        //                                    String correspondenceName = currentObject.Attributes["Name"].InnerText;

        //                                    Core.Correspondence correspondence = application.CorrespondenceGet (correspondenceName);

        //                                    if (correspondence == null) {

        //                                        System.Xml.XmlDocument correspondenceDocument = new System.Xml.XmlDocument ();

        //                                        System.Xml.XmlDeclaration xmlDeclaration = correspondenceDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

        //                                        correspondenceDocument.InsertBefore (xmlDeclaration, correspondenceDocument.DocumentElement);

        //                                        correspondenceDocument.AppendChild (correspondenceDocument.ImportNode (currentObject, true));

        //                                        response.AddRange (application.XmlConfigurationImport (correspondenceDocument));

        //                                    }

        //                                    break;

        //                                case "WorkQueue":

        //                                    String workQueueName = currentObject.Attributes["Name"].InnerText;

        //                                    Core.Work.WorkQueue workQueue = application.WorkQueueGet (workQueueName);

        //                                    if (workQueue == null) {

        //                                        System.Xml.XmlDocument workQueueDocument = new System.Xml.XmlDocument ();

        //                                        System.Xml.XmlDeclaration xmlDeclaration = workQueueDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

        //                                        workQueueDocument.InsertBefore (xmlDeclaration, workQueueDocument.DocumentElement);

        //                                        workQueueDocument.AppendChild (workQueueDocument.ImportNode (currentObject, true));

        //                                        response.AddRange (application.XmlConfigurationImport (workQueueDocument));

        //                                    }

        //                                    break;

        //                                case "WorkOutcome":

        //                                    String workOutcomeName = currentObject.Attributes["Name"].InnerText;

        //                                    Core.Work.WorkOutcome workOutcome = application.WorkOutcomeGet (workOutcomeName);

        //                                    if (workOutcome == null) {

        //                                        System.Xml.XmlDocument workOutcomeDocument = new System.Xml.XmlDocument ();

        //                                        System.Xml.XmlDeclaration xmlDeclaration = workOutcomeDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

        //                                        workOutcomeDocument.InsertBefore (xmlDeclaration, workOutcomeDocument.DocumentElement);

        //                                        workOutcomeDocument.AppendChild (workOutcomeDocument.ImportNode (currentObject, true));

        //                                        response.AddRange (application.XmlConfigurationImport (workOutcomeDocument));

        //                                    }

        //                                    break;

        //                                case "RoutingRule":

        //                                    String routingRuleName = currentObject.Attributes["Name"].InnerText;

        //                                    Core.Work.RoutingRule routingRule = application.RoutingRuleGet (routingRuleName);

        //                                    if (routingRule == null) {

        //                                        System.Xml.XmlDocument routingRuleDocument = new System.Xml.XmlDocument ();

        //                                        System.Xml.XmlDeclaration xmlDeclaration = routingRuleDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

        //                                        routingRuleDocument.InsertBefore (xmlDeclaration, routingRuleDocument.DocumentElement);

        //                                        routingRuleDocument.AppendChild (routingRuleDocument.ImportNode (currentObject, true));

        //                                        response.AddRange (application.XmlConfigurationImport (routingRuleDocument));

        //                                    }

        //                                    break;

        //                            }

        //                        }

        //                        break;

        //                }

        //            }


        //            Core.Action.Action baseAction = base.application.ActionById (actionId);

        //            if (baseAction != null) {

        //                actionName = baseAction.Name;

        //                describingParameterName = baseAction.describingParameterName;

        //                actionParameters = baseAction.ActionParameters;

        //                if (actionId == (Int64) Enumerations.StandardActions.Workflow) {

        //                    String workflowName = objectNode.Attributes["WorkflowName"].InnerText;

        //                    Work.Workflow workflow = base.application.WorkflowGet (workflowName);

        //                    if (workflow != null) {

        //                        foreach (String workflowParameterName in workflow.WorkflowParameters.Keys) {

        //                            actionParameters.Add (workflowParameterName, workflow.WorkflowParameters[workflowParameterName]);

        //                        }

        //                    }

        //                }

        //                System.Xml.XmlDocument xmlParameters = base.XmlEmptyDocument ();

        //                xmlParameters.AppendChild (xmlParameters.ImportNode (objectNode.ChildNodes[1], true));

        //                ParseParametersXml (xmlParameters);

        //                foreach (String currentParameterName in actionParameters.Keys) {

        //                    ActionParameter currentParameter = actionParameters[currentParameterName];

        //                    switch (currentParameter.DataType) {

        //                        case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Correspondence:

        //                            currentParameter.Value = base.application.CorrespondenceGetIdByName (currentParameter.ValueDescription).ToString ();

        //                            break;

        //                        case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.RoutingRule:

        //                            currentParameter.Value = base.application.RoutingRuleGetIdByName (currentParameter.ValueDescription).ToString ();

        //                            break;

        //                        case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.WorkQueue:

        //                            currentParameter.Value = base.application.WorkQueueGetIdByName (currentParameter.ValueDescription).ToString ();

        //                            break;

        //                        case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.WorkOutcome:

        //                            currentParameter.Value = base.application.WorkOutcomeGetIdByName (currentParameter.ValueDescription).ToString ();

        //                            break;

        //                        default: /* DO NOTHING */

        //                            break;

        //                    }

        //                }

        //            }

        //            else { importResponse.SetException (new ApplicationException ("Unsupported Action for Import: [" + actionName + ", " + actionId.ToString () + "].")); }

        //        }

        //        catch (Exception importException) {

        //            importResponse.SetException (importException);

        //        }

        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as Population.")); }


        //    response.Add (importResponse);

        //    return response;

        //}


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            if (Id == 0) { validationResponse.Add ("Action", "No Action Defined."); }

            foreach (String currentParameterKey in actionParameters.Keys) {

                if ((actionParameters[currentParameterKey].Required) && (String.IsNullOrEmpty (actionParameters[currentParameterKey].Value))) {

                    validationResponse.Add ("Action." + actionParameters[currentParameterKey].Name, "Value Required.");

                }

            }


            return validationResponse;

        }

        #endregion


        #region Public Methods

        public void AddParameter (String parameterName, Enumerations.ActionParameterDataType dataType, Boolean allowFixedValue) {

            AddParameter (parameterName, dataType, allowFixedValue, true);

            return;

        }

        public void AddParameter (String parameterName, Enumerations.ActionParameterDataType dataType, Boolean allowFixedValue, Boolean isRequired) {

            ActionParameter parameter = new ActionParameter ();

            parameter.Name = parameterName;

            parameter.DataType = dataType;

            parameter.AllowFixedValue = allowFixedValue;

            parameter.Required = isRequired;

            if (actionParameters.ContainsKey (parameterName)) {

                actionParameters[parameterName] = parameter;

            }

            else {

                actionParameters.Add (parameterName, parameter);

            }

            return;

        }

        virtual public System.Xml.XmlDocument ActionParametersXml {

            get {

                System.Xml.XmlDocument parametersXml = new System.Xml.XmlDocument ();

                System.Xml.XmlDeclaration xmlDeclaration = parametersXml.CreateXmlDeclaration ("1.0", "utf-8", null);

                System.Xml.XmlElement rootNode = parametersXml.CreateElement ("Parameters");

                parametersXml.InsertBefore (xmlDeclaration, parametersXml.DocumentElement);

                parametersXml.AppendChild (rootNode);


                foreach (ActionParameter currentParameter in actionParameters.Values) {

                    System.Xml.XmlElement parameterNode;


                    parameterNode = parametersXml.CreateElement ("Parameter");

                    parameterNode.SetAttribute ("Name", currentParameter.Name);

                    parameterNode.SetAttribute ("DataType", ((Int32) currentParameter.DataType).ToString ());

                    parameterNode.SetAttribute ("AllowFixedValue", (currentParameter.AllowFixedValue.ToString ()));

                    parameterNode.SetAttribute ("Required", (currentParameter.Required.ToString ()));

                    parameterNode.SetAttribute ("ValueType", ((Int32) currentParameter.ValueType).ToString ());


                    #region Re-map the Description Name in Case of Core Object Name Change

                    Int64 attributeDescriptionValue = 0;

                    String attributeDescription = String.Empty;

                    Int64.TryParse (currentParameter.Value, out attributeDescriptionValue);

                    switch (currentParameter.DataType) {

                        case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Correspondence: attributeDescription = application.CoreObjectGetNameById ("Correspondence", attributeDescriptionValue); break;

                        case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.WorkQueue: attributeDescription = application.CoreObjectGetNameById ("WorkQueue", attributeDescriptionValue); break;

                        case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Workflow: attributeDescription = application.CoreObjectGetNameById ("Workflow", attributeDescriptionValue); break;

                    }

                    if (String.IsNullOrEmpty (attributeDescription)) { attributeDescription = currentParameter.ValueDescription; }

                    #endregion

                    parameterNode.SetAttribute ("Description", attributeDescription);


                    parameterNode.InnerText = currentParameter.Value;

                    rootNode.AppendChild (parameterNode);

                }


                return parametersXml;

            }

        }

        public String ActionParametersXmlSqlParsedString {

            get {

                String parametersXml = String.Empty;

                parametersXml = ActionParametersXml.InnerXml;

                parametersXml = parametersXml.Replace ("'", "''");

                parametersXml = parametersXml.Replace ((char) 0xA0, (char) 0x20);

                parametersXml = parametersXml.Replace ((char) 0xB7, (char) 0x20);

                return parametersXml;

            }

        }

        public void ParseParametersXml (System.Xml.XmlDocument xmlParameters) {

            foreach (System.Xml.XmlNode currentParameterNode in xmlParameters.SelectNodes ("//Parameter")) {

                try {

                    if (actionParameters.ContainsKey (currentParameterNode.Attributes["Name"].Value)) {

                        String currentParameterName = currentParameterNode.Attributes["Name"].Value;

                        actionParameters[currentParameterName].Value = currentParameterNode.InnerText;

                        if (currentParameterNode.Attributes["Description"] != null) {

                            if (!String.IsNullOrEmpty (currentParameterNode.Attributes["Description"].Value)) {

                                actionParameters[currentParameterName].ValueDescription = currentParameterNode.Attributes["Description"].Value;

                            }

                            else { actionParameters[currentParameterName].ValueDescription = currentParameterNode.InnerText; }

                        }

                        else { actionParameters[currentParameterName].ValueDescription = currentParameterNode.InnerText; }

                        if (currentParameterNode.Attributes["ValueType"] != null) {

                            Int32 forValueType = 0;

                            if (Int32.TryParse (currentParameterNode.Attributes["ValueType"].Value, out forValueType)) {

                                actionParameters[currentParameterName].ValueType = (Mercury.Server.Core.Action.Enumerations.ActionParameterValueType) forValueType;

                            }

                        }

                    }

                }

                catch (Exception parameterException) {

                    System.Diagnostics.Trace.WriteLine ("Unable to map parameters: " + parameterException.Message);

                    System.Diagnostics.Trace.Flush ();

                }

            }

            return;

        }

        public void RebindActionParameters () {

            Action baseAction = base.application.ActionById (Id);


            // copy over workflow

            if ((baseAction.Name == "Workflow") && (baseAction.ActionParameters.ContainsKey ("Workflow"))) {

                if ((Name == "Workflow") && (actionParameters.ContainsKey ("Workflow"))) {

                    baseAction.ActionParameters["Workflow"] = actionParameters["Workflow"];

                }

                if (!String.IsNullOrEmpty (baseAction.ActionParameters["Workflow"].Value)) {

                    Work.Workflow workflow = new Work.Workflow (base.application, Convert.ToInt64 (baseAction.ActionParameters["Workflow"].Value));

                    foreach (String workflowParameterName in workflow.WorkflowParameters.Keys) {

                        baseAction.ActionParameters.Add (workflowParameterName, workflow.WorkflowParameters[workflowParameterName]);

                    }

                }

            }


            actionParameters.Clear ();

            actionParameters = baseAction.ActionParameters;

            return;

        }

        public void MapDataFields (String prefix, System.Data.DataRow currentRow) {

            Core.Action.Action baseAction;

            System.Xml.XmlDocument parametersXml = new System.Xml.XmlDocument ();



            id = Int64.Parse (currentRow[prefix + "ActionId"].ToString ());


            baseAction = base.application.ActionById (id);

            if (baseAction != null) {

                Name = baseAction.Name;

                describingParameterName = baseAction.describingParameterName;

                actionParameters = baseAction.ActionParameters;


                if (!String.IsNullOrEmpty ((String) currentRow[prefix + "ActionParameters"])) {

                    parametersXml.LoadXml ((String) currentRow[prefix + "ActionParameters"]);

                    ParseParametersXml (parametersXml);

                }


                if ((Name == "Workflow") && (actionParameters["Workflow"] != null)) {

                    if (!String.IsNullOrEmpty (actionParameters["Workflow"].Value)) {

                        Work.Workflow workflow = new Mercury.Server.Core.Work.Workflow (base.application, Convert.ToInt64 (baseAction.ActionParameters["Workflow"].Value));

                        foreach (String workflowParameterName in workflow.WorkflowParameters.Keys) {

                            baseAction.ActionParameters.Add (workflowParameterName, workflow.WorkflowParameters[workflowParameterName]);

                        }

                        ParseParametersXml (parametersXml);

                    }

                }

            }


            return;

        }

        #endregion


        #region Process

        public Boolean Process (CoreObject sender, CoreObject eventObject, Int64 eventInstanceId, Core.Action.EventArguments.EventArguments eventArguments, String eventDescription) {

            Boolean success = true;


            Int64 workQueueId;

            Int64 workQueueEntityId;

            Int32 priority = 0;

            String itemGroupKey = String.Empty;


            Int64 correspondenceId;

            Reference.Correspondence correspondence;

            Entity.EntityCorrespondence entityCorrespondence;


            try {

                switch ((Enumerations.StandardActions) Id) {


                    case Mercury.Server.Core.Action.Enumerations.StandardActions.Workflow:

                        success = ProcessWorkflow (sender, eventArguments);

                        break;

                    case Mercury.Server.Core.Action.Enumerations.StandardActions.AddToWorkQueue:

                        #region Add to Work Queue

                        workQueueId = Convert.ToInt64 (actionParameters["WorkQueue"].Value);

                        workQueueEntityId = Convert.ToInt64 (sender.EvaluateDataBinding (actionParameters["EntityId"].Value));

                        if (actionParameters.ContainsKey ("Priority")) { Int32.TryParse (actionParameters["Priority"].Value, out priority); }

                        if (actionParameters.ContainsKey ("ItemGroupKey")) {

                            ActionParameter itemGroupKeyParameter = actionParameters["ItemGroupKey"];

                            if (itemGroupKeyParameter.AllowFixedValue) {

                                if (itemGroupKeyParameter.ValueType == Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue) {

                                    itemGroupKey = itemGroupKeyParameter.Value;

                                }

                                else {

                                    itemGroupKey = sender.EvaluateDataBinding (itemGroupKeyParameter.Value);

                                }

                            }

                        }

                        success = base.application.WorkQueueInsertEntity (workQueueId, workQueueEntityId, itemGroupKey, sender, eventObject, eventInstanceId, eventDescription, priority);

                        #endregion

                        break;

                    case Mercury.Server.Core.Action.Enumerations.StandardActions.RemoveFromWorkQueue:

                        #region Remove from Work Queue

                        workQueueId = Convert.ToInt64 (actionParameters["WorkQueue"].Value);

                        workQueueEntityId = Convert.ToInt64 (sender.EvaluateDataBinding (actionParameters["EntityId"].Value));

                        Int64 workOutcomeId = Convert.ToInt64 (actionParameters["WorkOutcome"].Value);

                        success = base.application.WorkQueueRemoveEntity (workQueueId, workQueueEntityId, workOutcomeId, sender, eventObject, eventInstanceId, eventDescription);

                        #endregion

                        break;

                    case Mercury.Server.Core.Action.Enumerations.StandardActions.RouteByRules:

                        success = ProcessRoutingRule (sender, eventObject, eventInstanceId, eventArguments, eventDescription);

                        break;

                    case Mercury.Server.Core.Action.Enumerations.StandardActions.SendCorrespondence:

                        #region Send Correspondence (Generic Entity)

                        correspondenceId = Convert.ToInt64 (actionParameters["Correspondence"].Value);

                        correspondence = application.CorrespondenceGet (correspondenceId);


                        Int64 entityId = Convert.ToInt64 (sender.EvaluateDataBinding (actionParameters["EntityId"].Value));

                        Entity.Entity entity = base.application.EntityGet (entityId);


                        entityCorrespondence = new Entity.EntityCorrespondence (application, entity, correspondence);

                        success = entityCorrespondence.Save ();


                        #endregion

                        break;

                    case Mercury.Server.Core.Action.Enumerations.StandardActions.SendCorrespondenceMember:

                        success = ProcessSendCorrespondenceToMember (sender, eventArguments);

                        break;

                    case Mercury.Server.Core.Action.Enumerations.StandardActions.SendCorrespondenceProvider:

                        success = ProcessSendCorrespondenceToProvider (sender, eventArguments);

                        break;

                    case Mercury.Server.Core.Action.Enumerations.StandardActions.SendCorrespondenceMemberAndRoute:

                        success = ProcessSendCorrespondenceToMember (sender, eventArguments);

                        if (success) { success = ProcessRoutingRule (sender, eventObject, eventInstanceId, eventArguments, eventDescription); }

                        break;

                } // switch ((Enumerations.StandardActions) actionId) {


            } // try 

            catch (Exception processException) {

                success = false;

                System.Diagnostics.Debug.WriteLine (processException.Message);

                lastProcessException = processException;

            }


            return success;

        }

        protected Boolean ProcessWorkflow (CoreObject sender, Core.Action.EventArguments.EventArguments eventArguments) {

            Boolean success = false;


            Int64 workflowId;

            Core.Work.Workflow workflow;

            lastWorkflowResponse = null;

            try {

                workflowId = Convert.ToInt64 (actionParameters["Workflow"].Value);

                workflow = new Mercury.Server.Core.Work.Workflow (base.application, workflowId);


                if (workflow != null) {

                    if (workflow.Enabled) {

                        Dictionary<String, Object> workflowParameters = new Dictionary<String, Object> ();

                        foreach (String currentParameterName in actionParameters.Keys) {

                            #region Mapped Parameter Values (from Sender or specific instances)

                            switch (actionParameters[currentParameterName].DataType) {

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Workflow:

                                    workflowParameters.Add ("WorkflowId", workflowId);

                                    workflowParameters.Add ("WorkflowName", application.CoreObjectGetNameById ("Workflow", workflowId));

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.WorkQueue:

                                    Int64 workQueueId = 0;

                                    Int64.TryParse (actionParameters[currentParameterName].Value, out workQueueId);

                                    workflowParameters.Add ("WorkQueueId", workQueueId);

                                    workflowParameters.Add ("WorkQueueName", application.CoreObjectGetNameById ("WorkQueue", workQueueId));

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Correspondence:

                                    Int64 correspondenceId = 0;

                                    Int64.TryParse (actionParameters[currentParameterName].Value, out correspondenceId);

                                    workflowParameters.Add ("CorrespondenceId", correspondenceId);

                                    workflowParameters.Add ("CorrespondenceName", application.CorrespondenceGetNameById (correspondenceId));

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.RoutingRule:

                                    Int64 routingRuleId = 0;

                                    Int64.TryParse (actionParameters[currentParameterName].Value, out routingRuleId);

                                    workflowParameters.Add ("RoutingRuleId", routingRuleId);

                                    workflowParameters.Add ("RoutingRuleName", application.RoutingRuleGetNameById (routingRuleId));

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.WorkOutcome:

                                    Int64 workOutcomeId = 0;

                                    Int64.TryParse (actionParameters[currentParameterName].Value, out workOutcomeId);

                                    workflowParameters.Add ("WorkOutcomeId", workOutcomeId);

                                    workflowParameters.Add ("WorkOutcomeName", application.WorkOutcomeGetNameById (workOutcomeId));

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.CareOutcome:

                                    Int64 careOutcomeId = 0;

                                    Int64.TryParse (actionParameters[currentParameterName].Value, out careOutcomeId);

                                    workflowParameters.Add ("CareOutcomeId", careOutcomeId);

                                    // workflowParameters.Add ("CareOutcomeName", application.CareOutcomeGetNameById (careOutcomeId));

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.MemberId:

                                    Int64 memberId = 0;

                                    Int64.TryParse (sender.EvaluateDataBinding (actionParameters[currentParameterName].Value), out memberId);

                                    workflowParameters.Add (currentParameterName, memberId);

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.ProviderId:

                                    Int64 providerId = 0;

                                    Int64.TryParse (sender.EvaluateDataBinding (actionParameters[currentParameterName].Value), out providerId);

                                    workflowParameters.Add (currentParameterName, providerId);

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.EntityId:

                                    Int64 entityId = 0;

                                    Int64.TryParse (sender.EvaluateDataBinding (actionParameters[currentParameterName].Value), out entityId);

                                    workflowParameters.Add (currentParameterName, entityId);

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Id:

                                    Int64 parsedId = 0;

                                    if (actionParameters[currentParameterName].ValueType == Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue) {

                                        Int64.TryParse (actionParameters[currentParameterName].Value, out parsedId);

                                    }

                                    else {

                                        Int64.TryParse (sender.EvaluateDataBinding (actionParameters[currentParameterName].Value), out parsedId);

                                    }

                                    workflowParameters.Add (currentParameterName, parsedId);

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Integer:

                                    workflowParameters.Add (currentParameterName, Convert.ToInt32 (actionParameters[currentParameterName].Value));

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Decimal:

                                    workflowParameters.Add (currentParameterName, Convert.ToDecimal (actionParameters[currentParameterName].Value));

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.String:

                                    workflowParameters.Add (currentParameterName, Convert.ToString (actionParameters[currentParameterName].Value));

                                    break;

                                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.DateTime:

                                    DateTime parsedDateTime = DateTime.Now;

                                    if (actionParameters[currentParameterName].ValueType == Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue) {

                                        DateTime.TryParse (actionParameters[currentParameterName].Value, out parsedDateTime);

                                    }

                                    else {

                                        DateTime.TryParse (sender.EvaluateDataBinding (actionParameters[currentParameterName].Value), out parsedDateTime);

                                    }

                                    workflowParameters.Add (currentParameterName, parsedDateTime);

                                    break;

                                default:

                                    workflowParameters.Add (currentParameterName, actionParameters[currentParameterName].Value);

                                    break;

                            }

                            #endregion

                        }

                        if (!workflowParameters.ContainsKey ("WorkflowId")) {

                            workflowParameters.Add ("WorkflowId", workflowId);

                        }

                        Workflows.WorkflowStartRequest workflowStartRequest = new Mercury.Server.Workflows.WorkflowStartRequest (workflow);

                        workflowStartRequest.Arguments = workflowParameters;

                        lastWorkflowResponse = base.application.WorkflowStart (workflowStartRequest);

                        if (lastWorkflowResponse.WorkflowStatus == Mercury.Server.Workflows.WorkflowStatus.Completed) {

                            success = true;

                        }

                        else { throw new ApplicationException ((lastWorkflowResponse.HasException) ? lastWorkflowResponse.LastException.Message : "Workflow failed to complete."); }

                    }

                }

                else {

                    success = false;

                    application.SetLastException (new ApplicationException ("Workflow Disabled."));

                }

            }

            catch (Exception processException) {

                success = false;

                lastProcessException = processException;

                application.SetLastException (processException);

            }


            return success;

        }

        protected Boolean ProcessRoutingRule (CoreObject sender, CoreObject eventObject, Int64 eventInstanceId, Core.Action.EventArguments.EventArguments eventArguments, String eventDescription) {

            Boolean success = false;


            Int64 routingRuleId;

            Int64 memberId;

            Core.Work.RoutingRule routingRule;

            Member.Member member;

            Int64 workQueueId = 0;

            Int32 priority = 0;

            String itemGroupKey = String.Empty;


            routingRuleId = Convert.ToInt64 (actionParameters["RoutingRule"].Value);

            memberId = Convert.ToInt64 (sender.EvaluateDataBinding (actionParameters["MemberId"].Value));


            routingRule = application.RoutingRuleGet (routingRuleId);

            if (routingRule != null) {

                if (routingRule.Enabled) {

                    member = new Member.Member (base.application, memberId);


                    workQueueId = routingRule.Evaluate (member);


                    if (workQueueId != 0) {

                        if (actionParameters.ContainsKey ("Priority")) { Int32.TryParse (actionParameters["Priority"].Value, out priority); }

                        if (actionParameters.ContainsKey ("ItemGroupKey")) {

                            ActionParameter itemGroupKeyParameter = actionParameters["ItemGroupKey"];

                            if (itemGroupKeyParameter.AllowFixedValue) {

                                if (itemGroupKeyParameter.ValueType == Mercury.Server.Core.Action.Enumerations.ActionParameterValueType.FixedValue) {

                                    itemGroupKey = itemGroupKeyParameter.Value;

                                }

                                else {

                                    itemGroupKey = sender.EvaluateDataBinding (itemGroupKeyParameter.Value);

                                }

                            }

                        }

                        success = base.application.WorkQueueInsertObject (workQueueId, "Member", member.Id, member.Name, member.Description, itemGroupKey, sender, eventObject, eventInstanceId, eventDescription + " [" + routingRule.Name + "]", priority);

                    }

                    else { success = true; /* DO NOT ROUTE */ }

                }

                else {

                    success = false;

                    throw new ApplicationException ("Routing Rule Disabled: " + routingRule.Name);

                }

            }

            else {

                success = false;

                throw new ApplicationException ("Routing Rule Not Found: " + routingRuleId.ToString ());

            }

            return success;

        }

        protected Boolean ProcessSendCorrespondenceToMember (CoreObject sender, Core.Action.EventArguments.EventArguments eventArguments) {

            Boolean success = false;


            Int64 memberId;

            Int64 correspondenceId;

            Int64 memberCaseId = 0;


            Core.Member.Member member;

            Core.Reference.Correspondence correspondence;



            StringBuilder memberCorrespondenceInsert = new StringBuilder ();


            if (!Int64.TryParse (sender.EvaluateDataBinding (actionParameters["MemberId"].Value), out memberId)) { throw new ApplicationException ("Member Id not specified for required parameter."); }

            if (!Int64.TryParse (actionParameters["Correspondence"].Value, out correspondenceId)) { throw new ApplicationException ("Correspondence not specified for required parameter."); }

            if (actionParameters.ContainsKey ("MemberCaseId")) { Int64.TryParse (actionParameters["MemberCaseId"].Value, out memberCaseId); }


            member = base.application.MemberGet (memberId);

            correspondence = base.application.CorrespondenceGet (correspondenceId);


            if (member == null) { throw new ApplicationException ("Unable to load Member [" + memberId.ToString () + "]."); }

            if (correspondence == null) { throw new ApplicationException ("Unable to load Correspondence [" + correspondenceId.ToString () + "]."); }


            if (correspondence.Enabled) {

                Entity.EntityCorrespondence entityCorrespondence = new Entity.EntityCorrespondence (application, member.Entity, correspondence);

                success = entityCorrespondence.Save ();

            }

            else {

                throw new ApplicationException ("Correspondence Disabled: " + correspondence.Name);

            }


            return success;

        }

        protected Boolean ProcessSendCorrespondenceToProvider (CoreObject sender, Core.Action.EventArguments.EventArguments eventArguments) {

            Boolean success = false;


            Int64 providerId;

            Int64 regardingMemberId;

            Int64 correspondenceId;


            Core.Provider.Provider provider;

            Core.Member.Member regardingMember = null;

            Core.Reference.Correspondence correspondence;


            StringBuilder providerCorrespondenceInsert = new StringBuilder ();


            if (!Int64.TryParse (sender.EvaluateDataBinding (actionParameters["ProviderId"].Value), out providerId)) { throw new ApplicationException ("Provider Id not specified for required parameter."); }

            if (!Int64.TryParse (actionParameters["Correspondence"].Value, out correspondenceId)) { throw new ApplicationException ("Correspondence not specified for required parameter."); }


            provider = base.application.ProviderGet (providerId);

            correspondence = base.application.CorrespondenceGet (correspondenceId);


            if (provider == null) { throw new ApplicationException ("Unable to load Provider [" + providerId.ToString () + "]."); }

            if (correspondence == null) { throw new ApplicationException ("Unable to load Correspondence [" + correspondenceId.ToString () + "]."); }


            if (correspondence.Enabled) {

                if (Int64.TryParse (actionParameters["RegardingMemberId"].Value, out regardingMemberId)) {

                    regardingMember = base.application.MemberGet (regardingMemberId);

                    if (regardingMember == null) { throw new ApplicationException ("Unable to load Member [" + regardingMemberId.ToString () + "] for optional Parameter."); }

                }


                Entity.EntityCorrespondence entityCorrespondence = new Entity.EntityCorrespondence (application, provider.Entity, correspondence);

                if (regardingMember != null) { entityCorrespondence.RelatedEntityId = regardingMember.EntityId; }

                success = entityCorrespondence.Save ();


            }

            else {

                throw new ApplicationException ("Correspondence Disabled: " + correspondence.Name);

            }


            if (!success) { throw base.application.EnvironmentDatabase.LastException; }


            return success;

        }

        public Object GetParameterValue (CoreObject sender, String parameterName) {

            Object parameterValue = String.Empty;

            Int32 parameterValueInt32 = 0;

            Int64 parameterValueInt64 = 0;

            switch (actionParameters[parameterName].DataType) {

                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Correspondence:

                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.RoutingRule:

                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.WorkQueue:

                    if (!Int64.TryParse ((String) actionParameters[parameterName].Value, out parameterValueInt64)) { parameterValueInt64 = 0; }

                    parameterValue = parameterValueInt64;

                    break;

                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.MemberId:

                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.EntityId:

                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Id:

                    parameterValue = sender.EvaluateDataBinding (actionParameters[parameterName].Value);

                    if (parameterValue.ToString () == "!Error") { parameterValue = actionParameters[parameterName].Value; }

                    if (!Int64.TryParse ((String) parameterValue, out parameterValueInt64)) { parameterValueInt64 = 0; }

                    parameterValue = parameterValueInt64;

                    break;

                case Mercury.Server.Core.Action.Enumerations.ActionParameterDataType.Integer:

                    if (!Int32.TryParse ((String) actionParameters[parameterName].Value, out parameterValueInt32)) { parameterValueInt32 = 0; }

                    parameterValue = parameterValueInt32;

                    break;

            }


            return parameterValue;

        }

        #endregion

    }

}
